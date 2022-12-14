using System;
using UnityEngine;
using UnityEngine.Events;

public class CameraController : MonoBehaviour
{
    public static CameraController Instance;
    
    public static UnityEvent CameraChanged = new UnityEvent();
    
    [SerializeField] private Camera mainCamera;
    
    private Vector3 touch;

    private float minZoom = 2;
    private float maxZoom = 30;

    public GameObject objectForFollow;

    private void Awake()
    {
        Instance = this;
    }

    void Update()
    {
        if (UIManager.Instance.IsPointerOverUIObject()) return;
        
        if (Input.GetMouseButtonDown(0))
        {
            touch = mainCamera.ScreenToWorldPoint(Input.mousePosition);
            objectForFollow = null;
        }

        if (Input.touchCount == 2)
        {
            Touch touchZero = Input.GetTouch(0);
            Touch touchOne = Input.GetTouch(1);

            Vector2 touchZeroLastPos = touchZero.position - touchZero.deltaPosition;
            Vector2 touchOneLastPos = touchOne.position - touchOne.deltaPosition;

            float distTouch = (touchZeroLastPos - touchOneLastPos).magnitude;
            float currentDistTouch = (touchZero.position - touchOne.position).magnitude;

            float difference = currentDistTouch - distTouch;
            
            Zoom(difference * 0.03f);
        }
        
        else if (Input.GetMouseButton(0))
        {
            Vector3 direction = touch - mainCamera.ScreenToWorldPoint(Input.mousePosition);
            direction.y = 0;
            mainCamera.transform.position += direction;
            
            CameraChanged.Invoke();
        } 
        
        Zoom(Input.GetAxis("Mouse ScrollWheel") * 5f);

        if (objectForFollow != null)
        {
            Vector3 v;
            v.x = objectForFollow.transform.position.x + 36;
            v.y = transform.position.y;
            v.z = objectForFollow.transform.position.z - 36;
            
            transform.position = v;
        }
        
        //transform.position = ClampCamera(transform.position);
    }

    void Zoom(float increment)
    {
        mainCamera.orthographicSize = Mathf.Clamp(mainCamera.orthographicSize - increment, minZoom, maxZoom);
    }

    private Vector3 ClampCamera(Vector3 targetPosition)
    {
        float camHight = mainCamera.orthographicSize;
        float camWidht = mainCamera.orthographicSize * mainCamera.aspect;

        float minX = -25 + camWidht;
        float maxX = 25 - camWidht;
        float minZ = -25 + camHight;
        float maxZ = 25 - camHight;

        float newX = Mathf.Clamp(targetPosition.x, minX, maxX);
        float newZ = Mathf.Clamp(targetPosition.y, minZ, maxZ);

        return new Vector3(newX, targetPosition.y, newZ);
    }
}
