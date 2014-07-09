using UnityEngine;
using System.Collections;
using PYIV.Menu;
using PYIV.Gameplay;
using PYIV.Persistence;
using PYIV.Helper;
using PYIV.Gameplay.Character;
using PYIV.Gameplay.Enemy;

namespace PYIV.Menu
{
	public class GameView : BaseView
	{
		private GameObject game;
		private GameObject ui;
		private GameObject sprite;
		private GameObject inGameGui_Prefab;

		//Flynotes
		private GameObject onHit_Flynote_Prefab;
		private GameObject onHit_flynote;


		private UISprite village_bar;
		private UISprite stamina_bar;
		private float characterStamina;
		
		public GameView ()
		{
			game = new GameObject ("Game", typeof(Game));
			game.SetActive (false);

			inGameGui_Prefab = Resources.Load<GameObject> ("Prefabs/UI/InGameGUI_Prefab");
			onHit_Flynote_Prefab = Resources.Load<GameObject> ("Prefabs/UI/OnHit_Flynote_Prefab");
		}
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			game.transform.parent = sceneParent.transform;
			game.SetActive (true);

			ui = NGUITools.AddChild (guiParent, inGameGui_Prefab);
			onHit_flynote = NGUITools.AddChild (guiParent, onHit_Flynote_Prefab);
			onHit_flynote.SetActive(false);
			InitViewComponents ();

		}

		public void RemoveFromScreen ()
		{
			GameObject.Destroy (game);
			GameObject.Destroy (ui);

		}

		public bool ShouldBeCached ()
		{
			return false;
		}
		
		public void UnpackParameter (object initParameter)
		{
			GameData gameData = initParameter as GameData;
			game.GetComponent<Game> ().GameData = gameData;
			Score score = game.GetComponent<Score> ();
			score.OnScoreChanged += SetVillageBar;
			score.OnHitFlyNote += OnEnemyHit;

			Indian indian = game.transform.GetComponentInChildren<Indian> ();
			indian.OnStaminaChanged += SetStaminaBar;
			characterStamina = indian.IndianData.Stamina;
		}

		private void SetVillageBar (int villagePoints)
		{
			village_bar.fillAmount = (float)villagePoints / ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints"); // wenn federn implementiert werde muss das geändert werden
		}

		private void SetStaminaBar (float stamina)
		{
			UIWidget staminaWidget = stamina_bar.GetComponent<UIWidget> ();
			if (stamina < 1.0f / characterStamina)
				staminaWidget.color = new Color (1.0f, 0, 0);
			else if (stamina < 0.5f)
				staminaWidget.color = new Color (0.6f, 0.4f, 0);
			else
				staminaWidget.color = new Color (0, 1.0f, 0);

			stamina_bar.fillAmount = stamina;
		}


		private void OnEnemyHit(Enemy enemy, string message) {

			onHit_flynote.transform.position = enemy.transform.position;

			UILabel flynoteLabel = sprite.transform.FindChild("OnHit_Flynote_Prefab").gameObject.GetComponent<UILabel>();
			flynoteLabel.text = "Jeetroffeeeen!";
			onHit_flynote.SetActive(true);
		}


		private void InitViewComponents ()
		{		
			// size of UI
			Vector3 locScale = ui.transform.localScale;
			locScale.x = 5f;
			locScale.y = 5f;
			ui.transform.localScale = locScale;


			sprite = ui.transform.FindChild ("Sprite").gameObject;
			GameObject topRight = sprite.transform.FindChild ("TopRight").gameObject;
			GameObject topLeft = sprite.transform.FindChild ("TopLeft").gameObject;
			GameObject villageDamage = topRight.transform.FindChild ("Dorf_Damage_Prefab").gameObject;
			GameObject stamina = topLeft.transform.FindChild ("Stamina_Bar_Prefab").gameObject;
			
			village_bar = villageDamage.transform.FindChild ("dorf_fg").gameObject.GetComponent<UISprite> ();
			stamina_bar = stamina.transform.FindChild ("stamina_fg").gameObject.GetComponent<UISprite> ();

		}
		
		public void Back ()
		{
			throw new System.NotImplementedException ();
		}

	}

}