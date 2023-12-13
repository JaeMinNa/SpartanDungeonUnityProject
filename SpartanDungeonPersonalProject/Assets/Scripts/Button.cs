using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Button : MonoBehaviour
{
    [Header("Panel")]
    [SerializeField] private GameObject statusPanel;
    [SerializeField] private GameObject inventoryPanel;
    [SerializeField] private GameObject buttonPanel;

    [SerializeField ]private Inventory inventory;
    [SerializeField] private Equipment equipment;

    private Weapon weapon;
    private Armor armor;
    


    public void StatusButton()
    {
        statusPanel.SetActive(true);
        buttonPanel.SetActive(false);
        GameManager.I.GetStatus();
    }

    public void InventoryButton()
    {
        inventoryPanel.SetActive(true);
        buttonPanel.SetActive(false);
    }

    public void StatusPanelXButton()
    {
        buttonPanel.SetActive(true);
        statusPanel.SetActive(false);
    }

    public void InventoryPanelXButton()
    {
        buttonPanel.SetActive(true);
        inventoryPanel.SetActive(false);
    }

    public void EquipButton()
    {
        string str = GetButtonName().Substring(11);
        int count = int.Parse(str);

        if(inventory.InventorySlot[count].tag == "WeaponItem")
        {
            weapon = inventory.InventorySlot[count].GetComponent<Weapon>();
            if (weapon.isEquip == true)
            {
                weapon.isEquip = false;
                inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(false);
                equipment.DisEquip(inventory.InventorySlot[count]);
            }
            else
            {
                weapon.isEquip = true;
                inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
                equipment.Equip(inventory.InventorySlot[count]);
            }
        }
        else
        {
            armor = inventory.InventorySlot[count].GetComponent<Armor>();
            if (armor.isEquip == true)
            {
                armor.isEquip = false;
                inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(false);
                equipment.DisEquip(inventory.InventorySlot[count]);
            }
            else
            {
                armor.isEquip = true;
                inventory.InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
                equipment.Equip(inventory.InventorySlot[count]);
            }
        }

    }

    public string GetButtonName()
    {
        string EventButtonName = EventSystem.current.currentSelectedGameObject.name;

        return EventButtonName;
    }
}
