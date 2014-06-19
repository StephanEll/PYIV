using UnityEngine;
using System.Collections;
namespace PYIV.Helper
{
    public static class PlayingFieldBoundarys
    {
        private static float left = 0;
        public static float Left {
            get
            {
                if (left == 0)
                {
                    left = Camera.main.ScreenToWorldPoint(Vector3.zero).x;
                }
                return left;
            }
        }

        private static float right = 0;
        public static float Right
        {
            get
            {
                if (right == 0)
                {
                    right = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width, 0.0f)).x;
                }
                return right;
            }
        }

        private static float top = 0;
        public static float Top
        {
            get
            {
                if (top == 0)
                {
                    top = Camera.main.ScreenToWorldPoint(new Vector3(0.0f, Screen.height)).y;
                }
                return top;
            }
        }

        private static float bottom = 0;
        public static float Bottom
        {
            get
            {
                if (bottom == 0)
                {
                    bottom = Camera.main.ScreenToWorldPoint(Vector3.zero).y;
                }
                return bottom;
            }
        }

    }
}
