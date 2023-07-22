﻿using System;
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
                default:
                    break;
            }

            if (item.Name != "Aged Brie" && item.Name != "Backstage passes to a TAFKAL80ETC concert")
            {
                if (item.Quality > 0)
                {
                    item.Quality--;
                }
            }
            else
            {
                if (item.Quality < 50)
                {
                    item.Quality++;

                    if (item.Name == "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.SellIn < 11)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }

                        if (item.SellIn < 6)
                        {
                            if (item.Quality < 50)
                            {
                                item.Quality++;
                            }
                        }
                    }
                }
            }

            item.SellIn--;

            if (item.SellIn < 0)
            {
                if (item.Name != "Aged Brie")
                {
                    if (item.Name != "Backstage passes to a TAFKAL80ETC concert")
                    {
                        if (item.Quality > 0)
                        {
                            item.Quality--;
                        }
                    }
                    else
                    {
                        item.Quality -= item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        item.Quality++;
                    }
                }
            }
        }

        private static void UpdateAgedBrieQuality(Item item)
        {
            item.SellIn--;
            item.Quality = Math.Min(item.Quality + 1, MAX_QUALITY);
        }

        private static void UpdateLegendaryQuality(Item _) { }

        private static ItemType GetItemType(string name)
        {
            if (name.StartsWith("Sulfuras")) return ItemType.Legendary;
            if (name.StartsWith("Aged Brie")) return ItemType.AgedBrie;
            return ItemType.Regular;
        }
    }
}