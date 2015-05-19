using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using ExpectedObjects;

namespace ComparingObjectInTest
{
    [TestClass]
    public class ComparingObjectTests
    {
        [TestMethod]
        public void Test_Order_Equals_by_Assert_Equals()
        {
            var expected = new Order
            {
                Id = 1,
                Price = 10,
            };

            var actual = new Order
            {
                Id = 1,
                Price = 20,
            };

            //when actual is different from expected, we can't know what's different from test failed message; Id? or Price? we don't know.
            //when we need different definition of Equal between two Order instances in the other scenario or test case, we can't modify the Order Equals() dynamicly, becauses of that's production code; Remember, never modify your production design for testing.
            //lack of flexibility and hard to extend by need of 
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Person_Equals_Flat_all_properties_by_Assert_Equals()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            //flat all properties is a general solution for verify two instances were equal, but it wasted developers too much time and copy/paste sometimes makes mistakes; 
            //in addition, if you need to compare two collection of Person, you need design a for loop in testing; it obscured scenario meanings.            
            Assert.AreEqual(expected.Id, actual.Id);
            Assert.AreEqual(expected.Name, actual.Name);
            Assert.AreEqual(expected.Age, actual.Age);
        }

        [TestMethod]
        public void Test_Person_Equals_with_AnonymousType()
        {
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            //project expected Person to anonymous type
            var expectedAnonymous = new
            {
                Id = expected.Id,
                Name = expected.Name,
                Age = expected.Age
            };

            //project actual Person to anonymous type
            var actualAnonymous = new
            {
                Id = actual.Id,
                Name = actual.Name,
                Age = actual.Age,
            };

            //because of anonymous type comparing equals by value of each property, it saves time of overriding Equals() and provides flexibility for testing
            Assert.AreEqual(expectedAnonymous, actualAnonymous);
        }

        [TestMethod]
        public void Test_PersonCollection_Equals_with_AnonymousType_by_CollectionAssert()
        {
            //project collection from List<Person> to List<AnonymousType> by Select()
            var expected = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="C",Age=30},
            }.Select(x => new { Id = x.Id, Name = x.Name, Age = x.Age }).ToList();

            //project collection from List<Person> to List<AnonymousType> by Select()
            var actual = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="C",Age=30},
            }.Select(x => new { Id = x.Id, Name = x.Name, Age = x.Age }).ToList();

            //using CollectionAssert to iterate comparing each element from two collection, each anonymous instance will compare value of property
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void Test_Person_Equals_with_ExpectedObjects()
        {
            //use extension method ToExpectedObject() from using ExpectedObjects namespace to project Person to ExpectedObject
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
            };

            //use ShouldEqual to compare expected and actual instance, if they are not equal, it will throw a System.Exception and its message includes what properties were not match our expectation.
            expected.ShouldEqual(actual);
        }

        [TestMethod]
        public void Test_PersonCollection_Equals_with_ExpectedObjects()
        {
            //collection just invoke extension method: ToExpectedObject() to project Collection<Person> to ExpectedObject too
            var expected = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="C",Age=30},
            }.ToExpectedObject();

            var actual = new List<Person>
            {
                new Person { Id=1, Name="A",Age=10},
                new Person { Id=2, Name="B",Age=20},
                new Person { Id=3, Name="C",Age=30},
            };

            expected.ShouldEqual(actual);
        }

        [TestMethod]
        public void Test_ComposedPerson_Equals_with_ExpectedObjects()
        {
            //ExpectedObject will compare each value of property recursively, so composed type also simply compare equals.
            var expected = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order { Id = 91, Price = 910 },
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "A",
                Age = 10,
                Order = new Order { Id = 91, Price = 910 },
            };

            expected.ShouldEqual(actual);
        }

        [TestMethod]
        public void Test_PartialCompare_Person_Equals_with_ExpectedObjects()
        {            
            //when partial comparing, you need to use anonymous type too. Because only anonymous type can dynamic define only a few properties should be assign.
            var expected = new 
            {
                Id = 1,                
                Age = 10,
                Order = new { Id = 91}, // composed type should be used anonymous type too, only compare properties. If you trace ExpectedObjects's source code, you will find it invoke config.IgnoreType() first.
            }.ToExpectedObject();

            var actual = new Person
            {
                Id = 1,
                Name = "B",
                Age = 10,
                Order = new Order { Id = 91, Price = 910 },
            };


            // partial comparing use ShouldMatch(), rather than ShouldEqual()
            expected.ShouldMatch(actual);
        }
    }

    internal class Person //Person didn't override Equals
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }

        public DateTime Birthday { get; set; }

        public Order Order { get; set; }
    }

    //using ExpectedObjects, you needn't override Equals.
    internal class Order
    {
        public int Price { get; set; }

        public int Id { get; set; }        
    }
}