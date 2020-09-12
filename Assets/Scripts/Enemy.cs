using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Variables

    public float health;

    // Functions

    public void Update()
    {
        if (health <= 0)
            Die();
    }

    public void Die()
    {
        print("Enemy is supposedly dead");
        Destroy(this.gameObject);
    }

}
