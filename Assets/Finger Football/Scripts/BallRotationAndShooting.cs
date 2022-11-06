using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //Ball rotation and shooting logic
    public class BallRotationAndShooting : MonoBehaviour
    {

        [SerializeField]
        private GameObject arrow;
        [SerializeField]
        private Rigidbody2D rb;
        private bool invertControls;
        private float topMenu;

        void Start()
        {
            if (PlayerPrefs.GetInt("InvertControls") == 0)//If player has set invert controles in the settings menu
            {
                invertControls = false;
            }
            else
            {
                invertControls = true;
            }
            topMenu = Screen.height - Screen.height / 7;
        }

        void Update()
        {
            if (Input.mousePosition.y > topMenu) return;//Not to react if the player touches the upper side of the screen (where pause button is located)
            if (Input.GetMouseButton(0))
            {
                Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                float px = transform.position.x - mousePosition.x;
                float py = transform.position.y - mousePosition.y;
                if (invertControls)
                {
                    transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(py, px));
                }
                else
                {
                    transform.localRotation = Quaternion.Euler(0, 0, Mathf.Rad2Deg * Mathf.Atan2(-py, -px));
                }
            }

            if (Input.GetMouseButtonUp(0))
            {
                if (arrow == null) return;
                if (!arrow.activeSelf) Destroy(this.gameObject);
                rb.AddForce(transform.right * 1000);
                Destroy(arrow);
            }

            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider == null)
            {
                EnableArrowObject();
                return;
            }

            if (hit.collider.name == "Ball")
            {
                DisableArrowObject();
            }
            else
            {
                EnableArrowObject();
            }
        }

        private void EnableArrowObject()
        {
            if (arrow == null) return;
            if (!Input.GetMouseButton(0)) return;
            arrow.SetActive(true);
        }

        private void DisableArrowObject()
        {
            if (arrow == null) return;
            arrow.SetActive(false);
        }
    }
}
