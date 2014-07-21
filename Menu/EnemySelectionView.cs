using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;

namespace PYIV.Menu
{



	public class EnemySelectionView : GuiView
	{
		private UILabel fledermaus_count;
		private UILabel ratte_count;
		private UILabel adler_count;
		private UILabel panther_count;
		private UILabel nashorn_count;
		private UILabel elefant_count;

				
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

			GameObject grid = topAnchorInteraction.transform.FindChild("EnemyGrid").gameObject;
			GameObject fledermaus_button = grid.transform.FindChild("1_fledermaus_icon").gameObject;
			GameObject ratte_button = grid.transform.FindChild("2_ratte_icon").gameObject;
			GameObject adler_button = grid.transform.FindChild("3_adler_icon").gameObject;
			GameObject panther_button = grid.transform.FindChild("4_panther_icon").gameObject;
			GameObject nashorn_button = grid.transform.FindChild("5_nashorn_icon").gameObject;
			GameObject elefant_button = grid.transform.FindChild("6_elefant_icon").gameObject;

			fledermaus_count = fledermaus_button.transform.FindChild("fledermaus_count").gameObject.GetComponent<UILabel>();
			ratte_count = ratte_button.transform.FindChild("ratte_count").gameObject.GetComponent<UILabel>();
			adler_count = adler_button.transform.FindChild("adler_count").gameObject.GetComponent<UILabel>();
			panther_count = panther_button.transform.FindChild("panther_count").gameObject.GetComponent<UILabel>();
			nashorn_count = nashorn_button.transform.FindChild("nashorn_count").gameObject.GetComponent<UILabel>();
			elefant_count = elefant_button.transform.FindChild("elefant_count").gameObject.GetComponent<UILabel>();

			UIEventListener.Get(fledermaus_button).onClick += OnEnemyButtonClicked;
			UIEventListener.Get(ratte_button).onClick += OnEnemyButtonClicked;
			UIEventListener.Get(adler_button).onClick += OnEnemyButtonClicked;
			UIEventListener.Get(panther_button).onClick += OnEnemyButtonClicked;
			UIEventListener.Get(nashorn_button).onClick += OnEnemyButtonClicked;
			UIEventListener.Get(elefant_button).onClick += OnEnemyButtonClicked;

			UIEventListener.Get(resetButton).onClick += OnResetButtonClicked;
			UIEventListener.Get(nextButton).onClick += OnNextButtonClicked;

		}

		private void OnEnemyButtonClicked(GameObject button) {
			Debug.Log(button.name + " clicked");
		}

		private void OnResetButtonClicked(GameObject button) {
			Debug.Log("Reset clicked");
		}

		private void OnNextButtonClicked(GameObject button) {
			Debug.Log("Next clicked");
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

