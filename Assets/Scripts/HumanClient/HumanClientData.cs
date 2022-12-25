using UnityEngine;
using Random = UnityEngine.Random;

namespace HumanClient
{
    public class HumanClientData : MonoBehaviour
    {
        private Gender _gender;
        
        private int _mood;
        
        public int[] targetsArray;
        public bool[] targetsStatus;
        
        public int indexInTargetsArray;
        public bool trainingIsFinished;

        private void Start()
        {
            _mood = Random.Range(LevelManager.MoodRangeMin, LevelManager.MoodRangeMax + 1);
            targetsArray = new int[LevelManager.NumberTargetsHumanClient];
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
            targetsArray[0] = 0;
            targetsArray[1] = 1;
        
            for (int i = 2; i < LevelManager.NumberTargetsHumanClient - 2; i++)
            {
                targetsArray[i] = Random.Range(3, BuildingManager.Instance.objectsForBuilding.Count);
            
                while(targetsArray[i] == targetsArray[i-1])
                {
                    targetsArray[i] = Random.Range(3, BuildingManager.Instance.objectsForBuilding.Count);
                }
            }

            targetsArray[LevelManager.NumberTargetsHumanClient - 2] = 2;
            targetsArray[LevelManager.NumberTargetsHumanClient - 1] = 1;
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
    }
}
