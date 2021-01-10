using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DPINT_Wk3_Observer.Model
{
    public abstract class Observable<T> : IObservable<T>, IDisposable
    {
        private List<IObserver<T>> _observers;

        public Observable()
        {
            _observers = new List<IObserver<T>>();
        }

        private struct Unsubscriber : IDisposable
        {
            private Action _unsubscribe;
            public Unsubscriber(Action unsubscribe) { _unsubscribe = unsubscribe; }
            public void Dispose() { _unsubscribe(); }
        }

        public void Dispose()
        {
            //check the implementation on the microsoft docs.
            throw new NotImplementedException();
            //call "oncompleted" is you want
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            //TODO: keep track of who's looking at this object
            //TODO: put the observer in the list of observers.
            //this would allow us to know which obserrvers to notify
            _observers.Add(observer);
            //MS docs use onnext for each flight at this point
            //maybe override in the Baggageband?
            
            return new Unsubscriber(() => _observers.Remove(observer));
        }

        //this method can be accessed in the subclasses.
        //this notifies the observers that something important has changed.
        protected void Notify(T subject)
        {
            //check the implementation on the microsoft docs.
            //use this to notify each observer that a change has occured with OnNext.
            foreach(var obs in _observers)
            {
                obs.OnNext(subject);
            }
        }

    }
}
