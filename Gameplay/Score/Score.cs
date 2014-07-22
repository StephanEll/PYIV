using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;
using PYIV.Helper;
using PYIV.Persistence;

namespace PYIV.Gameplay.Score
{

  public class Score : MonoBehaviour
  {

    public int HitCount { get; private set; }
    public int KillCount { get; private set; }
    public int MissedShotCount { get; private set; }
    public int Livepoints { get; private set; }

    public Dictionary<string, int> ExtraPointCount { get; private set; }

    public delegate void ScoreChangedDelegate(int newScore);
    public event ScoreChangedDelegate OnScoreChanged;

    public delegate void HitDelegate(Enemy.Enemy enemy,FlyNoteData fnd);
    public event HitDelegate OnHitFlyNote;


    private List<Enemy.Enemy> lastHits = new List<Enemy.Enemy>();
    private List<Enemy.Enemy> lastKills = new List<Enemy.Enemy>();
  

    // Use this for initialization
    void Start()
    {
      HitCount = 0;
      MissedShotCount = 0;
      Livepoints = ConfigReader.Instance.GetSettingAsInt("game", "start-village-livepoints");
      KillCount = 0;
    }
  
    // Update is called once per frame
    void Update()
    {
    
    }

    public void AddHit(Enemy.Enemy lastHitEnemy)
    {
    
      Debug.Log("Enemy Hit: " + lastHitEnemy == null);

      HitCount++;
      lastHits.Insert(0, lastHitEnemy);
    
      string rememberType = lastHitEnemy.Type;
      int sameTypeHitCounter = 0;
      int counterHitsInARow = 0;

      foreach (Enemy.Enemy hit in lastHits)
      {
        if (hit == null)
          break;
        if (rememberType == hit.Type){
          sameTypeHitCounter ++;
        } else {
          rememberType = "stop counting";
        }

        counterHitsInARow ++;

      }

      bool sendStdMessage = true;

      FlyNoteData fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.HitsNotTypeSpecific, counterHitsInARow);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(lastHitEnemy, fnd);
        sendStdMessage = false;
        IncreaseExtraPoints(fnd);
      } 
      fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.HitsTypeSpecific, sameTypeHitCounter);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(lastHitEnemy, fnd);
        sendStdMessage = false;
        IncreaseExtraPoints(fnd);
      }
      if(sendStdMessage)
      {
        OnHitFlyNote(lastHitEnemy, new FlyNoteData(lastHitEnemy.LivePoints.ToString()));
      }

      // evtl liste begrenzen
    }


    public void AddKill(Enemy.Enemy enemy)
    {

      KillCount++;
      lastKills.Insert(0, enemy);

      string rememberType = enemy.Type;
      int sameTypeKillCounter = 0;
      int counterKillsInARow = 0;
      bool stopCountingKillsInARow = false;
      foreach (Enemy.Enemy killedEnemy in lastKills)
      {
        if(killedEnemy == null)
          stopCountingKillsInARow = true;
        else if (rememberType == killedEnemy.Type){
          sameTypeKillCounter ++;
        }
        if(!stopCountingKillsInARow)
          counterKillsInARow ++;
      }
      
      FlyNoteData fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.KillsNotTypeSpecific, counterKillsInARow);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd);
        IncreaseExtraPoints(fnd);
      }
      fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.KillsTypeSpecific, sameTypeKillCounter);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd);
        IncreaseExtraPoints(fnd);
      }
      // evtl liste begrenzen
    }

    public void AddMissed()
    {
      MissedShotCount++;
      lastHits.Insert(0, null);
      lastKills.Insert(0, null);
    }

    public void SubtractLivepoints(int damage)
    {
      this.Livepoints -= damage;
      if (OnScoreChanged != null)
      {
        OnScoreChanged(this.Livepoints);
      }
      Handheld.Vibrate();
    }

    public static Score AddAsComponentTo(GameObject go, int villageLivepoints)
    {
      Score score = go.AddComponent<Score>();
      score.Livepoints = villageLivepoints;
      return score;
    }

    public ScoreResult GetScoreResult()
    {
      return new ScoreResult(this.HitCount, this.MissedShotCount, this.KillCount, this.Livepoints);
    }

    private void IncreaseExtraPoints(FlyNoteData fnd){
      int points = 0;
      if (ExtraPointCount.TryGetValue(fnd.Type, out points))
      {
        points += fnd.ExtraPoints;
      } 
      else
      {
        ExtraPointCount.Add(fnd.Type, fnd.ExtraPoints);
      }
    }

  }
}
