using System.Collections;
using UI;
using UnityEngine;
using UnityEngine.UI;

public class LevelManager : MonoBehaviour
{
    public static LevelManager Instance;

    [SerializeField] private GameObject parentAllDynamicObjects;
    
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
}
