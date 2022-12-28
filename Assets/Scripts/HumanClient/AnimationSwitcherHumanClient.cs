using UnityEngine;
using UnityEngine.AI;

namespace HumanClient
{
    public class AnimationSwitcherHumanClient : MonoBehaviour
    {
        private Animator _animator;
        private NavMeshAgent _navMeshAgent;
        private HumanControls _humanControls;

        private GameObject _barbellInHands;
        private static readonly int DoAction = Animator.StringToHash("DoAction");
        private static readonly int IsWalk = Animator.StringToHash("IsWalk");

        void Start()
        {
            _animator = GetComponentInChildren<Animator>();
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _humanControls = GetComponent<HumanControls>();
        
            _humanControls.humanDoActionStart.AddListener(AnimationDoActionStart);
            _humanControls.humanDoActionStop.AddListener(AnimationDoActionStop);
        }

        void AnimationDoActionStart()
        {
            int i = _humanControls.currentGameObjectForAction.GetComponent<ObjectData>().indexInBuildingManagerList;
            _animator.runtimeAnimatorController =
                BuildingManager.Instance.objectsForBuilding[i].animatorOverrideController;
        
            _animator.SetBool(DoAction, true);

            if (BuildingManager.Instance.objectsForBuilding[i].needBarbellInHands)
            {
                _barbellInHands = FindNestedChild(transform, "Barbell 1").gameObject; 
                _barbellInHands.SetActive(true);
            
                _humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().HideElements();
            }
        }
    
        void AnimationDoActionStop()
        {
            if (_barbellInHands != null)
            {
                _barbellInHands.SetActive(false);
                _humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().ShowElements();
            }
            _barbellInHands = null;
            _animator.SetBool(DoAction, false);
        }
    
        void Update()
        {
            if (_navMeshAgent.velocity.magnitude > 0) _animator.SetBool(IsWalk, true);
            else _animator.SetBool(IsWalk, false);
        }
    
        private Transform FindNestedChild(Transform me, string childName)
        {
            for (var i = 0; i < me.childCount; ++i)
            {
                var child = me.GetChild(i);
 
                if (child.name == childName)
                    return child;
 
                var next = FindNestedChild(child, childName);
 
                if (next != null)
                    return next;
            }
 
            return null;
        }
    }
}
