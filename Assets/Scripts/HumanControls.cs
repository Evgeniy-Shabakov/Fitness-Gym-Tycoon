using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

public class HumanControls : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;

    private GameObject parentAllDynamicObjects;
    
    private int countTargets;
    private int[] targetsIndexes;
    private int index;

    private GameObject currentCollision;
    
    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");

        countTargets = 10;
        targetsIndexes = new int[countTargets];

        SetTargetsIndexes();

        MoveHuman();
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
        if (other.gameObject.GetComponent<ObjectData>() == null) return;
            
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
        {
            navMeshAgent.ResetPath();
            currentCollision = other.gameObject;
            StartCoroutine(DoActionInObject());
        }
    }

    IEnumerator DoActionInObject()
    {
        Vector3 positionBeforeAction = transform.position;

        if (targetsIndexes[index] != 0 && targetsIndexes[index] != 6)
        {
            navMeshAgent.enabled = false;
            transform.position = currentCollision.transform.position;
        }
        
        
        yield return new WaitForSeconds(1f);

        if (targetsIndexes[index] != 0 && targetsIndexes[index] != 6)
        {
            transform.position = positionBeforeAction;
            navMeshAgent.enabled = true;
        }
        
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
    }

    private void SetTargetsIndexes()
    {
        targetsIndexes[0] = 0;
        
        for (int i = 1; i < countTargets; i++)
        {
            targetsIndexes[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            
            while(targetsIndexes[i] == targetsIndexes[i-1])
            {
                targetsIndexes[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            }
        }
    }
    
    private void DestroyHuman()
    {
        Destroy(gameObject);
    }
}
