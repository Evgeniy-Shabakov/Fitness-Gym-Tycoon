using UnityEngine;
using System.IO;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instanse;

    private int money;
    private int rating;
    
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

            rating = LevelManager.raitingLevelStart;
            UIManagerMain.Instance.SetRating(rating);
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

    public void AddRating(int n)
    {
        rating += n;
        if (rating > 100) rating = 100;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();
    }

    public void TakeAwayRating(int n)
    {
        rating -= n;
        if (rating < 5) rating = 5;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();
    }

    public int GetRating()
    {
        return rating;
    }

    public void LoadRating(int value)
    {
        rating = value;
        UIManagerMain.Instance.SetRating(rating);
    }
}
