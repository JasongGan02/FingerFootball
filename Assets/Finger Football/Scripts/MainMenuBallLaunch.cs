using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //This is used to launch the ball inside the main menu
    public class MainMenuBallLaunch : MonoBehaviour
    {
        private Rigidbody2D rb;

        void OnEnable()
        {
            LaunchTheBall();
        }

        private void LaunchTheBall()
        {
            rb = GetComponent<Rigidbody2D>();
            rb.velocity = Vector2.zero;
            transform.position = new Vector2(Random.Range(-2f, 2f), Random.Range(-4f, 4f));
            float forceX = Random.Range(1, 3) == 1 ? 500 : -500;
            float forceY = Random.Range(1, 3) == 1 ? 500 : -500;
            rb.AddForce(new Vector2(forceX, forceY));

        }

    }
}
