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

			GameObject amazon = grid.transform.FindChild("Amazon").gameObject;
			GameObject indian = grid.transform.FindChild("Indian").gameObject;
			GameObject massai = grid.transform.FindChild("Massai").gameObject;

			UIEventListener.Get(nextButton).onClick += OnNextButtonClicked;
			UIEventListener.Get(amazon).onClick += OnIndianButtonClicked;
			UIEventListener.Get(indian).onClick += OnIndianButtonClicked;
			UIEventListener.Get(massai).onClick += OnIndianButtonClicked;
			
		}

		private void OnIndianButtonClicked(GameObject button) {

		}

		private void OnNextButtonClicked(GameObject button) {

		}
		
		public override bool ShouldBeCached ()
		{
			//immer neu erstellen, um keine Vorbelegung zu haben
			return false;
		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);

		}
		
		public override void Back ()
		{
			
		}
	}
}

