using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;

namespace PYIV.Menu
{
	public class VillageProtectedView : GuiView
	{

		private GameData gameData;
		
		public VillageProtectedView () : base("VillageProtectedPrefab")
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
			this.gameData = parameter as GameData;
		}
		

		
		private void InitViewComponents() {
			
			GameObject sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject bottomAnchorLinks = sprite.transform.FindChild("BottomAnchorLinks").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;

			GameObject okButton = bottomAnchorLinks.transform.FindChild("ok_button").gameObject;

			UIEventListener.Get(okButton).onClick += OnOKButtonClicked;
		}

		private void OnOKButtonClicked(GameObject button) {
			ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(VillageProtectedView), gameData);
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

