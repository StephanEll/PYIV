using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using PYIV.Gameplay;
using PYIV.Gameplay.Enemy;
using PYIV.Persistence;
using PYIV.Helper;
using PYIV.Menu;
using System.Runtime.Serialization;

namespace PYIV.Persistence{
	
	[DataContract]
	public class Round {
		
		
		private List<int> sentAttackerIds;
		
		[DataMember]
		public List<int> SentAttackerIds{
			
			get{
				return sentAttackerIds;
			}
			
			set{
				sentAttackerIds = value;
				SentAttackers = ConvertIdsToEnemyTypes(value);	
			}
			
		}
		
		[IgnoreDataMember]
		public List<EnemyType> SentAttackers {get; private set;}
		
		[DataMember]
		public int RemainingVillageLifepoints { get; set; }
		
		public Round(){
			
		}
		
		private List<EnemyType> ConvertIdsToEnemyTypes(List<int> enemyIds){
			
			return new List<EnemyType>();
		}
		
		public override string ToString ()
		{
			return string.Format ("[Round: SentAttackerIds={0}, RemainingVillageLifepoints={1}]", SentAttackerIds.Count, RemainingVillageLifepoints);
		}
		
		
	}
	
}
