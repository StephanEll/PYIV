using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;
using System.Collections.Generic;
using System.Linq;
namespace PYIV.Menu
{



	public class EnemySelectionView : GuiView
	{
		private List<EnemySelectionField> enemySelectionFields;
		private EnemySelectionModel enemySelectionModel;
		private GameObject grid;
				
		public EnemySelectionView () : base("EnemySelectionPrefab")
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
			GameObject resetButton = bottomAnchorButtons.transform.FindChild("Reset_Button").gameObject; 
			GameObject nextButton = bottomAnchorButtons.transform.FindChild("Next_Button").gameObject;

			grid = topAnchorInteraction.transform.FindChild("EnemyGrid").gameObject;
			
			

			UIEventListener.Get(resetButton).onClick += OnResetButtonClicked;
			UIEventListener.Get(nextButton).onClick += OnNextButtonClicked;

		}


		private void OnResetButtonClicked(GameObject button) {
			enemySelectionModel.ResetAttackers();
		}

		private void OnNextButtonClicked(GameObject button) {
			Debug.Log("Next clicked");
		}
		
		public override bool ShouldBeCached ()
		{
			//immer neu erstellen, um keine Vorbelegung zu haben
			return false;
		}
		
		public override void UnpackParameter (object parameter)
		{
			base.UnpackParameter (parameter);
			GameData activeGame = parameter as GameData;
			
			enemySelectionModel = new EnemySelectionModel(activeGame.MyStatus);
			enemySelectionModel.OnChange += () => Debug.Log (enemySelectionModel.Gold);
			
			string[] enemyTypeIds = { "Rat1", "Eagle1", "Panther1", "Rhino1", "Bat1", "Elephant1" };
			enemySelectionFields = (from id in enemyTypeIds select new EnemySelectionField(grid, id, enemySelectionModel)).ToList();
		}
		
		public override void Back ()
		{
			
		}
	}
}

