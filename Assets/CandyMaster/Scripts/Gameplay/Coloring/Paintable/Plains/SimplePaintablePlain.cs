using System.Linq;
using System.Threading.Tasks;
using CandyMaster.Scripts.Gameplay.Interfaces.Coloring;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace CandyMaster.Scripts.Gameplay.Coloring.Paintable.Plains
{
    public class SimplePaintablePlain : MonoBehaviour, IPaintablePlain
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image mainView;
        [SerializeField] private Image mask;

        [SerializeField] private SimplePaintablePlain[] plates;

        [SerializeField] private GameObject splashPrefab;
        [SerializeField] private float splashDuration;

        private Vector3 _virtualPosition;

        public Vector3 WorldPosition => transform.position;

        private void Start()
        {
            plates = transform.parent.GetComponentsInChildren<SimplePaintablePlain>();
        }

        public async void ApplyPaintAtWorldPosition(Vector3 position, Color color)
        {
            if (mainView.color != color)
                mainView.color = Color.clear;

            var image = Instantiate(splashPrefab, canvas.transform).GetComponent<Image>();
            image.color = color;

            var t = image.transform;
            t.SetParent(mask.transform, false);
            t.position = position;
            t.localScale = Vector3.zero;

            var task = image.transform.DOScale(1, splashDuration);
            while (task.IsPlaying()) await Task.Yield();
            mainView.color = color;

            Destroy(image.gameObject);
        }

        public void ResetPaint() => mainView.color = Color.clear;

        public void SetPosition(Vector3 localPosition)
        {
            _virtualPosition = localPosition;

            var list = plates.ToList();
            list.Sort((plain, paintablePlain) =>
                plain._virtualPosition.magnitude.CompareTo(paintablePlain._virtualPosition.magnitude));

            for (var i = 0; i < list.Count; i++) list[i].transform.SetSiblingIndex(i);
        }
    }
}