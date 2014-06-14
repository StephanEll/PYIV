﻿using UnityEngine;
using System.Collections;
using System;

namespace PYIV.Gameplay.Character.Weapon
{

    public abstract class ShotBehaviour : MonoBehaviour
    {

        private Vector2 startPosition = new Vector2(0, 0);
        private float startTime;

        private string bulletPreafabPath;
        private int bulletStrength;

        protected Score score;
        private GameObject game;


        void Start()
        {
            game = GameObject.Find("Game");
            score = game.GetComponent<Score>();
        }

        // Update is called once per frame
        void Update()
        {
            HandleSwipeGesture();
        }

        /* 
         * change this method if you add a new shot behaviour
        */
        public static void AddAsComponentFactory(GameObject go, string bulletPreafabPath, string ShotBehaviourClassName, int bulletStrenght){
            switch(ShotBehaviourClassName){
                case "WeaponBow":
                    go.AddComponent<WeaponBow>().bulletPreafabPath = bulletPreafabPath;
                    go.GetComponent<WeaponBow>().bulletStrength = bulletStrenght;
                    break;
                case "WeaponSpear":
                    go.AddComponent<WeaponSpear>().bulletPreafabPath = bulletPreafabPath;
                    go.GetComponent<WeaponSpear>().bulletStrength = bulletStrenght;
                    break;
                case "WeaponBlowTube":
                    go.AddComponent<WeaponBlowTube>().bulletPreafabPath = bulletPreafabPath;
                    go.GetComponent<WeaponBlowTube>().bulletStrength = bulletStrenght;
                    break;
            }
        }

        public abstract void EndSwipeHandler(Bullet bulletPrefab, Vector2 startPos, Vector2 endPos, float duration);

        public abstract void StartSwipeHandler(Vector2 startPos);

        public abstract void UpdateSwipeHandler(Vector2 startPos, Vector2 endPos, float duration);

        private void HandleSwipeGesture()
        {
            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Ended)
            {
                Vector2 endPosition = Input.GetTouch(0).position;
                Vector2 delta = endPosition - startPosition;

                //float dist = Mathf.Sqrt(Mathf.Pow(delta.x, 2) + Mathf.Pow(delta.y, 2));

                float duration = Time.time - startTime;
                //float speed = dist / duration;

                GameObject bullet = Instantiate(Resources.Load<GameObject>(bulletPreafabPath)) as GameObject;
                bullet.transform.parent = game.transform;
                Bullet.AddAsComponentTo(bullet, bulletStrength, score);

                EndSwipeHandler(
                    bullet.GetComponent<Bullet>(),
                    startPosition,
                    endPosition,
                    duration);

            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Moved)
            {
                UpdateSwipeHandler(startPosition, Input.GetTouch(0).position, Time.time - startTime);
            }

            if (Input.touchCount > 0 && Input.GetTouch(0).phase == TouchPhase.Began)
            {
                startPosition = Input.GetTouch(0).position;
                startTime = Time.time;
                StartSwipeHandler(startPosition);
            }
        }
    }
}
