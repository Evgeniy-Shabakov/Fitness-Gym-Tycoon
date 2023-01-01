using System;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Worker
{
    public class JanitorData : WorkerData
    {
        private GameObject _parentAllTrash;
        private GameObject _currentTrash;
        
        public override void Start()
        {
            base.Start();
            _parentAllTrash = GameObject.Find("TrashContainer");
            
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
            navMeshAgentComponent.SetDestination(target.transform.position);
            
            Invoke(nameof(CheckOnNoWork), 2f);
        }

        private void OnTriggerStay(Collider other)
        {
            if (_currentTrash != null) return;
            if (other.CompareTag("Trash") != true) return;
            
            navMeshAgentComponent.ResetPath();
            _currentTrash = other.gameObject;
            animatorControllerWorkers.SetDoAction(true);
            Invoke(nameof(StopDoActionWithTrash), 2f);
        }

        private void StopDoActionWithTrash()
        {
            Destroy(_currentTrash);
            animatorControllerWorkers.SetDoAction(false);
            Invoke(nameof(MoveTo),1);
        }

        private void CheckOnNoWork()
        {
            if (navMeshAgentComponent.velocity.magnitude == 0 && _currentTrash == null)
            {
                MoveTo();
            }
            else Invoke(nameof(CheckOnNoWork), 2f);
        }

        private GameObject FindTrash()
        {
            if (_parentAllTrash.transform.childCount == 0) return null;
            
            int r = Random.Range(0, _parentAllTrash.transform.childCount);
            return _parentAllTrash.transform.GetChild(r).gameObject;
        }
        
    }
}
