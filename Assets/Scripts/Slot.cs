using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class Slot : MonoBehaviour, IPointerClickHandler
{
    // Variables

    public Inventory inventory;

    public GameObject item;

    public bool occupied;

    public int ID;
    public string type; // Weapon, Armor, or Collectible
    public string description;
    public Sprite icon;

    public Transform slotImage;

    // Functions

    void Awake()
    {
        inventory = GameObject.FindWithTag("Player").GetComponent<Inventory>();
        slotImage = transform.GetChild(0);
    }

    public void UpdateSlot()
    {
        slotImage.GetComponent<Image>().sprite = icon;
    }

    public void OnPointerClick(PointerEventData pointerEventData)
    {
        UseItem();
    }

    public void UseItem()
    {
        print("Clicked UseItem in Slot script with ID" + ID + " and type " + type);

        inventory.EquipItem(ID, type);
    }
}
