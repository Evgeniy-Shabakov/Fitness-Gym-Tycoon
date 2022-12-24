using System.Collections.Generic;
using HumanClient;
using UnityEngine;

public class ClientSpawner : MonoBehaviour
{
    [SerializeField] private GameObject prefabHumanClient;
    [SerializeField] private List<GameObject> prefabsHumansModels;

    private GameObject _currentHumanClient;

    void Start()
    {
        Invoke(nameof(CreateClient), 2f);
    }

    private void CreateClient()
    {
        _currentHumanClient = Instantiate(prefabHumanClient, transform);
        
        GameObject humanModel = Instantiate(prefabsHumansModels[Random.Range(0,2)], _currentHumanClient.transform);
        
        if (humanModel.CompareTag("Male"))
        {
            _currentHumanClient.GetComponent<HumanClientData>().SetGender(HumanClientData.Gender.Male);
        }
        else
        {
            _currentHumanClient.GetComponent<HumanClientData>().SetGender(HumanClientData.Gender.Female);
        }
        
        Invoke(nameof(CreateClient), LevelManager.Instance.GetTimeSpawnClient());
    }
}
