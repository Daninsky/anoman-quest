using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    // Variables

    public int ID;
    public string type; // Weapon, Armor, or Collectible
    public string description;
    public Sprite icon;
    public bool pickedUp;
    public bool equipped;
    
    // Functions

    void Update()
    {
        if (!pickedUp)
        {
            transform.Rotate(Vector3.up * Time.deltaTime * 50f);
        }
    }
}
