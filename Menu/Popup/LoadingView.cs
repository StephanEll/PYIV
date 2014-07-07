using System;
using UnityEngine;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Persistence;


namespace PYIV.Menu.Popup
{
	public class LoadingView : GuiView
	{
		

		
		protected GameObject sprite;
		protected UILabel textLabel;
		
		public LoadingView () : base("Loading_Prefab")
		{
			
		}
		
		public LoadingView(string prefabName) : base(prefabName){
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			
			sprite = panel.transform.FindChild("Sprite").gameObject;
			textLabel = sprite.transform.FindChild("Popup_Text").GetComponent<UILabel>();
			textLabel.text = IndianSayings.GetSaying();
		}
				
		
		//Gets called after Panel is initialized
		public override void UnpackParameter (object parameter)
		{
			var param = parameter as LoadingPopupParam;
			param.Request.OnRequestCompleted += () => this.RemoveFromScreen();
			
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
		
		public override void Back ()
		{
			throw new NotImplementedException ();
		}
	}
}

