using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] GameObject popVFX;

    void OnTriggerEnter(Collider other)
    {
        GameObject vfx = Instantiate(popVFX, transform.position, Quaternion.identity);
        //Destroy(gameObject);
    }

    //void OnParticleCollision(GameObject other)
    //{
    //    print("lala");
    //    GameObject vfx = Instantiate(popVFX, transform.position, Quaternion.identity);
    //    Destroy(gameObject);
    //}
}
