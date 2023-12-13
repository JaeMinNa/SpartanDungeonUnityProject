# 🖥️ SpartanDungeonUnityProject

## 📆 Develop Schedule
* 23.12.11 ~ 23.12.12
<br/>

## ⚙️ Environment
- `Unity 2022.3.2f1`
- **IDE** : Visual Studio 2019
- **Envrionment** : PC `only`
- **Resolution** :	16:9 `Aspect`
<br/>

## ▶️ 게임 스크린샷
<img src="https://github.com/JaeMinNa/SpartanDungeonUnityProject/assets/149379194/8a2bd2f1-da90-4630-ad81-7b7b68a6b06a" width="1000">
<br/>

## 🎮 구현기능
* 필수요구사항
   * 메인 화면 구성
   * Status 보기
   * Inventory 보기
<br/>

## 🔍 세부내용
### 인벤토리
* List<GameObject>로 구현
* List를 순회하여 모든 Item을 인벤토리에 나타냄
* 인스펙터 창에서 인벤토리의 각 슬롯을 연결하여 각각 이미지를 변경해줌
<br/>

```
using System.Collections;
using System.Collections.Generc;
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
```
<br/>

### 장착 관리
* 장착을 관리하는 List<GameObject>를 생성
* 장착에 따라 캐릭터 능력치 변경
<br/>

```
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
```
<br/>

### 장착 버튼
* 인벤토리 슬롯에 각각 버튼으로 장착 구현
* EventSystem 로 사용한 버튼의 이름에 접근
<br/>

```
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
```
<br/>

## ❓ 프로젝트 시, 일어난 문제와 해결
### 부모 클래스 Item에 접근 불가
부모 클래스인 Item을 자식 클래스 Weapon, Armor가 상속받도록 했다. 각각 아이템 프리팹들은 Weapon 또는 Armor 스크립트를 가지고 있는데, 부모 클래스가 가지고 있는 isEuqip 변수에 접근할 수가 없었다.
그래서 각각 Item 프리팹들을 tag 설정했다.
<br/>

<img src="https://github.com/JaeMinNa/SpartanDungeonUnityProject/assets/149379194/87189de8-efd6-4f41-a59d-d693c3630f1a" width="1000">
```
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
```


## 📒 프로젝트 소감

