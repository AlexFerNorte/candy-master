using System.Threading.Tasks;
using UnityEngine;

namespace CandyMaster.Scripts.Interfaces.Utils
{
    public interface IMovable
    {
        Vector3 Position { get; set; }
        
        Task MoveTo(Vector3 position);
    }
}