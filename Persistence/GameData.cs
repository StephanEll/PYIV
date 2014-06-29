using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;
using UnityEngine;

namespace PYIV.Persistence
{
	[DataContract]
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

		}
		
		public GameData(Player challenger, Player defender) : this(){
			
			
			PlayerStatus.Add(new PlayerStatus(challenger));
			PlayerStatus.Add(new PlayerStatus(defender));
			
		}
		
		public override void ParseOnCreate (GameData responseObject)
		{
			base.ParseOnCreate (responseObject);
			this.CreatedAt = responseObject.CreatedAt;
			this.UpdatedAt = responseObject.UpdatedAt;
			
			//Parse player status
			GetPlayerOrOpponentStatus(true).ParseOnCreate(responseObject.GetPlayerOrOpponentStatus(true));
			//Parse opponent status
			GetPlayerOrOpponentStatus(false).ParseOnCreate(responseObject.GetPlayerOrOpponentStatus(false));	

			
		}
		
		
		
		public PlayerStatus GetPlayerOrOpponentStatus(bool isPlayerOfDevice){
			
			PlayerStatus player = PlayerStatus[0].Player.Equals(LoggedInPlayer.Instance.Player) ? PlayerStatus[0] : PlayerStatus[1];
			PlayerStatus opponent = !PlayerStatus[0].Player.Equals(LoggedInPlayer.Instance.Player) ? PlayerStatus[0] : PlayerStatus[1];		
			
			return isPlayerOfDevice ? player : opponent ;
		}
		
		
		
		public override string ToString ()
		{
			return string.Format ("[GameData: PlayerStatus={0}, CreatedAt={1}, UpdatedAt={2}]", PlayerStatus[0].ToString(), CreatedAt, UpdatedAt);
		}
		
		
	}
}

