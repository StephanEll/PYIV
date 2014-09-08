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
		private GameObject button_inactive;
		private UILabel countLabel;
		private UILabel priceLabel;
		
		private UISprite lifeBar;
		private UISprite powerBar;
		private UISprite speedBar;
		
		private EnemyType enemyType;
		private AttackConfigurationModel attackConfigurationModel;
		
		private bool isActive = true;
		public bool IsActive { 
			get 
			{
				return isActive;
			}
			
			set 
			{
				if(isActive != value){
					isActive = value;
					ChangeButtonState();
					
				}
			}
		}
		
		public EnemySelectionField (GameObject parent, string enemyId, AttackConfigurationModel attackConfigurationModel)
		{
			grid = parent;
			this.attackConfigurationModel = attackConfigurationModel;
			this.attackConfigurationModel.OnChange += OnRoundChanges;
			
			InitViewComponents(enemyId);
			SetValues();
			UIEventListener.Get(Button).onClick += OnClick;
			//Set initial state
			OnRoundChanges();
		}

		
		
		private void InitViewComponents(string enemyId){
			enemyType = EnemyTypeCollection.Instance.GetById(enemyId);
			
			Button = grid.transform.Find(enemyId).gameObject;
			countLabel = Button.transform.Find("count").gameObject.GetComponent<UILabel>();

			button_inactive = Button.transform.Find("Enemy_inactive").gameObject;
			
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
		
		private void ChangeButtonState(){
			if(IsActive){
				UIEventListener.Get(Button).onClick += OnClick;
				//Button.GetComponent<UIWidget>().alpha = 1.0f;
				button_inactive.SetActive(false);
			}
			else{
				UIEventListener.Get(Button).onClick -= OnClick;
				//Button.GetComponent<UIWidget>().alpha = 0.25f;
				button_inactive.SetActive(true);
			}
			
			
		}
		
		private bool ShouldBeActive(){
			return enemyType.Price <= attackConfigurationModel.Gold;
		}
		
		private void OnClick(GameObject button){
			attackConfigurationModel.BuyAttacker(enemyType);
		}
		
		private void OnRoundChanges ()
		{
			countLabel.text = attackConfigurationModel.CountEnemyByType(enemyType).ToString();			
			IsActive = ShouldBeActive();
		}
		
		
		
	}
}

