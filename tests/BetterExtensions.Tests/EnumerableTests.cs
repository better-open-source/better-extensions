using System.Collections.Generic;
using System.Linq;
using BetterExtensions.Collections;
using FluentAssertions;
using NUnit.Framework;

namespace BetterExtensions.Tests
{
    public class EnumerableTests
    {
        private IEnumerable<string> _data;
        
        [SetUp]
        public void Setup()
        {
            _data = new List<string> {"one", "two", "three"};
        }

        [Test]
        public void AppendWith_WhenConditionTrue_ShouldContain()
        {
            const bool condition = true;

            var result = _data.AppendWith("four", condition).ToList();

            result.Should().Contain("four");
        }

        [Test]
        public void AppendWith_WhenConditionFalse_ShouldNotContain()
        {
            const bool condition = false;
            
            var result = _data.AppendWith("four", condition).ToList();

            result.Should().NotContain("four");
        }

        [Test]
        public void WhereNotNull_WhenContainNull_ShouldNotContainNull()
        {
            var data = _data.Append(null);

            var result = data.WhereNotNull();

            result.All(value => value != null).Should().BeTrue();
        }
    }
}