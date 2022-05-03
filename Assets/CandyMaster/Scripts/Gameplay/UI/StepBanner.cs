using CandyMaster.Scripts.Gameplay;
using CandyMaster.Scripts.Interfaces;
using DG.Tweening;
using TMPro;
using UnityEngine;

namespace CandyMaster.Scripts.UI
{
    public class StepBanner : MonoBehaviour, IStepBanner
    {
        [SerializeField] private TMP_Text text;
        [SerializeField] private float wigglePower = .2f;
        [SerializeField] private float wiggleDuration = .5f;

        public void ShowStep(AbstractStep step)
        {
            text.text = step.StepTitle;
            transform.DOPunchScale(Vector3.one * wigglePower, wiggleDuration);
        }
    }
}