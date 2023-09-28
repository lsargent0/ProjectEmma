using System.Collections.Immutable;
using CommunityToolkit.Diagnostics;
using CSharpFunctionalExtensions;

namespace Emma.Core.DomainDrivenAbstractions;

public abstract record Error {
    private IEnumerable<Error> _innerErrors;
    public string Message { get; init; }
    public IEnumerable<Error> InnerErrors => _innerErrors.AsEnumerable(); 

    protected Error(string message, IEnumerable<Error>? innerErrors = null) {
        _innerErrors = innerErrors is not null ? innerErrors.ToList() : new List<Error>();
        Guard.IsNotNull(message);
        Guard.IsNotEmpty(message);
        Message = message;
    }

    public virtual T Include<T>(Error error) where T : Error {
        _innerErrors = _innerErrors.Append(error);
        return (T)this;
    }
}

public static class ErrorExtensions {
    public static Maybe<T> AggregateErrors<T>(this List<T> errors) where T : Error {
        if (!errors.Any()) {
            return Maybe<T>.None;
        }
        return errors.Skip(1).Aggregate(errors.First(), (error, include) => error.Include<T>(include));
    }
}