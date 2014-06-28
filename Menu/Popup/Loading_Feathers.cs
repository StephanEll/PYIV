using UnityEngine;
using System.Collections;

public class Loading_Feathers : MonoBehaviour {
	UISprite feathers_empty;
	UISprite feathers_full;
	
	// Use this for initialization
	void Start () {
		feathers_empty = gameObject.transform.FindChild("feathers_empty").gameObject.GetComponent<UISprite>();
		feathers_full = gameObject.transform.FindChild("feathers_full").gameObject.GetComponent<UISprite>();
	}
	
	// Update is called once per frame
	void Update () {

		feathers_full.fillAmount += 0.01f;
		feathers_empty.fillAmount -= 0.01f;

		if(feathers_full.fillAmount == 1) {
			feathers_full.fillAmount = 0;
			feathers_empty.fillAmount = 1;
		}
	}
}
