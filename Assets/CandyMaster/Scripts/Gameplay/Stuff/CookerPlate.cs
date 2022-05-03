using System;
using CandyMaster.Scripts.Gameplay.Interfaces;
using CandyMaster.Scripts.Interfaces;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Stuff
{
    public class CookerPlate : MonoBehaviour, ICookerPlate
    {
        [SerializeField] private bool currentState;
        [SerializeField] private bool isBlocked;
        [SerializeField] private Transform button;
        [SerializeField] private Vector3 buttonOn, buttonOff;
        [SerializeField] private float buttonTurnDuration = .3f;


        public event Action<bool> StateUpdated;

        public bool IsBlocked
        {
            get => isBlocked;
            set => isBlocked = value;
        }

        private void OnMouseUp()
        {
            if (IsBlocked) return;
            
            currentState = !currentState;
            button.DOLocalRotate(currentState ? buttonOn : buttonOff, buttonTurnDuration);
            StateUpdated?.Invoke(currentState);
        }

        public void Dispose()
        {
            Destroy(gameObject);
        }
    }
}