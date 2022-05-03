using System;
using CandyMaster.Project.Scripts.Data.Immutable;

namespace CandyMaster.Project.Scripts.Data.Mutable
{
    public abstract class TransferredData<TData> : Data
        where TData : ObjectData
    {
        private TData Data { get; set; }
        private Action<TData> OnUpdate { get; set; }


        public void Update(TData data)
        {
            Data = data;
            OnUpdate?.Invoke(Data);
        }

        public void Subscribe(Action<TData> onUpdate) => OnUpdate = onUpdate;
    }
}