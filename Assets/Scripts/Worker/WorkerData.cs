using UnityEngine;
using UnityEngine.AI;

namespace Worker
{
    public abstract class WorkerData : MonoBehaviour
    {
        public NavMeshAgent navMeshAgentComponent;
        public AnimatorControllerWorkers animatorControllerWorkers;
        
        public int Salary { get; set; }

        public virtual void Start()
        {
            navMeshAgentComponent = GetComponent<NavMeshAgent>();
            animatorControllerWorkers = GetComponent<AnimatorControllerWorkers>();
        }
    }
}
