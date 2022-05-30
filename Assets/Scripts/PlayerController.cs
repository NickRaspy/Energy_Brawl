using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    private float horizontal, vertical;
    public float speed = 5f;

    public bool canMove = true;
    // Update is called once per frame
    void Update()
    {
        if (canMove)
        {
            horizontal = Input.GetAxis("Horizontal");
            vertical = Input.GetAxis("Vertical");
            transform.Translate(horizontal * speed * Time.deltaTime, 0, vertical * speed * Time.deltaTime);
        }
    }

}
