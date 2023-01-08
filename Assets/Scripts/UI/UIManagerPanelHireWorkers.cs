using UnityEngine;
using TMPro;
using Unity.VisualScripting;
using Worker;

namespace UI
{
    public class UIManagerPanelHireWorkers : MonoBehaviour
    {
        public static UIManagerPanelHireWorkers Instance;
        
        public GameObject panel;
        
        [SerializeField] private TextMeshProUGUI textSalaryReceptionist;
        [SerializeField] private TextMeshProUGUI textSalaryJanitor;
        [SerializeField] private TextMeshProUGUI textSalaryTrainer;
        [SerializeField] private TextMeshProUGUI textSalaryEngineer;
        
        [SerializeField] private TextMeshProUGUI textNumberReceptionist;
        [SerializeField] private TextMeshProUGUI textNumberJanitor;
        [SerializeField] private TextMeshProUGUI textNumberTrainer;
        [SerializeField] private TextMeshProUGUI textNumberEngineer;
        
        private void Awake()
        {
            Instance = this;
        }

        public void Start()
        {
            textSalaryReceptionist.text = LevelManager.SalaryReceptionist + "$";
            textSalaryJanitor.text = LevelManager.SalaryJanitor + "$";
            textSalaryTrainer.text = LevelManager.SalaryTrainer + "$";
            textSalaryEngineer.text = LevelManager.SalaryEngineer + "$";
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
            textNumberTrainer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Trainer) + "";
            textNumberEngineer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Engineer) + "";
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
        
        public void AddTrainer()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Trainer);
            textNumberTrainer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Trainer) + "";
        }

        public void RemoveTrainer()
        {
            WorkerManager.Instance.RemoveWorker(WorkerType.Trainer);
            textNumberTrainer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Trainer) + "";
        }
        
        public void AddEngineer()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Engineer);
            textNumberEngineer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Engineer) + "";
        }

        public void RemoveEngineer()
        {
            WorkerManager.Instance.RemoveWorker(WorkerType.Engineer);
            textNumberEngineer.text = WorkerManager.Instance.CountNumberWorkers(WorkerType.Engineer) + "";
        }
    }
}
