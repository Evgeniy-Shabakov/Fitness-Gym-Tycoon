using UnityEngine;
using System.IO;

public class LevelManager : MonoBehaviour
{
    public static int moneyStartGame = 50000;
    
    private static int raitingOnStart = 50;
    
    public static int moodSad = 25;
    public static int moodHappy = 75;

    public static int moodRangeMin = 35;
    public static int moodRangeMax = 100;
    public static int countMoodAdd = 10;
    public static int countMoodTakeAway = 15;

    public static float minTimeExercise = 4;
    public static float maxTimeExercise = 7;
    
    private static int rating;
    
    private void Start()
    {
        if (File.Exists(SaveLoadManager.Instance.savePath) == false)
        {
            rating = raitingOnStart;
            UIManagerMain.Instance.SetRating(rating);
        }
    }
    
    public static void AddRating(int n)
    {
        rating += n;
        if (rating > 100) rating = 100;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();
    }

    public static void TakeAwayRating(int n)
    {
        rating -= n;
        if (rating < 5) rating = 5;
        
        UIManagerMain.Instance.SetRating(rating);
        SaveLoadManager.Instance.Save();
    }

    public static int GetRating()
    {
        return rating;
    }

    public static void LoadRating(int value)
    {
        rating = value;
        UIManagerMain.Instance.SetRating(rating);
    }
}
