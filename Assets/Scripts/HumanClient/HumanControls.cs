using System.Collections;
using System.IO;
using BuildingSystem;
using UI;
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
        
            if (_humanClientData.indexInTargetsArray == LevelManager.NumberTargetsHumanClient - 1)
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
        
            if (_humanClientData.indexInTargetsArray == 1)
            {
                PlayerData.SpendMoney(_humanClientData.GetPriceEntry());
                LevelAccounting.Instance.ChangeTotalProfitPerSubscription(-_humanClientData.GetPriceEntry(), _humanClientData.GetSubscriptionType());
                _humanReactionControl.SetMoneyAboveHuman();
                _humanReactionControl.SetTextAboveHuman("-" + _humanClientData.GetPriceEntry());
            
                _humanClientData.TakeAwayMood(100);
                Invoke(nameof(SendHumanHome), 1f);
                if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
                UIManagerPanelHumanClient.Instance.UpdateData();
                return;
            }
        
            _humanClientData.TakeAwayMood(LevelManager.CountMoodTakeAway);
            NextIndexInTargetsArray();
        
            if (_humanClientData.indexInTargetsArray < LevelManager.NumberTargetsHumanClient)
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
            if (_humanClientData.indexInTargetsArray >= LevelManager.NumberTargetsHumanClient) return;
            if (other.gameObject.GetComponent<ObjectData>() == null) return;
            if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != _humanClientData.targetsArray[_humanClientData.indexInTargetsArray]) return;    
        
            if (_navMeshAgent.enabled) _navMeshAgent.ResetPath();

            if (other.gameObject.GetComponent<ObjectData>().objectIsFree)
            {
                currentGameObjectForAction = other.gameObject;
                StartCoroutine(DoActionInObject());
            }
        
            else if (_humanClientData.indexInTargetsArray == LevelManager.NumberTargetsHumanClient - 1 && other.gameObject == _locker)
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
            if (_humanClientData.indexInTargetsArray >= LevelManager.NumberTargetsHumanClient) return;
            if (_humanDoAction) return;
            if (other.gameObject.GetComponent<ObjectData>() == false) return;
            if (other.gameObject.GetComponent<ObjectData>().indexInBuildingManagerList != _humanClientData.targetsArray[_humanClientData.indexInTargetsArray]) return;
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

            switch (_humanClientData.targetsArray[_humanClientData.indexInTargetsArray])
            {
                case 0:
                    _humanClientData.SetPriceEntry();
                    PlayerData.AddMoney(_humanClientData.GetPriceEntry());
                    LevelAccounting.Instance.ChangeTotalProfitPerSubscription(_humanClientData.GetPriceEntry(), _humanClientData.GetSubscriptionType());
                    _humanReactionControl.SetMoneyAboveHuman();
                    _humanReactionControl.SetTextAboveHuman("+" + _humanClientData.GetPriceEntry());
                    break;
                case 1:
                    if (_humanClientData.indexInTargetsArray == 1)
                    {
                        _locker = currentGameObjectForAction;
                        currentGameObjectForAction.GetComponent<ObjectData>().AddClient(gameObject);
                        
                        if(_humanClientData.GetGender() == Gender.Male)
                        {
                            LevelManager.Instance.AddNumberMen();
                        }
                        if(_humanClientData.GetGender() == Gender.Female)
                        {
                            LevelManager.Instance.AddNumberWomen();
                        }
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

            switch (_humanClientData.targetsArray[_humanClientData.indexInTargetsArray])
            {
                case 0:
                    _humanReactionControl.SetTextAboveHuman("");
                    break;
                case 1:
                    if (_humanClientData.indexInTargetsArray == LevelManager.NumberTargetsHumanClient - 1)
                    {
                        currentGameObjectForAction.GetComponent<ObjectData>().RemoveClient(gameObject);
                        
                        if(_humanClientData.GetGender() == Gender.Male)
                        {
                            LevelManager.Instance.TakeAwayNumberMen();
                        }
                        if(_humanClientData.GetGender() == Gender.Female)
                        {
                            LevelManager.Instance.TakeAwayNumberWomen();
                        }
                    }
                    break;
                default:
                    transform.rotation = rotationBeforeAction;
                    transform.position = positionBeforeAction;
                    _navMeshAgent.enabled = true;
            
                    currentGameObjectForAction.GetComponent<ObjectData>().RemoveClient(gameObject);
            
                    _humanClientData.AddMood(LevelManager.CountMoodAdd);
                
                    break;
            }
        
            _humanDoAction = false;
            humanDoActionStop.Invoke();

            _humanClientData.targetsStatus[_humanClientData.indexInTargetsArray] = true;
        
            NextIndexInTargetsArray();
            if (_humanClientData.indexInTargetsArray < LevelManager.NumberTargetsHumanClient)
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
            if (_humanClientData.indexInTargetsArray >= LevelManager.NumberTargetsHumanClient) return null;
        
            foreach(Transform child in _parentAllDynamicObjects.transform)
            {
                if (child.GetComponentInChildren<ObjectData>().indexInBuildingManagerList == _humanClientData.targetsArray[_humanClientData.indexInTargetsArray])
                {
                    if (child.GetComponentInChildren<ObjectData>().objectIsFree)
                    {
                        if (_humanClientData.indexInTargetsArray is 1 or 13)
                        {
                            if (_humanClientData.GetGender() == Gender.Male &&
                                LayerDetected.GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorMenLockerRoom"))
                            {
                                _humanReactionControl.ClearHumanReactionSprite();
                                return child.gameObject;
                            }
                            
                            if (_humanClientData.GetGender() == Gender.Female &&
                                LayerDetected.GetLayerUnderObject(child.gameObject) == LayerMask.NameToLayer("FloorWomenLockerRoom"))
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
        
        private void NextIndexInTargetsArray()
        {
            _humanClientData.indexInTargetsArray++;
        
            if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
            UIManagerPanelHumanClient.Instance.UpdateData();
        }

        private void SendHumanHome()
        {
            if (_navMeshAgent.enabled) _navMeshAgent.SetDestination(Vector3.zero);
            Invoke(nameof(DestroyHuman), 15f);
            _humanReactionControl.SetSmileAboveHuman();
            _humanReactionControl.SetTextAboveHuman("");
        
            if (_humanClientData.GetMood() <= LevelManager.MoodSad) LevelManager.Instance.TakeAwayRating(1);
            else if (_humanClientData.GetMood() > LevelManager.MoodHappy) LevelManager.Instance.AddRating(1);
        
            _humanClientData.trainingIsFinished = true;
        }
    
        private void DestroyHuman()
        {
            Destroy(gameObject);
        
            if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
            UIManagerPanelHumanClient.Instance.Close();
        }
    }
}
