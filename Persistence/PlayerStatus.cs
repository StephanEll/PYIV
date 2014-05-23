using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using System.Runtime.Serialization;



namespace PYIV.Persistence
{
	public class PlayerStatus : ServerModel<PlayerStatus>
	{
		
		[DataMember]
		public List<Round> Rounds { get; set; }
		
		private Player player;
		
		[DataMember]
		public readonly string playerId;
		
		public PlayerStatus(){
			resource = "playerStatus";
		}
		
		public PlayerStatus (Player player) : this()
		{
			
			this.player = player;
			playerId = player.Id;
			
			Rounds = new List<Round>();
		}
		
		protected override void PopulateModel (PlayerStatus responseObject)
		{
			throw new NotImplementedException ();
		}
		
		public void AddRound(Round round){
			Rounds.Add(round);
			
		}
		
	}
}

