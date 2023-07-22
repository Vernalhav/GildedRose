using GildedRose.Console;
using GildedRose.Domain;
using System.Collections.Generic;
using Xunit;

namespace GildedRose.Tests
{
    public class TestAssemblyTests
    {
        [Fact]
        public void TestQualityDegradesByOneBeforeSellDate()
        {
            Item given = new Item { Name = "A", Quality = 10, SellIn = 1 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(9, received.Quality);
        }

        [Fact]
        public void TestMultipleItemsDegradeBeforeSellDate()
        {
            Item given1 = new Item { Name = "A", Quality = 10, SellIn = 1 };
            Item given2 = new Item { Name = "B", Quality = 5, SellIn = 1 };
            var app = new Program
            {
                Items = new List<Item> { given1, given2 }
            };

            app.UpdateQuality();

            var received1 = app.Items[0];
            var received2 = app.Items[1];
            Assert.Equal(9, received1.Quality);
            Assert.Equal(4, received2.Quality);
        }

        [Fact]
        public void TestSingleItemValueIsNotNegative()
        {
            Item given = new Item { Name = "A", Quality = 0, SellIn = 1 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(0, received.Quality);
        }
        
        [Fact]
        public void TestQualityDegradesFasterAfterSellDate()
        {
            Item given = new Item { Name = "A", Quality = 10, SellIn = 0 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(8, received.Quality);
        }

        [Fact]
        public void TestSellInDateReducesByOneAfterUpdate()
        {
            Item given = new Item { Name = "A", Quality = 10, SellIn = 3 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(2, received.SellIn);
        }
        
        [Fact]
        public void TestAgedBrieIncreasesInQuality()
        {
            Item given = new Item { Name = "Aged Brie", Quality = 10, SellIn = 3 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.True(received.Quality > 10);
        }     
        
        [Fact]
        public void TestAgedBrieQualityDoesNotExceedMax()
        {
            Item given = new Item { Name = "Aged Brie", Quality = 50, SellIn = 3 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(50, received.Quality);
        }
        
        [Fact]
        public void TestSulfurasSellInDoesNotDecrease()
        {
            Item given = new Item { Name = "Sulfuras, Hand of Ragnaros", Quality = 50, SellIn = 5 };
            var app = new Program
            {
                Items = new List<Item> { given }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(5, received.SellIn);
        }

        [Fact]
        public void TestPassQualityIncreasesByOneWhenFarFromConcert()
        {
            Item pass = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 15 };
            var app = new Program
            {
                Items = new List<Item> { pass }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(11, received.Quality);
        }

        [Fact]
        public void TestPassQualityIncreasesByTwoTenDaysAwayFromConcert()
        {
            Item pass = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 10 };
            var app = new Program
            {
                Items = new List<Item> { pass }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(12, received.Quality);
        }

        [Fact]
        public void TestPassQualityIncreasesByThreeFiveDaysAwayFromConcert()
        {
            Item pass = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 5 };
            var app = new Program
            {
                Items = new List<Item> { pass }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(13, received.Quality);
        }

        [Fact]
        public void TestPassQualityDropsAfterConcert()
        {
            Item pass = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 0 };
            var app = new Program
            {
                Items = new List<Item> { pass }
            };

            app.UpdateQuality();

            var received = app.Items[0];
            Assert.Equal(0, received.Quality);
        }
    }
}