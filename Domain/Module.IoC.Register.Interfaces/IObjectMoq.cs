using System;

namespace Module.IoC.Register.Interfaces
{
    public interface IObjectMoq : IDisposable
    {
        void AddMoqObject<T>(T moqObject);

        T GetMoqObject<T>(T objectToCompare) 
            where T : class;
    }
}