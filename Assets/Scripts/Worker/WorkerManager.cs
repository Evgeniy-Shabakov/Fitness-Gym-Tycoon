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

        public void RemoveWorker(WorkerType type)
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
                    child.parent = null;
                    Destroy(child.gameObject);
                    return;
                }
            }
        }

        public int CountNumberWorkers(WorkerType type)
        {
            string tagsname;
            
            switch (type)
            {
                case WorkerType.Receptionist:
                    tagsname = prefabReceptionist.tag;
                    break;
                case WorkerType.Janitor:
                    tagsname = prefabJanitor.tag;
                    break;
                default:
                    return 0;
            }
            
            int number = 0;
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag(tagsname))
                {
                    number++;
                }
            }

            return number;
        }
    }
}
