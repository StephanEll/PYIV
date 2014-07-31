using System;
using PYIV.Persistence;
using UnityEngine;
using PYIV.Helper;
using PYIV.Menu.MenuHelper;

namespace PYIV.Menu
{
	public class GameResultView : GuiView
	{
		
		private UILabel titleLabel;
		private GameObject boardParent;
		private PlayerResultBoard playerResultBoard;
		private PlayerResultBoard opponentResultBoard;
		private GameObject buttonsParent;
		
		private GameObject menuButton;
		private GameData gameData;
		
		public GameResultView () : base("GameResultPrefab")
		{
			
		}

		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			var container = panel.transform.Find ("Sprite");
			titleLabel = container.transform.Find ("TopAnchorLabel/title_label").GetComponent<UILabel> ();
			boardParent = container.transform.Find ("TopAnchorInteraction").gameObject;
			buttonsParent = container.transform.Find("BottomAnchorButton").gameObject;
			menuButton = buttonsParent.transform.Find("MenuButton").gameObject;
			Debug.Log ("MenuButton : " + menuButton);
			UIEventListener.Get(menuButton).onClick += ToMenu;
		}

		public override bool ShouldBeCached ()
		{
			return false;
		}

		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			this.gameData = parameter as GameData;
			
			
			
			ConfigureScreen ();
			
			
			
		}
		
		private void ConfigureScreen ()
		{
			SetTitle ();
			
			playerResultBoard = new PlayerResultBoard (gameData.MyStatus);	
			playerResultBoard.AddBoardToParent (boardParent, new Vector2 (-280, -200));
			
			
			Debug.Log("configure screen, the status of game is: " + GameState.OPPONENT_NEEDS_TO_PLAY);
			
			if(gameData.State != GameState.OPPONENT_NEEDS_TO_PLAY){
				
				opponentResultBoard = new PlayerResultBoard (gameData.OpponentStatus);
				opponentResultBoard.AddBoardToParent (boardParent, new Vector2 (415, -200));
				
				menuButton.transform.localPosition = new Vector3(-320, 0, 0);
				
				
				if(gameData.State == GameState.PLAYER_NEEDS_TO_CONFIGURE){
					GameObject nextRoundButton = ButtonHelper.AddButtonToParent(buttonsParent, StringConstants.BUTTON_NEXT_ROUND, new Vector2(320, 0));
					UIEventListener.Get(nextRoundButton).onClick += StartNextRound;
				}
			}
			
		}
		
		private void StartNextRound(GameObject go){
			Debug.Log("latest round should be unconfigured: " + !gameData.MyStatus.LatestRound.IsConfigured);
			
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(EnemySelectionView), new AttackConfigurationModel(gameData));
		}
		
		private void ToMenu(GameObject go){
			Debug.Log("to menu clicked");
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
		
		private void SetTitle ()
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

