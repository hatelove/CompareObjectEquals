using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
                Price = 10,
            };

            //this test will pass; when you override Equals(), AreEqual will invoke Order's Equals(), rather than Object's Equals()
            Assert.AreEqual(expected, actual); 
        }
    }

    internal class Person
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