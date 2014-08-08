using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;
using System.Collections.Generic;
using System.Linq;
using PYIV.Menu.Commands;
namespace PYIV.Menu
{
	
	
	
	public class IndianSelectionView : GuiView
	{
		private List<EnemySelectionField> enemySelectionFields;
		private AttackConfigurationModel attackConfigurationModel;
		private GameObject grid;
		
		private List<IndianSelectionField> indianSelectionFields;
		
		public IndianSelectionView () : base("IndianSelectionPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();			
		}
		
		
		
		private void InitViewComponents() {
			
			GameObject sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;
			GameObject bottomAnchorButtons = sprite.transform.FindChild("BottomAnchorButton").gameObject; 
			GameObject nextButton = bottomAnchorButtons.transform.FindChild("Next_Button").gameObject;
			
			grid = topAnchorInteraction.transform.FindChild("IndianGrid").gameObject;
			UIEventListener.Get(nextButton).onClick += OnNextButtonClicked;
	
			
		}


		private void OnNextButtonClicked(GameObject button) {
			if(attackConfigurationModel.SelectedIndian != null){
				ViewRouter.TheViewRouter.ShowViewWithParameter(typeof(EnemySelectionView), attackConfigurationModel);
			}
			else{
				ViewRouter.TheViewRouter.ShowTextPopup(StringConstants.NO_INDIAN_SELECTED);
			}
		}
		
		public override bool ShouldBeCached ()
		{
			return true;
		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			
			attackConfigurationModel = parameter as AttackConfigurationModel;
			string[] indianIds = { "Amazone", "Indian", "Massai" };
			indianSelectionFields = (from id in indianIds select new IndianSelectionField(grid, id, attackConfigurationModel)).ToList();

		}
		
		public override void Back ()
		{
			
		}
	}
}

