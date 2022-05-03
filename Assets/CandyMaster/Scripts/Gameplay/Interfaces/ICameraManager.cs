using System.Threading.Tasks;

namespace CandyMaster.Scripts.Gameplay.Interfaces
{
    public interface ICameraManager
    {
        public Task GoToStep(AbstractStep step);
    }
}