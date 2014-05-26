using System.Collections.Generic;
using System.Linq;
using NUnit.Framework;

namespace Kata_Checkout_CS
{
    internal class Program
    {
        private static void Main()
        {
        }
    }

    public class Checkout
    {
        private readonly List<string> _scannedItems = new List<string>();
        private readonly SpecialPrice _specialPrice;
        private readonly IDictionary<string, int> _stockToPriceMap;
        private int _total;

        public Checkout(IDictionary<string, int> stockToPriceMap, SpecialPrice specialPrice)
        {
            _stockToPriceMap = stockToPriceMap;
            _specialPrice = specialPrice;
        }

        internal int GetTotalPrice()
        {
            if (_specialPrice != null && _specialPrice.Matches(_scannedItems))
            {
                return _specialPrice.Price;
            }

            return _total;
        }

        internal void Scan(string item)
        {
            _scannedItems.Add(item);
            _total += _stockToPriceMap[item];
        }
    }

    public class SpecialPrice
    {
        private readonly int _count;
        private readonly string _sku;

        public SpecialPrice(string sku, int count, int price)
        {
            _sku = sku;
            _count = count;
            Price = price;
        }

        public int Price { get; private set; }

        internal bool Matches(List<string> scannedItems)
        {
            return scannedItems.Count(o => o == _sku) == _count;
        }
    }

    [TestFixture]
    internal class Test
    {
        [Test]
        public void ShouldReturn130WhenScanning3As()
        {
            var map = new Dictionary<string, int>
            {
                {"A", 50}
            };

            var specialPriceProvider = new SpecialPrice("A", 3, 130);

            var checkout = new Checkout(map, specialPriceProvider);
            checkout.Scan("A");
            checkout.Scan("A");
            checkout.Scan("A");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(130));
        }

        [Test]
        public void ShouldReturn45WhenScanning2Bs()
        {
            var map = new Dictionary<string, int>
            {
                {"B", 30}
            };

            var specialPriceProvider = new SpecialPrice("B", 2, 45);

            var checkout = new Checkout(map, specialPriceProvider);
            checkout.Scan("B");
            checkout.Scan("B");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(45));
        }

        [Test]
        public void ShouldReturn50WhenScanning1A()
        {
            var map = new Dictionary<string, int>
            {
                {"A", 50}
            };

            var checkout = new Checkout(map, null);
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

            var checkout = new Checkout(map, null);
            checkout.Scan("A");
            checkout.Scan("B");

            Assert.That(checkout.GetTotalPrice(), Is.EqualTo(80));
        }
    }
}