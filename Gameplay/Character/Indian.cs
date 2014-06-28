using UnityEngine;
using System.Collections;

namespace PYIV.Gameplay.Character {

    public class Indian : MonoBehaviour {

        public IndianData IndianData { get; private set; }

        public static Indian AddAsComponent(GameObject gameObjectFromPreafab, IndianData indianData)
        {
            Indian indian = gameObjectFromPreafab.AddComponent<Indian>();
            indian.IndianData = indianData;
            return indian;
        }

        void Update()
        {

        }
    }

}
