using UnityEngine;

public static class LayerDetected
{
    public static LayerMask GetLayerUnderObject(GameObject current)
    {
        var ray = new Ray(current.transform.position + new Vector3(0, 5, 0), -Vector3.up);
        LayerMask layer = LayerMask.GetMask("FloorMenLockerRoom", "FloorWomenLockerRoom");
            
        Physics.Raycast(ray, out var hitInfo, 10f, layer);

        return hitInfo.transform.gameObject.layer;
    }
}
