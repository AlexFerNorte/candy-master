using CandyMasters.Project.Scripts.Data;
using UnityEngine;

namespace CandyMasters.Project.Scripts.UI.Core
{
    public abstract class Screen<TInitializeData> : MonoBehaviour
        where TInitializeData : InitializeData
    {
        protected TInitializeData Data;


        public virtual void Initialize(TInitializeData data) => Data = data;

        public virtual void Appear() => gameObject.SetActive(true);

        public virtual void Disappear() => gameObject.SetActive(false);
    }
}