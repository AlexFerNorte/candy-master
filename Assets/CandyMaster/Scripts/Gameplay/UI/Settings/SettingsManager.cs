using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.UI.Settings
{
    public class SettingsManager : MonoBehaviour
    {
        [SerializeField] private bool hideOnStart;
        
    
        [SerializeField] private GameObject settingsPlane;
    
        [SerializeField] private Button settingButton;
        [SerializeField] private Toggle soundToggle, hapticToggle;

        private void Start()
        {
            settingButton.onClick.AddListener(() => settingsPlane.SetActive(!settingsPlane.activeSelf));
            settingsPlane.SetActive(!hideOnStart);
        }
    }
}
