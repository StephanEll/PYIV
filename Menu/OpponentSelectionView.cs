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
		private GameObject proposalGrid;
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
			
			foreach(Player player in randomPlayers){
				var board = new OpponentProposalBoard(proposalGrid, player);
			}
			
			
			
			
		}

		private void InitViewComponents() {

			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject TopAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;
			GameObject searchOpponentButton = TopAnchorInteraction.transform.FindChild("search_button").gameObject;
			opponentNameInput = TopAnchorInteraction.transform.FindChild("username_search_input").gameObject.GetComponent<UIInput>();
			
			proposalGrid = sprite.transform.Find("OpponentsListAnchor/ProposalGrid").gameObject;
			UIEventListener.Get(searchOpponentButton).onClick += OnSearchOpponentButtonClicked;


		}

		private void OnSearchOpponentButtonClicked(GameObject searchOpponentButton) {
			Player.FetchByName(opponentNameInput.value, OnPlayerFound, (e) => ViewRouter.TheViewRouter.ShowTextPopup(e.Message));
		}
		
		private void OnPlayerFound(Player player){
			ICommand playerFoundCommand = new PlayerSearchSuccessfulCommand(player);
			playerFoundCommand.Execute();
			
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

