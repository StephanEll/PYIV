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
			
			Debug.Log(PlayerStatus.Count + ".::."+responseObject.PlayerStatus.Count);
			
			for(int i = 0; i < PlayerStatus.Count; i++){
				PlayerStatus[i].ParseOnCreate(responseObject.PlayerStatus[i]);
			}
			
		}
		
		
		public override string ToString ()
		{
			return string.Format ("[GameData: PlayerStatus={0}, CreatedAt={1}, UpdatedAt={2}]", PlayerStatus.Count, CreatedAt, UpdatedAt);
		}
		
		
	}
}

