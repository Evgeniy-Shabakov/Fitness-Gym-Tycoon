using System;
using UnityEngine;
using TMPro;

public class UIManager : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textForMoney;

    private void Start()
    {
        PlayerData.MoneyChanged.AddListener(ChengedTextForMoney);
    }

    private void ChengedTextForMoney()
    {
        textForMoney.text = "" + PlayerData.Instanse.GetMoney();
    }
}
