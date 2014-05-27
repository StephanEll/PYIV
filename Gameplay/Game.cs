using UnityEngine;
using System.Collections;
using PYIV.Persistence;

namespace PYIV.Gameplay{

	public class Game : MonoBehaviour
	{
		
		private GameObject background;
		
		public GameData GameData { get; set; }
		
	
		// Use this for initialization
		void Start ()
		{
			//var bgPrefab = Resources.Load("Environment/Forest");
			//background = Instantiate(bgPrefab) as GameObject;
		}
		
		// Update is called once per frame
		void Update ()
		{
		
		}
	}

}