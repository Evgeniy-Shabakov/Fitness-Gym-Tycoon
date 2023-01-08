using System.Collections;
using UI;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject parentAllDynamicObjects;
    
    public const int MoneyStartGame = 50000;
    
    public const int RentMonthly = 2000;
    public const int TaxInterestOnProfit = 20;

    public const int SalaryReceptionist = 500;
    public const int SalaryJanitor = 300;
    public const int SalaryTrainer = 800;
    public const int SalaryEngineer = 700;
    
    public const int NumberTargetsHumanClient = 15;
    
    public const int MoodSad = 25;
    public const int MoodHappy = 75;
    public const int MoodRangeMin = 35;
    public const int MoodRangeMax = 100;
    public const int CountMoodAdd = 10;
    public const int CountMoodTakeAway = 15;
    public const int CountMoodTakeAwayTrash = 5;

    public const float MinTimeExercise = 4;
    public const float MaxTimeExercise = 7;
    
    public const int RatingOnStart = 50;
    
    public const int PricePerVisitOnStart = 20;
    public const int PricePerMonthOnStart = 100;
    public const int PricePerSixMonthOnStart = 500;
    public const int PricePerYearOnStart = 900;

    public const int StepPricePerVisit = 1;
    public const int StepPricePerMonth = 5;
    public const int StepPricePerSixMonth = 10;
    public const int StepPricePerYear = 50;

    private const int MinPricePerVisit = 1;
    private const int MaxPricePerVisit = 100;
    private const int MinPricePerMonth = 20;
    private const int MaxPricePerMonth = 300;
    private const int MinPricePerSixMonth = 50;
    private const int MaxPricePerSixMonth = 1000;
    private const int MinPricePerYear = 100;
    private const int MaxPricePerYear = 10000;
    
    private const float TimeSpawnClientBaseVisit = 20;
    private const float TimeSpawnClientBaseMonth = 4;
    private const float TimeSpawnClientBaseSixMonth = 10;
    private const float TimeSpawnClientBaseYear = 30;
    
    private int _rating;
    
    private int _pricePerVisit;
    private int _pricePerMonth;
    private int _pricePerSixMonth;
    private int _pricePerYear;
    
    private float _timeSpawnClientVisit;
    private float _timeSpawnClientMonth;
    private float _timeSpawnClientSixMonth;
    private float _timeSpawnClientYear;
    
    private int _numberMenLockers;
    private int _numberWomenLockers;
    private int _numberMen;
    private int _numberWomen;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        StartCoroutine(CountNumberLockers(0.1f));
        Invoke(nameof(SetTimeSpawnClient), 1f);
    }

    private void SetTimeSpawnClient()
    {
        float k1;
        if (_rating <= MoodSad) k1 = 2;
        else if (_rating is > MoodSad and < MoodHappy) k1 = 1;
        else k1 = 0.5f;

        float k2;
        if (_pricePerVisit <= PricePerVisitOnStart) k2 = 0.5f;
        else k2 = (_pricePerVisit - PricePerVisitOnStart) / 100f + 1f;
        
        _timeSpawnClientVisit = TimeSpawnClientBaseVisit * k1 * k2;
        
        if (_pricePerMonth <= PricePerMonthOnStart) k2 = 0.5f;
        else k2 = (_pricePerMonth - PricePerMonthOnStart) / 100f + 1f;
        
        _timeSpawnClientMonth = TimeSpawnClientBaseMonth * k1 * k2;
        
        if (_pricePerSixMonth <= PricePerSixMonthOnStart) k2 = 0.5f;
        else k2 = (_pricePerSixMonth - PricePerSixMonthOnStart) / 100f + 1f;
        
        _timeSpawnClientSixMonth = TimeSpawnClientBaseSixMonth * k1 * k2;
        
        if (_pricePerYear <= PricePerYearOnStart) k2 = 0.5f;
        else k2 = (_pricePerYear - PricePerYearOnStart) / 100f + 1f;
        
        _timeSpawnClientYear = TimeSpawnClientBaseYear * k1 * k2;
    }
    
    public void AddRating(int n)
    {
        _rating += n;
        if (_rating > 100) _rating = 100;
        
        UIManagerMain.Instance.SetRating(_rating);
        SaveLoadManager.Instance.Save();

        SetTimeSpawnClient();
    }

    public void TakeAwayRating(int n)
    {
        _rating -= n;
        if (_rating < 5) _rating = 5;
        
        UIManagerMain.Instance.SetRating(_rating);
        SaveLoadManager.Instance.Save();
        
        SetTimeSpawnClient();
    }

    public int GetRating()
    {
        return _rating;
    }

    public void SetRating(int value)
    {
        _rating = value;
        UIManagerMain.Instance.SetRating(_rating);
    }

    public int GetPricePerVisit()
    {
        return _pricePerVisit;
    }

    public void SetPricePerVisit(int value)
    {
        _pricePerVisit = value;
        if (_pricePerVisit < MinPricePerVisit) _pricePerVisit = MinPricePerVisit;
        if (_pricePerVisit > MaxPricePerVisit) _pricePerVisit = MaxPricePerVisit;

        SetTimeSpawnClient();
    }
    
    public int GetPricePerMonth()
    {
        return _pricePerMonth;
    }

    public void SetPricePerMonth(int value)
    {
        _pricePerMonth = value;
        
        if (_pricePerMonth < MinPricePerMonth) _pricePerMonth = MinPricePerMonth;
        if (_pricePerMonth > MaxPricePerMonth) _pricePerMonth = MaxPricePerMonth;
        
        SetTimeSpawnClient();
    }
    
    public int GetPricePerSixMonth()
    {
        return _pricePerSixMonth;
    }

    public void SetPricePerSixMonth(int value)
    {
        _pricePerSixMonth = value;
        
        if (_pricePerSixMonth < MinPricePerSixMonth) _pricePerSixMonth = MinPricePerSixMonth;
        if (_pricePerSixMonth > MaxPricePerSixMonth) _pricePerSixMonth = MaxPricePerSixMonth;
        
        SetTimeSpawnClient();
    }
    
    public int GetPricePerYear()
    {
        return _pricePerYear;
    }

    public void SetPricePerYear(int value)
    {
        _pricePerYear = value;
        
        if (_pricePerYear < MinPricePerYear) _pricePerYear = MinPricePerYear;
        if (_pricePerYear > MaxPricePerYear) _pricePerYear = MaxPricePerYear;
        
        SetTimeSpawnClient();
    }

    public float GetTimeSpawnClientVisit()
    {
        return _timeSpawnClientVisit;
    }
    
    public float GetTimeSpawnClientMonth()
    {
        return _timeSpawnClientMonth;
    }
    
    public float GetTimeSpawnClientSixMonth()
    {
        return _timeSpawnClientSixMonth;
    }
    
    public float GetTimeSpawnClientYear()
    {
        return _timeSpawnClientYear;
    }

    public IEnumerator CountNumberLockers(float wait)
    {
        yield return new WaitForSeconds(wait);
        
        _numberMenLockers = 0;
        _numberWomenLockers = 0;

        foreach (Transform child in parentAllDynamicObjects.transform)
        {
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == 1)
            {
                if (LayerDetected.GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorMenLockerRoom"))
                {
                    _numberMenLockers += 5;
                }
                
                if (LayerDetected.GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorWomenLockerRoom"))
                {
                    _numberWomenLockers += 5;
                }
            }
        }
        
        UIManagerMain.Instance.SetTextMenLockers(_numberMenLockers);
        UIManagerMain.Instance.SetTextWomenLockers(_numberWomenLockers);
    }
    
    public void AddNumberMen()
    {
        _numberMen++;
        UIManagerMain.Instance.SetTextNumberMen(_numberMen);
    }

    public void TakeAwayNumberMen()
    {
        _numberMen--;
        UIManagerMain.Instance.SetTextNumberMen(_numberMen);
    }
    
    public void AddNumberWomen()
    {
        _numberWomen++;
        UIManagerMain.Instance.SetTextNumberWomen(_numberWomen);
    }

    public void TakeAwayNumberWomen()
    {
        _numberWomen--;
        UIManagerMain.Instance.SetTextNumberWomen(_numberWomen);
    }
    
    public void DoOnStartNewMonth()
    {
        PlayerData.SpendMoney(RentMonthly);
        UIManagerMain.Instance.AddNewMessage("rent: -" + RentMonthly + "$");

        int totalSalary = LevelAccounting.Instance.CountTotalSalary();
        PlayerData.SpendMoney(totalSalary);
        UIManagerMain.Instance.AddNewMessage("salaries: -" + totalSalary + "$");
        
        var tax = LevelAccounting.Instance.CountTaxMonthly();
        PlayerData.SpendMoney(tax);
        UIManagerMain.Instance.AddNewMessage("tax: -" + tax + "$");
        
        LevelAccounting.Instance.UpdateAccounting();
    }
}
