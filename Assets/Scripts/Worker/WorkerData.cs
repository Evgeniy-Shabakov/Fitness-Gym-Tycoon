using UnityEngine;
using UnityEngine.AI;

namespace Worker
{
    public abstract class WorkerData : MonoBehaviour
    {
        [HideInInspector] public NavMeshAgent navMeshAgentComponent;
        [HideInInspector] public AnimatorControllerWorkers animatorControllerWorkers;
        
        public int Salary { get; set; }

        public virtual void Start()
        {
            navMeshAgentComponent = GetComponent<NavMeshAgent>();
            animatorControllerWorkers = GetComponent<AnimatorControllerWorkers>();
        }
    }
}
