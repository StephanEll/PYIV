using UnityEngine;
using System.Collections;

public class DestroyParticle : MonoBehaviour {

	// Use this for initialization
	void Start () {
		//script attached to a particle which has a delay
		Destroy(this.gameObject, 3); // notice the lower case g
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
