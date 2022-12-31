using UnityEngine;
using UnityEngine.AI;

namespace Worker
{
    public abstract class WorkerData : MonoBehaviour
    {
        protected NavMeshAgent NavMeshAgent;
        
        public int Salary { get; set; }

        public virtual void Start()
        {
            NavMeshAgent = GetComponent<NavMeshAgent>();
        }
    }
}
