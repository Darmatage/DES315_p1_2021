
using StudentDavidDaskyScripts;
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

        private void Start()
        {
            max = door.GetMaxSwitches();
        }

        private void Update()
        {
            switchesOn = door.GetSwitchesOn();
            var curr = (float)switchesOn / max;
            mask.fillAmount = curr;
        }
    }
}
