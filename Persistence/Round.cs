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

namespace PYIV.Persistence
{
	[DataContract]
	[Serializable]
	public class Round
	{
		
		
		[DataMember]
		public List<string> SentAttackerIds {
			
			get {
				var ids = from type in SentAttackers select type.Id;
				return ids.ToList ();
			}
			
			set {
				SentAttackers = ConvertIdsToEnemyTypes (value);	
			}
			
		}
	
		
		private List<EnemyType> sentAttackers;
		
		[IgnoreDataMember]
		public List<EnemyType> SentAttackers {
			get {
				return sentAttackers;
			}
			set {
				sentAttackers = value;
			}
		}
		
		

		[DataMember]
		public ScoreResult ScoreResult { get; set; }
		
		public Round ()
		{
			SentAttackers = new List<EnemyType>();
		}
		
		private List<EnemyType> ConvertIdsToEnemyTypes (List<string> enemyIds)
		{
			
			return EnemyTypeCollection.Instance.GetSubCollection (enemyIds.ToArray ()).ToList<EnemyType> ();
		}
		
		public override string ToString ()
		{
			return string.Format ("[Round: SentAttackerIds={0}, RemainingVillageLifepoints={1}]", SentAttackerIds.Count, ScoreResult.RemainingVillageLifepoints);
		}
		
		
		
	}
	
}
