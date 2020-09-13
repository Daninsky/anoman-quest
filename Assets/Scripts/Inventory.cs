using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Variables

    private bool inventoryEnabled, lastWasWeapons;

    private int initSlots, weaponSlots, armorSlots, collectibleSlots;

    private GameObject[] weapons, armors, collectibles;

    public Button inventoryButton;
    public FixedJoystick joystick;
    public Button actionButton;
    public GameObject inventory;

    public Button equipmentButton, collectiblesButton, weaponsButton, armorsButton;

    public GameObject weaponsPanel, armorsPanel, collectiblesPanel;

    public Text type;

    // Functions

    void Awake()
    {
        inventoryButton = GameObject.FindWithTag("InventoryButton").GetComponent<Button>();
        joystick = GameObject.FindWithTag("Joystick").GetComponent<FixedJoystick>();
        actionButton = GameObject.FindWithTag("Shoot").GetComponent<Button>();
        inventoryButton.onClick.AddListener(OpenInventory);
        inventory.SetActive(false);

        armorsPanel.SetActive(false);
        collectiblesPanel.SetActive(false);
        equipmentButton.onClick.AddListener(EquipmentToggle);
        collectiblesButton.onClick.AddListener(CollectiblesToggle);
        weaponsButton.onClick.AddListener(WeaponsToggle);
        armorsButton.onClick.AddListener(ArmorsToggle);

        equipmentButton.interactable = false;
        weaponsButton.interactable = false;
        lastWasWeapons = true;
        type.text = "WEAPONS";

    }

    void Start()
    {
        initSlots = 5;

        weapons = new GameObject[initSlots];
        armors = new GameObject[initSlots];
        collectibles = new GameObject[initSlots];

        for (int i = 0; i < initSlots; i++)
        {
            weapons[i] = weaponsPanel.transform.GetChild(i).gameObject;
            armors[i] = armorsPanel.transform.GetChild(i).gameObject;
            collectibles[i] = collectiblesPanel.transform.GetChild(i).gameObject;
        }
    }


    void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

        print("Collide pls");

        if (other.tag == "Item")
        {



            GameObject itemPickedUp = other.gameObject;
            Item item = itemPickedUp.GetComponent<Item>();

            if (!item.pickedUp)
            {
                item.pickedUp = true;
                AddItem(itemPickedUp, item.ID, item.type, item.description, item.icon);
            }
        }
    }

    void AddItem(GameObject itemObject, int itemID, string itemType, string itemDescription, Sprite itemIcon)
    {

        GameObject[] currentArray;

        if (itemType == "weapon")
        {
            currentArray = weapons;
        } else if (itemType == "armor")
        {
            currentArray = armors;
        } else
        {
            currentArray = collectibles;
        }


        for (int i = 0; i < initSlots; i++)
        {
            if (!currentArray[i].GetComponent<Slot>().occupied)
            {
                // add the item to inventory
                itemObject.GetComponent<Item>().pickedUp = true;

                currentArray[i].GetComponent<Slot>().item = itemObject;
                currentArray[i].GetComponent<Slot>().ID = itemID;
                currentArray[i].GetComponent<Slot>().type = itemType;
                currentArray[i].GetComponent<Slot>().description = itemDescription;
                currentArray[i].GetComponent<Slot>().icon = itemIcon;

                itemObject.transform.parent = currentArray[i].transform;
                itemObject.SetActive(false);

                currentArray[i].GetComponent<Slot>().UpdateSlot();
                currentArray[i].GetComponent<Slot>().occupied = true;
                return;
            }
        }
    }

    public GameObject FindItem(int itemID, string itemType)
    {
        GameObject[] currentArray;

        if (itemType == "weapon")
        {
            currentArray = weapons;
        }
        else if (itemType == "armor")
        {
            currentArray = armors;
        }
        else
        {
            currentArray = collectibles;
        }

        for (int i = 0; i < initSlots; i++)
        {
            if (currentArray[i].GetComponent<Slot>().ID == itemID)
            {
                return currentArray[i].GetComponent<Slot>().item;
            }
        }

        return null;

    }


    void OpenInventory()
    {
        inventoryEnabled = !inventoryEnabled;
        
        if (inventoryEnabled)
        {
            inventory.SetActive(true);
            joystick.gameObject.SetActive(false);
            actionButton.gameObject.SetActive(false);
            Time.timeScale = 0;
        } else
        {
            inventory.SetActive(false);
            joystick.gameObject.SetActive(true);
            actionButton.gameObject.SetActive(true);
            Time.timeScale = 1;
        }
    }

    void EquipmentToggle()
    {
        collectiblesPanel.SetActive(false);

        equipmentButton.interactable = false;
        collectiblesButton.interactable = true;

        if (lastWasWeapons)
        {
            weaponsPanel.SetActive(true);
            armorsButton.interactable = true;
            type.text = "WEAPONS";
        } else
        {
            armorsPanel.SetActive(true);
            weaponsButton.interactable = true;
            type.text = "ARMORS";
        }
    }

    void CollectiblesToggle()
    {
        if (lastWasWeapons)
        {
            weaponsPanel.SetActive(false);
            armorsButton.interactable = false;
        } else
        {
            armorsPanel.SetActive(false);
            weaponsButton.interactable = false;
        }
        
        collectiblesPanel.SetActive(true);

        equipmentButton.interactable = true;
        collectiblesButton.interactable = false;
        type.text = "COLLECTIBLES";
    }

    void WeaponsToggle()
    {
        lastWasWeapons = true;
        weaponsPanel.SetActive(true);
        armorsPanel.SetActive(false);

        weaponsButton.interactable = false;
        armorsButton.interactable = true;
        type.text = "WEAPONS";
    }

    void ArmorsToggle()
    {
        lastWasWeapons = false;
        weaponsPanel.SetActive(false);
        armorsPanel.SetActive(true);

        weaponsButton.interactable = true;
        armorsButton.interactable = false;
        type.text = "ARMORS";

    }

    public void EquipItem(int ID, string type)
    {
        GameObject itemToEquip = FindItem(ID, type);

        print("EquipItem on Inventory script working properly. Now calling player:");

        this.GetComponentInParent<Player>().EquipWeapon(FindItem(ID,type), ID);
    }
}
