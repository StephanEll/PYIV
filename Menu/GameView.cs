using UnityEngine;
using System.Collections;
using PYIV.Menu;
using PYIV.Gameplay;
using PYIV.Persistence;

namespace PYIV.Menu{

	public class GameView : BaseView
	{
		private GameObject game;
		private GameObject ui;
		private GameObject sprite;
		private GameObject inGameGui_Prefab;

		private UISprite village_bar;
		private UISprite stamina_bar;

		
		public GameView(){
			game = new GameObject("Game", typeof(Game));
			game.SetActive(false);

			inGameGui_Prefab = Resources.Load<GameObject>("Prefabs/UI/InGameGUI_Prefab");
		}
		
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			game.transform.parent = sceneParent.transform;
			game.SetActive(true);

			ui = NGUITools.AddChild(guiParent, inGameGui_Prefab);
			InitViewComponents();

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
			Score score = game.GetComponent<Score>();
			score.OnScoreChanged += SetVillageBar;
		}

		private void SetVillageBar(int villagePoints) {
			village_bar.fillAmount = (float)((villagePoints*2)/1000);
		}

		private void SetStaminaBar(int stamina) {
			// TODO

		}


		private void InitViewComponents() {
			
			// size of UI
			Vector3 locScale = ui.transform.localScale;
			locScale.x = 5f;
			locScale.y = 5f;
			ui.transform.localScale = locScale;


			sprite = ui.transform.FindChild("Sprite").gameObject;
			GameObject topRight = sprite.transform.FindChild("TopRight").gameObject;
			GameObject topLeft = sprite.transform.FindChild("TopLeft").gameObject;
			GameObject villageDamage = topRight.transform.FindChild("Dorf_Damage_Prefab").gameObject;
			GameObject stamina = topLeft.transform.FindChild("Stamina_Bar_Prefab").gameObject;
			
			village_bar = villageDamage.transform.FindChild("dorf_fg").gameObject.GetComponent<UISprite>();
			stamina_bar = stamina.transform.FindChild("stamina_fg").gameObject.GetComponent<UISprite>();

		}

	}

}