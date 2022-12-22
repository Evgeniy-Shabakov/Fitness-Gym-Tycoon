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
        mr.material = BuildingManager.Instance.materialForPreview;

        if (ObjectAboveFloor())
        {
            placeForBuildIsClear = true;
            mr.material = BuildingManager.Instance.materialForPreview;
        }
        else
        {
            placeForBuildIsClear = false;
            mr.material = BuildingManager.Instance.materialForCollision;
        }
    }

    private void OnDestroy()
    {
        if (defaultMaterial != null) mr.material = defaultMaterial;
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor") return;
        if (other.tag == "HumanClient") return;
        if (other.tag == "Plane") return;
        Debug.Log(other.name);
        placeForBuildIsClear = false;
        if (mr != null) mr.material = BuildingManager.Instance.materialForCollision;
    }

    private void OnTriggerExit(Collider other)
    {
        placeForBuildIsClear = true;
        mr.material = BuildingManager.Instance.materialForPreview;
        
        if (ObjectAboveFloor())
        {
            placeForBuildIsClear = true;
            mr.material = BuildingManager.Instance.materialForPreview;
        }
        else
        {
            placeForBuildIsClear = false;
            mr.material = BuildingManager.Instance.materialForCollision;
        }
    }

    public bool PlaceForBuildIsClear()
    {
        return placeForBuildIsClear;
    }

    public void SetPlaceForBuildIsClear(bool value)
    {
        placeForBuildIsClear = value;
    }
    
    private bool ObjectAboveFloor()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        
        if (Physics.Raycast(ray, 10f, BuildingManager.Instance.layerMaskForFloor)) return true;
        else return false;
    }
}
