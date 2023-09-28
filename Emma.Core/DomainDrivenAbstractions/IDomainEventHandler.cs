﻿namespace Emma.Core.DomainDrivenAbstractions;

public interface IDomainEventHandler<in T> where T : IDomainEvent {
    void Handle(T domainEvent);
}