using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    // Variables

    private bool inventoryEnabled;
    private bool lastWasWeapons;

    public Button inventoryButton;
    public FixedJoystick joystick;
    public Button actionButton;
    public GameObject inventory;

    public Button equipmentButton;
    public Button collectiblesButton;
    public Button weaponsButton;
    public Button armorsButton;

    public GameObject weaponsPanel;
    public GameObject armorsPanel;
    public GameObject collectiblesPanel;

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


    void Update()
    {

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
}
