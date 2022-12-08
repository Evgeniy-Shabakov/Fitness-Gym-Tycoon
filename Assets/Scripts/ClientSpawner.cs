using System.Collections;
using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabClient;
    
    void Start()
    {
        Invoke("CreateClient", 2f);
    }

    private void CreateClient()
    {
        Instantiate(prefabClient, Vector3.zero, Quaternion.identity);
        Invoke("CreateClient", 2f);
    }
}
