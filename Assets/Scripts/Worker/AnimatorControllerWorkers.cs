using UnityEngine;
using UnityEngine.AI;

namespace Worker
{
    public class AnimatorControllerWorkers : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        
        private static readonly int DoAction = Animator.StringToHash("DoAction");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
        }
        
        void Update()
        {
            if (_navMeshAgent.velocity.magnitude > 0) _animator.SetBool(IsWalk, true);
            else _animator.SetBool(IsWalk, false);
        }
    }
}
