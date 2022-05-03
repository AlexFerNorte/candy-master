using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.UI.Temperature
{
    public class TemperaturePoint : MonoBehaviour
    {
        [SerializeField] private Image innerPoint;
        [SerializeField] private Color activateColor;
        [SerializeField] private Color disableColor;
        [SerializeField] private float activateValue;


        public void UpdatePoint(float barValue)
        {
            innerPoint.color = barValue >= activateValue ? activateColor : disableColor;
        }
    }
}