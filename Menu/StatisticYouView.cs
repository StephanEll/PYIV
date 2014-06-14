using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Menu.Commands;

namespace PYIV.Menu
{
	public class StatisticYouView : GuiView
	{
		private GameObject sprite;
		private UILabel goldComplete_label;
		private UILabel kills_label;
		private UILabel damage_label;
		private UILabel gold_label;
		
		
		public StatisticYouView () : base("StatisticYouPrefab")
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
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;

			GameObject goldGroup = topAnchorInteraction.transform.FindChild("gold_group").gameObject;
			goldComplete_label = goldGroup.transform.FindChild("gold_complete_nr_label").gameObject.GetComponent<UILabel>();

			GameObject infoBoard = topAnchorInteraction.transform.FindChild("info_board").gameObject;
			kills_label = infoBoard.transform.FindChild("kills_nr_label").gameObject.GetComponent<UILabel>();
			gold_label = infoBoard.transform.FindChild("gold_nr_label").gameObject.GetComponent<UILabel>();
			damage_label = infoBoard.transform.FindChild("damage_nr_label").gameObject.GetComponent<UILabel>();

			GameObject bottomAnchorButton = sprite.transform.FindChild("BottomAnchorButton").gameObject;
			GameObject gameViewButton = bottomAnchorButton.transform.FindChild("gamelist_Button").gameObject;

			UIEventListener.Get(gameViewButton).onClick += OnGameViewButtonClicked;

		}
	

		private void OnGameViewButtonClicked(GameObject button) {
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}



		public void SetKills(String killsStr) {
			kills_label.text = killsStr;
		}
		public void SetGold(String goldStr) {
			gold_label.text = goldStr;
		}
		public void SetDamage(String damageStr) {
			damage_label.text = damageStr;
		}
		public void SetGoldComplete(String goldCompleteStr) {
			goldComplete_label.text = goldCompleteStr;
		}


		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

