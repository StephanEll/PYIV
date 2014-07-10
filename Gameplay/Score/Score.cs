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

    public delegate void ScoreChangedDelegate(int newScore);
    public event ScoreChangedDelegate OnScoreChanged;

    public delegate void HitDelegate(Enemy.Enemy enemy,string message);
    public event HitDelegate OnHitFlyNote;

    public delegate void KillDelegate(Enemy.Enemy enemy,string message);
    public event HitDelegate OnKillFlyNote;

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

    public void AddHit(Enemy.Enemy enemy)
    {
    
      HitCount++;
      lastHits.Add(enemy);
    
      string rememberType = enemy.Type;
      int sameTypeHitCounter = 0;
      int counterHitsInARow = 0;
      foreach (Enemy.Enemy hit in lastHits)
      {
        if (rememberType == hit.Type)
          sameTypeHitCounter ++;
        else if (hit != null)
          counterHitsInARow ++;
        else 
          return;
      }

      FlyNoteData fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.HitsNotTypeSpecific, counterHitsInARow);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd.Message);
      }
      fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.HitsTypeSpecific, sameTypeHitCounter);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd.Message);
      }
      // evtl liste begrenzen
    }

    public void AddKill(Enemy.Enemy enemy)
    {

      KillCount++;
      lastKills.Add(enemy);

      string rememberType = enemy.Type;
      int sameTypeKillCounter = 0;
      int counterKillsInARow = 0;
      foreach (Enemy.Enemy killedEnemy in lastKills)
      {
        if (rememberType == killedEnemy.Type)
          sameTypeKillCounter ++;
        else if (killedEnemy != null)
          counterKillsInARow ++;
        else 
          return;
      }
      
      FlyNoteData fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.KillsNotTypeSpecific, counterKillsInARow);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd.Message);
      }
      fnd = FlyNoteDataCollection.Instance.GetFlyNote(FlyNoteData.KillsTypeSpecific, sameTypeKillCounter);
      if (OnHitFlyNote != null && fnd != null)
      {
        OnHitFlyNote(enemy, fnd.Message);
      }
      // evtl liste begrenzen
    }

    public void AddMissed()
    {
      MissedShotCount++;
      lastHits.Add(null);
      lastKills.Add(null);
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


  }
}
