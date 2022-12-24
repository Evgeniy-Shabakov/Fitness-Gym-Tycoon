using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public const int MoneyStartGame = 50000;
    
    public const int MoodSad = 25;
    public const int MoodHappy = 75;
    public const int MoodRangeMin = 35;
    public const int MoodRangeMax = 100;
    public const int CountMoodAdd = 10;
    public const int CountMoodTakeAway = 15;

    public const float MinTimeExercise = 4;
    public const float MaxTimeExercise = 7;
    
    public const int RatingOnStart = 50;
    public const int PricePerVisitOnStart = 20;
    public const int CountLockersOnStart = 0;
    
    private const float TimeSpawnClientBase = 4;
    
    private int _rating;
    private int _pricePerVisit;
    private float _timeSpawnClient;
    private int _countLockers;

    private int _countMen;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        Invoke(nameof(SetTimeSpawnClient), 1f);

        _countMen = 0;
    }

    private void SetTimeSpawnClient()
    {
        float k1;
        if (_rating <= MoodSad) k1 = 2;
        else if (_rating > MoodSad && _rating < MoodHappy) k1 = 1;
        else k1 = 0.5f;

        float k2;
        if (_pricePerVisit <= PricePerVisitOnStart) k2 = 0.5f;
        else k2 = (_pricePerVisit - PricePerVisitOnStart) / 100f + 1f;
        
        _timeSpawnClient = TimeSpawnClientBase * k1 * k2;
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

    public void SetPricePerVisitFromSlider(Slider sl)
    {
        _pricePerVisit = (int)sl.value;
        SetTimeSpawnClient();
    }
    
    public void SetPricePerVisit(int value)
    {
        _pricePerVisit = value;
    }

    public float GetTimeSpawnClient()
    {
        return _timeSpawnClient;
    }

    public void AddCountLockers(int n)
    {
        _countLockers += n;
        UIManagerMain.Instance.SetTextLockers(_countLockers);
    }

    public void TakeAwayCountLockers(int n)
    {
        _countLockers -= n;
        if (_countLockers < 0) _countLockers = 0;
        UIManagerMain.Instance.SetTextLockers(_countLockers);
    }

    public int GetCountLockers()
    {
        return _countLockers;
    }

    public void SetCountLockers(int value)
    {
        _countLockers = value;
        UIManagerMain.Instance.SetTextLockers(_countLockers);
    }

    public void AddCountMen()
    {
        _countMen++;
        UIManagerMain.Instance.SetTextCountMen(_countMen);
    }

    public void TakeAwayCountMen()
    {
        _countMen--;
        UIManagerMain.Instance.SetTextCountMen(_countMen);
    }
}
