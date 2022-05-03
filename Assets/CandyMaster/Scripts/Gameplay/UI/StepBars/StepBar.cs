using System.Collections.Generic;
using CandyMaster.Scripts.Interfaces.UI;
using UnityEngine;

namespace CandyMaster.Scripts.UI.StepBars
{
    public class StepBar : MonoBehaviour, IStepBar
    {
        [SerializeField] private GameObject stepPointPrefabUI;
        
        private Queue<StepPoint> _points;

        private void Clear()
        {
            foreach (var componentInChild in GetComponentInChildren<Transform>()) Destroy(transform.gameObject);
        }
        
        public void Init(int stepCount)
        {
            Clear();
            
            _points = new Queue<StepPoint>(stepCount);
            for (var i = 0; i < stepCount; i++)
            {
                var point = Instantiate(stepPointPrefabUI, transform).GetComponent<StepPoint>();
                _points.Enqueue(point);
            }
        }

        public void NextStetStarted() => _points.Peek().ActivateCenter();

        public void NextStepFinished() => _points.Dequeue().ActivateWhole();
    }
}