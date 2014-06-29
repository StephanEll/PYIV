using UnityEngine;
using System;
using System.Linq;
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
		
		
		private List<string> sentAttackerIds;
		
		[DataMember]
		public List<string> SentAttackerIds{
			
			get{
				return sentAttackerIds;
			}
			
			set{
				sentAttackerIds = value;
				SentAttackers = ConvertIdsToEnemyTypes(value);	
			}
			
		}
		
		private List<EnemyType> sentAttackers;
		
		[IgnoreDataMember]
		public List<EnemyType> SentAttackers {
			get{
				return sentAttackers;
			}
			set{
				sentAttackers = value;
				var ids = from type in value select type.Id;
				sentAttackerIds = ids.ToList();
			}
		}

    [DataMember]
    public ScoreResult ScoreResult { get; set; }
		
		public Round(){
			
		}
		
		private List<EnemyType> ConvertIdsToEnemyTypes(List<string> enemyIds){
			
			return EnemyTypeCollection.Instance.GetSubCollection(enemyIds.ToArray()).ToList<EnemyType>();
		}
		
		public override string ToString ()
		{
      return string.Format ("[Round: SentAttackerIds={0}, RemainingVillageLifepoints={1}]", SentAttackerIds.Count, ScoreResult.RemainingVillageLifepoints);
		}
		
		
	}
	
}
