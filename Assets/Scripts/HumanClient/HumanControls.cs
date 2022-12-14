using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class HumanControls : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private GameObject parentAllDynamicObjects;
    
    public int countTargets;
    public int[] targetsIndexes;
    public int index;

    private int numberOfAttempts;

    [HideInInspector] public GameObject currentGameObjectForAction;

    [HideInInspector] public UnityEvent humanDoActionStart = new UnityEvent();
    [HideInInspector] public UnityEvent humanDoActionStop = new UnityEvent();
    [HideInInspector] public UnityEvent NeededAndFreeObjectNoFinded = new UnityEvent();
    [HideInInspector] public UnityEvent NeededAndFreeObjectFinded = new UnityEvent();
    
    private bool humanDoAction;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");
        
        countTargets = 10;
        targetsIndexes = new int[countTargets];
        SetTargetsIndexes();
        index = 0;
        
        numberOfAttempts = 0;
        humanDoAction = false;

        MoveHuman();
    }

    private void MoveHuman()
    {
        if (humanDoAction) return;
        
        GameObject target = SearchNeededAndFreeObject();
        
        if (target != null)
        {
            numberOfAttempts = 0;
            
            if (navMeshAgent.enabled) navMeshAgent.SetDestination(target.transform.position);
            return;
        }

        if (numberOfAttempts < 3)
        {
            numberOfAttempts++;
            Invoke("MoveHuman", 0.5f);
            return;
        }
        
        index++;
        if (index < countTargets)
        {
            numberOfAttempts = 0;
            MoveHuman();
        }
        
        else
        {
            if (navMeshAgent.enabled) navMeshAgent.SetDestination(Vector3.zero);
            Invoke("DestroyHuman", 15f);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (index >= countTargets) return;
        if (other.gameObject.GetComponent<ObjectData>() == null) return;
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != targetsIndexes[index]) return;    
        
        if (navMeshAgent.enabled) navMeshAgent.ResetPath();

        if (other.gameObject.GetComponent<ObjectData>().objectIsFree)
        {
            currentGameObjectForAction = other.gameObject;
            StartCoroutine(DoActionInObject());
        }
        else
        {
            numberOfAttempts = 0;
            MoveHuman();
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (index >= countTargets) return;
        if (humanDoAction) return;
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != targetsIndexes[index]) return;
        if (other.gameObject.GetComponent<ObjectData>().objectIsFree == false) return;
        
        currentGameObjectForAction = other.gameObject;
        StartCoroutine(DoActionInObject());
    }

    IEnumerator DoActionInObject()
    {
        humanDoActionStart.Invoke();
        humanDoAction = true;
        
        Vector3 positionBeforeAction = transform.position;
        float wait = 1f;
        
        if (targetsIndexes[index] != 0)
        {
            navMeshAgent.enabled = false;

            transform.position = currentGameObjectForAction.transform.parent.Find("PivotForHuman").position;
            transform.rotation = currentGameObjectForAction.transform.parent.Find("PivotForHuman").rotation;
            
            currentGameObjectForAction.GetComponent<ObjectData>().objectIsFree = false;
            wait = Random.Range(3f, 5f);
        }
        
        yield return new WaitForSeconds(wait);
        
        if (targetsIndexes[index] != 0)
        {
            transform.position = positionBeforeAction;
            navMeshAgent.enabled = true;
            
            currentGameObjectForAction.GetComponent<ObjectData>().objectIsFree = true;
        }
        
        humanDoAction = false;
        humanDoActionStop.Invoke();
        
        index++;
        if (index < countTargets)
        {
            numberOfAttempts = 0;
            MoveHuman();
        }
        else
        {
            navMeshAgent.SetDestination(Vector3.zero);
            Invoke("DestroyHuman", 15f);
        }
    }

    private GameObject SearchNeededAndFreeObject()
    {
        foreach(Transform child in parentAllDynamicObjects.transform)
        {
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsIndexes[index])
            {
                if (child.GetComponentInChildren<ObjectData>().objectIsFree)
                {
                    NeededAndFreeObjectFinded.Invoke();
                    return child.gameObject;
                }
            }
        }
        
        NeededAndFreeObjectNoFinded.Invoke();
        return null;
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
