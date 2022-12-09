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

    private GameObject currentCollisionGameObject;
    
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
        foreach(Transform child in parentAllDynamicObjects.transform)
        {
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
            {
                if (child.GetComponentInChildren<ObjectData>().objectIsFree)
                {
                    navMeshAgent.SetDestination(child.position);
                    return;
                }
            }
        }

        if (index < countTargets - 1)
        {
            index++;
            MoveHuman();
        }
        
        else
        {
            navMeshAgent.SetDestination(Vector3.zero);
            Invoke("DestroyHuman", 15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (index >= countTargets) return;
        if (other.gameObject.GetComponent<ObjectData>() == null) return;
            
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
        {
            navMeshAgent.ResetPath();

            if (other.gameObject.GetComponent<ObjectData>().objectIsFree == false)
            {
                MoveHuman();
            }
            else
            {
                currentCollisionGameObject = other.gameObject;
                StartCoroutine(DoActionInObject());
            }
            
        }
    }

    IEnumerator DoActionInObject()
    {
        Vector3 positionBeforeAction = transform.position;
        float wait = 1f;
        
        if (targetsIndexes[index] != 0 && targetsIndexes[index] != 6)
        {
            navMeshAgent.enabled = false;
            transform.position = currentCollisionGameObject.transform.position;

            currentCollisionGameObject.GetComponent<ObjectData>().objectIsFree = false;
            wait = 3f;
        }
        
        yield return new WaitForSeconds(wait);
        
        if (targetsIndexes[index] != 0 && targetsIndexes[index] != 6)
        {
            transform.position = positionBeforeAction;
            navMeshAgent.enabled = true;
            
            currentCollisionGameObject.GetComponent<ObjectData>().objectIsFree = true;
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
        targetsIndexes[1] = 1;
        
        for (int i = 2; i < countTargets; i++)
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
