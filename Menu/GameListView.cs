using System;
using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;
using PYIV.Menu.Popup;
using System.Collections.Generic;

namespace PYIV.Menu
{
	public class GameListView : GuiView{
		
		private GameObject sprite;
		private GameObject gameBoardPrefab;
		GameObject GameList_Grid_GameObject;
		private GameCollection serverGameCollection;
		private Dictionary<GameObject, GameData> buttonToGameData;
				
		
		public GameListView() : base("GameListPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
			buttonToGameData = new Dictionary<GameObject,GameData>();
		}
		

		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();

			LoggedInPlayer.Instance.GetOrFetchGameList(OnServerCollectionReceived, OnServerError);
			

		}

		
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
		}

		private void OnHighscoreButtonClick(GameObject button){
			// TO-DO
			Debug.Log ("Highscore Button angelickt");
		}

		private void OnLogoutButtonClick(GameObject button){
			LoggedInPlayer.Instance.LogOut();
			ViewRouter.TheViewRouter.ShowView(typeof(LoginView));
		}


		private void OnServerCollectionReceived(GameCollection serverCollection) {
			
			serverGameCollection = serverCollection;
			gameBoardPrefab = Resources.Load<GameObject>("Prefabs/UI/GameBoard");
			
			CreateGameBoardsFromCollection();
			
						
		}
		
		private void CreateGameBoardsFromCollection() {
			foreach(var obj in serverGameCollection.ModelList) {
				FillGameBoard(obj);
			}
			GameList_Grid_GameObject.GetComponent<UIGrid>().Reposition();
		}

		private void OnServerError(RestException e) {
			ViewRouter.TheViewRouter.ShowTextPopup(e.Message);
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
			
			// setting GameBoard content
			playerName.text = gameData.MyStatus.Player.Name;
			opponentName.text = gameData.OpponentStatus.Player.Name;
			roundNr.text = gameData.MyStatus.Rounds.Count + " R";

			// TODO: Belegung des Icons je nach gespielten IndianType
			playerIcon.spriteName = "massai_icon";
			opponentIcon.spriteName = "amazone_icon";
			
		}

		private void InitViewComponents() {
			
			Debug.Log ("init view components");
			
			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject GameList_Panel = sprite.transform.FindChild("GameList_Panel").gameObject;
			GameList_Grid_GameObject = GameList_Panel.transform.FindChild("Grid").gameObject;
			UIGrid GameList_Grid = GameList_Panel.transform.FindChild("Grid").gameObject.GetComponent<UIGrid>();
			
			GameObject TopAnchor = sprite.transform.FindChild("TopAnchor").gameObject;
			GameObject newGameButton = TopAnchor.transform.FindChild("new_game_button").gameObject;
			
			GameObject BottomLeftAnchor = sprite.transform.FindChild("BottomLeftAnchor").gameObject;
			GameObject soundButton = BottomLeftAnchor.transform.FindChild("lautsprecher_icon").gameObject;
			
			GameObject BottomRightAnchor = sprite.transform.FindChild("BottomRightAnchor").gameObject;
			GameObject highscoreButton = BottomRightAnchor.transform.FindChild("pokal_icon").gameObject;
			
			GameObject TopRightAnchor = sprite.transform.FindChild("TopRightAnchor").gameObject;
			GameObject logoutButton = TopRightAnchor.transform.FindChild("logout_icon").gameObject;
			
			
			// adding listener
			UIEventListener.Get(newGameButton).onClick += OnNewGameButtonClick;
			UIEventListener.Get(soundButton).onClick += OnSoundButtonClick;
			UIEventListener.Get(highscoreButton).onClick += OnHighscoreButtonClick;
			UIEventListener.Get(logoutButton).onClick += OnLogoutButtonClick;
			GameList_Grid.Reposition();
		}


		public override bool ShouldBeCached ()
		{
			return false;
		}
		
	}
}

