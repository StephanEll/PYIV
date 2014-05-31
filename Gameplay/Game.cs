using UnityEngine;
using System.Collections;
using PYIV.Persistence;

namespace PYIV.Gameplay{

	public class Game : MonoBehaviour
	{
		
		private GameObject background;
		
		private GameData gameData;
		private SpawnController spawnController;
		
		public GameData GameData { 
			
			get {
				return gameData;
			}
			set {
				gameData = value;
				Init();
			}
		}
		
		private void Init(){
			this.spawnController = this.gameObject.AddComponent<SpawnController>();
			
			
		}
		
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