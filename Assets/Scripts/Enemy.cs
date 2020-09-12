using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables

    public float health;
    public GameObject bulletSpawnPoint;
    public GameObject bullet;
    public float waitTime;
    public float maxWaitTime;

    public GameObject player;

    // Functions

    public void Start()
    {
        player = GameObject.FindWithTag("Player");
        bulletSpawnPoint = this.gameObject.transform.GetChild(0).transform.GetChild(1).gameObject;
        print(bulletSpawnPoint);
    }

    public void Update()
    {

        if (health <= 0)
            Die();

        this.transform.LookAt(new Vector3(player.transform.position.x,0.5f,player.transform.position.z));
        this.transform.Translate(Vector3.forward * Time.deltaTime);

        waitTime -= Time.deltaTime;

        if (waitTime < 0)
        {
            Shoot();
        }

    }

    public void Die()
    {
        print("Enemy is supposedly dead");
        Destroy(this.gameObject);
    }

    public void Shoot()
    {
        Instantiate(bullet.transform, bulletSpawnPoint.transform.position, bulletSpawnPoint.transform.rotation);
        waitTime = maxWaitTime;
    }

}
