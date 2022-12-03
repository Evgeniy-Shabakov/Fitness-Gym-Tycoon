using System;
using UnityEngine;

public class WallChangeHeight : MonoBehaviour
{
    private Transform tr;
    private MeshRenderer mr;
    
    private LayerMask layerMaskWalls;
    
    private void Start()
    {
        tr = GetComponent<Transform>();
        mr = GetComponent<MeshRenderer>();
        
        layerMaskWalls =  LayerMask.GetMask("Walls");
        
        CheсkWallPisition();
    }

    private void CheсkWallPisition()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, layerMaskWalls))
        {
            if (hit.transform.gameObject == gameObject)
            {
                mr.enabled = false;
                tr.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            mr.enabled = true;
            tr.GetChild(0).gameObject.SetActive(false);
        }
        
        Invoke("CheсkWallPisition", 0.5f);
    }
}
