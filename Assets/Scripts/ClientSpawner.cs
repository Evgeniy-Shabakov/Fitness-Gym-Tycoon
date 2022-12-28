using System.Collections.Generic;
using HumanClient;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    void Start()
    {
        Invoke(nameof(CreateClientVisit), 15);
        Invoke(nameof(CreateClientMonth), 2);
        Invoke(nameof(CreateClientSixMonth), 10);
        Invoke(nameof(CreateClientYear), 20);
    }

    private void CreateClientVisit()
    {
        InstantiateHumanClient(SubscriptionType.Visit);
        
        Invoke(nameof(CreateClientVisit), LevelManager.Instance.GetTimeSpawnClientVisit());
    }
    
    private void CreateClientMonth()
    {
        InstantiateHumanClient(SubscriptionType.Month);
        
        Invoke(nameof(CreateClientMonth), LevelManager.Instance.GetTimeSpawnClientMonth());
    }
    
    private void CreateClientSixMonth()
    {
        InstantiateHumanClient(SubscriptionType.SixMonth);
        
        Invoke(nameof(CreateClientSixMonth), LevelManager.Instance.GetTimeSpawnClientSixMonth());
    }
    
    private void CreateClientYear()
    {
        InstantiateHumanClient(SubscriptionType.Year);
        
        Invoke(nameof(CreateClientYear), LevelManager.Instance.GetTimeSpawnClientYear());
    }

    private void InstantiateHumanClient(SubscriptionType type)
    {
        var currentHumanClient = Instantiate(prefabHumanClient, transform);
        var humanClientData = currentHumanClient.GetComponent<HumanClientData>();
        
        GameObject humanModel = Instantiate(prefabsHumansModels[Random.Range(0,2)], currentHumanClient.transform);
        
        humanClientData.SetSubscriptionType(type);
        
        if (humanModel.CompareTag("Male"))
        {
            humanClientData.SetGender(Gender.Male);
        }
        else
        {
            humanClientData.SetGender(Gender.Female);
        }
    }
}
