using System;
using UnityEngine;
using PYIV.Persistence;

namespace PYIV.Menu
{
	public class HighscoreField
	{
		
		private GameObject parent;
		private HighscoreModel highscoreModel;
		
		private UILabel rank; 
		private	UILabel playerName; 
		private	UILabel score; 
		private	UILabel lostCount; 
		private GameObject attackButton;
		
		public HighscoreField (GameObject parent, HighscoreModel highscoreModel)
		{
			this.parent = parent;
			this.highscoreModel = highscoreModel;
			
			
			GameObject highscoreBoard = Resources.Load<GameObject>("Prefabs/UI/HighscoreBoard");
			
			var position = highscoreModel.Position > 5 ? "6" : highscoreModel.Position.ToString();
			
			highscoreBoard.name = "item"+position;

			// Child hinzufügen
			var highscoreBoardObj = NGUITools.AddChild(parent, highscoreBoard);

			
			// Boardinhalt holen:
			rank = highscoreBoardObj.transform.Find("rank").gameObject.GetComponent<UILabel>();
			playerName = highscoreBoardObj.transform.Find("playerName").gameObject.GetComponent<UILabel>();
			score = highscoreBoardObj.transform.Find("score").gameObject.GetComponent<UILabel>();
			GameObject attackButton = highscoreBoardObj.transform.Find("war_symbol").gameObject;
			
			
			rank.text = highscoreModel.Position.ToString();
			playerName.text = highscoreModel.PlayerName;
			score.text = highscoreModel.Score.ToString();
			
		}
	}
}

