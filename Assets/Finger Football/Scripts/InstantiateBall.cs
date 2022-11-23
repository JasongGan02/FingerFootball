using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //Used to instantiate new ball when player touches the screen, or ball goes off the screen
    public class InstantiateBall : MonoBehaviour
    {

        public Vector2 ballPosition;
        private float topMenu;
        public Vector3 hitPos;
        void Start()
        {
            topMenu = Screen.height - Screen.height / 7;
        }
        void Update()
        {
            if (Input.mousePosition.y > topMenu) return;//Not to react if the player touches the upper side of the screen (where pause button is located)
            if (Input.GetMouseButtonDown(0) && !GameObject.Find("GameManager").GetComponent<Menus>().aim)
            {
                InstantiateNewBall();
            }
            if (GameObject.Find("Ball") == null && GameObject.Find("GameManager").GetComponent<Menus>().aim)
            {
                InstantiateNewBall();
            }
        }

        public void InstantiateNewBall()
        {
            if (GameObject.Find("Ball") != null) Destroy(GameObject.Find("Ball"));
            GameObject ball = Instantiate(Resources.Load("Ball"), ballPosition, Quaternion.identity) as GameObject;
            ball.transform.rotation = Quaternion.Euler(0, 0, 0);
            ball.name = "Ball";
        }
    }
}
