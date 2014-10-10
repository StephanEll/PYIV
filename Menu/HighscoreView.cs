using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Helper;
using PYIV.Menu.Commands;
using System.Collections.Generic;

namespace PYIV.Menu
{
	public class HighscoreView : GuiView
	{

		private GameObject firstRanksGrid;

		public HighscoreView () : base("HighscorePrefab")
		{
			TouchScreenKeyboard.hideInput = true;

		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();
			
			LoadHighscoreFromServer();
		}
		
		private void LoadHighscoreFromServer(){
			var req = new Request<List<HighscoreModel>>("highscore", RestSharp.Method.GET, false);
			req.OnSuccess += OnHighscoreReceived;
			req.ExecuteAsync();
		}

		void OnHighscoreReceived (List<HighscoreModel> highscoreList)
		{
			
			bool dotsAdded = false;
			
			for(int i = 0; i < highscoreList.Count; i++){
				
				if(highscoreList[i].Position != i+1){
					AddDots((i+1).ToString());
					dotsAdded = true;
					Debug.Log ("Add dots at pos: "+i);
				}
				
				HighscoreField field = new HighscoreField(firstRanksGrid, highscoreList[i]);
			}
			
			if(!dotsAdded){
				AddDots("zDots");		
			}
			firstRanksGrid.GetComponent<UIGrid>().Reposition();

		}
		
		private void AddDots(string index){
			GameObject dots = Resources.Load<GameObject>("Prefabs/UI/HighscoreDots");
		
			dots = NGUITools.AddChild(firstRanksGrid, dots);
			dots.name = "item"+index;
			dots.transform.localScale = new Vector3(0.25f, 0.25f, 1f);
		}
	
		
		private void InitViewComponents() {			
			GameObject sprite = panel.transform.FindChild("Sprite").gameObject;

			// zum Menü-Button
			GameObject to_menu_button = sprite.transform.Find("BottomAnchor/to_menu").gameObject;
			UIEventListener.Get (to_menu_button).onClick += (go) => Back ();

			// Panels mit den Highscore-Boards
			firstRanksGrid = sprite.transform.Find("EntryPanel/FirstRanksGrid").gameObject;

			
		}


	
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
		
		public override void Back ()
		{
			ViewRouter.TheViewRouter.ShowView(typeof(GameListView));
		}
	}
}

