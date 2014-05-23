using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace PYIV.Persistence
{
	[DataContract]
	public class GameData : ServerModel<GameData>
	{		
		[DataMember]
		public PlayerStatus[] PlayerStatus { get; set; }
		
		[IgnoreDataMember]
		public DateTime CreatedAt { get; set; }
		
		[IgnoreDataMember]
		public DateTime UpdatedAt { get; set; }
		
		
		public GameData(){
			resource = "gameData";

		}
		
		public GameData(Player challenger, Player defender) : this(){
			
			PlayerStatus = new PlayerStatus[2];
			PlayerStatus[0] = new PlayerStatus(challenger);
			PlayerStatus[1] = new PlayerStatus(defender);
			
		}
		
		protected override void PopulateModel (GameData responseObject)
		{
			
		}
		
		
		
	}
}

