using System.Reactive.Linq;
using System.Reactive.Subjects;

namespace Emma.Core.DomainDrivenAbstractions;

public abstract class AggregateRoot : Entity {
    public virtual int Version { get; protected set; }
}

public abstract class AggregateRoot<T> : AggregateRoot where T : IDomainEvent {
    private readonly ReplaySubject<T> _events;
    private readonly List<T> _eventsList;

    public IObservable<T> EventsObservable => _events.AsObservable();
    public IEnumerable<T> Events => _eventsList.AsEnumerable();

    protected AggregateRoot() {
        _events = new ReplaySubject<T>();
        _eventsList = new List<T>();
    }

    protected virtual void AddDomainEvent(T domainEvent) {
        _eventsList.Add(domainEvent);
        _events.OnNext(domainEvent);
    }
}

