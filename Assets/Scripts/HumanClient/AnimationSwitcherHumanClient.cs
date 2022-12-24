using System.Collections;
using System.Collections.Generic;
using HumanClient;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSwitcherHumanClient : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private HumanControls humanControls;

    private GameObject barbellInHands;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
        humanControls = GetComponent<HumanControls>();
        
        humanControls.humanDoActionStart.AddListener(AnimationDoActionStart);
        humanControls.humanDoActionStop.AddListener(AnimationDoActionStop);
    }

    void AnimationDoActionStart()
    {
        int i = humanControls.currentGameObjectForAction.GetComponent<ObjectData>().indexInBuildingManagerList;
        animator.runtimeAnimatorController =
            BuildingManager.Instance.objectsForBuilding[i].animatorOverrideController;
        
        animator.SetBool("DoAction", true);

        if (BuildingManager.Instance.objectsForBuilding[i].needBarbellInHands)
        {
            barbellInHands = FindNestedChild(transform, "Barbell 1").gameObject; 
            barbellInHands.SetActive(true);
            
            humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().HideElements();
        }
    }
    
    void AnimationDoActionStop()
    {
        if (barbellInHands != null)
        {
            barbellInHands.SetActive(false);
            humanControls.currentGameObjectForAction.GetComponent<HidingObjectElements>().ShowElements();
        }
        barbellInHands = null;
        animator.SetBool("DoAction", false);
    }
    
    void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0) animator.SetBool("IsWalk", true);
        else animator.SetBool("IsWalk", false);
    }
    
    private Transform FindNestedChild(Transform me, string childName)
    {
        for (int i = 0; i < me.childCount; ++i)
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
