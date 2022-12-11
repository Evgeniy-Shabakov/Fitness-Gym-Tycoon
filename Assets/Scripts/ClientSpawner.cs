using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    private GameObject currentHumanClient;

    void Start()
    {
        Invoke("CreateClient", 2f);
    }

    private void CreateClient()
    {
        currentHumanClient = Instantiate(prefabHumanClient, Vector3.zero, Quaternion.identity);
        Instantiate(prefabsHumansModels[Random.Range(0,2)], currentHumanClient.transform);
        
        Invoke("CreateClient", 2f);
    }
}
