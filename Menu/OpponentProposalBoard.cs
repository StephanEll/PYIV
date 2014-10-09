using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Menu.Commands;

namespace PYIV.Menu
{
	public class OpponentProposalBoard
	{
		
		private GameObject parent;
		private Player player;
		
		private UILabel nameLabel;
		private UILabel scoreLabel;
		
		public OpponentProposalBoard (GameObject parent, Player player)
		{
			this.parent = parent;
			this.player = player;
			
			GameObject proposalBoardPrefab = Resources.Load<GameObject>("Prefabs/UI/OpponentProposalBoard");
			var proposalBoard = NGUITools.AddChild(parent, proposalBoardPrefab);


			scoreLabel = proposalBoard.transform.Find("score_label").gameObject.GetComponent<UILabel>();
			nameLabel = proposalBoard.transform.Find("name_label").gameObject.GetComponent<UILabel>();

			nameLabel.text = player.Name;
			scoreLabel.text = player.Score.ToString() + " Points";
			
			UIEventListener.Get(proposalBoard).onClick += HandleClick;
			
		}
		
		private void HandleClick(GameObject proposalBoard){
			ICommand playerFoundCommand = new PlayerSearchSuccessfulCommand(player);
			playerFoundCommand.Execute();
		}
		
		

	}
}

