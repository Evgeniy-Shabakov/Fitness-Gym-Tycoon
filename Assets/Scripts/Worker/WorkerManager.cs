using UnityEngine;

namespace Worker
{
    public class WorkerManager : MonoBehaviour
    {
        public static WorkerManager Instance;
        
        [SerializeField] private GameObject prefabReceptionist;
        [SerializeField] private GameObject prefabJanitor;
        
        private void Awake()
        {
            Instance = this;
        }
        
        public void AddWorker(WorkerType type)
        {
            GameObject prefab;
            
            switch (type)
            {
                case WorkerType.Receptionist:
                    prefab = prefabReceptionist;
                    break;
                case WorkerType.Janitor:
                    prefab = prefabJanitor;
                    break;
                default:
                    return;
            }
            
            Instantiate(prefab, transform);
        }

        public void RemoveReceptionist(WorkerType type)
        {
            string tag;
            
            switch (type)
            {
                case WorkerType.Receptionist:
                    tag = prefabReceptionist.tag;
                    break;
                case WorkerType.Janitor:
                    tag = prefabJanitor.tag;
                    break;
                default:
                    return;
            }
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag(tag))
                {
                    Destroy(child.gameObject);
                    return;
                }
            }
        }

        private int CountNumberReceptionist()
        {
            int number = 0;
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag("Receptionist"))
                {
                    number++;
                }
            }

            return number;
        }
    }
}
