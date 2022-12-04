using UnityEngine;

public class WallChangeHeight : MonoBehaviour
{
    private MeshRenderer mr;
    
    private LayerMask layerMaskWalls;
    
    private void Start()
    {
        mr = GetComponent<MeshRenderer>();
        
        layerMaskWalls =  LayerMask.GetMask("Walls");
        
        CameraController.CameraChanged.AddListener(CheсkWallPosition);
    }

    private void OnMouseEnter()
    {
        if (UIManager.Instance.IsPointerOverUIObject()) return;
        
        mr.enabled = false;
        transform.GetChild(0).gameObject.SetActive(true);
    }

    private void OnMouseExit()
    {
        CheсkWallPosition();
    }

    private void CheсkWallPosition()
    {
        Ray ray = new Ray(Camera.main.transform.position, Camera.main.transform.forward);
        RaycastHit[] hit;
        hit = Physics.SphereCastAll(ray, 7f, 100f, layerMaskWalls);
        
        if (hit.Length > 0)
        {
            for (int i = 0; i < hit.Length; i++)
            {
                if (hit[i].transform.gameObject == gameObject)
                {
                    mr.enabled = false;
                    transform.GetChild(0).gameObject.SetActive(true);
                    break;
                }
            
                mr.enabled = true;
                transform.GetChild(0).gameObject.SetActive(false);
            }
        }
        
        else
        {
            mr.enabled = true;
            transform.GetChild(0).gameObject.SetActive(false);
        }
    }
}
