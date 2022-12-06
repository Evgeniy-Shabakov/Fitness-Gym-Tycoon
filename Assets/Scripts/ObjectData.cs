using System;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [HideInInspector] public bool isNew;
    public int indexInBuildingManagerList;

    private void Start()
    {
        isNew = true;
    }
}
