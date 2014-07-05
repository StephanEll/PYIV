using System;
using PYIV.Persistence;

namespace PYIV.Menu.Popup
{
	public class LoadingPopupParam : PopupParam
	{
		
		public Request Request { get; set; }
		
		public LoadingPopupParam (Request request) : base("")
		{
			this.Request = request;
		}
	}
}

