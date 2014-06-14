using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;

public class Score : MonoBehaviour {


    public int HitCount { get; private set; }
    public int KillCount { get; private set; }
    public int MissedShotCount { get; private set; }
    public int Livepoints { get; private set; }

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
        if(lastHits.Count > 10)
            lastHits.RemoveAt(0);
    }

    public void AddKill(Enemy enemy)
    {
        KillCount++;
        lastKills.Add(enemy);
        if (lastKills.Count > 10)
            lastKills.RemoveAt(0);
    }

    public void AddMissed()
    {
        MissedShotCount++;
    }

    public void SubtractLivepoints(int damage)
    {
        this.Livepoints -= damage;
    }

    public static void AddAsGameobjectTo(GameObject go, int villageLivepoints)
    {
        go.AddComponent<Score>().Livepoints = villageLivepoints;
    }
}
