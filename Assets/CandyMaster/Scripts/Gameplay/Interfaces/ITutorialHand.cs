using UnityEngine;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface ITutorialHand
    {
        void PointAt(Vector3 position, Mode mode);

        void Hide();

        enum Mode
        {
            Simple,
            UpDown,
            Clock
        }
    }
}