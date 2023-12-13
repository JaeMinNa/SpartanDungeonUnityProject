using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public int Atk { get; private set; }
    public int Def { get; private set; }
    public int Hp { get; private set; }

    private void Start()
    {
        Atk = 10;
        Def = 10;
        Hp = 10;
    }

    public void SetPlusAtk(int value)
    {
        Atk += value;
    }

    public void SetPlusDef(int value)
    {
        Def += value;
    }

    public void SetPlusHp(int value)
    {
        Hp += value;
    }
}
