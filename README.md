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

## ❓ 프로젝트 시, 일어난 문제와 해결

## 📒 프로젝트 소감

