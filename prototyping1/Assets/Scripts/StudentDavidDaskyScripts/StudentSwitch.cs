
using UnityEngine;

namespace StudentDavidDaskyScripts
{
    public class StudentSwitch : MonoBehaviour
    {
        public GameObject switchOffArt;
        public GameObject switchOnArt;

        public bool isOn = false;
        
        private void Start()
        {
            switchOffArt.SetActive(true);
            switchOnArt.SetActive(false);
            isOn = false;
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (!other.gameObject.CompareTag("Player")) return;
            switchOffArt.SetActive(false);
            switchOnArt.SetActive(true);
            isOn = true;
        }
    }   
}
