using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnergyBank : MonoBehaviour
{
    private Renderer energyBank;

    public bool isCharged = false;
    // Start is called before the first frame update
    void Start()
    {
        energyBank = GetComponent<Renderer>();
        energyBank.material.SetColor("_EmissionColor", Color.black * 3f);
        energyBank.material.color = Color.black * 3f;
    }

    IEnumerator ChangeColor(Renderer rend, Color startColor, Color endColor, float time)
    {
        float elapsedTime = 0.0f;

        rend.material.color = startColor;

        while (elapsedTime <= time)
        {
            rend.material.color = Color.Lerp(startColor, endColor, elapsedTime / time) * 2f;
            rend.material.SetColor("_EmissionColor", Color.Lerp(startColor, endColor, elapsedTime / time) * 3f);

            elapsedTime += Time.deltaTime;

            yield return null;
        }
    }
    public void EnergyGet()
    {
        StartCoroutine(ChangeColor(energyBank, energyBank.material.color, Color.yellow, 1f));
    }
    public void EnergyOut()
    {
        StartCoroutine(ChangeColor(energyBank, energyBank.material.color, Color.black, 1f));
    }
}
