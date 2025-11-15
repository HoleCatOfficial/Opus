using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OpusLib.Content.Helpers
{
    public struct ChestID
    {
        public int TileType;
        public int Style; // style = frameX / 36

        public ChestID(int tileType, int style)
        {
            TileType = tileType;
            Style = style;
        }

        public bool Matches(Tile tile)
        {
            return tile.TileType == TileType && tile.TileFrameX == Style * 36;
        }

        // Common vanilla chests
        public static readonly ChestID Wooden = new(TileID.Containers, 0);
        public static readonly ChestID Gold   = new(TileID.Containers, 1);
        public static readonly ChestID GoldLocked  = new(TileID.Containers, 2);
        public static readonly ChestID Shadow   = new(TileID.Containers, 3);
        public static readonly ChestID ShadowLocked   = new(TileID.Containers, 4);
        public static readonly ChestID Barrel   = new(TileID.Containers, 5);
        public static readonly ChestID TrashCan   = new(TileID.Containers, 6);
        public static readonly ChestID Ebonwood  = new(TileID.Containers, 7);
        public static readonly ChestID RichMahogany   = new(TileID.Containers, 8);
        public static readonly ChestID Pearlwood   = new(TileID.Containers, 9);
        public static readonly ChestID Ivy = new(TileID.Containers, 10);
        public static readonly ChestID Frozen = new(TileID.Containers, 11);
        public static readonly ChestID LivingWood = new(TileID.Containers, 12);
        public static readonly ChestID SkyChest = new(TileID.Containers, 13);
        public static readonly ChestID Shadewood = new(TileID.Containers, 14);
        public static readonly ChestID Webbed = new(TileID.Containers, 15);
        public static readonly ChestID Lihzahrd = new(TileID.Containers, 16);
        public static readonly ChestID Water = new(TileID.Containers, 17);
        public static readonly ChestID DungeonJungle = new(TileID.Containers, 18);
        public static readonly ChestID DungeonCorrupt = new(TileID.Containers, 19);
        public static readonly ChestID DungeonCrimson = new(TileID.Containers, 20);
        public static readonly ChestID DungeonHallow = new(TileID.Containers, 21);
        public static readonly ChestID DungeonIce = new(TileID.Containers, 22);
        public static readonly ChestID DungeonJungleLocked = new(TileID.Containers, 23);
        public static readonly ChestID DungeonCorruptLocked = new(TileID.Containers, 24);
        public static readonly ChestID DungeonCrimsonLocked = new(TileID.Containers, 25);
        public static readonly ChestID DungeonHallowLocked = new(TileID.Containers, 26);
        public static readonly ChestID DungeonIceLocked = new(TileID.Containers, 27);
        public static readonly ChestID Dynasty = new(TileID.Containers, 28);
        public static readonly ChestID Honey = new(TileID.Containers, 29);
        public static readonly ChestID Steampunk = new(TileID.Containers, 30);
        public static readonly ChestID PalmWood = new(TileID.Containers, 31);
        public static readonly ChestID Mushroom = new(TileID.Containers, 32);
        public static readonly ChestID BorealWood = new(TileID.Containers, 33);
        public static readonly ChestID Slime = new(TileID.Containers, 34);
        public static readonly ChestID DungeonBrickGreen = new(TileID.Containers, 35);
        public static readonly ChestID DungeonBrickGreenLocked = new(TileID.Containers, 36);
        public static readonly ChestID DungeonBrickPink = new(TileID.Containers, 37);
        public static readonly ChestID DungeonBrickPinkLocked = new(TileID.Containers, 38);
        public static readonly ChestID DungeonBrickBlue = new(TileID.Containers, 39);
        public static readonly ChestID DungeonBrickBlueLocked = new(TileID.Containers, 40);
        public static readonly ChestID Bone = new(TileID.Containers, 41);
        public static readonly ChestID Cactus = new(TileID.Containers, 42);
        public static readonly ChestID Flesh = new(TileID.Containers, 43);
        /// <summary>
        /// Does not contain Hell Loot. Use ChestID.Shadow or ChestID.ShadowLocked for that.
        /// </summary>
        public static readonly ChestID Obsidian = new(TileID.Containers, 44);
        public static readonly ChestID Pumpkin = new(TileID.Containers, 45);
        public static readonly ChestID Spooky = new(TileID.Containers, 46);
        public static readonly ChestID Glass = new(TileID.Containers, 47);
        public static readonly ChestID MartianChest = new(TileID.Containers, 48);
        public static readonly ChestID Meteorite = new(TileID.Containers, 49);
        public static readonly ChestID Granite = new(TileID.Containers, 50);
        public static readonly ChestID Marble = new(TileID.Containers, 51);
        public static readonly ChestID Crystal = new(TileID.Containers2, 0);
        /// <summary>
        /// Golden Chests are from the pirate invasion. For regular chests in the dungeon, use ChestID.Gold.
        /// </summary>
        public static readonly ChestID Golden = new(TileID.Containers2, 1);
        public static readonly ChestID Spider = new(TileID.Containers2, 2);
        public static readonly ChestID Lesion = new(TileID.Containers2, 3);
        public static readonly ChestID DeadMansChest = new(TileID.Containers2, 4);
        public static readonly ChestID Solar = new(TileID.Containers2, 5);
        public static readonly ChestID Vortex = new(TileID.Containers2, 6);
        public static readonly ChestID Nebula = new(TileID.Containers2, 7);
        public static readonly ChestID Stardust = new(TileID.Containers2, 8);
        public static readonly ChestID Golf = new(TileID.Containers2, 9);
        public static readonly ChestID Sandstone = new(TileID.Containers2, 10);
        public static readonly ChestID Bamboo = new(TileID.Containers2, 11);
        public static readonly ChestID DungeonDesert = new(TileID.Containers2, 12);
        public static readonly ChestID DungeonDesertLocked = new(TileID.Containers2, 13);
        public static readonly ChestID Reef = new(TileID.Containers2, 14);
        public static readonly ChestID Balloon = new(TileID.Containers2, 15);
        public static readonly ChestID AshWood = new(TileID.Containers2, 16);

        /// Trapped
        
        public static readonly ChestID TrappedTrappedWooden = new(TileID.FakeContainers, 0);
        public static readonly ChestID TrappedTrappedGold   = new(TileID.FakeContainers, 1);
        public static readonly ChestID TrappedTrappedGoldLocked  = new(TileID.FakeContainers, 2);
        public static readonly ChestID TrappedShadow   = new(TileID.FakeContainers, 3);
        public static readonly ChestID TrappedShadowLocked   = new(TileID.FakeContainers, 4);
        public static readonly ChestID TrappedBarrel   = new(TileID.FakeContainers, 5);
        public static readonly ChestID TrappedTrashCan   = new(TileID.FakeContainers, 6);
        public static readonly ChestID TrappedEbonwood  = new(TileID.FakeContainers, 7);
        public static readonly ChestID TrappedRichMahogany   = new(TileID.FakeContainers, 8);
        public static readonly ChestID TrappedPearlwood   = new(TileID.FakeContainers, 9);
        public static readonly ChestID TrappedIvy = new(TileID.FakeContainers, 10);
        public static readonly ChestID TrappedFrozen = new(TileID.FakeContainers, 11);
        public static readonly ChestID TrappedLivingWood = new(TileID.FakeContainers, 12);
        public static readonly ChestID TrappedSkyChest = new(TileID.FakeContainers, 13);
        public static readonly ChestID TrappedShadewood = new(TileID.FakeContainers, 14);
        public static readonly ChestID TrappedWebbed = new(TileID.FakeContainers, 15);
        public static readonly ChestID TrappedLihzahrd = new(TileID.FakeContainers, 16);
        public static readonly ChestID TrappedWater = new(TileID.FakeContainers, 17);
        public static readonly ChestID TrappedDungeonJungle = new(TileID.FakeContainers, 18);
        public static readonly ChestID TrappedDungeonCorrupt = new(TileID.FakeContainers, 19);
        public static readonly ChestID TrappedDungeonCrimson = new(TileID.FakeContainers, 20);
        public static readonly ChestID TrappedDungeonHallow = new(TileID.FakeContainers, 21);
        public static readonly ChestID TrappedDungeonIce = new(TileID.FakeContainers, 22);
        public static readonly ChestID TrappedDungeonJungleLocked = new(TileID.FakeContainers, 23);
        public static readonly ChestID TrappedDungeonCorruptLocked = new(TileID.FakeContainers, 24);
        public static readonly ChestID TrappedDungeonCrimsonLocked = new(TileID.FakeContainers, 25);
        public static readonly ChestID TrappedDungeonHallowLocked = new(TileID.FakeContainers, 26);
        public static readonly ChestID TrappedDungeonIceLocked = new(TileID.FakeContainers, 27);
        public static readonly ChestID TrappedDynasty = new(TileID.FakeContainers, 28);
        public static readonly ChestID TrappedHoney = new(TileID.FakeContainers, 29);
        public static readonly ChestID TrappedSteampunk = new(TileID.FakeContainers, 30);
        public static readonly ChestID TrappedPalmWood = new(TileID.FakeContainers, 31);
        public static readonly ChestID TrappedMushroom = new(TileID.FakeContainers, 32);
        public static readonly ChestID TrappedBorealWood = new(TileID.FakeContainers, 33);
        public static readonly ChestID TrappedSlime = new(TileID.FakeContainers, 34);
        public static readonly ChestID TrappedDungeonBrickGreen = new(TileID.FakeContainers, 35);
        public static readonly ChestID TrappedDungeonBrickGreenLocked = new(TileID.FakeContainers, 36);
        public static readonly ChestID TrappedDungeonBrickPink = new(TileID.FakeContainers, 37);
        public static readonly ChestID TrappedDungeonBrickPinkLocked = new(TileID.FakeContainers, 38);
        public static readonly ChestID TrappedDungeonBrickBlue = new(TileID.FakeContainers, 39);
        public static readonly ChestID TrappedDungeonBrickBlueLocked = new(TileID.FakeContainers, 40);
        public static readonly ChestID TrappedBone = new(TileID.FakeContainers, 41);
        public static readonly ChestID TrappedCactus = new(TileID.FakeContainers, 42);
        public static readonly ChestID TrappedFlesh = new(TileID.FakeContainers, 43);
        /// <summary>
        /// Does not contain Hell Loot. Use ChestID.Shadow or ChestID.ShadowLocked for that.
        /// </summary>
        public static readonly ChestID TrappedObsidian = new(TileID.FakeContainers, 44);
        public static readonly ChestID TrappedPumpkin = new(TileID.FakeContainers, 45);
        public static readonly ChestID TrappedSpooky = new(TileID.FakeContainers, 46);
        public static readonly ChestID TrappedGlass = new(TileID.FakeContainers, 47);
        public static readonly ChestID TrappedMartianChest = new(TileID.FakeContainers, 48);
        public static readonly ChestID TrappedMeteorite = new(TileID.FakeContainers, 49);
        public static readonly ChestID TrappedGranite = new(TileID.FakeContainers, 50);
        public static readonly ChestID TrappedMarble = new(TileID.FakeContainers, 51);
        /// <summary>
        /// Golden Chests are from the pirate invasion. For regular chests in the dungeon, use ChestID.Gold.
        /// </summary>
        public static readonly ChestID TrappedGolden = new(TileID.FakeContainers2, 1);
        public static readonly ChestID TrappedSpider = new(TileID.FakeContainers2, 2);
        public static readonly ChestID TrappedLesion = new(TileID.FakeContainers2, 3);
        public static readonly ChestID TrappedDeadMansChest = new(TileID.FakeContainers2, 4);
        public static readonly ChestID TrappedSolar = new(TileID.FakeContainers2, 5);
        public static readonly ChestID TrappedVortex = new(TileID.FakeContainers2, 6);
        public static readonly ChestID TrappedNebula = new(TileID.FakeContainers2, 7);
        public static readonly ChestID TrappedStardust = new(TileID.FakeContainers2, 8);
        public static readonly ChestID TrappedGolf = new(TileID.FakeContainers2, 9);
        public static readonly ChestID TrappedSandstone = new(TileID.FakeContainers2, 10);
        public static readonly ChestID TrappedBamboo = new(TileID.FakeContainers2, 11);
        public static readonly ChestID TrappedDungeonDesert = new(TileID.FakeContainers2, 12);
        public static readonly ChestID TrappedDungeonDesertLocked = new(TileID.FakeContainers2, 13);
        public static readonly ChestID TrappedReef = new(TileID.FakeContainers2, 14);
        public static readonly ChestID TrappedBalloon = new(TileID.FakeContainers2, 15);
        public static readonly ChestID TrappedAshWood = new(TileID.FakeContainers2, 16);

        public static readonly ChestID TrappedIce    = TrappedFrozen; // alias
        // ...add as many as you want
    }

    public class ChestLootEntry
    {
        public ChestID ChestType;
        public int ItemType;
        public int Stack;
        public float Rarity; // 0–1 chance per chest

        public ChestLootEntry(ChestID chestType, int item, int stack, float rarity)
        {
            ChestType = chestType;
            ItemType = item;
            Stack = stack;
            Rarity = Math.Clamp(rarity, 0f, 1f);
        }
    }
    
    /// <summary>
    /// 
    /// </summary>
    public class ChestLootSystem : ModSystem
    {
        private static readonly List<ChestLootEntry> LootQueue = new();

        public static void RegisterChestLoot(ChestID chestID, int itemType, int stack = 1, float rarity = 1f)
        {
            LootQueue.Add(new ChestLootEntry(chestID, itemType, stack, rarity));
        }

        public override void PostWorldGen()
        {
            foreach (var entry in LootQueue)
            {
                AddLootToChests(entry);
            }
        }

        private void AddLootToChests(ChestLootEntry entry)
        {
            for (int i = 0; i < Main.maxChests; i++)
            {
                Chest chest = Main.chest[i];
                if (chest == null)
                    continue;

                Tile tile = Main.tile[chest.x, chest.y];
                if (!entry.ChestType.Matches(tile))
                    continue;

                if (WorldGen.genRand.NextFloat() > entry.Rarity)
                    continue;

                for (int slot = 0; slot < Chest.maxItems; slot++)
                {
                    if (chest.item[slot].type == ItemID.None)
                    {
                        chest.item[slot].SetDefaults(entry.ItemType);
                        chest.item[slot].stack = entry.Stack;
                        break;
                    }
                }
            }
        }
    }
}