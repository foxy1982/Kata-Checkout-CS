using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace Kata_Checkout_CS
{
    class Program
    {
        static void Main(string[] args)
        {
        }
    }

    class Checkout
    {
        private readonly IDictionary<string, int> _stockToPriceMap;
        private int _total;

        public Checkout(IDictionary<string, int> stockToPriceMap)
        {
            _stockToPriceMap = stockToPriceMap;
        }

        internal int GetTotalPrice()
        {
            return _total;
        }

        internal void Scan(string item)
        {
            _total += _stockToPriceMap[item];
        }
    }

    [TestFixture]
    class Test
    {
        [Test]
        public void ShouldReturn50WhenScanning1A()
        {
            var map = new Dictionary<string, int>
            {
                {"A", 50}
            };

            var checkout = new Checkout(map);
            checkout.Scan("A");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(50));
        }

        [Test]
        public void ShouldReturn80WhenScanning1AAnd1B()
        {
            var map = new Dictionary<string, int>
            {
                {"A", 50},
                {"B", 30}
            };

            var checkout = new Checkout(map);
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(80));
        }
    }
}
