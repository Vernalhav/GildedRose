using System;
using System.Collections.Generic;

namespace GildedRose.Domain
{
    public static class Market
    {
        private static readonly sbyte MAX_QUALITY = 50;

        private enum ItemType
        {
            Regular,
            Legendary,
            AgedBrie,
            ConcertPass,
        }

        public static void UpdateQuality(IList<Item> items)
        {
            foreach (var item in items)
                UpdateQuality(item);
        }

        public static void UpdateQuality(Item item)
        {
            var itemType = GetItemType(item.Name);
            switch (itemType)
            {
                case ItemType.Legendary:
                    UpdateLegendaryQuality(item);
                    return;
                case ItemType.AgedBrie:
                    UpdateAgedBrieQuality(item);
                    return;
                case ItemType.ConcertPass:
                    UpdateConcertPassQuality(item);
                    return;
                default:
                    break;
            }

            if (item.Quality > 0)
            {
                item.Quality--;
            }
 
            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Quality > 0)
                {
                    item.Quality--;
                }
            }
        }

        private static void UpdateConcertPassQuality(Item item)
        {
            if (item.SellIn <= 0) item.Quality = 0;
            else if (item.SellIn <= 5) item.Quality += 3;
            else if (item.SellIn <= 10) item.Quality += 2;
            else item.Quality++;
            
            item.Quality = Math.Min(item.Quality, MAX_QUALITY);

            item.SellIn--;
        }

        private static void UpdateAgedBrieQuality(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Min(item.Quality + 1, MAX_QUALITY);
        }

        private static void UpdateLegendaryQuality(Item _) { }

        private static ItemType GetItemType(string name)
        {
            var cmp = StringComparison.OrdinalIgnoreCase;
            if (name.StartsWith("Sulfuras", cmp)) return ItemType.Legendary;
            if (name.StartsWith("Aged Brie", cmp)) return ItemType.AgedBrie;
            if (name.ToLower().Contains("pass")) return ItemType.ConcertPass;
            return ItemType.Regular;
        }
    }
}