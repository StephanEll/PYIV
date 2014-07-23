using System;
using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;
using PYIV.Menu.Popup;
using System.Collections.Generic;
using PYIV.Menu.Commands;

namespace PYIV.Menu
{
	public class GameListView : GuiView{
		
		private GameObject sprite;
		private GameObject gameBoardPrefab;
		GameObject GameList_Grid_GameObject;
		private Dictionary<GameObject, GameData> buttonToGameData;

		
		//########################
		//CONSTRUCTORS
		//########################
		
		public GameListView() : base("GameListPrefab")
		{			
			TouchScreenKeyboard.hideInput = true;
			buttonToGameData = new Dictionary<GameObject,GameData>();
			
		}
		
		//########################
		//OVERRIDDEN METHODS FROM BASEVIEW
		//########################
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();
			LoggedInPlayer.Instance.GetOrFetchGameList(OnGameCollectionReceived, OnServerError);
		}
		
		public override void AddToScreen (GameObject guiParent, GameObject sceneParent)
		{
			base.AddToScreen (guiParent, sceneParent);
			LoggedInPlayer.Instance.NotificationHandler.CommandQueue.OnFirstCommandAdded += ExecuteFirstCommand;
			ExecuteFirstCommand();
		}
		
		public override void RemoveFromScreen ()
		{
			base.RemoveFromScreen();
			
			//Unregister listeners
			if(LoggedInPlayer.IsLoggedIn()){
				LoggedInPlayer.Instance.GameList.OnChange -= RefreshGameBoardsFromCollection;
				LoggedInPlayer.Instance.NotificationHandler.CommandQueue.OnFirstCommandAdded -= ExecuteFirstCommand;
			}
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
		
		public override void Back ()
		{
			Application.Quit();
		}
		
		//########################
		// BUTTON CALLBACKS
		//########################

		
		private void OnNewGameButtonClick(GameObject button){
			ViewRouter.TheViewRouter.ShowView(typeof(OpponentSelectionView));
		}

		private void OnGameBoardClick(GameObject button){
			GameData data = buttonToGameData[button];
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(GameView), data);
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
			var syncCommand = new SyncCommand(false ,LoggedInPlayer.Instance.NotificationHandler.CommandQueue);
			syncCommand.Execute();
		}
		
		
		//########################
		// PANEL INITIALIZATION
		//########################
		
		
		private void ShowNoGamesSign() {
			GameObject BottomAnchor = sprite.transform.FindChild("BottomAnchor").gameObject;
			if(LoggedInPlayer.Instance.GameList.RunningGames.Count == 0) {
				BottomAnchor.SetActive(true);
			} else {
				BottomAnchor.SetActive(false);
			}
		}
		
		private void FillGameBoard(GameData gameData) {
			var gameBoardObj = NGUITools.AddChild(GameList_Grid_GameObject, gameBoardPrefab);
			
			buttonToGameData[gameBoardObj] = gameData;
			UIEventListener.Get(gameBoardObj).onClick += OnGameBoardClick;

			UILabel playerName = gameBoardObj.transform.FindChild("player_name_label").gameObject.GetComponent<UILabel>();
			UILabel opponentName = gameBoardObj.transform.FindChild("opponent_name_label").gameObject.GetComponent<UILabel>();
			UILabel roundNr = gameBoardObj.transform.FindChild("game_nr_label").gameObject.GetComponent<UILabel>();
			UISprite playerIcon = gameBoardObj.transform.FindChild("player_icon").gameObject.GetComponent<UISprite>();
			UISprite opponentIcon = gameBoardObj.transform.FindChild("opponent_icon").gameObject.GetComponent<UISprite>();
			GameObject player_arrow = gameBoardObj.transform.FindChild("player_arrow").gameObject;
			GameObject opponent_arrow = gameBoardObj.transform.FindChild("opponent_arrow").gameObject;
			GameObject inactive = gameBoardObj.transform.FindChild("inactive").gameObject;
			
			// setting GameBoard content
			playerName.text = gameData.MyStatus.Player.Name;
			opponentName.text = gameData.OpponentStatus.Player.Name;
			roundNr.text = gameData.MyStatus.Rounds.Count + ". R";
			playerIcon.spriteName = gameData.MyStatus.IndianData.SpriteImageName;
			opponentIcon.spriteName = gameData.OpponentStatus.IndianData.SpriteImageName;

			// inactive gameboard, if game not accepted yet
			if(!gameData.OpponentStatus.IsChallengeAccepted) {
				inactive.SetActive(true);
			}

			// wer dran ist, ist bunt - wer nicht, schwarz-weiss
			if(gameData.IsMyTurn()) {
				opponentIcon.spriteName += "_bw";
				opponent_arrow.SetActive(false);
			} else {
				playerIcon.spriteName += "_bw";				
				player_arrow.SetActive(false);
			}

		}

