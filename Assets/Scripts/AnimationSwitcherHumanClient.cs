using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSwitcherHumanClient : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    private HumanControls humanControls;

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
    }
    
    void AnimationDoActionStop()
    {
        animator.SetBool("DoAction", false);
    }
    
    void Update()
    {
        if (navMeshAgent.velocity.magnitude > 0) animator.SetBool("IsWalk", true);
        else animator.SetBool("IsWalk", false);
    }
}
