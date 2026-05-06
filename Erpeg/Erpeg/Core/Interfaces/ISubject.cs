namespace Erpeg.Core.Interfaces;

public interface ISubject<T>
{
    void RegisterObserver(Erpeg.Core.Interfaces.IObserver<T> observer);
    void RemoveObserver(Erpeg.Core.Interfaces.IObserver<T> observer);
    void NotifyObservers(T eventData);
}