using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Menu.Commands;

namespace PYIV.Menu
{
	public class VillageProtectedView : GuiView
	{

		private GameData gameData;
		private GameObject okButton;
		private UILabel msgLabel;
		private TweenPosition tp;

		public VillageProtectedView () : base("VillageProtectedPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents ();

		}

		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			this.gameData = parameter as GameData;

			ICommand saveResultsCommand = new SaveGameResultsCommand (this.gameData, tp);
			saveResultsCommand.Execute ();
			
			if (gameData.MyStatus.LatestCompletedRound.ScoreResult.IsVillageDestroyed) {
				msgLabel.text = StringConstants.VILLAGE_DESTROYED;
			} else {
				msgLabel.text = StringConstants.VILLAGE_PROTECTED;
			}
		}
		
		private void InitViewComponents ()
		{
			
			GameObject sprite = panel.transform.FindChild ("Sprite").gameObject;
			GameObject bottomAnchorLinks = sprite.transform.FindChild ("BottomAnchorLinks").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild ("TopAnchorInteraction").gameObject;
			msgLabel = topAnchorInteraction.transform.FindChild ("VillageProtectedLabel").gameObject.GetComponent<UILabel> ();
			okButton = bottomAnchorLinks.transform.FindChild ("ok_button").gameObject;
			tp = okButton.GetComponent<TweenPosition> ();


			

			UIEventListener.Get (okButton).onClick += OnOKButtonClicked;
		}

		private void OnOKButtonClicked (GameObject button)
		{
			ViewRouter.TheViewRouter.ShowViewWithParameter (typeof(GameResultView), gameData);
		}
		
		public override bool ShouldBeCached ()
		{
			//immer neu erstellen, um keine Vorbelegung zu haben
			return false;
		}
		
		public override void Back ()
		{
			
		}
	}
}

