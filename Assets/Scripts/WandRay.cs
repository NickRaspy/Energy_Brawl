using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WandRay : MonoBehaviour
{
    private Image aim;
    private Slider recharge;

    private GameObject energyBank;
    public GameObject player;
    public GameObject wand;
    public GameObject musket;
    public GameObject bullet;
    public GameObject energy;
    public GameObject shootPoint;

    public BankEnter trigger;
    public BankEnter triggerEX;

    public float magnetForce = 100f;
    public float speed = 5f;
    public float cooldown = 2f;

    public bool isWand = true;
    public bool canShoot = true;
    public bool isMagnet = false;
    // Start is called before the first frame update
    void Start()
    {
        energyBank = wand.transform.Find("Glass").transform.Find("Energy Bank").gameObject;
        aim = GameObject.Find("Aiming").transform.Find("Aim").gameObject.GetComponent<Image>();
        recharge = GameObject.Find("Aiming").transform.Find("Recharge").gameObject.GetComponent<Slider>();
    }

    // Update is called once per frame
    void Update()
    {
        bool canDoSmth = transform.parent.GetComponent<PlayerController>().canMove;
        Debug.DrawRay(transform.position, transform.forward * 30f, Color.yellow);
        Animator musketAnim = musket.GetComponent<Animator>();
        Animator wandAnim = wand.GetComponent<Animator>();
        GameObject ball = wand.transform.Find("Glass").gameObject;

        if (bullet.activeInHierarchy)
        {
            Physics.IgnoreCollision(bullet.transform.GetComponent<Collider>(), transform.GetComponentInParent<Collider>());
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            isWand = !isWand;
        }

        if (isWand && canDoSmth)
        {
            wand.SetActive(true);
            musket.SetActive(false);
            WandGathering(wandAnim, ball);
        }
        else if (!isWand && canDoSmth)
        {
            wand.SetActive(false);
            musket.SetActive(true);
            aim.color = Color.black;
            MusketShoot(musketAnim);
        }

        if (!canShoot)
        {
            recharge.gameObject.SetActive(true);
        }
        else
        {
            recharge.gameObject.SetActive(false);
        }
    }
    void MusketShoot(Animator musketAnim)
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (canShoot)
            {
                Shoot();
                StartCoroutine(ShootCooldown(cooldown));
            }
        }

        if (Input.GetKey(KeyCode.Mouse1))
        {
            musketAnim.Play("Ready Shoot");
            musketAnim.SetBool("Move", true);
        }
        else if (Input.GetKeyUp(KeyCode.Mouse1))
        {
            musketAnim.Play("Stop Shoot");
            musketAnim.SetBool("Move", false);
        }
    }
    void WandGathering(Animator wandAnim, GameObject ball)
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit) && !energyBank.GetComponent<EnergyBank>().isCharged)
        {
            GameObject target = hit.collider.gameObject;
            Rigidbody energyRb = target.GetComponent<Rigidbody>();
            if (target.CompareTag("Energy"))
            {
                if (target.GetComponent<EnergyBall>().inPlayerArea)
                {
                    aim.color = Color.yellow;
                    if (Input.GetKey(KeyCode.Mouse1))
                    {
                        trigger.isMagnet = true;
                        triggerEX.isMagnet = true;
                        wandAnim.Play("Rotate In");
                        wandAnim.SetBool("Rotate", true);
                        Physics.IgnoreCollision(target.GetComponent<Collider>(), transform.parent.GetComponent<Collider>());
                        energyRb.velocity = (transform.position - target.transform.position) * Time.deltaTime * magnetForce;
                    }
                    else if (Input.GetKeyUp(KeyCode.Mouse1))
                    {
                        trigger.isMagnet = false;
                        triggerEX.isMagnet = false;
                        wandAnim.Play("Rotate Out");
                        wandAnim.SetBool("Rotate", false);
                    }
                }
            }
            else
            {
                trigger.isMagnet = false;
                triggerEX.isMagnet = false;
                aim.color = Color.black;
                if (wandAnim.GetBool("Rotate") == true)
                {
                    wandAnim.Play("Rotate Out");
                    wandAnim.SetBool("Rotate", false);
                }
            }
        }
        else if(Physics.Raycast(transform.position, transform.forward, out hit, Mathf.Infinity))
        {
            if (energyBank.GetComponent<EnergyBank>().isCharged)
            {
                trigger.isMagnet = false;
                triggerEX.isMagnet = false;
                aim.color = Color.black;
                if (wandAnim.GetBool("Rotate") == true)
                {
                    wandAnim.Play("Rotate Out");
                    wandAnim.SetBool("Rotate", false);
                }
                if (Input.GetKeyDown(KeyCode.Mouse0))
                {
                    GameObject outEnergy = Instantiate(energy, energyBank.transform.position, energy.transform.rotation);
                    Physics.IgnoreCollision(outEnergy.GetComponent<Collider>(), player.GetComponent<Collider>());
                    outEnergy.GetComponent<EnergyBall>().hitPoint = hit.point;
                    outEnergy.GetComponent<EnergyBall>().isSummoned = true;
                    energyBank.GetComponent<EnergyBank>().EnergyOut();
                    energyBank.GetComponent<EnergyBank>().isCharged = false;
                }
            }
        }
    }
    IEnumerator ShootCooldown(float cooldownTime)
    {
        canShoot = false;
        StartCoroutine(AnimateSliderOverTime(cooldownTime));
        yield return new WaitForSeconds(cooldownTime);
        canShoot = true;
    }
    IEnumerator AnimateSliderOverTime(float seconds)
    {
        float animationTime = 0f;
        while (animationTime < seconds)
        {
            animationTime += Time.deltaTime;
            float lerpValue = animationTime / seconds;
            recharge.value = Mathf.Lerp(0f, 1f, lerpValue);
            yield return null;
        }
    }
    void Shoot()
    {
        RaycastHit hit;
        if (Physics.Raycast(musket.transform.position, (-musket.transform.right), out hit, Mathf.Infinity))
        {
            Debug.DrawRay(musket.transform.position, musket.transform.right * (-20f), Color.red);
            Quaternion bulletRotation = new Quaternion(bullet.transform.rotation.x, transform.rotation.y, transform.rotation.z, bullet.transform.rotation.w);
            GameObject tempBullet = Instantiate(bullet, shootPoint.transform.position, bulletRotation);
            Physics.IgnoreCollision(tempBullet.GetComponent<Collider>(), player.GetComponent<Collider>());
            tempBullet.GetComponent<MoveBullet>().hitPoint = hit.point;
        }
    }
}
