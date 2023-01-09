using UnityEngine;

namespace BuildingSystem
{
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
            _allowedLayer = BuildingManager.Instance.FindObject(_objectData.type).layerFloor;
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
            if (other.CompareTag("Floor")) return;
            if (other.CompareTag("HumanClient")) return;
            if (other.CompareTag("Plane")) return;

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

            return Physics.Raycast(ray, 10f, _allowedLayer);
        }
    }
}
