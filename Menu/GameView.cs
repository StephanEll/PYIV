using UnityEngine;
using System.Collections;
using PYIV.Menu;
using PYIV.Gameplay;
using PYIV.Persistence;
using PYIV.Helper;
using PYIV.Gameplay.Character;
using PYIV.Gameplay.Enemy;
using PYIV.Gameplay.Score;
using PYIV.Menu.MenuHelper;
using Holoville.HOTween; 
using Holoville.HOTween.Plugins;


namespace PYIV.Menu
{
	public class GameView : BaseView
	{
		private GameObject game;
		private GameObject ui;
		private GameObject sprite;
		private GameObject inGameGui_Prefab;
		private GameObject pauseButton;
		private GameObject pauseMessage;

		public bool isPaused = false;
    	private float rememberTimeScale;

		//Flynotes
		private GameObject onHit_Flynote_Prefab;
		private GameObject onHit_flynote;
		private int flynoteCounter = 0;


		private UISprite village_bar;
		private UISprite stamina_bar;
		private float characterStamina;
		private PointsHelper villagePointsHelper;
		private PointsHelper goldPointsHelper;
		
		public GameView ()
		{
			game = new GameObject ("Game", typeof(Game));
			game.SetActive (false);

			inGameGui_Prefab = Resources.Load<GameObject> ("Prefabs/UI/InGameGUI_Prefab");
			onHit_Flynote_Prefab = Resources.Load<GameObject> ("Prefabs/UI/OnHit_Flynote_Prefab");
      rememberTimeScale = Time.timeScale;
		}
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			game.transform.parent = sceneParent.transform;
			game.SetActive (true);

			ui = NGUITools.AddChild (guiParent, inGameGui_Prefab);

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
			Debug.Log("unpack: " + gameData.MyStatus);
			game.GetComponent<Game> ().GameData = gameData;
			Score score = game.GetComponent<Score> ();
			score.OnScoreChanged += SetVillageBar;
			score.OnHitFlyNote += OnEnemyHit;
      score.OnExtraPointsChanged += SetExtraPointLable;

			Indian indian = game.transform.GetComponentInChildren<Indian> ();
			indian.OnStaminaChanged += SetStaminaBar;
			characterStamina = indian.IndianData.Stamina;
		}

    private void SetExtraPointLable(int extraPoints){

    }

		private void SetVillageBar (int villagePoints)
		{
			village_bar.fillAmount = (float)villagePoints / ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints"); // wenn federn implementiert werde muss das geändert werden
			villagePointsHelper.points = (float)villagePoints;
		}

		private void SetStaminaBar (float stamina)
		{
			UIWidget staminaWidget = stamina_bar.GetComponent<UIWidget> ();
			if (stamina < 1.0f / characterStamina) {
				//HOTween.To(staminaWidget.color, 0.1f, "color", new Color(1.0f, 0, 0));
				staminaWidget.color = new Color (0.603f, 0, 0);
			}
			else if (stamina < 0.5f) {
				//HOTween.To(staminaWidget.color, 0.1f, "color", new Color(0.6f, 0.4f, 0));
				staminaWidget.color = new Color (1.0f, 0.765f, 0);
			}
			else {
				//HOTween.To(staminaWidget.color, 0.1f, "color", new Color(0, 1.0f, 0));
				staminaWidget.color = new Color (0.294f, 0.843f, 0);
			}
			stamina_bar.fillAmount = stamina;
		}


    private void OnEnemyHit(Enemy enemy, FlyNoteData fld) {
      		string message = fld.Message;
    		if(message.Substring(0,1).Equals("-")) {
				message = "0";
			}
			message = string.Format(message, enemy.Type + "s");

			flynoteCounter += 1;
			onHit_flynote = NGUITools.AddChild (sprite, onHit_Flynote_Prefab);
			onHit_flynote.AddComponent("FlyNote");
			onHit_flynote.name = "Flynote_" + flynoteCounter;
			onHit_flynote.SetActive(false);
			onHit_flynote.transform.position = enemy.transform.position;
			//Debug.Log("msg:" + message + " at position: " + enemy.transform.position);

			UILabel flynoteLabel = sprite.transform.FindChild("Flynote_" + flynoteCounter).gameObject.GetComponent<UILabel>();
			UIWidget flynoteWidget = flynoteLabel.GetComponent<UIWidget>();


			if (fld.Type == FlyNoteData.HitsTypeSpecific || fld.Type == FlyNoteData.HitsNotTypeSpecific) {
				flynoteWidget.color = new Color(0.0f, 1.0f, 0.0f);
				flynoteLabel.text = message;
				onHit_flynote.SetActive(true);

			} else if( fld.Type == FlyNoteData.KillsTypeSpecific || fld.Type == FlyNoteData.KillsNotTypeSpecific) {
				flynoteWidget.color = new Color(1.0f, 0f, 0f);
				flynoteLabel.text = message;
				onHit_flynote.SetActive(true);
			}
			else {
				flynoteWidget.color = new Color(1.0f, 1.0f, 1.0f);
				flynoteLabel.text = message;
				onHit_flynote.SetActive(true);
			} 
					

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
			GameObject center = sprite.transform.FindChild ("Center").gameObject;
			pauseMessage = center.transform.FindChild("PauseLabel").gameObject;
			GameObject villageDamage = topRight.transform.FindChild ("Dorf_Damage_Prefab").gameObject;
			GameObject villagePoints = topRight.transform.FindChild ("villagePoints_label").gameObject;
			villagePointsHelper = villagePoints.GetComponent<PointsHelper>();
      		villagePointsHelper.points = (float) ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints");
			pauseButton = topRight.transform.FindChild ("pause_icon").gameObject;
			GameObject stamina = topLeft.transform.FindChild ("Stamina_Bar_Prefab").gameObject;

			GameObject goldPoints = topRight.transform.FindChild ("goldPoints_label").gameObject;
			goldPointsHelper = goldPoints.GetComponent<PointsHelper>();

			village_bar = villageDamage.transform.FindChild ("dorf_fg").gameObject.GetComponent<UISprite> ();
			stamina_bar = stamina.transform.FindChild ("stamina_fg").gameObject.GetComponent<UISprite> ();

			UIEventListener.Get(pauseButton).onClick += OnPauseButtonClicked;
		}

		private void OnPauseButtonClicked(GameObject button) {
			// TODO!

			UISprite pauseSprite = pauseButton.GetComponent<UISprite>();
			if(!isPaused) {
        		Time.timeScale = 0;
				isPaused = true;
				pauseSprite.spriteName = "play_icon";
				pauseMessage.SetActive(true);
			} else {
        		Time.timeScale = rememberTimeScale;
				isPaused = false;
				pauseSprite.spriteName = "pause_icon";
				pauseMessage.SetActive(false);
			}

		}
		
		public void Back ()
		{
			throw new System.NotImplementedException ();
		}

	}

}