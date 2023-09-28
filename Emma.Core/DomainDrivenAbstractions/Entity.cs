namespace Emma.Core.DomainDrivenAbstractions;

public abstract class Entity {
    public abstract TaggedGuid Id { get; }
    public DateTimeOffset Created { get; }
    public override bool Equals(object? obj) {
        return ReferenceEquals(this, obj) || (obj is Entity entity && entity.GetType() == GetType() && entity.Id == Id);
    }
    public override int GetHashCode() {
        return $"{GetType()}{Id}".GetHashCode();
    }
    public static bool operator ==(Entity entity1, Entity entity2) {
        return entity1.Equals(entity2);
    }
    public static bool operator !=(Entity entity1, Entity entity2) {
        return !(entity1 == entity2);
    }
}