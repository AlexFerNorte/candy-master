using System;
using UnityEngine;
using UnityEngine.EventSystems;

namespace CandyMaster.Scripts.Gameplay.Coloring.PaintTubeSystem
{
    [Obsolete]
    public class PaintInputPanel : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
    {
        public event Action StartPaint, EndPaint;
        
        public void OnPointerEnter(PointerEventData eventData)
        {
            StartPaint?.Invoke();
        }

        public void OnPointerExit(PointerEventData eventData)
        {
            EndPaint?.Invoke();
        }
    }
}