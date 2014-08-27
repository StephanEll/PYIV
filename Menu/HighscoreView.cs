using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Menu.Commands;

namespace PYIV.Menu
{
	public class HighscoreView : GuiView
	{

		private GameObject firstRanksGrid;
		private GameObject yourRanksGrid;


		public HighscoreView () : base("HighscorePrefab")
		{
			TouchScreenKeyboard.hideInput = true;

		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();			
		}
		
	
		
		private void InitViewComponents() {
			
			GameObject sprite = panel.transform.FindChild("Sprite").gameObject;

			// Buttons in den Ecken
			GameObject TopRightAnchor = sprite.transform.FindChild("TopRightAnchor").gameObject;
			GameObject logoutButton = TopRightAnchor.transform.FindChild("logout_icon").gameObject;
			GameObject TopLeftAnchor = sprite.transform.FindChild("TopLeftAnchor").gameObject;
			GameObject soundButton = TopLeftAnchor.transform.FindChild("lautsprecher_icon").gameObject;
			GameObject BottomLeftAnchor = sprite.transform.FindChild("BottomLeftAnchor").gameObject;
			GameObject highscoreButton = BottomLeftAnchor.transform.FindChild("pokal_icon").gameObject;
			GameObject BottomRightAnchor = sprite.transform.FindChild("BottomRightAnchor").gameObject;
			GameObject refreshButton = BottomRightAnchor.transform.FindChild("refresh_icon").gameObject;

			// zum Menü-Button
			GameObject to_menu_button = sprite.transform.Find("BottomAnchor/to_menu").gameObject;

			// Panels mit den Highscore-Boards
			firstRanksGrid = sprite.transform.Find("EntryPanel/FirstRanksGrid").gameObject;
			yourRanksGrid = sprite.transform.Find("EntryPanel/YourRanksGrid").gameObject;

			// Füllen der Highscore-Panels
			FillFirstRanksGrid();
			FillYourRanksGrid();

			// Eventlistener
			UIEventListener.Get(soundButton).onClick += OnSoundButtonClick;
			UIEventListener.Get(highscoreButton).onClick += OnHighscoreButtonClick;
			UIEventListener.Get(logoutButton).onClick += OnLogoutButtonClick;
			UIEventListener.Get(refreshButton).onClick += OnRefreshButtonClick;
		}


		private void FillFirstRanksGrid() {
			GameObject highscoreBoard = Resources.Load<GameObject>("Prefabs/UI/HighscoreBoard");

			// Child hinzufügen
			var highscoreBoardObj = NGUITools.AddChild(firstRanksGrid, highscoreBoard);

			// Boardinhalt holen:
			UILabel rank = highscoreBoardObj.transform.Find("rank").gameObject.GetComponent<UILabel>();
			UILabel playerName = highscoreBoardObj.transform.Find("playerName").gameObject.GetComponent<UILabel>();
			UILabel wonCount = highscoreBoardObj.transform.Find("wonCount").gameObject.GetComponent<UILabel>();
			UILabel lostCount = highscoreBoardObj.transform.Find("lostCount").gameObject.GetComponent<UILabel>();
			GameObject attackButton = highscoreBoardObj.transform.Find("war_symbol").gameObject;

			// Testinhalt:
			rank.text = "1.";
			playerName.text = "BlubBlub";
		}


		private void FillYourRanksGrid() {
			GameObject highscoreBoard = Resources.Load<GameObject>("Prefabs/UI/HighscoreBoard");
			
			// Child hinzufügen
			var highscoreBoardObj = NGUITools.AddChild(yourRanksGrid, highscoreBoard);
			
			// Boardinhalt holen:
			UILabel rank = highscoreBoardObj.transform.Find("rank").gameObject.GetComponent<UILabel>();
			UILabel playerName = highscoreBoardObj.transform.Find("playerName").gameObject.GetComponent<UILabel>();
			UILabel wonCount = highscoreBoardObj.transform.Find("wonCount").gameObject.GetComponent<UILabel>();
			UILabel lostCount = highscoreBoardObj.transform.Find("lostCount").gameObject.GetComponent<UILabel>();
			GameObject attackButton = highscoreBoardObj.transform.Find("war_symbol").gameObject;
			
			
			// Testinhalt:
			rank.text = "1.";
			playerName.text = "BlubBlub";
		}

		private void OnSoundButtonClick(GameObject button){
			// TO-DO
			Debug.Log ("SoundButton angelickt");
			ViewRouter.TheViewRouter.ShowPopupWithParameter(typeof(LoadingView), PopupParam.FromText(IndianSayings.GetSaying()));
		}
		
		private void OnHighscoreButtonClick(GameObject button){
			// TO-DO
			Debug.Log ("Highscore Button angelickt");
		}
		
		private void OnLogoutButtonClick(GameObject button){
			var logoutCommand = new LogOutCommand();
			logoutCommand.Execute();
		}
		
		private void OnRefreshButtonClick(GameObject button){
			Debug.Log ("Refresh Button angelickt");
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
		
		public override void Back ()
		{
			
		}
	}
}

