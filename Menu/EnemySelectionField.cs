using System;
using UnityEngine;
using PYIV.Gameplay.Enemy;
using PYIV.Persistence;

namespace PYIV.Menu
{
	public class EnemySelectionField
	{
		private GameObject grid;
		
		public GameObject Button { get; private set; }
		private UILabel countLabel;
		private UILabel priceLabel;
		
		private UISprite lifeBar;
		private UISprite powerBar;
		private UISprite speedBar;
		
		private EnemyType enemyType;
		private AttackConfigurationModel attackConfigurationModel;
		
		public EnemySelectionField (GameObject parent, string enemyId, AttackConfigurationModel attackConfigurationModel)
		{
			grid = parent;
			this.attackConfigurationModel = attackConfigurationModel;
			this.attackConfigurationModel.OnChange += OnRoundChanges;
			
			InitViewComponents(enemyId);
			SetValues();
			UIEventListener.Get(Button).onClick += OnClick;
		}

		
		
		private void InitViewComponents(string enemyId){
			enemyType = EnemyTypeCollection.Instance.GetSubCollection(new string[]{enemyId})[0];
			
			Button = grid.transform.Find(enemyId).gameObject;
			countLabel = Button.transform.Find("count").gameObject.GetComponent<UILabel>();
			
			lifeBar = Button.transform.Find("enemyInfo/bars/lifeBar").gameObject.GetComponent<UISprite>();
			powerBar = Button.transform.Find("enemyInfo/bars/powerBar").gameObject.GetComponent<UISprite>();
			speedBar = Button.transform.Find("enemyInfo/bars/speedBar").gameObject.GetComponent<UISprite>();
			
			priceLabel = Button.transform.Find("enemyInfo/price/price").gameObject.GetComponent<UILabel>();
		}
		
		private void SetValues(){

			countLabel.text = attackConfigurationModel.CountEnemyByType(enemyType).ToString();
			priceLabel.text = enemyType.Price.ToString();
			lifeBar.fillAmount = enemyType.EnemyData.LivePoints / (float)EnemyDataCollection.Instance.MaxLifePoints();
			powerBar.fillAmount = enemyType.EnemyData.AttackPower / (float)EnemyDataCollection.Instance.MaxAttackPower();
			speedBar.fillAmount = enemyType.EnemyData.MoveSpeed / EnemyDataCollection.Instance.MaxMoveSpeed();
			
		}
		
		private void OnClick(GameObject button){
			attackConfigurationModel.BuyAttacker(enemyType);
		}
		
		private void OnRoundChanges ()
		{
			countLabel.text = attackConfigurationModel.CountEnemyByType(enemyType).ToString();
		}
		
		
		
	}
}

