using System;
using UnityEngine;

public class PreBuildingCollision : MonoBehaviour
{
    private bool placeForBuildIsClear;

    private GameObject parent;
    private Material defaultMaterial;
    private MeshRenderer mr;

    private void Start()
    {
        parent = transform.parent.gameObject;
        mr = parent.GetComponent<MeshRenderer>();
        
        placeForBuildIsClear = true;
        defaultMaterial = mr.material;
        mr.material = BuildingManager.Instanse.materialForPreview;
    }

    private void OnDestroy()
    {
        mr.material = defaultMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor") return;

        placeForBuildIsClear = false;
        mr.material = BuildingManager.Instanse.materialForCollision;
    }

    private void OnTriggerExit(Collider other)
    {
        placeForBuildIsClear = true;
        mr.material = BuildingManager.Instanse.materialForPreview;
    }

    public bool PlaceForBuildIsClear()
    {
        return placeForBuildIsClear;
    }
}
