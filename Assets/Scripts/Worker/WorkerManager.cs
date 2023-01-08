using UnityEngine;

namespace Worker
{
    public class WorkerManager : MonoBehaviour
    {
        public static WorkerManager Instance;
        
        [SerializeField] private GameObject prefabReceptionist;
        [SerializeField] private GameObject prefabJanitor;
        [SerializeField] private GameObject prefabTrainer;
        [SerializeField] private GameObject prefabEngineer;
        
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
                case WorkerType.Trainer:
                    prefab = prefabTrainer;
                    break;
                case WorkerType.Engineer:
                    prefab = prefabEngineer;
                    break;
                default:
                    return;
            }
            
            Instantiate(prefab, transform);
        }

        public void RemoveWorker(WorkerType type)
        {
            string tagname;
            
            switch (type)
            {
                case WorkerType.Receptionist:
                    tagname = prefabReceptionist.tag;
                    break;
                case WorkerType.Janitor:
                    tagname = prefabJanitor.tag;
                    break;
                case WorkerType.Trainer:
                    tagname = prefabTrainer.tag;
                    break;
                case WorkerType.Engineer:
                    tagname = prefabEngineer.tag;
                    break;
                default:
                    return;
            }
            
            foreach (Transform child in transform)
            {
                if (child.CompareTag(tagname))
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
                case WorkerType.Trainer:
                    tagsname = prefabTrainer.tag;
                    break;
                case WorkerType.Engineer:
                    tagsname = prefabEngineer.tag;
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
