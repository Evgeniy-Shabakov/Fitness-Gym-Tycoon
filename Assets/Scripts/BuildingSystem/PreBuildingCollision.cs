using UnityEngine;

public class PreBuildingCollision : MonoBehaviour
{
    private bool _installationAllowed;

    private bool _isCollision;

    private GameObject _parent;
    private Material _defaultMaterial;
    private MeshRenderer _mr;

    private ObjectData _objectData;
    private LayerMask _allowedLayer;
    
    private void Start()
    {
        _parent = transform.parent.gameObject;
        _mr = _parent.GetComponent<MeshRenderer>();
        
        _defaultMaterial = _mr.material;
        _mr.material = BuildingManager.Instance.materialForPreview;

        _objectData = GetComponent<ObjectData>();
        _allowedLayer = BuildingManager.Instance.objectsForBuilding[_objectData.indexInBuildingManagerList].layerFloor;
    }

    private void Update()
    {
        if (_isCollision == false && ObjectAboveAllowedLayer())
        {
            _installationAllowed = true;
            _mr.material = BuildingManager.Instance.materialForPreview;
        }
        else
        {
            _installationAllowed = false;
            _mr.material = BuildingManager.Instance.materialForCollision;
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Floor") return;
        if (other.tag == "HumanClient") return;
        if (other.tag == "Plane") return;

        _isCollision = true;
    }

    private void OnTriggerExit(Collider other)
    {
        _isCollision = false;
    }

    private void OnDestroy()
    {
        if (_defaultMaterial != null) _mr.material = _defaultMaterial;
    }
    
    public bool GetInstallationAllowed()
    {
        return _installationAllowed;
    }

    public void SetInstallationAllowed(bool value)
    {
        _installationAllowed = value;
    }
    
    private bool ObjectAboveAllowedLayer()
    {
        Ray ray = new Ray(transform.position, -Vector3.up);
        
        if (Physics.Raycast(ray, 10f, _allowedLayer)) return true;
        else return false;
    }
}
