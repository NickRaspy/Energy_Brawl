using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerArea : MonoBehaviour
{
    public void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            other.GetComponent<EnergyBall>().inPlayerArea = true;
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Energy"))
        {
            other.GetComponent<EnergyBall>().inPlayerArea = false;
        }
    }
}
