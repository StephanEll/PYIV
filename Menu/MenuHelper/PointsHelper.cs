using UnityEngine;
using System.Collections;

namespace PYIV.Menu.MenuHelper
{
public class PointsHelper : MonoBehaviour {

		public float points;
		private UILabel pointsLabel;
		// Use this for initialization
		void Start () {
			pointsLabel = gameObject.GetComponent<UILabel>();
			pointsLabel.text = points.ToString();
		}

		// Update is called once per frame
		void Update () {
			if(float.Parse(pointsLabel.text) > points) {
				float p = float.Parse(pointsLabel.text) - 0.05f;
				pointsLabel.text = ((int)p).ToString();
			} 

			if(float.Parse(pointsLabel.text) < points) {
				float p = float.Parse(pointsLabel.text) + 1f;
				pointsLabel.text = ((int)p).ToString();
			} 
		}
	}
}
