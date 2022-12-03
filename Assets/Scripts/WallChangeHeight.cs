using System;
using System.Collections;
using UnityEngine;

public class WallChangeHeight : MonoBehaviour
{
    private MeshRenderer mr;
    
    private LayerMask layerMaskWalls;
    
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        
        layerMaskWalls =  LayerMask.GetMask("Walls");
    }

    private void OnBecameVisible()
    {
        StartCoroutine("CheсkWallPisition");
    }

    private void OnBecameInvisible()
    {
        StopCoroutine("CheсkWallPisition");
    }

    private void OnMouseEnter()
    {
        if (UIManager.Instance.IsPointerOverUIObject()) return;
        
        mr.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        mr.enabled = true;
        transform.GetChild(0).gameObject.SetActive(false);
    }

    IEnumerator CheсkWallPisition()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit hit;
        
        if (Physics.Raycast(ray, out hit, 100f, layerMaskWalls))
        {
            if (hit.transform.gameObject == gameObject)
            {
                mr.enabled = false;
                transform.GetChild(0).gameObject.SetActive(true);
            }
        }
        else
        {
            mr.enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }

        yield return 0.5f;
        
        StartCoroutine("CheсkWallPisition");
    }
}
