using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyHitParticle : MonoBehaviour
{
    void Start()
    {
        Destroy(this.gameObject, 0.2f);
    }
}
