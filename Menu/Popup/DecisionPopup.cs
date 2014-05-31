using System;

namespace PYIV.Menu.Popup
{
	public class DecisionPopup : BasePopupView
	{
		public DecisionPopup () : base()
		{
		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			DecisionPopupParam param = parameter as DecisionPopupParam;
			
		}
	}
}

