namespace Emma.Core.DomainDrivenAbstractions;

public interface IDomainEvent {
    Guid Id { get; }
    DateTimeOffset Created { get; }
}

public interface IDomainEvent<out T> : IDomainEvent where T : notnull {
    public T Details { get; }
}