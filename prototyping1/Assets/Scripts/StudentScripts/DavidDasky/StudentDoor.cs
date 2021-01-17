using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudentDavidDaskyScripts
{
    public class StudentDoor : MonoBehaviour
    {
        public GameObject doorClosedArt;
        public GameObject doorOpenArt;
        public string nextScene = "MainMenu";
        [SerializeField]
        private List<StudentSwitch> switches = new List<StudentSwitch>();
        
        private void Start()
        {
            doorClosedArt.SetActive(true);
            doorOpenArt.SetActive(false);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        private void Update()
        {
            if (CheckConditions())
            {
                DoorOpen();
            }
        }

        private bool CheckConditions()
        {
            // if any switch is off return false
            foreach (var _switch in switches)
            {
                if(!_switch.isOn) return false;
            }
            return true;
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.gameObject.CompareTag("Player"))
            {
                SceneManager.LoadScene(nextScene);
            }
        }
        
        private void DoorOpen()
        {
            // swap art assets 
            doorClosedArt.SetActive(false);
            doorOpenArt.SetActive(true);
            // enable the collider
            gameObject.GetComponent<Collider2D>().enabled = true;
        }
        
    }
}