		private void InitViewComponents() {
			
			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject GameList_Panel = sprite.transform.FindChild("GameList_Panel").gameObject;
			GameList_Grid_GameObject = GameList_Panel.transform.FindChild("Grid").gameObject;
			UIGrid GameList_Grid = GameList_Panel.transform.FindChild("Grid").gameObject.GetComponent<UIGrid>();
			GameList_Grid.repositionNow = true;
			
			GameObject TopAnchor = sprite.transform.FindChild("TopAnchor").gameObject;
			GameObject newGameButton = TopAnchor.transform.FindChild("new_game_button").gameObject;

			GameObject TopRightAnchor = sprite.transform.FindChild("TopRightAnchor").gameObject;
			GameObject logoutButton = TopRightAnchor.transform.FindChild("logout_icon").gameObject;

			GameObject TopLeftAnchor = sprite.transform.FindChild("TopLeftAnchor").gameObject;
			GameObject soundButton = TopLeftAnchor.transform.FindChild("lautsprecher_icon").gameObject;
			
			GameObject BottomLeftAnchor = sprite.transform.FindChild("BottomLeftAnchor").gameObject;
			GameObject highscoreButton = BottomLeftAnchor.transform.FindChild("pokal_icon").gameObject;
			
			GameObject BottomRightAnchor = sprite.transform.FindChild("BottomRightAnchor").gameObject;
			GameObject refreshButton = BottomRightAnchor.transform.FindChild("refresh_icon").gameObject;



			// adding listener
			UIEventListener.Get(newGameButton).onClick += OnNewGameButtonClick;
			UIEventListener.Get(soundButton).onClick += OnSoundButtonClick;
			UIEventListener.Get(highscoreButton).onClick += OnHighscoreButtonClick;
			UIEventListener.Get(logoutButton).onClick += OnLogoutButtonClick;
			UIEventListener.Get(refreshButton).onClick += OnRefreshButtonClick;
			GameList_Grid.Reposition();
		}
		
		
		
		//########################
		// GAMEBOARD-MANAGEMENT
		//########################

		private void OnGameCollectionReceived(GameCollection serverCollection) {
			
			gameBoardPrefab = Resources.Load<GameObject>("Prefabs/UI/GameBoard");
			CreateGameBoardsFromCollection();
			LoggedInPlayer.Instance.GameList.OnChange += RefreshGameBoardsFromCollection;			
		}
		
		private void CreateGameBoardsFromCollection() {
			foreach(var obj in LoggedInPlayer.Instance.GameList.RunningGames) {
				FillGameBoard(obj);
			}
			
			GameList_Grid_GameObject.GetComponent<UIGrid>().Reposition();
			ShowNoGamesSign();
		}


		private void RefreshGameBoardsFromCollection() {
			var itemsToDelete = new List<GameObject>();
			//copy list
			foreach(Transform obj in GameList_Grid_GameObject.transform) {
				itemsToDelete.Add(obj.gameObject);
			}
			
			itemsToDelete.ForEach((obj) => NGUITools.Destroy(obj));

			CreateGameBoardsFromCollection();
		}

		

		private void OnServerError(RestException e) {
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
		}
		
		
		//########################
		// COMMAND EXECUTION
		//########################
		
		
		private void ExecuteFirstCommand(){
			if(LoggedInPlayer.Instance.NotificationHandler.CommandQueue.Count > 0){
				LoggedInPlayer.Instance.NotificationHandler.CommandQueue.Dequeue().Execute();

			}
		}

		
		
	}
}

