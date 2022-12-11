using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationSwitcherHumanClient : MonoBehaviour
{
    private Animator animator;
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponentInChildren<Animator>();
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (navMeshAgent.enabled == false)
        {
            animator.SetBool("DoExercise", true);
            return;
        }
        
        animator.SetBool("DoExercise", false);
        
        if (navMeshAgent.velocity.magnitude > 0) animator.SetBool("IsWalk", true);
        else animator.SetBool("IsWalk", false);
    }
}
