using UnityEngine;

namespace Worker
{
    public class JanitorData : WorkerData
    {
        [SerializeField] private GameObject _parentAllTrash;
        
        public override void Start()
        {
            base.Start();
            _parentAllTrash = GameObject.Find("TrashParent");
        }
    }
}
