using System;
using UnityEngine;
using System.IO;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;
    
    public const int moneyStartGame = 50000;
    
    public const int moodSad = 25;
    public const int moodHappy = 75;
    public const int moodRangeMin = 35;
    public const int moodRangeMax = 100;
    public const int countMoodAdd = 10;
    public const int countMoodTakeAway = 15;

    public const float minTimeExercise = 4;
    public const float maxTimeExercise = 7;
    
    private const int ratingOnStart = 50;
    private const int pricePerVisitOnStart = 20;
    private const float timeSpawnClientBase = 4;
    
    private int rating;
    private int pricePerVisit;
    private float timeSpawnClient;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        if (File.Exists(SaveLoadManager.Instance.savePath) == false)
        {
            rating = ratingOnStart;
            UIManagerMain.Instance.SetRating(rating);

            pricePerVisit = pricePerVisitOnStart;
        }
        
        Invoke(nameof(SetTimeSpawnClient), 1f);
    }

    private void SetTimeSpawnClient()
    {
        float k1;
        if (rating <= moodSad) k1 = 2;
        else if (rating > moodSad && rating < moodHappy) k1 = 1;
        else k1 = 0.5f;

        float k2;
        if (pricePerVisit <= pricePerVisitOnStart) k2 = 0.5f;
        else k2 = (pricePerVisit - pricePerVisitOnStart) / 100f + 1f;
        
        timeSpawnClient = timeSpawnClientBase * k1 * k2;
    }
    
    public void AddRating(int n)
    {
        rating += n;
        if (rating > 100) rating = 100;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();

        SetTimeSpawnClient();
    }

    public void TakeAwayRating(int n)
    {
        rating -= n;
        if (rating < 5) rating = 5;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();
        
        SetTimeSpawnClient();
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

    public int GetPricePerVisit()
    {
        return pricePerVisit;
    }

    public void SetPricePerVisitFromSlider(Slider sl)
    {
        pricePerVisit = (int)sl.value;
        SetTimeSpawnClient();
    }
    
    public void LoadPricePerVisit(int value)
    {
        pricePerVisit = value;
    }

    public float GetTimeSpawnClient()
    {
        return timeSpawnClient;
    }
}
