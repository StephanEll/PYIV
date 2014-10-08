using System;
using UnityEngine;
using PYIV.Persistence;

namespace PYIV.Menu
{
	public class PlayerResultBoard
	{
		private GameObject playerResultBoard;
		private UILabel playerNameLabel;
		private UILabel goldCount;
		private UILabel efficiencyCount;
		private UILabel villageCount;
		private UILabel specialKillCount;
		
		private PlayerStatus playerStatus;

		public PlayerResultBoard (PlayerStatus status)
		{
			this.playerStatus = status;
		}
		
		public void AddBoardToParent(GameObject parent, Vector2 position){
			var boardPrefab = Resources.Load<GameObject>("Prefabs/UI/PlayerResultBoard");
			playerResultBoard = NGUITools.AddChild(parent, boardPrefab);
			
			playerResultBoard.transform.localPosition = new Vector3(position.x, position.y);
			
			playerNameLabel = playerResultBoard.transform.Find ("player_name").GetComponent<UILabel>();
			goldCount = playerResultBoard.transform.Find ("gold_group/gold_count").GetComponent<UILabel>();
			efficiencyCount = playerResultBoard.transform.Find("info_board/row1_count").GetComponent<UILabel>();
			villageCount = playerResultBoard.transform.Find("info_board/row2_count").GetComponent<UILabel>();
			specialKillCount = playerResultBoard.transform.Find("info_board/row3_count").GetComponent<UILabel>();
		
			PopulateLabels();
		}
		
		private void PopulateLabels(){
			playerNameLabel.text = playerStatus.Player.Equals(LoggedInPlayer.Instance.Player) ? "You" : playerStatus.Player.Name;
			
			goldCount.text = playerStatus.LatestCompletedRound.ScoreResult.Gold.ToString();
			efficiencyCount.text = (playerStatus.LatestCompletedRound.ScoreResult.ShotEfficiencyPercent * 5).ToString();
			villageCount.text = playerStatus.LatestCompletedRound.ScoreResult.RemainingVillageLifepoints.ToString();
			specialKillCount.text = playerStatus.LatestCompletedRound.ScoreResult.ExtraPoints.ToString();
		}
	}
}

