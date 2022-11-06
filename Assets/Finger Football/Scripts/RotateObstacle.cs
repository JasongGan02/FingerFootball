using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //It will rotate the obstacle on which this script is attached to
    public class RotateObstacle : MonoBehaviour
    {

        public float speed = 50;

        void Update()
        {
            transform.Rotate(0, 0, speed * Time.deltaTime);
        }
    }
}
