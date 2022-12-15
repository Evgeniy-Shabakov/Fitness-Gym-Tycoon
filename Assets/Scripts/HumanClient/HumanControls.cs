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
    public int[] targetsArray;
    public int indexInTargetsArray;

    public bool[] targetsStatus;

    private int numberOfAttempts;

    [HideInInspector] public GameObject currentGameObjectForAction;

    [HideInInspector] public UnityEvent humanDoActionStart = new UnityEvent();
    [HideInInspector] public UnityEvent humanDoActionStop = new UnityEvent();
    [HideInInspector] public UnityEvent NeededAndFreeObjectNoFinded = new UnityEvent();
    [HideInInspector] public UnityEvent NeededAndFreeObjectFinded = new UnityEvent();
    [HideInInspector] public UnityEvent IndexInTargetsArrayChanged = new UnityEvent();
    
    private bool humanDoAction;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");
        
        countTargets = 10;
        targetsArray = new int[countTargets];
        SetTargetsArray();
        indexInTargetsArray = 0;

        targetsStatus = new bool[countTargets];

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
        
        NextIndexInTargetsArray();
        if (indexInTargetsArray < countTargets)
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
        if (indexInTargetsArray >= countTargets) return;
        if (other.gameObject.GetComponent<ObjectData>() == null) return;
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != targetsArray[indexInTargetsArray]) return;    
        
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
        if (indexInTargetsArray >= countTargets) return;
        if (humanDoAction) return;
        if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != targetsArray[indexInTargetsArray]) return;
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
        
        if (targetsArray[indexInTargetsArray] != 0)
        {
            navMeshAgent.enabled = false;

            transform.position = currentGameObjectForAction.transform.parent.Find("PivotForHuman").position;
            transform.rotation = currentGameObjectForAction.transform.parent.Find("PivotForHuman").rotation;
            
            currentGameObjectForAction.GetComponent<ObjectData>().objectIsFree = false;
            wait = Random.Range(3f, 5f);
        }
        
        yield return new WaitForSeconds(wait);
        
        if (targetsArray[indexInTargetsArray] != 0)
        {
            transform.position = positionBeforeAction;
            navMeshAgent.enabled = true;
            
            currentGameObjectForAction.GetComponent<ObjectData>().objectIsFree = true;
        }
        
        humanDoAction = false;
        humanDoActionStop.Invoke();

        targetsStatus[indexInTargetsArray] = true;
        
        NextIndexInTargetsArray();
        if (indexInTargetsArray < countTargets)
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
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsArray[indexInTargetsArray])
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

    private void NextIndexInTargetsArray()
    {
        indexInTargetsArray++;
        IndexInTargetsArrayChanged.Invoke();
    }
    
    private void SetTargetsArray()
    {
        targetsArray[0] = 0;
        
        for (int i = 1; i < countTargets; i++)
        {
            targetsArray[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            
            while(targetsArray[i] == targetsArray[i-1])
            {
                targetsArray[i] = Random.Range(1, BuildingManager.Instance.objectsForBuilding.Count);
            }
        }
    }
    
    private void DestroyHuman()
    {
        Destroy(gameObject);
    }
}
