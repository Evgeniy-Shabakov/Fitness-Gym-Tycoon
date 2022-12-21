using System.Collections.Generic;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    private GameObject currentHumanClient;

    void Start()
    {
        Invoke(nameof(CreateClient), 2f);
    }

    private void CreateClient()
    {
        currentHumanClient = Instantiate(prefabHumanClient, transform);
        Instantiate(prefabsHumansModels[Random.Range(0,2)], currentHumanClient.transform);
        
        Invoke(nameof(CreateClient), LevelManager.Instance.GetTimeSpawnClient());
    }
}
