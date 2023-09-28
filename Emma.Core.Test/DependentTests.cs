using System.Reactive.Linq;
using System.Text;
using Emma.Core.LanguageEnhancements;
using Emma.Core.Test.Data;
using Emma.Core.Test.Data.Dependents;
using Dependent = Emma.Core.Express.Dependent;

namespace Emma.Core.Test
{
    public class DependentTests {

        private readonly EmmaData _emmaData;
        private readonly Data.Dependent _data;

        public DependentTests() {
            _emmaData = new EmmaData();
            _data = _emmaData.Dependent();
        }

        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Has_first_name() {
            var emma = _data.Emma();
            var dependent = Dependent.FromScratch(emma.FirstName, emma.LastName).Value;
            Assert.That(dependent.FirstName, Is.EqualTo(emma.FirstName));
        }

        [Test]
        public void Has_last_name() {
            var emma = _data.Emma();
            var dependent = Dependent.FromScratch(emma.FirstName, emma.LastName).Value;
            Assert.That(dependent.LastName, Is.EqualTo(emma.LastName));
        }

        [Test]
        public void Can_change_last_name() {
            var emma = _data.Emma();
            var dependent = Dependent.FromScratch(emma.FirstName, emma.LastNameTypo).Value;
            dependent.ChangeLastName(emma.LastName);
            Assert.That(dependent.LastName, Is.EqualTo(emma.LastName));
            
        }

        [Test]
        public void Generates_last_name_changed_event() {
            var emma = _data.Emma();
            var dependent = Dependent.FromScratch(emma.FirstName, emma.LastNameTypo).Value;
            Assert.That(dependent.Events.Count(e => e.IsT2), Is.EqualTo(0), $"Unexpected number events. Expected no {_data.Names.LastNameChangedEvent} events.");
            dependent.ChangeLastName(emma.LastName);
            Assert.That(dependent.Events.Count(e => e.IsT2), Is.EqualTo(1), $"Unexpected number events. Expected a single {_data.Names.LastNameChangedEvent} event.");
            var @event = dependent.Events.First(e => e.IsT2).AsT2;
            var details = @event.Details;
            Assert.That(details.Previous, Is.EqualTo(emma.LastNameTypo));
            Assert.That(details.Current, Is.EqualTo(emma.LastName));
        }

        [Test]
        public void Construction_fails_with_empty_first_name() {
            var result = Dependent.FromScratch("", "Sargent");
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.InstanceOf(typeof(Dependent.Errors.Validation.EmptyFirstName)));
            Assert.That(result.Error is Dependent.Errors.Validation.EmptyFirstName name && !name.Message.IsEmpty());
        }

        [Test]
        public void Construction_fails_with_empty_last_name() {
            var result = Dependent.FromScratch("Sargent", "");
            Assert.That(result.IsFailure, Is.True);
            Assert.That(result.Error, Is.InstanceOf(typeof(Dependent.Errors.Validation.EmptyLastName)));
            Assert.That(result.Error is Dependent.Errors.Validation.EmptyLastName name && !name.Message.IsEmpty());
        }

        [Test]
        public void Sort_an_array_of_strings() {
            var list = new List<string> { "hello", "my", "name", "is", "bob", "what", "Is", "your", "name", "Or", "what", "do", "I", "call", "you" };
            var built1 = ConvertToMessage(list);
            var sortedList = list.Distinct().OrderBy(s => s);
            var built2 = ConvertToMessage(sortedList);
            var built3 = ConvertToMessage(sortedList.Where(s => s.StartsWith("y", StringComparison.InvariantCultureIgnoreCase)));
        }

        private string ConvertToMessage(IEnumerable<string> strings) {
            return strings.Aggregate(new StringBuilder(), (builder, s) => builder.Append($" {s}")).ToString().Trim();
        }
    }

    
}