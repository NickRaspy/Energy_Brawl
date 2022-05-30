using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MouseSensitivity : MonoBehaviour
{
    public GameObject player;
    public Slider sensChange;
    private GameObject camera;
    public void Start()
    {
        camera = player.transform.Find("Camera").gameObject;
        sensChange.value = camera.GetComponent<CameraRotate>().mouseSensitivity;
    }
    public void Update()
    {
        sensChange.transform.Find("Count").GetComponent<Text>().text = sensChange.value.ToString();
    }
    public void Sensitivity()
    {
        camera.GetComponent<CameraRotate>().mouseSensitivity = sensChange.value;
    }
}
