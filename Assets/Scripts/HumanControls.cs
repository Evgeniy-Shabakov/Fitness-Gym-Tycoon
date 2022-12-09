using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class HumanControls : MonoBehaviour
{
    private Camera mainCamera;
    private NavMeshAgent navMeshAgent;

    private GameObject parentAllDynamicObjects;
    
    private int countTargets;
    private int[] targetsIndexes;
    private int index;
    
    void Start()
    {
        mainCamera = Camera.main;
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");

        countTargets = 10;
        targetsIndexes = new int[countTargets];
        
        targetsIndexes[0] = 0;
        
        for (int i = 1; i < countTargets; i++)
        {
            targetsIndexes[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            
            while(targetsIndexes[i] == targetsIndexes[i-1])
            {
                targetsIndexes[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            }
        }
        
        Invoke("MoveHuman", 2f);
    }

    private void MoveHuman()
    {
        Vector3 targetPosition = transform.position;
        
        foreach(Transform child in parentAllDynamicObjects.transform)
        {
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
            {
                targetPosition = child.position;
                break;
            }
        }
        
        navMeshAgent.SetDestination(targetPosition);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (index >= countTargets) return;
        
        Debug.Log("Collision " + other.transform.parent.name);
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
        {
            navMeshAgent.ResetPath();
            StartCoroutine(DoActionInObject());
        }
    }

    IEnumerator DoActionInObject()
    {
        yield return new WaitForSeconds(0f);
        Debug.Log("Do Action " + targetsIndexes[index]);
        index++;
        if (index < countTargets)
        {
            MoveHuman();
        }
        else
        {
            navMeshAgent.SetDestination(Vector3.zero);
            Invoke("DestroyHuman", 15f);
        }

        Debug.Log("Next target " + targetsIndexes[index]);
    }
    
    private void DestroyHuman()
    {
        Destroy(gameObject);
    }
}
