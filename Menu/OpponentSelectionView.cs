using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Menu.Commands;
using System.Collections.Generic;

namespace PYIV.Menu
{
	public class OpponentSelectionView : GuiView
	{
		private GameObject sprite;
		private UIInput opponentNameInput;
		private GameObject opponentsListAnchor;
		private List<Player> randomPlayers;
		public OpponentSelectionView () : base("OpponentSelectionPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();
			

		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			randomPlayers = parameter as List<Player>;
			
			Debug.Log("RAND PLYR: "+randomPlayers[0].ToString());
			
			for(int i = 0; i < 3; i++){
				GameObject opponent_board = opponentsListAnchor.transform.FindChild("opponent_"+(i+1)).gameObject;
				opponent_board.transform.Find("opponent_"+(i+1)+"_label").GetComponent<UILabel>().text = randomPlayers[i].Name;
				
				UIEventListener.Get(opponent_board).onClick += OnRandomAttackClicked;
				
				
			}
			
			
		}

		private void InitViewComponents() {

			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject TopAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;
			GameObject searchOpponentButton = TopAnchorInteraction.transform.FindChild("search_button").gameObject;
			opponentNameInput = TopAnchorInteraction.transform.FindChild("username_search_input").gameObject.GetComponent<UIInput>();
			
			opponentsListAnchor = sprite.transform.FindChild("OpponentsListAnchor").gameObject;
			UIEventListener.Get(searchOpponentButton).onClick += OnSearchOpponentButtonClicked;


		}

		private void OnSearchOpponentButtonClicked(GameObject searchOpponentButton) {
			Player.FetchByName(opponentNameInput.value, OnPlayerFound, (e) => ViewRouter.TheViewRouter.ShowTextPopup(e.Message));
		}
		
		private void OnPlayerFound(Player player){
			ICommand playerFoundCommand = new PlayerSearchSuccessfulCommand(player);
			playerFoundCommand.Execute();
			
		}

		private void OnRandomAttackClicked(GameObject opponent_1_button) {
			OnPlayerFound(randomPlayers[0]);
		}

		
		public override bool ShouldBeCached ()
		{
			return true;
		}
		
		public override void Back ()
		{
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
	}
}

