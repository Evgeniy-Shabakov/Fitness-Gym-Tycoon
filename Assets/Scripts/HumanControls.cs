using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class HumanControls : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;
    
    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = new Ray(Camera.main.ScreenToWorldPoint(Input.mousePosition), Camera.main.transform.forward);
            RaycastHit hit;
            
            if (Physics.Raycast(ray, out hit, 100f, BuildingManager.Instance.layerMaskForPlane))
            {
                navMeshAgent.SetDestination(hit.point);
            }
            
        }
    }
}
