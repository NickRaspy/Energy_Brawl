using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBall : MonoBehaviour
{
    public float power = 100f;

    public int num;

    public bool isSummoned = false;
    public bool inPlayerArea = false;
    public bool isLanded = false;

    public new ParticleSystem particleSystem;

    public GameObject player;
    public GameObject spawnpoint;

    public Vector3 hitPoint;
    // Start is called before the first frame update
    void Start()
    {
        spawnpoint = GameObject.Find("Spawnpoint");
        spawnpoint.GetComponent<SpawnManager>().spawnedEnergy.Add(gameObject);
        num = spawnpoint.GetComponent<SpawnManager>().spawnedEnergy.IndexOf(gameObject);
        if (isSummoned)
        {
            GetComponent<Rigidbody>().mass = 10f; 
            GetComponent<Rigidbody>().AddForce((hitPoint - transform.position).normalized * power);
        }
        particleSystem.Play();
    }
    public void OnCollisionEnter(Collision collision)
    {
        if (isSummoned)
        {
            isSummoned = false;
            GetComponent<Rigidbody>().mass = 10000f;
        }
        isLanded = true;
    }

    // Update is called once per frame
    void Update()
    {

    }
}
