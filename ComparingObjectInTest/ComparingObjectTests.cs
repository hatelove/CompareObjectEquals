using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

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
    }

    internal class Person //Person didn't override Equals
    {
        public string Name { get; set; }

        public int Age { get; set; }

        public int Id { get; set; }

        public DateTime Birthday { get; set; }

        public Order Order { get; set; }
    }

    internal class Order : IEquatable<Order>
    {
        public int Price { get; set; }

        public int Id { get; set; }

        // remind: when you override Equals(), you should override GetHashCode() too.
        public override bool Equals(object obj)
        {
            var order = obj as Order;
            if (order != null)
            {
                return this.Equals(order);
            }

            return false;
        }
        public bool Equals(Order other)
        {
            //define Equals of Order type between two Order instances
            return this.Id == other.Id && this.Price == other.Price;
        }
    }
}