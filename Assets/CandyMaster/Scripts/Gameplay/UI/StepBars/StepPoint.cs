using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.UI.StepBars
{
    public class StepPoint : MonoBehaviour
    {
        [SerializeField] private Image center, whole;
        [SerializeField] private Color activateColor;
        
        
        public void ActivateCenter() => center.color = activateColor;

        public void ActivateWhole() => whole.color = activateColor;
    }
}