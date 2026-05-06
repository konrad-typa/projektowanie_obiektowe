using Erpeg.Core.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Erpeg.Systems.EventSystems;

public class EventBus<T> : ISubject<T>
{
    private readonly List<Core.Interfaces.IObserver<T>> _observers = new();

    public void RegisterObserver(Core.Interfaces.IObserver<T> observer)
    {
        if (!_observers.Contains(observer))
        {
            _observers.Add(observer);
        }
    }

    public void RemoveObserver(Core.Interfaces.IObserver<T> observer)
    {
        if (_observers.Contains(observer))
        {
            _observers.Remove(observer);
        }
    }

    public void NotifyObservers(T eventData)
    {
        foreach (var observer in _observers) // .tolist
        {
            observer.OnNotify(eventData);
        }
    }
}