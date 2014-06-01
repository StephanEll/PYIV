using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;

namespace PYIV.Menu
{
	public class OpponentSelectionView : GuiView
	{
		private GameObject sprite;

		
		public OpponentSelectionView () : base("OpponentSelectionPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();

		}

		private void InitViewComponents() {

			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject TopAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;
			GameObject searchOpponentButton = TopAnchorInteraction.transform.FindChild("search_button").gameObject;
			UIInput opponentNameInput = TopAnchorInteraction.transform.FindChild("username_search_input").gameObject.GetComponent<UIInput>();
			
			GameObject OpponentsListAnchor = sprite.transform.FindChild("OpponentsListAnchor").gameObject;
			GameObject opponent_1 = OpponentsListAnchor.transform.FindChild("opponent_1").gameObject;
			GameObject opponent_2 = OpponentsListAnchor.transform.FindChild("opponent_2").gameObject;
			GameObject opponent_3 = OpponentsListAnchor.transform.FindChild("opponent_3").gameObject;

			UIEventListener.Get(searchOpponentButton).onClick += OnSearchOpponentButtonClicked;
			UIEventListener.Get(opponent_1).onClick += OnOpponent_1_BoardClicked;
			UIEventListener.Get(opponent_2).onClick += OnOpponent_2_BoardClicked;
			UIEventListener.Get(opponent_3).onClick += OnOpponent_3_BoardClicked;

		}

		private void OnSearchOpponentButtonClicked(GameObject searchOpponentButton) {
			// TODO
			Debug.Log("search opponent button clicked");
		}

		private void OnOpponent_1_BoardClicked(GameObject opponent_1_button) {
			// TODO
			Debug.Log("search opponent 1 button clicked");
		}

		private void OnOpponent_2_BoardClicked(GameObject opponent_2_button) {
			// TODO
			Debug.Log("search opponent 2 button clicked");
		}

		private void OnOpponent_3_BoardClicked(GameObject opponent_3_button) {
			// TODO
			Debug.Log("search opponent 3 button clicked");
		}

	
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

