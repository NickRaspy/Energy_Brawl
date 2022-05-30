using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpawnManager : MonoBehaviour
{
    public Text stealerCounter;

    public GameObject player;
    public GameObject energy;
    public GameObject energyArea;
    public GameObject enemySpawnpoint;
    public GameObject spawnedPlayer;

    public List<GameObject> spawnedEnergy;
    public List<GameObject> enemyList;

    public int stealerCount = 0;
    // Start is called before the first frame update
    void Start()
    {
        stealerCounter.text = ": " + stealerCount + "/2";
        spawnedPlayer = Instantiate(player, transform.position, player.transform.rotation);
        InvokeRepeating("EnergySpawn", 5f, 5f);
        InvokeRepeating("EnemySpawn", 10f, 20f);
    }

    // Update is called once per frame
    void Update()
    {

    }
    void EnergySpawn()
    {
        Collider energyBounds = energyArea.GetComponent<Collider>();
        float rndX = Random.Range(energyBounds.bounds.min.x, energyBounds.bounds.max.x);
        float rndZ = Random.Range(energyBounds.bounds.min.z, energyBounds.bounds.max.z);
        if(rndX < energyBounds.bounds.center.x)
        {
            rndX -= energyBounds.bounds.center.x;
        }
        else if(rndX > energyBounds.bounds.center.x)
        {
            rndX += energyBounds.bounds.center.x;
        }
        if(rndZ < energyBounds.bounds.center.z)
        {
            rndZ -= energyBounds.bounds.center.z;
        }
        else if(rndZ > energyBounds.bounds.center.z)
        {
            rndZ += energyBounds.bounds.center.z;
        }
        Vector3 energySpawnPosition = new Vector3(rndX, energyArea.transform.position.y, rndZ);
        Debug.Log(energyBounds.bounds.center);
        Debug.Log(energySpawnPosition);
        GameObject newEnergy = Instantiate(energy, energySpawnPosition, energy.transform.rotation);
        newEnergy.GetComponent<EnergyBall>().num = spawnedEnergy.IndexOf(newEnergy);
    }
    void EnemySpawn()
    {
        GameObject spawnedEnemy = null;
        switch (EnemyChoose())
        {
            case 0:
                if (stealerCount < 2)
                {
                    spawnedEnemy = Instantiate(enemyList[EnemyChoose()], enemySpawnpoint.transform.position, enemySpawnpoint.transform.rotation);
                    stealerCount++;
                    stealerCounter.text = ": " + stealerCount + "/2";
                }
                break;
        }
    }
    int EnemyChoose()
    {
        int r = Random.Range(0, enemyList.Count);
        return r;
    }
}
