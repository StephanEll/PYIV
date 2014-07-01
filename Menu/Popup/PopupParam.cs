using System;
using UnityEngine;

namespace PYIV.Menu.Popup
{
	public class PopupParam
	{
		
		public string Text { get; set; }
		public UIEventListener.VoidDelegate OnClose { get; set; }
		
		public PopupParam (string text)
		{
			this.Text = text;
		}
		
		public static PopupParam FromText(string text){
			return new PopupParam(text);
		}
		
	}
}

