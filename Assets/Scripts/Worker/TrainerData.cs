using BuildingSystem;
using UnityEngine;

namespace Worker
{
    public class TrainerData : WorkerData
    {
        [SerializeField] private float waitBetweenTargets;
        private GameObject _parentAllDynamicObjects;
        
        public override void Start()
        {
            base.Start();
            _parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");

            Invoke(nameof(MoveToNextPoint), 2f);
        }

        private void MoveToNextPoint()
        {
            navMeshAgentComponent.SetDestination(FindTargetPosition());
            Invoke(nameof(DetectDestination), 2f);
        }

        private void DetectDestination()
        {
            if (navMeshAgentComponent.velocity.magnitude == 0)
            {
                Invoke(nameof(MoveToNextPoint), 2f);
            }

            else
            {
                Invoke(nameof(DetectDestination), 2f);
            }
        }

        private Vector3 FindTargetPosition()
        {
            int i;
            ObjectType type;
            
            do
            {
                i = Random.Range(0, _parentAllDynamicObjects.transform.childCount);
                type = _parentAllDynamicObjects.transform.GetChild(i).GetComponentInChildren<ObjectData>().type;
            } 
            while (BuildingManager.Instance.FindObject(type).layerFloor != LayerMask.GetMask("FloorGym"));

            return _parentAllDynamicObjects.transform.GetChild(i).position;
        }
    }
}
