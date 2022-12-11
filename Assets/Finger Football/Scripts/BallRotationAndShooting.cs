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
        private GameObject AngleArrow;
        [SerializeField]
        private Rigidbody2D rb;
        private bool invertControls;
        private float topMenu;
        private float pressedTime;
        private bool isKeyActive = false;
        private readonly Vector2 STOPPING_VELOCITY = new Vector2(0.01f,0.01f);
        public float maxKickForce = 2000;
        public float timeKickRatio = 1300;
        private bool aim;
        private bool shot;
        Vector3 kickPos;
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
            aim = GameObject.Find("GameManager").GetComponent<Menus>().aim;
            if(aim)
            {
                if(transform.position.x != GameObject.Find("GameManager").GetComponent<InstantiateBall>().ballPosition.x &&transform.position.y != GameObject.Find("GameManager").GetComponent<InstantiateBall>().ballPosition.y) 
                {
                    Destroy(this.gameObject);
                }
                EnableAngleArrowObject();
                if(Input.GetMouseButton(0))
                {
                    ChangeShootAngle();  
                }
                if(Input.GetMouseButtonUp(0))
                {
                    GameObject.Find("GameManager").GetComponent<InstantiateBall>().hitPos = AngleArrow.transform.position;
                    AngleArrow.transform.localPosition = new Vector3(-0.21f,0,0);
                    AngleArrow.transform.localRotation = Quaternion.Euler(0, 0,90);
                    DisableAngleArrowObject();
                }
                
                
            }
            else
            {
                float rad = 0;
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
                    rad = Mathf.Atan2(py, px);
                    
                }

                if (Input.GetMouseButtonUp(0))
                {
                    if (AngleArrow == null) return;
                    //if (!AngleArrow.activeSelf) Destroy(this.gameObject);
                    Destroy(AngleArrow);
                }
                
                if (Input.GetMouseButton(0) && !isKeyActive)
                {
                    pressedTime = Time.time;
                    isKeyActive = true;
                }
                if (Input.GetMouseButtonUp(0)&& isKeyActive)
                {
                    if (arrow == null) return;
                    if (!arrow.activeSelf) Destroy(this.gameObject);
                    
                    float kickForce =  Mathf.Min((Time.time - pressedTime)*timeKickRatio+500, maxKickForce);
                    pressedTime = Time.time;
                    isKeyActive = false;
                    Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    float px = transform.position.x - mousePosition.x;
                    float py = transform.position.y - mousePosition.y;
                    rad = 2*Mathf.PI-(Mathf.Atan2(py, px)+Mathf.PI/2);
                    //Debug.Log("HitPos "+ GameObject.Find("GameManager").GetComponent<InstantiateBall>().hitPos);
                    Vector3 hitPos = GameObject.Find("GameManager").GetComponent<InstantiateBall>().hitPos - transform.position;
                    //Debug.Log("Rad: "+ rad);
                    
                    kickPos = new Vector3(hitPos.x*Mathf.Cos(rad)+hitPos.y*Mathf.Sin(rad), hitPos.y*Mathf.Cos(rad)-hitPos.x*Mathf.Sin(rad), 0);
                    kickPos += transform.position;
                    //Debug.Log(transform.position +" kickPos: " + kickPos);
                    //GameObject.Find("GameManager").GetComponent<InstantiateBall>().hitPos = transform.position;
                    rb.AddForceAtPosition(transform.right * kickForce/100/2.6f, kickPos, ForceMode2D.Impulse);
                    
                    //rb.AddForce();
                    
                    
                    //Debug.Log(AngleArrow.transform.position);
                    //Debug.Log(transform.right * kickForce);
                    shot = true;
                    Destroy(arrow);
                    
                }
            }
            //Debug.Log(rb.angularVelocity);
            if(shot)
            {
                Debug.Log(rb.velocity);
                shot = false;
            }
                
            Vector2 worldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit2D hit = Physics2D.Raycast(worldPoint, Vector2.zero);

            if (hit.collider == null && !aim)
            {
                EnableArrowObject();
                return;
            }

            if (!aim && hit.collider.name == "Ball")
            {
                DisableArrowObject();
            }
            else 
            {
                if(!aim)
                    EnableArrowObject();
            }
        }

        private void ChangeShootAngle()
        {
            if(AngleArrow.transform.localPosition.x > 0.15f) 
            {
                Debug.Log(AngleArrow.transform.localPosition.y);
                return;
            }
            AngleArrow.transform.RotateAround(transform.position, new Vector3(0,0,1), 80f*Time.deltaTime);
            //Debug.Log(AngleArrow.transform.rotation);
        }
        
        private void EnableArrowObject()
        {
            if (arrow == null) return;
            if (!Input.GetMouseButton(0)) return;
            arrow.SetActive(true);
        }

        private void EnableAngleArrowObject()
        {
            if (AngleArrow == null) return;
            if (!Input.GetMouseButton(0)) return;
            AngleArrow.SetActive(true);
        }

        private void DisableAngleArrowObject()
        {
            if (AngleArrow == null) return;
            AngleArrow.SetActive(false);
        }

        private void DisableArrowObject()
        {
            if (arrow == null) return;
            arrow.SetActive(false);
        }


    }
}
