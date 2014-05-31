using System;
using UnityEngine;

namespace PYIV.Menu.Popup
{
	public class DecisionPopupParam : PopupParam
	{
		
		public UIEventListener.VoidDelegate OnAccept { get; set; }
		public UIEventListener.VoidDelegate OnDecline { get; set; }
		
		public DecisionPopupParam(string text, UIEventListener.VoidDelegate OnAccept, UIEventListener.VoidDelegate OnDecline) : base(text){
			this.OnAccept = OnAccept;
			this.OnDecline = OnDecline;
		}
		
		
	}
}

