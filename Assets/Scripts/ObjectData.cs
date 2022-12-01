using System;
using UnityEngine;

public class ObjectData : MonoBehaviour
{
    [HideInInspector] public bool IsNew;

    private void Start()
    {
        IsNew = true;
    }
}
