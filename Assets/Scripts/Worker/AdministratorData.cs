using UnityEngine;

namespace Worker
{
    public class AdministratorData : WorkerData
    {
        private GameObject _parentAllDynamicObjects;
        private GameObject _pivot;
        
        public override void Start()
        {
            base.Start();
            _parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");

            Invoke(nameof(FindAndSetPath), 0.3f);
        }

        private void FindAndSetPath()
        {
            _pivot = FindReception();
            NavMeshAgentComponent.SetDestination(_pivot.transform.position);
            Invoke(nameof(CheckDestination), 0.3f);
        }
        
        private void CheckDestination()
        {
            if (NavMeshAgentComponent.remainingDistance < 0.1f)
            {
                transform.position = _pivot.transform.position;
                transform.rotation = _pivot.transform.rotation;
            }
            else
            {
                Invoke(nameof(CheckDestination), 0.3f);
            }
        }
        
        private GameObject FindReception()
        {
            foreach (Transform child in _parentAllDynamicObjects.transform)
            {
                if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == 0)
                {
                    var target = child.Find("PivotForAdministrator").gameObject;
                    return target;
                }
            }

            return null;
        }
    }
}
