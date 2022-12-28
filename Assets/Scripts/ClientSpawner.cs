using System.Collections.Generic;
using HumanClient;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    void Start()
    {
        Invoke(nameof(CreateClient), 2f);
    }

    private void CreateClient()
    {
        var currentHumanClient = Instantiate(prefabHumanClient, transform);
        var humanClientData = currentHumanClient.GetComponent<HumanClientData>();
        
        GameObject humanModel = Instantiate(prefabsHumansModels[Random.Range(0,2)], currentHumanClient.transform);
        
        humanClientData.SetPriceEntry(LevelManager.Instance.GetPricePerVisit());
        
        if (humanModel.CompareTag("Male"))
        {
            humanClientData.SetGender(Gender.Male);
        }
        else
        {
            humanClientData.SetGender(Gender.Female);
        }
        
        Invoke(nameof(CreateClient), LevelManager.Instance.GetTimeSpawnClient());
    }
}
