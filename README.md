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
그래서 각각 Item 프리팹들을 tag 설정해서 if문으로 tag를 판별하여 각각 스크립트에 접근하도록 했다.
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

<br/>

### 9개의 버튼 함수 생성
인벤토리에서 각각 슬롯을 만들어서 인스펙터 창에서 연결했기 때문에, 9개의 버튼을 생성했다. 이 버튼들은 각각 이 버튼들이 가지고 있는 Text에 접근해서
gameObject를 활성화해서 장착[E] 표시를 해야 한다. 그렇게되면 각각 버튼에 연결할 9개의 함수가 필요해서, 불필요하게 코드가 엄청 길어졌다. 
그래서 EventSystem를 사용해서 마지막에 클릭한 버튼의 이름에 접근해서, 모든 버튼에 적용가능한 하나의 함수를 작성했다.
<br/>

```
using UnityEngine.EventSystems;

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

## 📒 프로젝트 소감
이번 개인 프로젝트를 하면서 가장 크게 느낀 것은 게임 제작은 기초 공사가 중요하다라는 것이다.
조금 귀찮고 어렵다고 초반부터 잘못된 방향으로 작업을 하게되면 이러한 부분이 스노우볼처럼 굴러와 나중에는 수정이 어려운 상황이 올 수도 있다라는 것을 느꼈다.
이번 프로젝트를 하면서 아쉬웠던 점은, 인벤토리가 항상 처음부터 정렬된다는 것이다. 큰 게임을 만들기 위해서는 슬롯 각각 독립적으로 Item을 관리하는 인벤토리가 필요하기 때문에 보완이 필요한 것 같다.
그리고 부모 클래스인 Item에 접근할 수가 없어서 Item 프리팹을 Tag로 나눈것은 효율적인 방법이 아니라고 생각한다. 정작 Item Tag가 필요한 상황에서 사용할 수 없다. 이 부분은 해결방법을 따로 공부하도록 하겠다.
지난번 프로젝트보다 많이 성장했다고 생각하지만, 아직 부족한 부분도 많았다. 다음 프로젝트에서는 더 좋은 프로젝트를 완성하겠다.


