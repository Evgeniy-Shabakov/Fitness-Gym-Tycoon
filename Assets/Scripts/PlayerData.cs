using UnityEngine;
using UnityEngine.Events;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instanse;

    private int money;
    public static UnityEvent MoneyChanged = new UnityEvent();
    
    private void Awake()
    {
        Instanse = this;
    }

    private void Start()
    {
        money = 10000;
        MoneyChanged.Invoke();
    }

    public void AddMoney(int amount)
    {
        money += amount;
        MoneyChanged.Invoke();
    }

    public void SpendMoney(int amount)
    {
        money -= amount;
        MoneyChanged.Invoke();
    }

    public int GetMoney()
    {
        return money;
    }
}
