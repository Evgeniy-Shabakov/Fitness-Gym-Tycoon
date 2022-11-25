using System;
using UnityEngine;

public class PreBuildingCollision : MonoBehaviour
{
    [SerializeField] private Material red;
    [SerializeField] private Material blue;

    private bool placeForBuildIsClear;
    
    private Material defaultMaterial;
    private MeshRenderer mr;

    private void Start()
    {
        mr = GetComponent<MeshRenderer>();

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

    public bool PlaceForBuilIsClear()
    {
        return placeForBuildIsClear;
    }
}
