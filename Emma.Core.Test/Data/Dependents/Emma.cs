namespace Emma.Core.Test.Data.Dependents;

internal class Emma {
    public static readonly Emma Instance = new Emma();
    public readonly string FirstName = "Emma";
    public readonly string LastName = "Lowman";
    public readonly string LastNameTypo = "Lowmen";
    public readonly Express.Dependent.Identifier Id = Express.Dependent.Identifier.Create();
    public Express.Dependent AsDependent() {
        return Express.Dependent.FromExistingId(Id, FirstName, LastName).Value;
    }
}

internal static class EmmaExtension {
    public static Emma Emma(this Dependent dependent) {
        return Dependents.Emma.Instance;
    }
}

