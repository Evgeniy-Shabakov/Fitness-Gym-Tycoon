using System;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [HideInInspector] public bool isNew;
    [HideInInspector] public int indexInBuildingManagerList;
    
    [HideInInspector] public bool objectIsFree;

    private void Start()
    {
        objectIsFree = true;
    }
}
