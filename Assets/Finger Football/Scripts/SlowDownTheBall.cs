using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //This is used to slow down the ball when it enters into the goal
    public class SlowDownTheBall : MonoBehaviour
    {
        private Rigidbody2D rb;
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
        }
        void FixedUpdate()
        {
            rb.velocity = rb.velocity * Time.fixedDeltaTime * 90;
            
        }
    }
}
