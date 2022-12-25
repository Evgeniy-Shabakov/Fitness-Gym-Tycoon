using UnityEngine;

namespace HumanClient
{
    public class HumanClientData : MonoBehaviour
    {
        private Gender _gender;

        public Gender GetGender()
        {
            return _gender;
        }

        public void SetGender(Gender value)
        {
            _gender = value;
        }
    }
}
