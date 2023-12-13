using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Equipment : MonoBehaviour
{
    public List<GameObject> equipmentList;
    [SerializeField] private Player player;

    private Weapon weapon;
    private Armor armor;

    private void Start()
    {
        equipmentList = new List<GameObject>();
    }

    public void Equip(GameObject item)
    {
        equipmentList.Add(item);

        if (item.tag == "WeaponItem")
        {
            weapon = item.GetComponent<Weapon>();
            player.SetPlusAtk(weapon.atk);

        }
        else
        {
            armor = item.GetComponent<Armor>();
            player.SetPlusDef(armor.def);
            player.SetPlusHp(armor.Hp);
        }
    }

    public void DisEquip(GameObject item)
    {
        equipmentList.Remove(item);

        if (item.tag == "WeaponItem")
        {
            weapon = item.GetComponent<Weapon>();
            player.SetPlusAtk(-weapon.atk);

        }
        else
        {
            armor = item.GetComponent<Armor>();
            player.SetPlusDef(-armor.def);
            player.SetPlusHp(-armor.Hp);
        }
    }
}
