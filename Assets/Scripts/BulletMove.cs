using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    public float speed = 100f;
    public Vector3 targetPosition;
    public GameObject gun;
    void Start()
    {
        GetComponent<Rigidbody>().AddForce(gun.transform.forward * speed, ForceMode.Impulse);
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (!collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
        }
    }

}
