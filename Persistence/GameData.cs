using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace PYIV.Persistence
{
	[DataContract]
	[Serializable]
	public class GameData : ServerModel<GameData>
	{		
		[DataMember]
		public List<PlayerStatus> PlayerStatus { get; set; }		
		
		[IgnoreDataMember]
		public DateTime CreatedAt { get; set; }
		
		[IgnoreDataMember]
		public DateTime UpdatedAt { get; set; }
		
		[IgnoreDataMember]
		public GameState State {
			get{
				return DetermineGameState();
			}
		}
		
		[IgnoreDataMember]
		public PlayerStatus MyStatus {
			get{
				return this.GetPlayerOrOpponentStatus(true);
			}
			
		}
		
		public bool IsSynced { get; set; }
		
		[IgnoreDataMember]
		public PlayerStatus OpponentStatus {
			get{
				return this.GetPlayerOrOpponentStatus(false);
			}
		}
		
		
		public GameData(){
			PlayerStatus = new List<PlayerStatus>(2);
			IsSynced = true;

		}
		
		public GameData(Player challenger, Player defender) : this(){
			
			
			PlayerStatus.Add(new PlayerStatus(challenger));
			PlayerStatus.Add(new PlayerStatus(defender));
			MyStatus.IsChallengeAccepted = true;
		}
		
		public override void ParseOnCreate (GameData responseObject)
		{
			base.ParseOnCreate (responseObject);
			
			IsSynced = true;
			
			this.CreatedAt = responseObject.CreatedAt;
			this.UpdatedAt = responseObject.UpdatedAt;
			
			//Parse player status
			MyStatus.ParseOnCreate(responseObject.MyStatus);
			//Parse opponent status
			OpponentStatus.ParseOnCreate(responseObject.OpponentStatus);	

			
		}
		
		protected override void HandleError (PYIV.Persistence.Errors.RestException e)
		{
			base.HandleError (e);
			IsSynced = false;
		}
		
		public PlayerStatus GetPlayerOrOpponentStatus(bool isPlayerOfDevice){
			
			PlayerStatus player = PlayerStatus[0].Player.Equals(LoggedInPlayer.Instance.Player) ? PlayerStatus[0] : PlayerStatus[1];
			PlayerStatus opponent = !PlayerStatus[0].Player.Equals(LoggedInPlayer.Instance.Player) ? PlayerStatus[0] : PlayerStatus[1];		
			
			return isPlayerOfDevice ? player : opponent ;
		}


		public bool IsMyTurn() {
			System.Random rand = new System.Random();
			return rand.Next(2) == 0;
		}
		
		private GameState DetermineGameState(){
			if(MyStatus.LatestRound.IsComplete && OpponentStatus.LatestRound.IsComplete){
				if(MyStatus.LatestRound.ScoreResult.IsVillageDestroyed && OpponentStatus.LatestRound.ScoreResult.IsVillageDestroyed){
					return GameState.DRAW;
				}
				else if(MyStatus.LatestRound.ScoreResult.IsVillageDestroyed){
					return GameState.LOST;
				}
				else if(OpponentStatus.LatestRound.ScoreResult.IsVillageDestroyed){
					return GameState.WON;
				}
				
			}
			else if(MyStatus.LatestRound.IsComplete){
				return GameState.OPPONENT_NEEDS_TO_PLAY;
			}
			else if(!MyStatus.LatestRound.IsConfigured){
				return GameState.PLAYER_NEEDS_TO_CONFIGURE;
			}
			else if(OpponentStatus.LatestRound.IsConfigured && !MyStatus.LatestRound.IsComplete){
				return GameState.READY_TO_PLAY;
			}
			else if(!OpponentStatus.LatestRound.IsConfigured){
				return GameState.OPPONENT_NEEDS_TO_CONFIGURE;
			}
			
			return GameState.CONTINUE;
			
		}
		
		
		
		public override string ToString ()
		{
			return string.Format ("[GameData: PlayerStatus={0}, CreatedAt={1}, UpdatedAt={2}]", PlayerStatus[0].ToString(), CreatedAt, UpdatedAt);
		}
		
		
	}
}

