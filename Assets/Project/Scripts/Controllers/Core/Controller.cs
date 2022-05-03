using CandyMasters.Project.Scripts.Data;

namespace CandyMasters.Project.Scripts.Controllers.Core
{
    public abstract class Controller<TInitializeData>
        where TInitializeData : InitializeData
    {
        protected TInitializeData Data;

        public virtual void Initialize(TInitializeData data) => Data = data;
    }
}