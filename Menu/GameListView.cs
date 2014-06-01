using System;
using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Helper;

namespace PYIV.Menu
{
	public class GameListView : GuiView{
		
		private GameObject sprite;
		private GameObject[] gameBoard;
		private UILabel playerName;
		private UILabel opponentName;
		private UILabel roundNr;
		private UISprite playerIcon;
		private UISprite opponentIcon;


		// TestData
		int gameCount = 8;
		string myName = "sergej";
		string opponentNameStr = "EvilMassai";
		int round = 2;


		
		
		public GameListView() : base("GameListPrefab")
		{
			TouchScreenKeyboard.hideInput = true;


		}
		
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();

			gameBoard = new GameObject[gameCount];
			for(int i = 0; i < gameCount; i++) {
				gameBoard[i] = Resources.Load<GameObject>("UI/GameBoard");

			}

			// Getting Components of View
			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject GameList_Panel = sprite.transform.FindChild("GameList_Panel").gameObject;
			GameObject GameList_Grid_GameObject = GameList_Panel.transform.FindChild("Grid").gameObject;
			UIGrid GameList_Grid = GameList_Panel.transform.FindChild("Grid").gameObject.GetComponent<UIGrid>();
			GameObject TopAnchor = sprite.transform.FindChild("TopAnchor").gameObject;
			GameObject newGameButton = TopAnchor.transform.FindChild("new_game_button").gameObject;


			// adding listener
			UIEventListener.Get(newGameButton).onClick += OnNewGameButtonClick;


			// adding Gameboards to View
			for(int i = 0; i < gameCount; i++) {

				// getting GameBoard components
				playerName = gameBoard[i].transform.FindChild("player_name_label").gameObject.GetComponent<UILabel>();
				opponentName = gameBoard[i].transform.FindChild("opponent_name_label").gameObject.GetComponent<UILabel>();
				roundNr = gameBoard[i].transform.FindChild("game_nr_label").gameObject.GetComponent<UILabel>();
				playerIcon = gameBoard[i].transform.FindChild("player_icon").gameObject.GetComponent<UISprite>();
				opponentIcon = gameBoard[i].transform.FindChild("opponent_icon").gameObject.GetComponent<UISprite>();

				// setting GameBoard content
				playerName.text = myName;
				opponentName.text = opponentNameStr;
				roundNr.text = round + "R";
				playerIcon.spriteName = "massai_icon";
				opponentIcon.spriteName = "amazone_icon";

				// adding Gameboard to Grid
				var obj = NGUITools.AddChild(GameList_Grid_GameObject, gameBoard[i]);
				UIEventListener.Get(obj).onClick += OnGameBoardClick;

			}
			GameList_Grid.Reposition();
		}

		
		private void OnNewGameButtonClick(GameObject button){

			// TO-DO
			Debug.Log ("NEW GAME angelickt");

		}

		private void OnGameBoardClick(GameObject button){
			
			// TO-DO
			Debug.Log ("Game angelickt");
			
		}

		
		public override bool ShouldBeCached ()
		{
			return true;
		}
		
	}
}

