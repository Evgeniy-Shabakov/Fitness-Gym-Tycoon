using System.Collections.Generic;
using HumanClient;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private GameObject prefabHumanModel;

    [SerializeField] private List<Mesh> femaleMeshes;
    [SerializeField] private List<Mesh> maleMeshes;

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
        
        GameObject humanModel = Instantiate(prefabHumanModel, currentHumanClient.transform);
        
        humanClientData.SetSubscriptionType(type);
        
        int gender = Random.Range(0, 2);
        
        if (gender == 0)
        {
            int i = Random.Range(0, maleMeshes.Count);
            humanModel.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = maleMeshes[i];
            humanClientData.SetGender(Gender.Male);
        }
        else
        {
            int i = Random.Range(0, femaleMeshes.Count);
            humanModel.GetComponentInChildren<SkinnedMeshRenderer>().sharedMesh = femaleMeshes[i];
            humanClientData.SetGender(Gender.Female);
        }
    }
}
