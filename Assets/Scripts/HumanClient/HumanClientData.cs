using BuildingSystem;
using UnityEngine;
using Random = UnityEngine.Random;

namespace HumanClient
{
    public class HumanClientData : MonoBehaviour
    {
        private Gender _gender;
        private SubscriptionType _subscriptionType;

        private int _priceEntry;
        
        private int _mood;
        
        public ObjectType[] targetsArray;
        public bool[] targetsStatus;
        
        public int indexInTargetsArray;
        public bool trainingIsFinished;

        private void Start()
        {
            _mood = Random.Range(LevelManager.MoodRangeMin, LevelManager.MoodRangeMax + 1);
            targetsArray = new ObjectType[LevelManager.NumberTargetsHumanClient];
            targetsStatus = new bool[LevelManager.NumberTargetsHumanClient];
            trainingIsFinished = false;
            
            SetTargetsArray();
        }

        public Gender GetGender()
        {
            return _gender;
        }

        public void SetGender(Gender value)
        {
            _gender = value;
        }
        
        private void SetTargetsArray()
        {
            targetsArray[0] = ObjectType.Reсeption;
            targetsArray[1] = ObjectType.Lockers;
        
            for (int i = 2; i < LevelManager.NumberTargetsHumanClient - 2; i++)
            {
                targetsArray[i] = (ObjectType)Random.Range(3, BuildingManager.Instance.objectsForBuilding.Count);
            
                while(targetsArray[i] == targetsArray[i-1])
                {
                    targetsArray[i] = (ObjectType)Random.Range(3, BuildingManager.Instance.objectsForBuilding.Count);
                }
            }

            targetsArray[LevelManager.NumberTargetsHumanClient - 2] = ObjectType.Shower;
            targetsArray[LevelManager.NumberTargetsHumanClient - 1] = ObjectType.Lockers;
        }
        
        public void AddMood(int countMood)
        {
            _mood += countMood;
            if (_mood > 100) _mood = 100;
        }

        public void TakeAwayMood(int countMood)
        {
            _mood -= countMood;
            if (_mood < 5) _mood = 5;
        }

        public int GetMood()
        {
            return _mood;
        }

        public SubscriptionType GetSubscriptionType()
        {
            return _subscriptionType;
        }
        public void SetSubscriptionType(SubscriptionType type)
        {
            _subscriptionType = type;
        }

        public int GetPriceEntry()
        {
            return _priceEntry;
        }
        
        public void SetPriceEntry()
        {
            _priceEntry = DeterminePriceEntry();
        }
        
        private int DeterminePriceEntry()
        {
            switch (_subscriptionType)
            {
                case SubscriptionType.Visit:
                    return LevelManager.Instance.GetPricePerVisit();
                case SubscriptionType.Month:
                    return LevelManager.Instance.GetPricePerMonth();
                case SubscriptionType.SixMonth:
                    return LevelManager.Instance.GetPricePerSixMonth();
                case SubscriptionType.Year:
                    return LevelManager.Instance.GetPricePerYear();
                
                default:
                    return 0;
            }
        }
    }
}
