using System;
using UnityEngine;
using PYIV.Menu.Popup;


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
		}
				
		
		//Gets called after Panel is initialized
		public override void UnpackParameter (object parameter)
		{
			PopupParam param = parameter as PopupParam;
			textLabel.text = param.Text;
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

