using System;
using System.Collections.Generic;
using PYIV.Gameplay.Enemy;
using PYIV.Persistence;

namespace PYIV.Menu
{
	public class AttackConfigurationModel
	{
		
		private Dictionary<EnemyType, int> buyedEnemys;
		
		public delegate void ChangeDelegate();
		
		public event ChangeDelegate OnChange;
		
		public GameData GameData { get; set; }
		
		public int Gold { get; private set; }
		
		private int goldSpentForEnemies = 0;
		
		public AttackConfigurationModel (GameData gameData)
		{
			buyedEnemys = new Dictionary<EnemyType, int>();
			this.GameData = gameData;
			AddFreeRats();
			InitDictFromList(gameData.MyStatus.LatestRound.SentAttackers);
			
			Gold = GameData.MyStatus.Gold;
		}
		
		public void BuyAttacker(EnemyType enemyType){
			AddTypeToDict(enemyType);
			Gold -= enemyType.Price;
			goldSpentForEnemies += enemyType.Price;
			ExecuteChangeEvent();
		}
		
		private void AddFreeRats(){
			AddTypeToDict( EnemyTypeCollection.Instance.GetById("Rat1") );
			
		}
		
		public void ResetAttackers(){
			buyedEnemys.Clear();
			AddFreeRats();
			Gold += goldSpentForEnemies;
			goldSpentForEnemies = 0;
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
			GameData.MyStatus.Gold = Gold;
			
			List<EnemyType> attackerList = new List<EnemyType>();
			
			foreach(var pair in buyedEnemys){
				for(int i = 0; i < pair.Value; i++){
					attackerList.Add(pair.Key);
				}
			}
			
			GameData.MyStatus.LatestRound.SentAttackers = attackerList;
			
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

