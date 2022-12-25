using UI;
using UnityEngine;

public class DateSimulation : MonoBehaviour
{
    private const float WaitOneDay = 1f;
    
    private int _day;
    private int _month;
    private int _year;
    

    private void Start()
    {
        Invoke(nameof(StartDateSimulation), WaitOneDay);
    }

    private void StartDateSimulation()
    {
        _day++;
        
        if (_day == 31)
        {
            _day = 0;
            _month++;
        }

        if (_month == 12)
        {
            _month = 0;
            _year++;
        }
        
        UIManagerMain.Instance.SetTextDateSimulation(_day, _month, _year);
        
        Invoke(nameof(StartDateSimulation), WaitOneDay);
    }
}
