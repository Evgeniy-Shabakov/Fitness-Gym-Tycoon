using System.Collections.Generic;
using BuildingSystem;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [HideInInspector] public bool isNew;
    [HideInInspector] public int indexInBuildingManagerList;
    
    [HideInInspector] public bool objectIsFree;
    [HideInInspector] public int price;

    [HideInInspector] public Vector3 positionBeforeMove;
    [HideInInspector] public Quaternion rotationBeforeMove;

    private readonly List<GameObject> _listClients = new List<GameObject>();
    
    private void Start()
    {
        objectIsFree = true;
    }

    public void AddClient(GameObject client)
    {
        _listClients.Add(client);

        if (_listClients.Count >= BuildingManager.Instance.objectsForBuilding[indexInBuildingManagerList].maxCountClients)
        {
            objectIsFree = false;
        }
    }

    public void RemoveClient(GameObject client)
    {
        _listClients.Remove(client);

        if (_listClients.Count < BuildingManager.Instance.objectsForBuilding[indexInBuildingManagerList].maxCountClients)
        {
            objectIsFree = true;
        }
    }
}
