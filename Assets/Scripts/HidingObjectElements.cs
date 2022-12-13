using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidingObjectElements : MonoBehaviour
{
    [SerializeField] private List<MeshRenderer> mrList;

    public void HideElements()
    {
        foreach (var mr in mrList)
        {
            mr.enabled = false;
        }
    }

    public void ShowElements()
    {
        foreach (var mr in mrList)
        {
            mr.enabled = true;
        }
    }
}
