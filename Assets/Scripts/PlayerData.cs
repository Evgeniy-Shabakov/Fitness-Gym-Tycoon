using System;
using UnityEngine;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instanse;

    private int maney;
    
    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        maney = 10000;
    }

    public void AddMoney(int amount)
    {
        maney += amount;
    }

    public void SpendMoney(int amount)
    {
        maney -= amount;
    }
}
