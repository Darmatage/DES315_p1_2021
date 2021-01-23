
using StudentDavidDaskyScripts;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace StudentScripts.DavidDasky
{
    public class RadialProgress : MonoBehaviour
    {
        [SerializeField] private StudentDoor door;
        [SerializeField] private Image mask;
        private int switchesOn;
        private int max;
        private TextMeshProUGUI switchCount;
        [SerializeField]
        private GameObject switchIconOffArt;
        [SerializeField]
        private GameObject switchIconOnArt;

        
        private void Start()
        {
            max = door.GetMaxSwitches();
            switchCount = GetComponentInChildren<TextMeshProUGUI>();
        }

        private void Update()
        {
            UpdateCount();
        }

        private void UpdateCount()
        {
            switchesOn = door.GetSwitchesOn();
            switchCount.text = "Switches:" + switchesOn + "/" + max;
            mask.fillAmount = (float)switchesOn / max;
            ChangeIcon();
        }

        private void ChangeIcon()
        {
            if (switchesOn == max)
            {
                switchIconOffArt.SetActive(false);
                switchIconOnArt.SetActive(true);
            } else if(switchesOn != max) {
                switchIconOnArt.SetActive(false);
                switchIconOffArt.SetActive(true);
            }
        }
        
    }
}
