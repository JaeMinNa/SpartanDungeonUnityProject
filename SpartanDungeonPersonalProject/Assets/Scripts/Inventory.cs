using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    public List<GameObject> InventorySlot = new List<GameObject>();
    public List<GameObject> InventorySlotTempImage = new List<GameObject>();

    private Image image;
    private SpriteRenderer spriteRenderer;
    private Weapon weapon;
    private Armor armor;

    private void Start()
    {
        InventoryFalseReset();
        ShowItemInInventory();
    }

    private void InventoryFalseReset()
    {
        foreach (GameObject item in InventorySlot)
        {
            if (item.tag == "WeaponItem")
            {
                weapon = item.GetComponent<Weapon>();
                weapon.isEquip = false;
            }
            else
            {
                armor = item.GetComponent<Armor>();
                armor.isEquip = false;
                
            }
        }
    }

    private void ShowItemInInventory()
    {
        int count = 0;
        foreach(GameObject item in InventorySlot)
        {
            // 인벤토리 창에 이미지 표시
            InventorySlotTempImage[count].SetActive(true);
            image = InventorySlotTempImage[count].GetComponent<Image>();
            spriteRenderer = item.GetComponent<SpriteRenderer>();
            image.sprite = spriteRenderer.sprite;

            // 장비 장착 여부 확인
            if(item.tag == "WeaponItem")
            {
                weapon = item.GetComponent<Weapon>();
                if(weapon.isEquip == true)
                {
                    InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
                }
            }
            else
            {
                armor = item.GetComponent<Armor>();
                if (armor.isEquip == true)
                {
                    InventorySlotTempImage[count].transform.Find($"EquipButton{count}/Text (TMP)").gameObject.SetActive(true);
                }
            }

            count++;
        }
    }
}
