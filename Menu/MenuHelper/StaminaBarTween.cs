using UnityEngine;
using System.Collections;

namespace PYIV.Menu.MenuHelper
{
	public class StaminaBarTween : MonoBehaviour {
		UIAnchor staminaBarAnchor;
		// Use this for initialization
		void Start () {
			staminaBarAnchor = gameObject.GetComponent<UIAnchor>();
		}
		
		// Update is called once per frame
		void Update () {
			if(staminaBarAnchor.relativeOffset.y > 0.04f) {
				staminaBarAnchor.relativeOffset.y -= 0.002f;
			}
		}
	}
}
