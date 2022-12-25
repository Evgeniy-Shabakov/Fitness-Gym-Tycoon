using System.Collections;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.Events;
using Random = UnityEngine.Random;

namespace HumanClient
{
    public class HumanControls : MonoBehaviour
    {
        private NavMeshAgent _navMeshAgent;
        private HumanReactionControl _humanReactionControl;
        private HumanClientData _humanClientData;
        private GameObject _parentAllDynamicObjects;
    
        public int countTargets;
        public int[] targetsArray;
        public int indexInTargetsArray;

        public bool[] targetsStatus;

        public bool trainingIsFinished;

        private int _mood;

        private int _numberOfAttempts;

        [HideInInspector] public GameObject currentGameObjectForAction;

        [HideInInspector] public UnityEvent humanDoActionStart = new UnityEvent();
        [HideInInspector] public UnityEvent humanDoActionStop = new UnityEvent();
    
        private bool _humanDoAction;
        private GameObject _locker;

        void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _humanReactionControl = GetComponent<HumanReactionControl>();
            _humanClientData = GetComponent<HumanClientData>();
            
            _parentAllDynamicObjects = GameObject.Find("DynamicObjectsForSaveLoad");
        
            countTargets = 15;
            targetsArray = new int[countTargets];
            SetTargetsArray();
            indexInTargetsArray = 0;

            trainingIsFinished = false;
            _mood = Random.Range(LevelManager.MoodRangeMin, LevelManager.MoodRangeMax + 1);
        
            targetsStatus = new bool[countTargets];

            _numberOfAttempts = 0;
            _humanDoAction = false;

            MoveHuman();
        }

        public void OnMouseUpAsButton()
        {
            if (UIManagerMain.Instance.IsPointerOverUIObject()) return;
        
            UIManagerPanelHumanClient.Instance.OpenAndFill(gameObject);
        }
    
        private void MoveHuman()
        {
            if (_humanDoAction) return;
        
            if (indexInTargetsArray == countTargets - 1)
            {
                if (_navMeshAgent.enabled) _navMeshAgent.SetDestination(_locker.transform.position);
                _humanReactionControl.ClearHumanReactionSprite();
                return;
            }
        
            GameObject target = SearchNeededAndFreeObject();
        
            if (target != null)
            {
                _numberOfAttempts = 0;
            
                if (_navMeshAgent.enabled) _navMeshAgent.SetDestination(target.transform.position);
                return;
            }

            if (_numberOfAttempts < 3)
            {
                _numberOfAttempts++;
                Invoke(nameof(MoveHuman), 0.5f);
                return;
            }
        
            if (indexInTargetsArray == 1)
            {
                PlayerData.SpendMoney(LevelManager.Instance.GetPricePerVisit());
                _humanReactionControl.SetMoneyAboveHuman();
                _humanReactionControl.SetTextAboveHuman("-" + LevelManager.Instance.GetPricePerVisit());
            
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
                _numberOfAttempts = 0;
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
        
            if (_navMeshAgent.enabled) _navMeshAgent.ResetPath();

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
                _numberOfAttempts = 0;
                MoveHuman();
            }
        }

        private void OnTriggerStay(Collider other)
        {
            if (indexInTargetsArray >= countTargets) return;
            if (_humanDoAction) return;
            if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != targetsArray[indexInTargetsArray]) return;
            if (other.gameObject.GetComponent<ObjectData>().objectIsFree == false) return;
        
            currentGameObjectForAction = other.gameObject;
            StartCoroutine(DoActionInObject());
        }

        private IEnumerator DoActionInObject()
        {
            humanDoActionStart.Invoke();
            _humanDoAction = true;
            _humanReactionControl.ClearHumanReactionSprite();
        
            Vector3 positionBeforeAction = transform.position;
            Quaternion rotationBeforeAction = transform.rotation;
            float wait = 1f;

            switch (targetsArray[indexInTargetsArray])
            {
                case 0:
                    PlayerData.AddMoney(LevelManager.Instance.GetPricePerVisit());
                    _humanReactionControl.SetMoneyAboveHuman();
                    _humanReactionControl.SetTextAboveHuman("+" + LevelManager.Instance.GetPricePerVisit());
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
                    _navMeshAgent.enabled = false;

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
                    _humanReactionControl.SetTextAboveHuman("");
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
                    _navMeshAgent.enabled = true;
            
                    currentGameObjectForAction.GetComponent<ObjectData>().RemoveClient(gameObject);
            
                    AddMood(LevelManager.CountMoodAdd);
                
                    break;
            }
        
            _humanDoAction = false;
            humanDoActionStop.Invoke();

            targetsStatus[indexInTargetsArray] = true;
        
            NextIndexInTargetsArray();
            if (indexInTargetsArray < countTargets)
            {
                _numberOfAttempts = 0;
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
        
            foreach(Transform child in _parentAllDynamicObjects.transform)
            {
                if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == targetsArray[indexInTargetsArray])
                {
                    if (child.GetComponentInChildren<ObjectData>().objectIsFree)
                    {
                        if (indexInTargetsArray == 1)
                        {
                            if (_humanClientData.GetGender() == HumanClientData.Gender.Male &&
                                GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorMenLockerRoom"))
                            {
                                _humanReactionControl.ClearHumanReactionSprite();
                                return child.gameObject;
                            }
                            
                            if (_humanClientData.GetGender() == HumanClientData.Gender.Female &&
                                GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorWomenLockerRoom"))
                            {
                                _humanReactionControl.ClearHumanReactionSprite();
                                return child.gameObject;
                            }
                        }
                        else
                        {
                            _humanReactionControl.ClearHumanReactionSprite();
                            return child.gameObject;
                        }
                    }
                }
            }

            _humanReactionControl.SetNoFindObjectSprite();
            return null;
        }
        
        private LayerMask GetLayerUnderObject(GameObject current)
        {
            var ray = new Ray(current.transform.position + new Vector3(0, 5, 0), -Vector3.up);
            LayerMask layer = LayerMask.GetMask("FloorMenLockerRoom", "FloorWomenLockerRoom");
            
            Physics.Raycast(ray, out var hitInfo, 10f, layer);

            return hitInfo.transform.gameObject.layer;
        }

        private void AddMood(int countMood)
        {
            _mood += countMood;
            if (_mood > 100) _mood = 100;
        }

        private void TakeAwayMood(int countMood)
        {
            _mood -= countMood;
            if (_mood < 5) _mood = 5;
        }

        public int GetMood()
        {
            return _mood;
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
            if (_navMeshAgent.enabled) _navMeshAgent.SetDestination(Vector3.zero);
            Invoke(nameof(DestroyHuman), 15f);
            _humanReactionControl.SetSmileAboveHuman();
            _humanReactionControl.SetTextAboveHuman("");
        
            if (_mood <= LevelManager.MoodSad) LevelManager.Instance.TakeAwayRating(1);
            else if (_mood > LevelManager.MoodHappy) LevelManager.Instance.AddRating(1);
        
            trainingIsFinished = true;
        }
    
        private void DestroyHuman()
        {
            Destroy(gameObject);
        
            if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
            UIManagerPanelHumanClient.Instance.Close();
        }
    }
}
