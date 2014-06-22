using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;

public class Score : MonoBehaviour {

    public int HitCount { get; private set; }
    public int KillCount { get; private set; }
    public int MissedShotCount { get; private set; }
    public int Livepoints { get; private set; }

	public delegate void ScoreChangedDelegate(int newScore);
	public event ScoreChangedDelegate OnScoreChanged;

    private List<Enemy> lastHits = new List<Enemy>();
    private List<Enemy> lastKills = new List<Enemy>();

	// Use this for initialization
	void Start () {
        HitCount = 0;
        MissedShotCount = 0;
        Livepoints = 0;
        KillCount = 0;
	}
	
	// Update is called once per frame
	void Update () {
        
	}

    public void AddHit(Enemy enemy)
    {
        HitCount++;
        lastHits.Add(enemy);
        // evtl liste begrenzen
    }

    public void AddKill(Enemy enemy)
    {
        KillCount++;
        lastKills.Add(enemy);
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
		if(OnScoreChanged != null){
			OnScoreChanged(this.Livepoints);
		}
    }

    public static void AddAsGameobjectTo(GameObject go, int villageLivepoints)
    {
        go.AddComponent<Score>().Livepoints = villageLivepoints;
    }
}
