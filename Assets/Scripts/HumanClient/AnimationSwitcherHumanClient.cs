using BuildingSystem;
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
        private GameObject _dumbbellOneInHands;
        private GameObject _dumbbellTwoInHands;
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
            ObjectType type = _humanControls.currentGameObjectForAction.GetComponent<ObjectData>().type;
            ObjectForBuilding objectForBuilding = BuildingManager.Instance.FindObject(type);
                
            _animator.runtimeAnimatorController = objectForBuilding.animatorOverrideController;
            
            _animator.SetBool(DoAction, true);

            if (objectForBuilding.needBarbellInHands)
            {
                _barbellInHands = FindNestedChild(transform, "Barbell 1").gameObject; 
                _barbellInHands.SetActive(true);
            
                _humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().HideElements();
            }
            
            if (objectForBuilding.needDumbbellsInHands)
            {
                _dumbbellOneInHands = FindNestedChild(transform, "Dumbbells 1-1").gameObject; 
                _dumbbellOneInHands.SetActive(true);
                
                _dumbbellTwoInHands = FindNestedChild(transform, "Dumbbells 1-2").gameObject; 
                _dumbbellTwoInHands.SetActive(true);
            
                _humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().HideElements();
            }
        }
    
        void AnimationDoActionStop()
        {
            if (_barbellInHands != null)
            {
                _barbellInHands.SetActive(false);
                _humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().ShowElements();
                
                _barbellInHands = null;
            }

            if (_dumbbellOneInHands != null)
            {
                _dumbbellOneInHands.SetActive(false);
                _dumbbellTwoInHands.SetActive(false);
                
                _dumbbellOneInHands = null;
                _dumbbellTwoInHands = null;
            }
            
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
