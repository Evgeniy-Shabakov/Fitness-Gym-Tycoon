using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instanse;

    private int money;
    
    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        if (File.Exists(SaveLoadManager.Instance.savePath) == false)
        {
            money = LevelManager.moneyStartGame;
            UIManagerMain.Instance.SetTextMoney(money);
        }
    }

    public void AddMoney(int amount)
    {
        money += amount;
        
        UIManagerMain.Instance.SetTextMoney(money);
        SaveLoadManager.Instance.Save();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        
        UIManagerMain.Instance.SetTextMoney(money);
        SaveLoadManager.Instance.Save();
    }

    public int GetMoney()
    {
        return money;
    }

    public void LoadMoney(int value)
    {
        money = value;
        UIManagerMain.Instance.SetTextMoney(money);
    }
}
