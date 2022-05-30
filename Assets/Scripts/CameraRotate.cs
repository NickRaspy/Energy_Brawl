using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CameraRotate : MonoBehaviour
{
    private float rotationOnX;
    private float mouseX, mouseY;
    public float mouseSensitivity = 5f;

    public GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(player.GetComponent<PlayerController>().canMove)
            Rotating();
    }
    void Rotating()
    {
        mouseX = Input.GetAxis("Mouse X") * Time.deltaTime * mouseSensitivity;
        mouseY = Input.GetAxis("Mouse Y") * Time.deltaTime * mouseSensitivity;

        rotationOnX -= mouseY;
        rotationOnX = Mathf.Clamp(rotationOnX, -90, 90);
        transform.localEulerAngles = new Vector3(rotationOnX, 0f, 0f);
        player.transform.Rotate(Vector3.up * mouseX);
    }
}
