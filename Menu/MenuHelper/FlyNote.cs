using UnityEngine;
using System.Collections;

namespace PYIV.Menu.MenuHelper
{
		public class FlyNote : MonoBehaviour
		{
				public bool wait = false;
				private int framesToWait = 20;
				private UILabel label;
				// Use this for initialization
				void Start ()
				{
					label = gameObject.GetComponent<UILabel> ();
				}
	
				// Update is called once per frame
				void Update ()
				{
					// ich weiß, das ist nicht schön....
					if(wait) {
						label.alpha = 0.0f;
						if(framesToWait > 0) {
							framesToWait -= 1;
						} else {
							wait = false;
							label.alpha = 1.0f;
						}
					} else {
						transform.Translate (Vector3.up * (Time.deltaTime * 5));
						if (label.alpha <= 0f) {
								NGUITools.Destroy (gameObject);
						} else {
								label.alpha -= 0.02f;
						}
					}
				}

		}
}
