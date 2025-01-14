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
            var given = new Item { Name = "A", Quality = 10, SellIn = 1 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(9, received.Quality);
        }

        [Fact]
        public void TestMultipleItemsDegradeBeforeSellDate()
        {
            var given1 = new Item { Name = "A", Quality = 10, SellIn = 1 };
            var given2 = new Item { Name = "B", Quality = 5, SellIn = 1 };
            var items = new List<Item> { given1, given2 };
            Market.UpdateQuality(items);

            var received1 = items[0];
            var received2 = items[1];
            Assert.Equal(9, received1.Quality);
            Assert.Equal(4, received2.Quality);
        }

        [Fact]
        public void TestSingleItemValueIsNotNegative()
        {
            var given = new Item { Name = "A", Quality = 0, SellIn = 1 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(0, received.Quality);
        }

        [Fact]
        public void TestQualityDegradesFasterAfterSellDate()
        {
            var given = new Item { Name = "A", Quality = 10, SellIn = 0 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(8, received.Quality);
        }

        [Fact]
        public void TestSellInDateReducesByOneAfterUpdate()
        {
            var given = new Item { Name = "A", Quality = 10, SellIn = 3 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(2, received.SellIn);
        }

        [Fact]
        public void TestAgedBrieIncreasesInQuality()
        {
            var given = new Item { Name = "Aged Brie", Quality = 10, SellIn = 3 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.True(received.Quality > 10);
        }

        [Fact]
        public void TestAgedBrieQualityDoesNotExceedMax()
        {
            var given = new Item { Name = "Aged Brie", Quality = 50, SellIn = 3 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(50, received.Quality);
        }

        [Fact]
        public void TestSulfurasSellInDoesNotDecrease()
        {
            var given = new Item { Name = "Sulfuras, Hand of Ragnaros", Quality = 50, SellIn = 5 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(5, received.SellIn);
        }

        [Fact]
        public void TestPassQualityIncreasesByOneWhenFarFromConcert()
        {
            var given = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 15 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(11, received.Quality);
        }

        [Fact]
        public void TestPassQualityIncreasesByTwoTenDaysAwayFromConcert()
        {
            var given = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 10 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(12, received.Quality);
        }

        [Fact]
        public void TestPassQualityIncreasesByThreeFiveDaysAwayFromConcert()
        {
            var given = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 5 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(13, received.Quality);
        }

        [Fact]
        public void TestPassQualityDoesntExceedMax()
        {
            var given = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 48, SellIn = 5 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(50, received.Quality);
        }

        [Fact]
        public void TestPassQualityDropsAfterConcert()
        {
            var given = new Item { Name = "Backstage passes to a TAFKAL80ETC concert", Quality = 10, SellIn = 0 };
            Market.UpdateQuality(given);

            var received = given;
            Assert.Equal(0, received.Quality);
        }
    }
}