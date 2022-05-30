using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.AI;

public class EnemyExistance : MonoBehaviour
{
    public Slider hp;

    private NavMeshAgent agent;

    private SpawnManager spawnedEnergy;

    public GameObject energy;
    private GameObject energyGate;

    public Vector3 goingToNearEnergy;

    public float damageRatio;
    public float speed = 10f;

    public int ID;

    public bool canGo = false;
    public bool gotEnergy = false;
    // Start is called before the first frame update
    void Start()
    {
        spawnedEnergy = GameObject.Find("Spawnpoint").GetComponent<SpawnManager>();
        energyGate = GameObject.Find("Enemy Energy Gate");
    }

    // Update is called once per frame
    void Update()
    {
        switch (ID)
        {
            case 0:
                agent = GetComponent<NavMeshAgent>();
                StealerLogic();
                hp.transform.parent.transform.LookAt(spawnedEnergy.spawnedPlayer.transform.position);
                break;
        }
    }
    public void Damage()
    {
        hp.gameObject.SetActive(true);
        StartCoroutine(Cooldown(5f));
        hp.value -= (1 / damageRatio) + 0.01f;
        if(hp.value <= 0f)
        {
            gameObject.GetComponent<Animator>().Play("Die");
        }
        else
        {
            gameObject.GetComponent<Animator>().Play("GetHit");
        }
    }
    IEnumerator Cooldown(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        hp.gameObject.SetActive(false);
    }
    public void StealerLogic()
    {
        if (GameObject.FindGameObjectWithTag("Energy") != null && !gotEnergy)
        {
            if (GameObject.FindGameObjectWithTag("Energy").GetComponent<EnergyBall>().isLanded)
            {
                goingToNearEnergy = spawnedEnergy.spawnedEnergy[0].transform.position;
                canGo = true;
                int setter = 0;
                for (int i = 0; i < spawnedEnergy.spawnedEnergy.Count; i++)
                {
                    if (Vector3.Distance(gameObject.transform.position, spawnedEnergy.spawnedEnergy[i].transform.position) <= Vector3.Distance(gameObject.transform.position, goingToNearEnergy) && spawnedEnergy.spawnedEnergy[i].GetComponent<EnergyBall>().isLanded)
                    {
                        goingToNearEnergy = spawnedEnergy.spawnedEnergy[i].transform.position;
                        setter = i;
                    }
                }
                if (canGo)
                {
                    gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
                    gameObject.transform.LookAt(goingToNearEnergy);
                    agent.destination = goingToNearEnergy;
                }
            }
        }
        else if (gotEnergy)
        {
            gameObject.GetComponent<Animator>().SetBool("IsWalking", true);
            goingToNearEnergy = energyGate.transform.position;
            gameObject.transform.LookAt(goingToNearEnergy);
            agent.destination = goingToNearEnergy;
        }
        else
        {
            agent.ResetPath();
            canGo = false;
            gameObject.GetComponent<Animator>().SetBool("IsWalking", false);
        }
    }
    public void Die()
    {
        if (ID == 0)
        {
            spawnedEnergy.stealerCount--;
            spawnedEnergy.stealerCounter.text = ": " + spawnedEnergy.stealerCount + "/2";
            if (gotEnergy)
            {
                Vector3 spawnPos = new Vector3(gameObject.transform.position.x, gameObject.transform.position.y + 2f, gameObject.transform.position.z);
                Instantiate(energy, spawnPos, energy.transform.rotation);
            }
        }
        Destroy(gameObject);
    }
}
