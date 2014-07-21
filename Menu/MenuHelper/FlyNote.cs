﻿using UnityEngine;
using System.Collections;

namespace PYIV.Menu.MenuHelper
{
		public class FlyNote : MonoBehaviour
		{
	
				// Use this for initialization
				void Start ()
				{

				}
	
				// Update is called once per frame
				void Update ()
				{
						transform.Translate (Vector3.up * (Time.deltaTime * 5));
						UILabel label = gameObject.GetComponent<UILabel> ();

						if (label.alpha <= 0f) {
								NGUITools.Destroy (gameObject);
						} else {
								label.alpha -= 0.02f;
						}
				}
		}
}
