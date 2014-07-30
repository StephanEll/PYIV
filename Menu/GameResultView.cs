using System;
using PYIV.Persistence;
using UnityEngine;
using PYIV.Helper;

namespace PYIV.Menu
{
	public class GameResultView : GuiView
	{
		
		private UILabel titleLabel;
		private GameObject boardParent;
		private PlayerResultBoard playerResultBoard;
		private PlayerResultBoard opponentResultBoard;
		
		public GameResultView () : base("GameResultPrefab")
		{
			
		}

		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			var container = panel.transform.Find ("Sprite");
			titleLabel = container.transform.Find ("TopAnchorLabel/title_label").GetComponent<UILabel> ();
			boardParent = container.transform.Find ("TopAnchorInteraction").gameObject;
			
			
		}

		public override bool ShouldBeCached ()
		{
			return false;
		}

		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			GameData gameData = parameter as GameData;
			
			
			
			ConfigureScreen (gameData);
			
			
			
		}
		
		private void ConfigureScreen (GameData gameData)
		{
			SetTitle (gameData);
			
			playerResultBoard = new PlayerResultBoard (gameData.MyStatus);	
			playerResultBoard.AddBoardToParent (boardParent, new Vector2 (-280, -200));
			
			if(gameData.State != GameState.OPPONENT_NEEDS_TO_PLAY){
				
				opponentResultBoard = new PlayerResultBoard (gameData.OpponentStatus);
				opponentResultBoard.AddBoardToParent (boardParent, new Vector2 (415, -200));
			}
			
		}
		
		private void SetTitle (GameData gameData)
		{
			switch(gameData.State){
			case GameState.WON:
				titleLabel.text = StringConstants.GAME_WON;
				break;
			case GameState.DRAW:
				titleLabel.text = StringConstants.GAME_DRAW;
				break;
			case GameState.LOST:
				titleLabel.text = StringConstants.GAME_LOST;
				break;
			}
		}

		public override void Back ()
		{
			ViewRouter.TheViewRouter.ShowView (typeof(GameListView));
		}
		
		
	}
}

