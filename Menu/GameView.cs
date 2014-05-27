using UnityEngine;
using System.Collections;
using PYIV.Menu;
using PYIV.Gameplay;
using PYIV.Persistence;

namespace PYIV.Menu{

	public class GameView : BaseView
	{
		
		
		private GameObject game;
		
		public GameView(){
			game = new GameObject("Game", typeof(Game));
			game.SetActive(false);
			
		}
		
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			game.transform.parent = sceneParent.transform;
			game.SetActive(true);
		}

		public void RemoveFromScreen ()
		{
			GameObject.Destroy(game);
		}

		public bool ShouldBeCached ()
		{
			return false;
		}
		
		public void UnpackParameter (object initParameter)
		{
			GameData gameData = initParameter as GameData;
			game.GetComponent<Game>().GameData = gameData;
		}
	}

}