using UnityEngine;
using System.Collections;

namespace PYIV.Demo
{

    public class AlignmentController : MonoBehaviour
    {

        public GameObject Tree;

        // Use this for initialization
        void Start()
        {
            Tree.transform.position = new Vector3(Camera.main.orthographicSize * -Camera.main.aspect, 0, 0);
        }

        // Update is called once per frame
        void Update()
        {

        }
    }
}
