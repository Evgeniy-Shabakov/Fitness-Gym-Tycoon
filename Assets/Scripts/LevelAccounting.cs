using HumanClient;
using UI;
using UnityEngine;

public class LevelAccounting : MonoBehaviour
{
    public static LevelAccounting Instance;
    
    private int _totalRevenueVisit;
    private int _totalRevenueMonth;
    private int _totalRevenueSixMonth;
    private int _totalRevenueYear;
    private int _totalSaleEquipment;
    
    private int _totalPurchaseEquipment;
    
    private int _taxMonthly;
    private int _moneyOnStartMonth;
    private int _moneyOnEndMonth;
    
    private int _totalMonthly;

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

    public void ChangeTotalProfitPerSubscription(int value, SubscriptionType type)
    {
        switch (type)
        {
            case SubscriptionType.Visit:
                _totalRevenueVisit += value;
                break;
            case SubscriptionType.Month:
                _totalRevenueMonth += value;
                break;
            case SubscriptionType.SixMonth:
                _totalRevenueSixMonth += value;
                break;
            case SubscriptionType.Year:
                _totalRevenueYear += value;
                break;
        }
    }
    
    public void AddTotalSaleEquipment(int value)
    {
        _totalSaleEquipment += value;
    }
    
    public void AddTotalPurchaseEquipment(int value)
    {
        _totalPurchaseEquipment += value;
    }

    public void UpdateAccounting()
    {
        UIManagerPanelAccounting.Instance.SetTextTotalVisit(_totalRevenueVisit);
        UIManagerPanelAccounting.Instance.SetTextTotalMonth(_totalRevenueMonth);
        UIManagerPanelAccounting.Instance.SetTextTotalSixMonth(_totalRevenueSixMonth);
        UIManagerPanelAccounting.Instance.SetTextTotalYear(_totalRevenueYear);
        UIManagerPanelAccounting.Instance.SetTextSaleEquipment(_totalSaleEquipment);
        
        UIManagerPanelAccounting.Instance.SetTextRent(-LevelManager.RentMonthly);
        UIManagerPanelAccounting.Instance.SetTextPurchaseEquipment(-_totalPurchaseEquipment);
        UIManagerPanelAccounting.Instance.SetTextTax(-_taxMonthly);
        
        CountTotalMonthly();
        UIManagerPanelAccounting.Instance.SetTextTotal(_totalMonthly);

        _totalRevenueVisit = 0;
        _totalRevenueMonth = 0;
        _totalRevenueSixMonth = 0;
        _totalRevenueYear = 0;
        _totalSaleEquipment = 0;
        _totalPurchaseEquipment = 0;

    }

    private void CountTotalMonthly()
    {
        var totalRevenue = _totalRevenueVisit + _totalRevenueMonth + _totalRevenueSixMonth + _totalRevenueYear +
                          _totalSaleEquipment;

        var totalConsumption = LevelManager.RentMonthly + _totalPurchaseEquipment + _taxMonthly;

        _totalMonthly = totalRevenue - totalConsumption;
    }
}
