using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;

    public GameObject gunHolder;
    public Inventory inventory;

    public GameObject[] bullets;
    public GameObject bulletSpawnPoint;
    public float waitTime;
    public float maxWaitTime;

    private FixedJoystick joystick;
    private Button actionButton;
    private Animator animator;

    private bool weaponEquip;
    private int currentBulletID;

    // Functions
    void Awake()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        animator = GetComponent<Animator>();
        actionButton = GameObject.FindWithTag("Shoot").GetComponent<Button>();
        actionButton.onClick.AddListener(Shoot);

        bullets = Resources.LoadAll<GameObject>("Prefabs");
    }

    void Update()
    {
        // Movement

        float x = joystick.Horizontal;
        float z = joystick.Vertical;

        Vector3 velocity = new Vector3(x, 0f, z);
        transform.position += velocity * movementSpeed * Time.deltaTime;

        if (joystick.Horizontal != 0 || joystick.Vertical != 0) {
            animator.SetBool("isWalking", true);
            Quaternion rotation = Quaternion.LookRotation(velocity);
            rotation.x = 0;
            rotation.z = 0;
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, 10f * Time.deltaTime);
        } else
        {
            animator.SetBool("isWalking", false);
        }

        // Shoot

        waitTime -= Time.deltaTime;
        if (waitTime < 0 && weaponEquip)
        {
            Shoot();
        }



    }

    void Shoot()
    {

            Instantiate(bullets[currentBulletID - 1].transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            waitTime = maxWaitTime;

    }

    public void EquipWeapon(GameObject weaponToEquip, int bulletID)
    {

        if (weaponEquip)
        {
            Destroy(this.gunHolder.transform.GetChild(0).gameObject);
        }

        var newWeapon = Instantiate(weaponToEquip, this.gunHolder.transform.position,Quaternion.identity, this.gunHolder.transform);
        newWeapon.transform.parent = this.gunHolder.transform;
        newWeapon.SetActive(true);
        Destroy(newWeapon.GetComponent<Rigidbody>());

        weaponEquip = true;
        this.currentBulletID = bulletID;
    }

}
