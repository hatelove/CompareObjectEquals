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

            Assert.AreEqual(expected, actual); //this would be failed because of "Order" is a reference type; if Order didn't override Equals(), AreEqual() will invoke Object.Equals()
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

    internal class Order
    {
        public int Price { get; set; }

        public int Id { get; set; }
    }
}