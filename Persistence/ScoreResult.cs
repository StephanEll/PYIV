using UnityEngine;
using System.Collections;
using PYIV.Helper;
using System;
using System.Runtime.Serialization;

namespace PYIV.Persistence
{
	[DataContract]
	[Serializable]
  	public class ScoreResult
	{

		[DataMember]
		public int HitCount { get; set; }

		[DataMember]
		public int KillCount { get; set; }

		[DataMember]
		public int MissedShotCount { get; set; }

		[DataMember]
		public int RemainingVillageLifepoints { get; set; }

	    [DataMember]
	    public int ExtraPoints { get; set; }
		
		[IgnoreDataMember]
		public bool IsVillageDestroyed{
			get { return RemainingVillageLifepoints <= 0; }
		}
		
		[IgnoreDataMember]
		public int AllShots { 
			get {
				return HitCount + MissedShotCount;
			}
		}
		
		[IgnoreDataMember]
		public float ShotEfficiencyPercent { 
			get {
				return (float)Math.Round((double)HitCount / AllShots * 100);
			}
		}
		
		[IgnoreDataMember]
		public float VillageStatusPercent { 
			get {
				int startVillageLifePoints = ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints");
				float villageStatus = (float)RemainingVillageLifepoints / startVillageLifePoints * 100;				
				return (float)Math.Round(villageStatus);
			}
		}

		[IgnoreDataMember]
		public int VillageDamage {
			get {
				return ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints") - RemainingVillageLifepoints;
			}
		}

		[IgnoreDataMember]
		public int Gold { 
			get {
				return (int)(((HitCount * 100.0f) / (AllShots + (VillageDamage / 20.0f))) * 10.0f) - VillageDamage;
			}
		}

		public ScoreResult ()
		{
		}

		public ScoreResult (int hitCount, int missedShotCount, int killCount, int remainingVillageLifepoints, int extraPoints)
		{
			HitCount = hitCount;
			KillCount = killCount;
			MissedShotCount = missedShotCount;
			RemainingVillageLifepoints = remainingVillageLifepoints;
      		ExtraPoints = extraPoints;
		}

	}
}
