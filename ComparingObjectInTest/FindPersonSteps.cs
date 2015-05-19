using System.Collections.Generic;
using TechTalk.SpecFlow;
//we need table's extension method of TechTalk.SpecFlow.Assist
using TechTalk.SpecFlow.Assist;
namespace ComparingObjectInTest
{
    [Binding]
    public class FindPersonSteps
    {
        [When(@"I got a acutal person")]
        public void WhenIGotAAcutalPerson(Table table)
        {
            var actual = table.CreateInstance<Person>();
            ScenarioContext.Current.Set<Person>(actual);
        }

        [Then(@"I hope actual person should be equal to expected person")]
        public void ThenIHopeActualPersonShouldBeEqualToExpectedPerson(Table expected)
        {
            var actual = ScenarioContext.Current.Get<Person>();
            expected.CompareToInstance<Person>(actual);
        }

        [When(@"I got a actual person collection")]
        public void WhenIGotAActualPersonCollection(Table table)
        {
            var actual = table.CreateSet<Person>();
            ScenarioContext.Current.Set<IEnumerable<Person>>(actual);
        }

        [Then(@"I hope actual person collection should be equal to expected person collection")]
        public void ThenIHopeActualPersonCollectionShouldBeEqualToExpectedPersonCollection(Table expected)
        {
            var actual = ScenarioContext.Current.Get<IEnumerable<Person>>();
            expected.CompareToSet<Person>(actual);
        }

    }
}