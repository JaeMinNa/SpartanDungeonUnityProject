using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameManager : MonoBehaviour
{
    [Header("Text")]
    [SerializeField] private TMP_Text atkText;
    [SerializeField] private TMP_Text defText;
    [SerializeField] private TMP_Text hpText;

    public static GameManager I;
    private Player player;

    private void Awake()
    {
        I = this;
        player = GameObject.Find("Player").GetComponent<Player>();
    }

    public void GetStatus()
    {
        int value = player.Atk;
        atkText.text = value.ToString();
        value = player.Def;
        defText.text = value.ToString();
        value = player.Hp;
        hpText.text = value.ToString();
    }
}
