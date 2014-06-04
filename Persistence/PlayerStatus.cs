using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RestSharp;
using System.Runtime.Serialization;
using PYIV.Gameplay.Character;



namespace PYIV.Persistence
{
	[DataContract]
	public class PlayerStatus : ServerModel<PlayerStatus>
	{
		
		[DataMember]
		public List<Round> Rounds { get; set; }
		
		[IgnoreDataMember] 
		public Round LatestRound {
			get{
				return Rounds[Rounds.Count-1];
			}
		}
		
		[DataMember]
		public Player Player {get; set;}
		
		[IgnoreDataMember]
        public IndianData IndianData { get; set; }
		
		[DataMember]
		public string IndianId { 
			get {
				return IndianData.Id;
			}
			
			set{
				var indians = IndianDataCollection.Instance.GetSubCollection(new [] { value });
				IndianData = indians[0];
			}
		}
		
		public PlayerStatus(){
			Rounds = new List<Round>();
		}
		
		public PlayerStatus (Player player) : this()
		{
			
			this.Player = player;
			
			
		}
		
		public override void ParseOnCreate (PlayerStatus responseObject)
		{
            Debug.Log("PARSE: "+responseObject);
			base.ParseOnCreate (responseObject);
			this.Rounds = responseObject.Rounds;
			this.IndianId = responseObject.IndianId;
			this.Player.ParseOnCreate(responseObject.Player);
			
		}
		
		public void AddRound(Round round){
			Rounds.Add(round);
			
		}
		public override string ToString ()
		{
			return string.Format ("[PlayerStatus: Rounds={0}, Player={1}, IndianId={2}]", Rounds.Count, Player.ToString(), IndianId);
		}
		
	}
}

