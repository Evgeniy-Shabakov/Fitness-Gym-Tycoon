using UnityEngine;
using TMPro;
using Worker;

namespace UI
{
    public class UIManagerPanelHireWorkers : MonoBehaviour
    {
        public static UIManagerPanelHireWorkers Instance;
        
        public GameObject panel;
        
        [SerializeField] private TextMeshProUGUI textNumberReceptionist;
        [SerializeField] private TextMeshProUGUI textNumberJanitor;

        private void Awake()
        {
            Instance = this;
        }
        
        public void Close()
        {
            panel.SetActive(false);
        }

        public void Open()
        {
            panel.SetActive(true);

            textNumberReceptionist.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Receptionist) + "";
            textNumberJanitor.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Janitor) + "";
        }

        public void AddReceptionist()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Receptionist);
            textNumberReceptionist.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Receptionist) + "";
        }

        public void RemoveReceptionist()
        {
            WorkerManager.Instance.RemoveWorker(WorkerType.Receptionist);
            textNumberReceptionist.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Receptionist) + "";
        }
        
        public void AddJanitor()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Janitor);
            textNumberJanitor.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Janitor) + "";
        }

        public void RemoveJanitor()
        {
            WorkerManager.Instance.RemoveWorker(WorkerType.Janitor);
            textNumberJanitor.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Janitor) + "";
        }
    }
}
