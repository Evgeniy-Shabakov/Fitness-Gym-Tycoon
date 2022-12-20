using UnityEngine;
using UnityEngine.Events;

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
        money = 50000;
        UIManagerMain.Instance.SetTextMoney(money);
    }

    public void AddMoney(int amount)
    {
        money += amount;
        UIManagerMain.Instance.SetTextMoney(money);
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        UIManagerMain.Instance.SetTextMoney(money);
    }

    public int GetMoney()
    {
        return money;
    }
}
