using System;
using UI;
using UnityEngine;

public class DateSimulation : MonoBehaviour
{
    public static DateSimulation Instance;
    
    private const float WaitOneDay = 2f;
    
    private int _day;
    private int _month;
    private int _year;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(StartDateSimulation), WaitOneDay);
    }

    public void SetDate(int day, int month, int year)
    {
        _day = day;
        _month = month;
        _year = year;
        
        UIManagerMain.Instance.SetTextDateSimulation(_day, _month, _year);
    }
    private void StartDateSimulation()
    {
        _day++;
        
        if (_day == 31)
        {
            _day = 0;
            _month++;
            
            LevelManager.Instance.DoOnStartNewMonth();
        }

        if (_month == 12)
        {
            _month = 0;
            _year++;
        }
        
        UIManagerMain.Instance.SetTextDateSimulation(_day, _month, _year);
        
        Invoke(nameof(StartDateSimulation), WaitOneDay);
    }

    public int GetDay()
    {
        return _day;
    }
    
    public int GetMonth()
    {
        return _month;
    }
    
    public int GetYear()
    {
        return _year;
    }
}
