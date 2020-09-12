using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    // Variables
    public float movementSpeed;

    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float waitTime;
    public float maxWaitTime;

    private FixedJoystick joystick;
    private Button actionButton;
    private Animator animator;

    // Functions
    void Awake()
    {
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        animator = GetComponent<Animator>();
        actionButton = GameObject.FindWithTag("Shoot").GetComponent<Button>();
        actionButton.onClick.AddListener(Shoot);

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
        
    }

    void Shoot()
    {
        print("You pressed the button!");

        if (waitTime < 0)
        {
            Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
            waitTime = maxWaitTime;
        }
        

    }

}
