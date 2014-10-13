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
				if(AllShots>0){
					return (float)Math.Round((double)HitCount / AllShots * 100);
				}else{
					return 0.0f;
				}
			}
		}
		
		[IgnoreDataMember]
		public float VillageStatusPercent { 
			get {
				if (RemainingVillageLifepoints > 0){
					int startVillageLifePoints = ConfigReader.Instance.GetSettingAsInt ("game", "start-village-livepoints");
					float villageStatus = (float)RemainingVillageLifepoints / startVillageLifePoints * 100;				
					return (float)Math.Round(villageStatus);
				}else{
					return 0.0f;
				}
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
				//TODO: Die Nummer der Runde berücksichtigen, um mehr Gold in späteren Runden zu erhalten
				int gold = (int)(ShotEfficiencyPercent * 5.0f + RemainingVillageLifepoints + ExtraPoints);
				return gold;
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
			if(remainingVillageLifepoints < 0)
				RemainingVillageLifepoints = 0;
			else
				RemainingVillageLifepoints = remainingVillageLifepoints;
      		ExtraPoints = extraPoints;
		}

	}
}
