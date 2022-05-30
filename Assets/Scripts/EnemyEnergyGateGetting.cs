using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyEnergyGateGetting : MonoBehaviour
{
    public Text counter;

    public int count = 0;

    public GameObject UI;
    public GameObject lateGameUI;

    public SpawnManager objects;
    public void Update()
    {
        if(count == 20)
        {
            objects.gameObject.GetComponent<Pause>().isCompleted = true;
            objects.CancelInvoke();
            if (GameObject.FindGameObjectWithTag("Enemy") != null)
            {
                GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
                foreach (GameObject enemy in enemies)
                    Destroy(enemy);
            }
            if (GameObject.FindGameObjectWithTag("Energy") != null)
            {
                GameObject[] energyBalls = GameObject.FindGameObjectsWithTag("Energy");
                foreach (GameObject energy in energyBalls)
                    Destroy(energy);
            }
            objects.spawnedPlayer.GetComponent<PlayerController>().canMove = false;
            UI.SetActive(false);
            lateGameUI.SetActive(true);
            lateGameUI.transform.Find("WinLose").GetComponent<Text>().text = "You Lose!";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    public void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            count++;
            counter.text = "Count: " + count + "/20";
            other.transform.Find("Glass").transform.Find("Energy Bank").GetComponent<EnergyBank>().EnergyOut();
            other.GetComponent<EnemyExistance>().gotEnergy = false;
        }
    }
}
