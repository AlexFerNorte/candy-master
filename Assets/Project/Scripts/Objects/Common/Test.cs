using System;
using UnityEngine;

namespace CandyMasters.Project.Scripts.Objects.Common
{
    public class Test : MonoBehaviour
    {
        private void Update()
        {
            transform.position = Input.mousePosition;
        }
    }
}