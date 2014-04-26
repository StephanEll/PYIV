using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    private int hits = 0;
    private int missed = 0;

    private GameObject top;
    private GameObject right;


	// Use this for initialization
	void Start () {
        top = GameObject.Find("UILabelTop");
        right = GameObject.Find("UILabelRight");
        right.GetComponent<UILabel>().text = "Hits: " + hits;
        top.GetComponent<UILabel>().text = "Missed: " + missed;
	}

    public void addHit()
    {
        hits++;
        right.GetComponent<UILabel>().text = "Hits: " + hits;
    }

    public void addMissed()
    {
        missed++;
        top.GetComponent<UILabel>().text = "Missed: " + missed;
    }
}
