using System;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;
using PYIV.Persistence;

namespace PYIV.Menu
{
	public class EnemySelectionModel
	{
		
		private Dictionary<EnemyType, int> buyedEnemys;
		
		public delegate void ChangeDelegate();
		
		public event ChangeDelegate OnChange;
		
		private PlayerStatus playerStatus;
		
		public int Gold { get; private set; }
		
		public EnemySelectionModel (PlayerStatus playerStatus)
		{
			buyedEnemys = new Dictionary<EnemyType, int>();
			InitDictFromList(playerStatus.LatestRound.SentAttackers);
			this.playerStatus = playerStatus;
			Gold = playerStatus.Gold;
		}
		
		public void BuyAttacker(EnemyType enemyType){
			AddTypeToDict(enemyType);
			Gold -= enemyType.Price;
			ExecuteChangeEvent();
		}
		
		public void ResetAttackers(){
			buyedEnemys.Clear();
			Gold = playerStatus.Gold;
			ExecuteChangeEvent();
		}
		
		public int CountEnemyByType(EnemyType type){
			try{
				return buyedEnemys[type];
			}
			catch(Exception e){
				return 0;
			}
		}
		
		public void ApplyToPlayerStatus(){
			playerStatus.Gold = Gold;
			
			List<EnemyType> attackerList = new List<EnemyType>();
			
			foreach(var pair in buyedEnemys){
				for(int i = 0; i < pair.Value; i++){
					attackerList.Add(pair.Key);
				}
			}
			
			playerStatus.LatestRound.SentAttackers = attackerList;
			
		}
		
		private void InitDictFromList(List<EnemyType> attackerList){
			
			foreach(EnemyType type in attackerList){
				AddTypeToDict(type);
			}
			
			
		}
		private void AddTypeToDict(EnemyType type){
			try{
				buyedEnemys[type] += 1;
			}
			catch(Exception e){
				buyedEnemys.Add(type, 1);
			}
		}
		
		private void ExecuteChangeEvent(){
			if(OnChange != null)
				OnChange();
		}
		
		
		
	}
}

