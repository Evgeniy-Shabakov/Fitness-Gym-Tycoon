using UnityEngine;

namespace Worker
{
    public abstract class WorkerData : MonoBehaviour
    {
        private int _salary;

        public int GetSalary()
        {
            return _salary;
        }

        public void SetSalary(int value)
        {
            _salary = value;
        }
    }
}
