using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace StudentDavidDaskyScripts
{
    public class StudentDoor : MonoBehaviour
    {
        public GameObject doorClosedArt;
        public GameObject doorOpenArt;
        public string nextScene = "MainMenu";
        private Collider2D collider2D;
        private int switchesOn = 0;
        private bool allOn = false;
        [SerializeField]
        private List<StudentSwitch> switches = new List<StudentSwitch>();

        public int GetMaxSwitches() => switches.Count;

        public int GetSwitchesOn()
        {
            return switches.Count(studentSwitch => studentSwitch.isOn);
        }
        private void Awake()
        {
            collider2D = GetComponent<Collider2D>();
        }

        private void Start()
        {
            doorClosedArt.SetActive(true);
            doorOpenArt.SetActive(false);
            gameObject.GetComponent<Collider2D>().enabled = false;
        }

        private void Update()
        {
            if(!CheckConditions()) DoorClose();
            if(CheckConditions()) DoorOpen();
        }
        
       private bool CheckConditions()
       {
           foreach (var s in switches)
           {
               if(!s.isOn) return false;
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
            collider2D.enabled = true;
        }

        private void DoorClose()
        {
            // swap art assets 
            doorClosedArt.SetActive(true);
            doorOpenArt.SetActive(false);
            // enable the collider
            collider2D.enabled = false;
        }
    }
}
