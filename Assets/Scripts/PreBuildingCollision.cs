using System;
using UnityEngine;

public class PreBuildingCollision : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material blue;

    private bool placeForBuildIsClear;

    private GameObject parent;
    private Material defaultMaterial;
    private MeshRenderer mr;

    private void Start()
    {
        parent = transform.parent.gameObject;
        mr = parent.GetComponent<MeshRenderer>();
        Debug.Log(mr.gameObject.name);
        
        placeForBuildIsClear = true;
        defaultMaterial = mr.material;
        mr.material = blue;
    }

    private void OnDestroy()
    {
        mr.material = defaultMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.name == "Plane") return;

        placeForBuildIsClear = false;
        mr.material = red;
    }

    private void OnTriggerExit(Collider other)
    {
        placeForBuildIsClear = true;
        mr.material = blue;
    }

    public bool PlaceForBuildIsClear()
    {
        return placeForBuildIsClear;
    }
}
