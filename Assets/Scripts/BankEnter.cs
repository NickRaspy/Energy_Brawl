using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BankEnter : MonoBehaviour
{
    public bool isMagnet = false;

    private GameObject spawnpoint;

    public GameObject energyBank;
    public void Start()
    {
        spawnpoint = GameObject.Find("Spawnpoint");
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Energy") && isMagnet)
        {
            spawnpoint.GetComponent<SpawnManager>().spawnedEnergy.Remove(other.gameObject);
            Destroy(other.gameObject);
            energyBank.GetComponent<EnergyBank>().EnergyGet();
            energyBank.GetComponent<EnergyBank>().isCharged = true;
        }
    }
}
