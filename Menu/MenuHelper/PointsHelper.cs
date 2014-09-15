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

				// Bei großem Unterschied in größeren Schritten abziehen
				if((float.Parse(pointsLabel.text) - points) > 100) {

					float p = float.Parse(pointsLabel.text) - 5f;
					pointsLabel.text = ((int)p).ToString();

				} else {

					float p = float.Parse(pointsLabel.text) - 0.05f;
					pointsLabel.text = ((int)p).ToString();
				}
			} 

			if(float.Parse(pointsLabel.text) < points) {

				if((points - float.Parse(pointsLabel.text)) > 100) {

					float p = float.Parse(pointsLabel.text) + 5f;
					pointsLabel.text = ((int)p).ToString();

				} else {

					float p = float.Parse(pointsLabel.text) + 1f;
					pointsLabel.text = ((int)p).ToString();

				}
			} 
		}
	}
}
