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
    }
}