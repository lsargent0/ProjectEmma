using CSharpFunctionalExtensions;
using Emma.Core.DomainDrivenAbstractions;

namespace Emma.Core.Notify;

public sealed class CareGiver : AggregateRoot {
    public override Identifier Id { get; }
    public IReadOnlyCollection<object> Notifications { get; }
    public CareGiver(Identifier id, IReadOnlyCollection<object> notifications) {
        Id = id;
        Notifications = notifications;
    }
    public void Notify(IDomainEvent notification) {
        
    }
    public record Identifier : TaggedGuid {
        private static string TagName => nameof(CareGiver);
        private Identifier(Guid guid, string tag) : base(guid, tag) {
        }
        public static Result<Identifier, Error> Parse(string value) {
            return Parse(value, DefaultTagValidator(TagName))
                .Map(parsed => new Identifier(parsed.Guid, parsed.Tag));
        }
        public static Identifier Create() {
            return new Identifier(Guid.NewGuid(), TagName);
        }
    }
}