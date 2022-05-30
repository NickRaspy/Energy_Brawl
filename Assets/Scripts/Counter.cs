using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Counter : MonoBehaviour
{
    public Text CounterText;

    public int Count = 0;

    private GameObject spawnpoint;
    public GameObject UI;
    public GameObject lateGameUI;

    public SpawnManager objects;
    public void Update()
    {
        if (Count == 20)
        {
            spawnpoint.GetComponent<Pause>().isCompleted = true;
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
            lateGameUI.transform.Find("WinLose").GetComponent<Text>().text = "You Win!";
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }
    private void Start()
    {
        Count = 0;
        CounterText.text = "Count : " + Count + "/20";
        spawnpoint = GameObject.Find("Spawnpoint");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            Count += 1;
            CounterText.text = "Count : " + Count + "/20";
            spawnpoint.GetComponent<SpawnManager>().spawnedEnergy.Remove(other.gameObject);
            Destroy(other.gameObject);
        }
    }
}
