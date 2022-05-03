using CandyMaster.Project.Scripts.Data;
using CandyMaster.Project.Scripts.Data.Immutable;

namespace CandyMaster.Project.Scripts.Controllers.Core
{
    public abstract class Controller<TInitializeData>
        where TInitializeData : InitializeData
    {
        protected TInitializeData InitializeData;

        public virtual void Initialize(TInitializeData data) => InitializeData = data;
    }
}