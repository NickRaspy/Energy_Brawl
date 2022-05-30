using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;

public class MoveBullet : MonoBehaviour
{
    public Vector3 hitPoint;

    public int speed;

    public bool isTargeted;
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Rigidbody>().AddForce((hitPoint - this.transform.position).normalized * speed);
        StartCoroutine(BulletLifetime(5f));
    }
    void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Enemy"))
        {
            col.gameObject.GetComponent<EnemyExistance>().Damage();
        }
        Destroy(gameObject);
    }
    IEnumerator BulletLifetime(float lifetime)
    {
        yield return new WaitForSeconds(lifetime);
        Destroy(gameObject);
    }
    IEnumerator EnergyStay(float seconds, Collision energy)
    {
        energy.gameObject.GetComponent<Rigidbody>().Sleep();
        yield return new WaitForSeconds(seconds);
        energy.gameObject.GetComponent<Rigidbody>().WakeUp();
    }
}