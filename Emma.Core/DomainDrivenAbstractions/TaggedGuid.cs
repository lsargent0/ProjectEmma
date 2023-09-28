using CSharpFunctionalExtensions;
using OneOf;

namespace Emma.Core.DomainDrivenAbstractions;

public abstract record TaggedGuid(Guid Guid, string Tag) {
    private const string Separator = "!@!";
    public override string ToString() {
        return $"{Guid}{Separator}{Tag}";
    }
    public static Func<string, string?> DefaultTagValidator(string tagName) {
        return t => t != tagName ? $"Expected {tagName} tag. There is no matching {tagName} for this guid" : null;
    }
    public static Result<ParseResult.ParsedGuid, Error> Parse(string value, Func<string, string?> tagValidator) {
        var result = ParseValue(value, tagValidator);
        return result.Match(
            parsed => parsed,
            Result.Failure<ParseResult.ParsedGuid, Error>,
            Result.Failure<ParseResult.ParsedGuid, Error>,
            Result.Failure<ParseResult.ParsedGuid, Error>
        );
    }
    protected static ParseResult ParseValue(string value, Func<string, string?> tagValidator) {
        var split = value.Split(Separator, StringSplitOptions.TrimEntries);
        if (split.Length != 2) {
            return new ParseResult.MissingTag(value);
        }
        var tag = split[1];
        if (tagValidator(tag) is { } validationMessage) {
            return new ParseResult.InvalidTag(validationMessage);
        }
        if (!Guid.TryParse(split[0], out var guid)) {
            return new ParseResult.InvalidGuid(split[0]);
        }
        return new ParseResult.ParsedGuid(guid, tag);
    }
    public class ParseResult : OneOfBase<ParseResult.ParsedGuid, ParseResult.MissingTag, ParseResult.InvalidGuid, ParseResult.InvalidTag> {
        public record ParsedGuid(Guid Guid, string Tag);
        public record MissingTag(string Value) : Error($"{Value} does not contain the correct tag");
        public record InvalidGuid(string Value) : Error($"{Value} could not be parsed into a valid Guid");
        public record InvalidTag(string ValidationMessage) : Error($"Invalid Tag: {ValidationMessage}");
        protected ParseResult(OneOf<ParsedGuid, MissingTag, InvalidGuid, InvalidTag> input) : base(input) {
        }
        public static implicit operator ParseResult(ParsedGuid _) => new(_);
        public static implicit operator ParseResult(MissingTag _) => new(_);
        public static implicit operator ParseResult(InvalidGuid _) => new(_);
        public static implicit operator ParseResult(InvalidTag _) => new(_);
    }
}