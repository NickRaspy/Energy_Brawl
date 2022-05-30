using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StealEnergy : MonoBehaviour
{
    public GameObject stealer;
    public GameObject energyBank;
    private GameObject spawnpoint;
    public void Start()
    {
        spawnpoint = GameObject.Find("Spawnpoint");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            spawnpoint.GetComponent<SpawnManager>().spawnedEnergy.Remove(other.gameObject);
            Destroy(other.gameObject);
            energyBank.GetComponent<EnergyBank>().EnergyGet();
            stealer.GetComponent<EnemyExistance>().gotEnergy = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {

    }
}
