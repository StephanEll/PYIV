using System;
using UnityEngine;

namespace PYIV.Menu.MenuHelper
{
	public static class ButtonHelper
	{
		
		
		public static GameObject AddButtonToParent(GameObject parent, string title, Vector2 position){
			
			GameObject buttonPrefab = Resources.Load<GameObject>("Prefabs/UI/Button");
			
			GameObject nextRoundButton = NGUITools.AddChild(parent, buttonPrefab);
			UILabel nextRoundLabel = nextRoundButton.transform.Find("Label").gameObject.GetComponent<UILabel>();
			nextRoundLabel.text = title;
			
			nextRoundButton.transform.localPosition = new Vector3(position.x, position.y);
			
			return nextRoundButton;
		}
	}
}

