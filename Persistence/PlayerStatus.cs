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
		public string PlayerId {
			get {
				return player.Id;
			}
			
			set{
				Debug.Log(value + "has not been set");
			}
		}
		
		public PlayerStatus() : this(null){
			
		}
		
		public PlayerStatus (Player player)
		{
			resource = "playerStatus";
			this.player = player;
			
			Rounds = new List<Round>();
		}
		
		protected override void UpdateModel (PlayerStatus responseObject)
		{
			throw new NotImplementedException ();
		}
		
		public void AddRound(Round round){
			Rounds.Add(round);
			
		}
		
	}
}

