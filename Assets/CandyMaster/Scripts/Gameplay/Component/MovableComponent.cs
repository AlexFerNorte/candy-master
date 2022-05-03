using System;
using System.Threading.Tasks;
using CandyMaster.Scripts.Interfaces.Utils;
using DG.Tweening;
using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Component
{
    public class MovableComponent : MonoBehaviour, IMovable
    {
        [SerializeField] private Mode currentMode = Mode.ByDuration;
        [SerializeField] private float moveSpeed = 1;
        [SerializeField] private float moveDuration = .5f;


        public Vector3 Position
        {
            get => transform.position;
            set => transform.position = value;
        }

        public async Task MoveTo(Vector3 position)
        {
            var moving = true;
            transform.DOMove(
                    position,
                    currentMode is Mode.ByDuration
                        ? moveDuration
                        : Vector3.Distance(transform.position, position) / moveSpeed)
                .OnComplete(() => moving = false);
            while (moving) await Task.Yield();
        }

        public enum Mode
        {
            BySpeed,
            ByDuration
        }
    }
}