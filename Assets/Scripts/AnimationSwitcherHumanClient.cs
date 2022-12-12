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
    }

    void Update()
    {
        if (navMeshAgent.enabled == false && humanControls.currentGameObjectForAction != null)
        {
            int i = humanControls.currentGameObjectForAction.GetComponent<ObjectData>().indexInBuildingManagerList;
            animator.runtimeAnimatorController =
                BuildingManager.Instance.objectsForBuilding[i].animatorOverrideController;
            
            animator.SetBool("DoExercise", true);
            return;
        }
        animator.SetBool("DoExercise", false);
        
        if (navMeshAgent.velocity.magnitude > 0) animator.SetBool("IsWalk", true);
        else animator.SetBool("IsWalk", false);
    }
}
