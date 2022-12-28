using System.Collections.Generic;
using HumanClient;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    void Start()
    {
        Invoke(nameof(CreateClientVisit), 6);
        Invoke(nameof(CreateClientMonth), 2);
        Invoke(nameof(CreateClientSixMonth), 15);
        Invoke(nameof(CreateClientYear), 20);
    }

    private void CreateClientVisit()
    {
        InstantiateHumanClient(LevelManager.Instance.GetPricePerVisit());
        
        Invoke(nameof(CreateClientVisit), LevelManager.Instance.GetTimeSpawnClientVisit());
    }
    
    private void CreateClientMonth()
    {
        InstantiateHumanClient(LevelManager.Instance.GetPricePerMonth());
        
        Invoke(nameof(CreateClientMonth), LevelManager.Instance.GetTimeSpawnClientMonth());
    }
    
    private void CreateClientSixMonth()
    {
        InstantiateHumanClient(LevelManager.Instance.GetPricePerSixMonth());
        
        Invoke(nameof(CreateClientSixMonth), LevelManager.Instance.GetTimeSpawnClientSixMonth());
    }
    
    private void CreateClientYear()
    {
        InstantiateHumanClient(LevelManager.Instance.GetPricePerYear());
        
        Invoke(nameof(CreateClientYear), LevelManager.Instance.GetTimeSpawnClientYear());
    }

    private void InstantiateHumanClient(int price)
    {
        var currentHumanClient = Instantiate(prefabHumanClient, transform);
        var humanClientData = currentHumanClient.GetComponent<HumanClientData>();
        
        GameObject humanModel = Instantiate(prefabsHumansModels[Random.Range(0,2)], currentHumanClient.transform);
        
        humanClientData.SetPriceEntry(price);
        
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
