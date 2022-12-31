using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

namespace Worker
{
    public abstract class WorkerData : MonoBehaviour
    {
        protected NavMeshAgent NavMeshAgentComponent;
        protected AnimatorControllerWorkers AnimatorControllerWorkers;
        
        public int Salary { get; set; }

        public virtual void Start()
        {
            NavMeshAgentComponent = GetComponent<NavMeshAgent>();
            AnimatorControllerWorkers = GetComponent<AnimatorControllerWorkers>();
        }
    }
}
