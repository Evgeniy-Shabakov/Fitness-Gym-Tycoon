using System;
using UnityEngine;
using Worker;

namespace UI
{
    public class UIManagerPanelHireWorkers : MonoBehaviour
    {
        public static UIManagerPanelHireWorkers Instance;
        
        public GameObject panel;

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
        }

        public void AddReceptionist()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Receptionist);
        }

        public void RemoveReceptionist()
        {
            WorkerManager.Instance.RemoveReceptionist(WorkerType.Receptionist);
        }
        
        public void AddJanitor()
        {
            WorkerManager.Instance.AddWorker(WorkerType.Janitor);
        }

        public void RemoveJanitor()
        {
            WorkerManager.Instance.RemoveReceptionist(WorkerType.Janitor);
        }
    }
}
