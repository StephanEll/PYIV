using System;
using UnityEngine;
using PYIV.Persistence;
using PYIV.Persistence.Errors;
using PYIV.Menu.Popup;
using PYIV.Menu.Commands;
using PYIV.Helper;

namespace PYIV.Menu
{
	public class StatisticView : GuiView
	{
		private GameObject sprite;
		private UILabel you_goldComplete_label;
		private UILabel you_kills_label;
		private UILabel you_damage_label;
		private UILabel you_gold_label;

		private UILabel enemy_kills_label;
		private UILabel enemy_damage_label;
		private UILabel enemy_gold_label;
		private UILabel enemy_name_label;
		
		
		public StatisticView () : base("StatisticPrefab")
		{
			TouchScreenKeyboard.hideInput = true;
		}
		
		
		protected override void OnPanelCreated ()
		{
			base.OnPanelCreated ();
			InitViewComponents();	
		}
		
		private void InitViewComponents() {		
			sprite = panel.transform.FindChild("Sprite").gameObject;
			GameObject topAnchorInteraction = sprite.transform.FindChild("TopAnchorInteraction").gameObject;

			GameObject youGroup = topAnchorInteraction.transform.FindChild("YOU").gameObject;
			GameObject enemyGroup = topAnchorInteraction.transform.FindChild("ENEMY").gameObject;
			GameObject goldGroup = youGroup.transform.FindChild("gold_group").gameObject;
			you_goldComplete_label = goldGroup.transform.FindChild("gold_complete_nr_label").gameObject.GetComponent<UILabel>();
			
			GameObject you_infoBoard = youGroup.transform.FindChild("info_board").gameObject;
			you_kills_label = you_infoBoard.transform.FindChild("kills_nr_label").gameObject.GetComponent<UILabel>();
			you_gold_label = you_infoBoard.transform.FindChild("gold_nr_label").gameObject.GetComponent<UILabel>();
			you_damage_label = you_infoBoard.transform.FindChild("damage_nr_label").gameObject.GetComponent<UILabel>();

			GameObject enemy_infoBoard = enemyGroup.transform.FindChild("info_board").gameObject;
			enemy_name_label = enemyGroup.transform.FindChild("enemy_label").gameObject.GetComponent<UILabel>();;
			enemy_kills_label = enemy_infoBoard.transform.FindChild("kills_nr_label").gameObject.GetComponent<UILabel>();
			enemy_gold_label = enemy_infoBoard.transform.FindChild("gold_nr_label").gameObject.GetComponent<UILabel>();
			enemy_damage_label = enemy_infoBoard.transform.FindChild("damage_nr_label").gameObject.GetComponent<UILabel>();


			GameObject bottomAnchorButton = sprite.transform.FindChild("BottomAnchorButton").gameObject;
			GameObject nextRoundButton = bottomAnchorButton.transform.FindChild("nextRound_Button").gameObject;
			
			UIEventListener.Get(nextRoundButton).onClick += OnNextRoundButtonClicked;
			
		}


    public override void UnpackParameter (object initParameter)
    {
      base.UnpackParameter (initParameter);
      Score score = initParameter as Score;

      int allShots = (score.HitCount + score.MissedShotCount);
      int Livepoints = ConfigReader.Instance.GetSettingAsInt("game", "start-village-livepoints") - score.Livepoints;
      float gold = (((score.HitCount * 100.0f) / (allShots + (Livepoints / 20.0f))) * 10.0f) - Livepoints;

      SetYouGold(gold.ToString());
      SetYouDamage(Livepoints.ToString());
      SetYouKills(score.KillCount.ToString());
    }
		
		private void OnNextRoundButtonClicked(GameObject button) {

		}
		
		
		
		public void SetYouKills(String killsStr) {
			you_kills_label.text = killsStr;
		}
		public void SetYouGold(String goldStr) {
			you_gold_label.text = goldStr;
		}
		public void SetYouDamage(String damageStr) {
			you_damage_label.text = damageStr;
		}
		public void SetYouGoldComplete(String goldCompleteStr) {
			you_goldComplete_label.text = goldCompleteStr;
		}

		public void SetEnemyName(String enemyNameStr) {
			enemy_name_label.text = enemyNameStr;
		}

		public void SetEnemyKills(String killsStr) {
			enemy_kills_label.text = killsStr;
		}
		public void SetEnemyGold(String goldStr) {
			enemy_gold_label.text = goldStr;
		}
		public void SetEnemyDamage(String damageStr) {
			enemy_damage_label.text = damageStr;
		}
		
		
		
		public override bool ShouldBeCached ()
		{
			return false;
		}
	}
}

