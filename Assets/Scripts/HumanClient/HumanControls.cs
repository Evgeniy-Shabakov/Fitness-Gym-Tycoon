using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;
using UnityEngine.Events;

public class HumanControls : MonoBehaviour
{
    private NavMeshAgent navMeshAgent;
    private HumanReactionControl humanReactionControl;
    private GameObject parentAllDynamicObjects;
    
    public int countTargets;
    public int[] targetsArray;
    public int indexInTargetsArray;

    public bool[] targetsStatus;

    public bool trainingIsFinished;

    private int mood;

    private int numberOfAttempts;

    [HideInInspector] public GameObject currentGameObjectForAction;

    [HideInInspector] public UnityEvent humanDoActionStart = new UnityEvent();
    [HideInInspector] public UnityEvent humanDoActionStop = new UnityEvent();
    
    private bool humanDoAction;
    private GameObject _locker;

    void Start()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
        humanReactionControl = GetComponent<HumanReactionControl>();
        parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");
        
        countTargets = 15;
        targetsArray = new int[countTargets];
        SetTargetsArray();
        indexInTargetsArray = 0;

        trainingIsFinished = false;
        mood = Random.Range(LevelManager.MoodRangeMin, LevelManager.MoodRangeMax + 1);
        
        targetsStatus = new bool[countTargets];

        numberOfAttempts = 0;
        humanDoAction = false;

        MoveHuman();
    }

    public void OnMouseUpAsButton()
    {
        if (UIManagerMain.Instance.IsPointerOverUIObject()) return;
        
        UIManagerPanelHumanClient.Instance.OpenAndFill(gameObject);
    }
    
    private void MoveHuman()
    {
        if (humanDoAction) return;
        
        if (indexInTargetsArray == countTargets - 1)
        {
            if (navMeshAgent.enabled) navMeshAgent.SetDestination(_locker.transform.position);
            humanReactionControl.ClearHumanReactionSprite();
            return;
        }
        
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
        
        if (indexInTargetsArray == 1)
        {
            PlayerData.SpendMoney(LevelManager.Instance.GetPricePerVisit());
            humanReactionControl.SetMoneyAboveHuman();
            humanReactionControl.SetTextAboveHuman("-" + LevelManager.Instance.GetPricePerVisit());
            
            TakeAwayMood(100);
            Invoke(nameof(SendHumanHome), 1f);
            if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
            UIManagerPanelHumanClient.Instance.UpdateData();
            return;
        }
        
        TakeAwayMood(LevelManager.CountMoodTakeAway);
        NextIndexInTargetsArray();
        
        if (indexInTargetsArray < countTargets)
        {
            numberOfAttempts = 0;
            MoveHuman();
        }

        else
        {
            SendHumanHome();
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
        
        else if (indexInTargetsArray == countTargets - 1 && other.gameObject == _locker)
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
        humanReactionControl.ClearHumanReactionSprite();
        
        Vector3 positionBeforeAction = transform.position;
        Quaternion rotationBeforeAction = transform.rotation;
        float wait = 1f;

        switch (targetsArray[indexInTargetsArray])
        {
            case 0:
                PlayerData.AddMoney(LevelManager.Instance.GetPricePerVisit());
                humanReactionControl.SetMoneyAboveHuman();
                humanReactionControl.SetTextAboveHuman("+" + LevelManager.Instance.GetPricePerVisit());
                break;
            case 1:
                if (indexInTargetsArray == 1)
                {
                    _locker = currentGameObjectForAction;
                    currentGameObjectForAction.GetComponent<ObjectData>().AddClient(gameObject);
                    LevelManager.Instance.AddCountMen();
                }
                break;
            default:
                navMeshAgent.enabled = false;

                transform.position = currentGameObjectForAction.transform.parent.Find("PivotForHuman").position;
                transform.rotation = currentGameObjectForAction.transform.parent.Find("PivotForHuman").rotation;
            
                currentGameObjectForAction.GetComponent<ObjectData>().AddClient(gameObject);
                wait = Random.Range(LevelManager.MinTimeExercise, LevelManager.MaxTimeExercise);
                
                break;
        }

        yield return new WaitForSeconds(wait);

        switch (targetsArray[indexInTargetsArray])
        {
            case 0:
                humanReactionControl.SetTextAboveHuman("");
                break;
            case 1:
                if (indexInTargetsArray == countTargets - 1)
                {
                    currentGameObjectForAction.GetComponent<ObjectData>().RemoveClient(gameObject);
                    LevelManager.Instance.TakeAwayCountMen();
                }
                break;
            default:
                transform.rotation = rotationBeforeAction;
                transform.position = positionBeforeAction;
                navMeshAgent.enabled = true;
            
                currentGameObjectForAction.GetComponent<ObjectData>().RemoveClient(gameObject);
            
                AddMood(LevelManager.CountMoodAdd);
                
                break;
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
            SendHumanHome();
        }
    }

    private GameObject SearchNeededAndFreeObject()
    {
        if (indexInTargetsArray >= countTargets) return null;
        
        foreach(Transform child in parentAllDynamicObjects.transform)
        {
            if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsArray[indexInTargetsArray])
            {
                if (child.GetComponentInChildren<ObjectData>().objectIsFree)
                {
                    humanReactionControl.ClearHumanReactionSprite();
                    return child.gameObject;
                }
            }
        }

        humanReactionControl.SetNoFindObjectSprite();
        return null;
    }

    private void AddMood(int countMood)
    {
        mood += countMood;
        if (mood > 100) mood = 100;
    }

    private void TakeAwayMood(int countMood)
    {
        mood -= countMood;
        if (mood < 5) mood = 5;
    }

    public int GetMood()
    {
        return mood;
    }
    
    private void NextIndexInTargetsArray()
    {
        indexInTargetsArray++;
        
        if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
        UIManagerPanelHumanClient.Instance.UpdateData();
    }

    private void SetTargetsArray()
    {
        targetsArray[0] = 0;
        targetsArray[1] = 1;
        
        for (int i = 2; i < countTargets - 1; i++)
        {
            targetsArray[i] = Random.Range(2, BuildingManager.Instance.objectsForBuilding.Count);
            
            while(targetsArray[i] == targetsArray[i-1])
            {
                targetsArray[i] = Random.Range(2, BuildingManager.Instance.objectsForBuilding.Count);
            }
        }

        targetsArray[countTargets - 1] = 1;
    }

    private void SendHumanHome()
    {
        if (navMeshAgent.enabled) navMeshAgent.SetDestination(Vector3.zero);
        Invoke("DestroyHuman", 15f);
        humanReactionControl.SetSmileAboveHuman();
        humanReactionControl.SetTextAboveHuman("");
        
        if (mood <= LevelManager.MoodSad) LevelManager.Instance.TakeAwayRating(1);
        else if (mood > LevelManager.MoodHappy) LevelManager.Instance.AddRating(1);
        
        trainingIsFinished = true;
    }
    
    private void DestroyHuman()
    {
        Destroy(gameObject);
        
        if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
        UIManagerPanelHumanClient.Instance.Close();
    }
}
