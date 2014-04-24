using UnityEngine;
using System.Collections;

namespace PYIV.Demo
{

    public class ShotController : MonoBehaviour
    {

        public ShootableObject shootableObject;

        private Vector2 startPosition = new Vector2(0, 0);
        private float startTime;


        void Update()
        {
            HandleSwipeGesture();
        }

        private void HandleSwipeGesture()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 endPosition = Input.GetTouch(0).position;
                Vector2 delta = endPosition - startPosition;


                //float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));
                
                float duration = Time.time - startTime;
                //float speed = dist / duration;

                Vector3 posPlayer = GameObject.Find("Player").transform.position;
                if (delta.x > 2)
                {
                    GameObject shootableObjectClone = (GameObject)Instantiate(shootableObject.gameObject, new Vector3(posPlayer.x, posPlayer.y, 0), Quaternion.EulerAngles(0, 0, 0));
                    shootableObjectClone.GetComponent<ShootableObject>().SwipeHandler(delta, duration);
                }
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPosition = Input.GetTouch(0).position;
                startTime = Time.time;
            }
        }
    }

}
