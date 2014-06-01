using System;

namespace PYIV.Menu.Popup
{
	public static class ViewRouterConvenienceExtensions
	{
		public static void ShowTextPopup(this ViewRouter router, string text){
			router.ShowPopupWithParameter(typeof(BasePopupView), PopupParam.FromText(text));
		}
	}
}

