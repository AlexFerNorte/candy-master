using CandyMaster.Project.Scripts.UI.Core;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Project.Scripts.UI.Implementations.Bars
{
    public class TemperatureBar : Bar
    {
        [SerializeField] private Image filler;
        [SerializeField] private Image leftPoint;
        [SerializeField] private Image centerPoint;
        [SerializeField] private Image rightPoint;


        public void Fill(float percent)
        {
            filler.fillAmount = percent;

            if (percent >= 0f)
            {
                leftPoint.gameObject.SetActive(true);
            }

            if (percent >= 0.5f)
            {
                centerPoint.gameObject.SetActive(true);
            }
            
            if (percent >= 1f)
            {
                rightPoint.gameObject.SetActive(true);
            }
        }
    }
}