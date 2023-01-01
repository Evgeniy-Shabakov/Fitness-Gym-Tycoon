using System;
using System.Runtime.Serialization;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;
using Random = UnityEngine.Random;

namespace HumanClient
{
    public class TrashSpawner : MonoBehaviour
    {
        [SerializeField] private GameObject prefabTrash;
        private HumanClientData _humanClientData;
        private NavMeshAgent _navMeshAgent;

        private GameObject _parentForAllTrash;
        private float _wait;

        private void Start()
        {
            _navMeshAgent = GetComponent<NavMeshAgent>();
            _humanClientData = GetComponent<HumanClientData>();
            _parentForAllTrash = GameObject.Find("TrashContainer");
            
            _wait = Random.Range(20f, 40f);
            Invoke(nameof(CreateTrash), _wait);
        }

        private void CreateTrash()
        {
            if (_navMeshAgent.enabled == false)
            {
                Invoke(nameof(CreateTrash), 2f);
                return;
            }
            
            var euler = transform.eulerAngles;
            euler.y = Random.Range(0, 360);
            GameObject trash = Instantiate(prefabTrash, transform.position, Quaternion.Euler(euler));
            trash.transform.SetParent(_parentForAllTrash.transform);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.CompareTag("Trash"))
            {
                _humanClientData.TakeAwayMood(LevelManager.CountMoodTakeAwayTrash);
                
                if (gameObject != UIManagerPanelHumanClient.Instance.currentGameObjectForPanelHumanClient) return;
                UIManagerPanelHumanClient.Instance.UpdateData();
            }
        }
    }
}
