using Emma.Core.DomainDrivenAbstractions;
using Emma.Core.Express;

namespace Emma.Core.Notify;

public class EmoteExpressionHandler : IDomainEventHandler<Dependent.Event.EmoteExpressed> {
    public void Handle(Dependent.Event.EmoteExpressed domainEvent) {
        //Add notifications to all 
    }
}