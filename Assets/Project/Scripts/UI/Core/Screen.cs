using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;
using UnityEngine;

namespace CandyMaster.Project.Scripts.UI.Core
{
    public class Screen : MonoBehaviour
    {
        public virtual void Appear() => gameObject.SetActive(true);

        public virtual void Disappear() => gameObject.SetActive(false);
    }
}