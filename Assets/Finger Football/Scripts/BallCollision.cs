using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace FingerFootball
{
    //Used to detect when the ball collides with the object or enters the collider that is set as trigger
    public class BallCollision : MonoBehaviour
    {
        private AudioSource ballHitSound;
        private Rigidbody2D rb;

        private void Start()
        {
            ballHitSound = GameObject.Find("BallHitSound").GetComponent<AudioSource>();
            rb = GetComponent<Rigidbody2D>();
        }
        private void OnCollisionEnter2D(Collision2D collision)
        {
            ballHitSound.Play();//Play the sound when the ball collides with any object
            Vector2 contactsPos = collision.contacts[0].point;
            GameObject hitParticle = Instantiate(Resources.Load("HitParticle", typeof(GameObject))) as GameObject;
            hitParticle.transform.position = contactsPos;
            rb.velocity = rb.velocity * 0.88f;
        }

        void OnTriggerEnter2D(Collider2D col)
        {
            if (col.gameObject.name.Contains("Border"))//If the ball goes off the field and hits the border
            {
                PlayerPrefs.SetInt("NumberOfMisses", PlayerPrefs.GetInt("NumberOfMisses") + 1);
                GameObject.Find("GameManager").GetComponent<InstantiateBall>().InstantiateNewBall();
                Destroy(this.gameObject);
            }
            else if (col.gameObject.name.Equals("Goal"))//If the ball enters into the goal
            {
                PlayerPrefs.SetInt("NumberOfGoals", PlayerPrefs.GetInt("NumberOfGoals") + 1);
                GameObject.Find("GameManager").GetComponent<Menus>().LevelComplete();
                GetComponent<SlowDownTheBall>().enabled = true;
                if (PlayerPrefs.GetInt("Vibration") == 1)
                {
                    //Handheld.Vibrate();
                }
            }
        }
    }
}
