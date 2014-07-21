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
		public PlayerStatus MyStatus {
			get{
				Debug.Log("created at: " + CreatedAt.ToString());
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
			GetPlayerOrOpponentStatus(true).ParseOnCreate(responseObject.GetPlayerOrOpponentStatus(true));
			//Parse opponent status
			GetPlayerOrOpponentStatus(false).ParseOnCreate(responseObject.GetPlayerOrOpponentStatus(false));	

			
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
		
		
		
		public override string ToString ()
		{
			return string.Format ("[GameData: PlayerStatus={0}, CreatedAt={1}, UpdatedAt={2}]", PlayerStatus[0].ToString(), CreatedAt, UpdatedAt);
		}
		
		
	}
}

