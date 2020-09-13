using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{

    // Variables

    public int weaponID;
    public float speed;
    public float maxDistance;
    public float damage;

    public GameObject triggeringEnemy;

    // Functions

    void Update()
    {
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
        maxDistance += 1 * Time.deltaTime;

        if (maxDistance >= 2)
            Destroy(this.gameObject);

    }

    public void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Enemy")
        {

            print("You shot the enemy!");

            triggeringEnemy = other.gameObject;
            triggeringEnemy.GetComponent<Enemy>().health -= damage;
            Destroy(this.gameObject);
        }
        else if(other.tag == "Player")
        {
            print("You got shot!");
        }
    }
}
