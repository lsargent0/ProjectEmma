namespace Emma.Core.Test.Data;

using DependentCore = Express.Dependent;

internal class Dependent {
    public static readonly Dependent Instance = new(); 
    public readonly PropertyNames Names = new PropertyNames();

    public class PropertyNames {
        public string Events => nameof(DependentCore.Events);
        public string FirstNameChangedEvent => nameof(DependentCore.Event.FirstNameChanged);
        public string LastNameChangedEvent => nameof(DependentCore.Event.LastNameChanged);
    }
}

internal static class DependentExtensions {
    public static Dependent Dependent(this EmmaData data) {
        return Data.Dependent.Instance;
    }
}

