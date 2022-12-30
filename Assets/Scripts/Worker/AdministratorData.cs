using UnityEngine;

namespace Worker
{
    public class AdministratorData : WorkerData
    {
        private GameObject _parentAllDynamicObjects;
        
        public override void Start()
        {
            base.Start();
            _parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");
            
            NavMeshAgent.SetDestination(FindReception());
        }

        private Vector3 FindReception()
        {
            Vector3 target = new Vector3();

            foreach (Transform child in _parentAllDynamicObjects.transform)
            {
                if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == 0)
                {
                    target = child.Find("PivotForAdministrator").position;
                    break;
                }
            }

            return target;
        }
    }
}
