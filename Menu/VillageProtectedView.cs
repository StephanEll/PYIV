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

		
		public VillageProtectedView () : base("VillageProtectedPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();			
		}
		

		
		private void InitViewComponents() {
			

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

