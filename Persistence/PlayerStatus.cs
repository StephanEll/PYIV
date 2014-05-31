using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using System.Runtime.Serialization;



namespace PYIV.Persistence
{
	[DataContract]
	public class PlayerStatus : ServerModel<PlayerStatus>
	{
		
		[DataMember]
		public IList<Round> Rounds { get; set; }
		
		[IgnoreDataMember] 
		public Round LatestRound {
			get{
				return Rounds[Rounds.Count-1];
			}
		}
		
		
		[DataMember]
		public Player Player {get; set;}
		
		public PlayerStatus(){
			Rounds = new List<Round>();
		}
		
		public PlayerStatus (Player player) : this()
		{
			
			this.Player = player;
			
			
		}
		
		public override void ParseOnCreate (PlayerStatus responseObject)
		{
			base.ParseOnCreate (responseObject);
			this.Rounds = responseObject.Rounds;
			this.Player.ParseOnCreate(responseObject.Player);
		}
		
		public void AddRound(Round round){
			Rounds.Add(round);
			
		}
		public override string ToString ()
		{
			return string.Format ("[PlayerStatus: Rounds={0}, Player={1}]", Rounds.Count, Player.ToString());
		}
		
	}
}

