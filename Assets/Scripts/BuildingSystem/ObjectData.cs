using System.Collections.Generic;
using UnityEngine;

namespace BuildingSystem
{
    public class ObjectData : MonoBehaviour
    {
        [HideInInspector] public ObjectType type;
        [HideInInspector] public bool isNew;
        [HideInInspector] public bool objectIsFree;
        [HideInInspector] public int price;

        [HideInInspector] public Vector3 positionBeforeMove;
        [HideInInspector] public Quaternion rotationBeforeMove;

        private int _maxNumberClient;
        private readonly List<GameObject> _listClients = new List<GameObject>();
    
        private void Start()
        {
            objectIsFree = true;
            _maxNumberClient = BuildingManager.Instance.FindObject(type).maxCountClients;
        }

        public void AddClient(GameObject client)
        {
            _listClients.Add(client);

            if (_listClients.Count >= _maxNumberClient)
            {
                objectIsFree = false;
            }
        }

        public void RemoveClient(GameObject client)
        {
            _listClients.Remove(client);

            if (_listClients.Count < _maxNumberClient)
            {
                objectIsFree = true;
            }
        }
    }
}
