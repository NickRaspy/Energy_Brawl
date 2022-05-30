using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class Gun : MonoBehaviour
{
    public float cooldownSpeed;

    public float fireRate;

    public float recoilCooldown;

    private float accuracy;

    public float maxSpreadAngle;

    public float timeTillMaxSpread;

    public GameObject bullet;

    public GameObject shootPoint;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        cooldownSpeed += Time.deltaTime * 60f;

        if (Input.GetButton("Fire1"))
        {
            accuracy += Time.deltaTime * 4f;
            if (cooldownSpeed >= fireRate)
            {
                Shoot();
                cooldownSpeed = 0;
                recoilCooldown = 1;
            }
        }
        else
        {
            recoilCooldown -= Time.deltaTime;
            if(recoilCooldown <= 1)
            {
                accuracy = 0.0f;
            }
        }
    }

    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, (-transform.right), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(transform.position, transform.right*(-20f), Color.red);
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, transform.rotation);
            tempBullet.GetComponent<MoveBullet>().hitPoint = hit.point;
        }
    }
}