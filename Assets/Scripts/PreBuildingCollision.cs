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
        
        defaultMaterial = mr.material;
        mr.material = BuildingManager.Instanse.materialForPreview;

        if (ObjectAboveFloor())
        {
            placeForBuildIsClear = true;
            mr.material = BuildingManager.Instanse.materialForPreview;
        }
        else
        {
            placeForBuildIsClear = false;
            mr.material = BuildingManager.Instanse.materialForCollision;
        }
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
        
        if (ObjectAboveFloor())
        {
            placeForBuildIsClear = true;
            mr.material = BuildingManager.Instanse.materialForPreview;
        }
        else
        {
            placeForBuildIsClear = false;
            mr.material = BuildingManager.Instanse.materialForCollision;
        }
    }

    public bool PlaceForBuildIsClear()
    {
        return placeForBuildIsClear;
    }

    private bool ObjectAboveFloor()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        
        if (Physics.Raycast(ray, 10f, BuildingManager.Instanse.layerMaskForFloor)) return true;
        else return false;
    }
}
