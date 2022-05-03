using System;
using System.Collections.Generic;
using UnityEngine;

namespace CandyMaster.Scripts.Coloring.PaintTubeSystem
{
    public class PaintTearPool : MonoBehaviour
    {
        [SerializeField] private int count;
        [SerializeField] private List<PaintTear> tears;
        [SerializeField] private GameObject tearPrefab;


        private void Init(int count)
        {
            tears = new List<PaintTear>();
            for (int i = 0; i < count; i++)
            {
                var t = Instantiate(tearPrefab).GetComponent<PaintTear>();
                t.gameObject.SetActive(false);
                tears.Add(t);
            }
        }

        private void Start()
        {
            Init(count);

            PaintTear.OnDone += AddToPool;
        }

        private void OnDestroy()
        {
            PaintTear.OnDone -= AddToPool;
        }

        public void AddToPool(PaintTear paintTear)
        {
            paintTear.gameObject.SetActive(false);
            tears.Add(paintTear);
        }

        public PaintTear GetTear()
        {
            if (tears.Count == 0)
                throw new Exception();
            var tear = tears[0];
            tears.RemoveAt(0);
            tear.gameObject.SetActive(true);
            return tear;
        }
    }
}