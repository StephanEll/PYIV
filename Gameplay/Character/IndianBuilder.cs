using UnityEngine;
using System.Collections;
using PYIV.Persistence;
using PYIV.Helper;
using PYIV.Gameplay.Character;
using PYIV.Gameplay.Character.Weapon;
using System;

namespace PYIV.Gameplay.Character
{

    public class IndianBuilder
    {
        /*
        * Hier soll ein Gameobject mit der Komponente Indian erzeugt werden.
        * 
        * (noch nicht implementiert
        * außerdem sollen die Federn im PlayerStatus Objekt benutzt werden um die
        * Fähigkeiten des Indianers (indianData) anzupassen. 
        */

        public static Indian CreateIndian(PlayerStatus playerStatus)
        {

            playerStatus.IndianData = IndianDataCollection.Instance.IndianData[0];

            GameObject indianGO = GameObject.Instantiate(Resources.Load<GameObject>(playerStatus.IndianData.PreafabPath)) as GameObject;

            ShotBehaviour.AddAsComponentFactory(indianGO, playerStatus.IndianData.BulletPreafabPath, playerStatus.IndianData.ShotBehaviourClassName);

            Indian indian = Indian.AddAsComponent(indianGO, playerStatus.IndianData);

            return indian;
        }
    }
}
