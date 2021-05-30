using System;
using System.Collections.Generic;
using Module.IoC.Register.Interfaces;

namespace Module.IoC.Mapper
{
    public class ObjectMoq : IObjectMoq
    {
        private bool disposedValue;

        private readonly Dictionary<string, object> _mockedObject = new();

        public void AddMoqObject<T>(T moqObject)
        {
            var name = moqObject.GetType().Name;
            if (this._mockedObject.ContainsKey(name))
                this._mockedObject[name] = moqObject;
            
            this._mockedObject.Add(name, moqObject);
        }

        public T GetMoqObject<T>(T objectToCompare)
            where T : class
        {
            var name = objectToCompare.GetType().Name;
            if (!this._mockedObject.ContainsKey(name))
                return default;

            return this._mockedObject[name] as T;
        }

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this._mockedObject.Clear();
                }

                disposedValue = true;
            }
        }

        public void Dispose()
        {
            Dispose(disposing: true);

            GC.SuppressFinalize(this);
        }
    }
}