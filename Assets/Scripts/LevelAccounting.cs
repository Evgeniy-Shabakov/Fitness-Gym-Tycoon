using UnityEngine;

public class LevelAccounting : MonoBehaviour
{
    public static LevelAccounting Instance;
    
    private int _totalProfitVisit;
    private int _totalProfitMonth;
    private int _totalProfitSixMonth;
    private int _totalProfitYear;
    private int _totalSaleEquipment;
    
    private int _totalPurchaseEquipment;
    
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

    public void AddTotalProfitVisit(int value)
    {
        _totalProfitVisit += value;
    }
    
    public void RemoveTotalProfitVisit(int value)
    {
        _totalProfitVisit -= value;
    }

    public void AddTotalProfitMonth(int value)
    {
        _totalProfitMonth += value;
    }
    
    public void RemoveTotalProfitMonth(int value)
    {
        _totalProfitMonth -= value;
    }
    
    public void AddTotalProfitSixMonth(int value)
    {
        _totalProfitSixMonth += value;
    }
    
    public void RemoveTotalProfitSixMonth(int value)
    {
        _totalProfitSixMonth -= value;
    }
    
    public void AddTotalProfitYear(int value)
    {
        _totalProfitYear += value;
    }
    
    public void RemoveTotalProfitYear(int value)
    {
        _totalProfitYear -= value;
    }
    
    public void AddTotalSaleEquipment(int value)
    {
        _totalSaleEquipment += value;
    }
    
    public void AddTotalPurchaseEquipment(int value)
    {
        _totalPurchaseEquipment += value;
    }
}
