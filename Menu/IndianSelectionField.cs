using UnityEngine;
using System.Collections;
using PYIV.Gameplay.Character;


namespace PYIV.Menu
{

	public class IndianSelectionField
	{
	
		private GameObject grid;
		private AttackConfigurationModel attackConfigurationModel;
		private IndianData indianData;

		
		public GameObject Button;
		
		public IndianSelectionField(GameObject parent, string indianId, AttackConfigurationModel attackConfigurationModel)
		{
			this.grid = parent;
			this.Button = this.grid.transform.Find(indianId).gameObject;
			Debug.Log(grid);
			this.attackConfigurationModel = attackConfigurationModel;
			this.attackConfigurationModel.OnChange += OnChange;
			
			this.indianData = IndianDataCollection.Instance.GetById(indianId);
			Debug.Log(grid);
			
			Debug.Log(grid);
			UIEventListener.Get(Button).onClick += OnClick;
			OnChange();
			
		}
		
		private void SelectIndian(bool selectIndian){
			Button.GetComponent<UIWidget>().alpha = selectIndian ? 1.0f : 0.5f;
		}
		
		private void OnClick(GameObject go){
			attackConfigurationModel.SelectedIndian = indianData;
		}
		
		private void OnChange(){
			if(attackConfigurationModel.SelectedIndian == indianData){
				SelectIndian(true);
			}
			else{
				SelectIndian(false);
			}
		}
	
	}

}