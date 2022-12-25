using UnityEngine;
using Random = UnityEngine.Random;

namespace HumanClient
{
    public class HumanClientData : MonoBehaviour
    {
        private Gender _gender;
        
        public int[] targetsArray;
        public int indexInTargetsArray;

        private void Start()
        {
            targetsArray = new int[LevelManager.NumberTargetsHumanClient];
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
    }
}
