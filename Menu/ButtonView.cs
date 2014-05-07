using System;
using UnityEngine;
namespace PYIV.Menu
{
	public class ButtonView : GuiView
	{
		private GameObject button;
		
		public ButtonView () : base("Button")
		{
			
		}
		
		public void AddToScreen (GameObject guiParent, GameObject sceneParent) {
			base.AddToScreen(guiParent, sceneParent);
		}
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			button = panel.transform.FindChild("Button").gameObject;
			UIEventListener.Get(button).onClick += OnClick;
		}
		
		private void OnClick(GameObject button){
			this.GetViewRouter().ShowView(typeof(RegisterView));
		}
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

