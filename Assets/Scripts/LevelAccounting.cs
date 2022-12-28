using UnityEngine;

public class LevelAccounting : MonoBehaviour
{
    public static LevelAccounting Instance;
    
    private int _subscriptionsPerVisit;
    private int _subscriptionsPerMonth;
    private int _subscriptionsPerSixMonth;
    private int _subscriptionsPerYear;
    private int _saleEquipment;
    
    private int _purchaseEquipment;
    
    private int _taxMonthly;
    private int _moneyOnStartMonth;
    private int _moneyOnEndMonth;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        _moneyOnEndMonth = PlayerData.GetMoney();
    }
    
    public int CountTaxMonthly()
    {
        _moneyOnStartMonth = _moneyOnEndMonth;
        _moneyOnEndMonth = PlayerData.GetMoney();
        
        _taxMonthly = (_moneyOnEndMonth - _moneyOnStartMonth) * LevelManager.InterestOnProfit/100;
        if (_taxMonthly < 0) _taxMonthly = 0;
        
        return _taxMonthly;
    }
}
