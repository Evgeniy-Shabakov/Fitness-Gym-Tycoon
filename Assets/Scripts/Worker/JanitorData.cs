using System;
using UnityEngine;

namespace Worker
{
    public class JanitorData : WorkerData
    {
        [SerializeField] private GameObject _parentAllTrash;
        
        public override void Start()
        {
            base.Start();
            _parentAllTrash = GameObject.Find("TrashParent");
            
            Invoke(nameof(MoveTo),2f);
        }

        private void MoveTo()
        {
            if (_currentTrash != null) return;
            
            GameObject target = FindTrash();
            if (target == null)
            {
                Invoke(nameof(MoveTo),2f);
                return;
            }
            NavMeshAgentComponent.SetDestination(target.transform.position);
        }

        private GameObject _currentTrash;
        private void OnTriggerStay(Collider other)
        {
            if (other.CompareTag("Trash"))
            {
                if (_currentTrash != null) return;
                
                NavMeshAgentComponent.ResetPath();
                _currentTrash = other.gameObject;
                AnimatorControllerWorkers.SetDoAction(true);
                Invoke(nameof(StopDoActionWithTrash), 2f);
            }
        }

        private void StopDoActionWithTrash()
        {
            Destroy(_currentTrash);
            AnimatorControllerWorkers.SetDoAction(false);
            Invoke(nameof(MoveTo),1);
        }

        private GameObject FindTrash()
        {
            if (_parentAllTrash.transform.childCount == 0) return null;
            return _parentAllTrash.transform.GetChild(0).gameObject;
        }
        
    }
}
