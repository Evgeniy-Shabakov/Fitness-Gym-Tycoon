using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

public class HumanControls : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;

    private GameObject parentAllDynamicObjects;
    
    private int countTargets;
    private int[] targetsIndexes;
    
    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");

        countTargets = 10;
        targetsIndexes = new int[countTargets];
        
        Invoke("MoveHuman", 2f);
    }

    private void MoveHuman()
    {
        targetsIndexes[0] = 0;
        
        for (int i = 1; i < countTargets; i++)
        {
            targetsIndexes[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
        }

        StartCoroutine("DelayMovingHuman");
    }

    IEnumerator DelayMovingHuman()
    {
        for (int i = 0; i < countTargets; i++)
        {
            Vector3 targetPosition = transform.position;
        
            foreach(Transform child in parentAllDynamicObjects.transform)
            {
                if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsIndexes[i])
                {
                    if (i == 0) targetPosition = child.position - Vector3.forward;
                    else targetPosition = child.position;
                    break;
                }
            }
        
            navMeshAgent.SetDestination(targetPosition);
            
            yield return new WaitForSeconds(15f);
        }
        
        navMeshAgent.SetDestination(Vector3.zero);
    }
    
    // Update is called once per frame
    /*void Update()
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
    }*/
}
