using System;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [HideInInspector] public bool isNew;
    [HideInInspector] public int indexInBuildingManagerList;
    
    [HideInInspector] public bool objectIsFree;
    [HideInInspector] public int price;

    [HideInInspector] public Vector3 positionBeforeMove;
    [HideInInspector] public Quaternion rotationBeforeMove;

    private void Start()
    {
        objectIsFree = true;
    }
}
