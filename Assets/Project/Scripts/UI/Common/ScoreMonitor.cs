using TMPro;
using UnityEngine;

namespace CandyMasters.Project.Scripts.UI.Common
{
    public class ScoreMonitor : MonoBehaviour
    {
        [SerializeField] private TMP_Text crystalsAmountText;
        [SerializeField] private TMP_Text coinsAmountText;
        

        public void SetCrystals(int amount)
        {
            crystalsAmountText.text = amount.ToString();
        }
        
        public void SetCoins(int amount)
        {
            coinsAmountText.text = amount.ToString();
        }
    }
}