using UnityEngine;
using System.Collections;

public class ScoreController : MonoBehaviour {

    private int hits = 0;
    private int missed = 0;
    private int damage = 0;

    private GameObject top;
    private GameObject right;
    private GameObject left;


	// Use this for initialization
	void Start () {
        top = GameObject.Find("UILabelTop");
        right = GameObject.Find("UILabelRight");
        left = GameObject.Find("UILabelLeft");

        top.GetComponent<UILabel>().text = "Missed: " + missed;
        right.GetComponent<UILabel>().text = "Hits: " + hits;
        left.GetComponent<UILabel>().text = "Damage: " + damage;
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

    public void addDamage()
    {
        damage++;
        left.GetComponent<UILabel>().text = "Damage: " + damage;
    }
}
