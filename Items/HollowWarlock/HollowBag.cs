using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.HollowWarlock
{
    public class HollowBag : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Treasure Bag");
            Tooltip.SetDefault("Right Click to open");
        }
        public override void SetDefaults()
        {
            item.maxStack = 999;
            item.consumable = true;
            item.width = 32;
            item.height = 32;
            item.rare = -12;
            bossBagNPC = mod.NPCType("TheKeeperofHollow2");
            item.expert = true;
        }
        public override bool CanRightClick()
        {
            return true;
        }

        public override void OpenBossBag(Player player)
        {
            player.TryGettingDevArmor();
            player.TryGettingDevArmor();
            player.QuickSpawnItem(mod.ItemType("GleamingCrag"), Main.rand.Next(15, 25));
            player.QuickSpawnItem(mod.ItemType("ShardofNight"));
            int choice = Main.rand.Next(4);
            if (choice == 0)
            {
                player.QuickSpawnItem(mod.ItemType("StormSword"));
            }

            if (choice == 1)
            {
                player.QuickSpawnItem(mod.ItemType("TheNightfall"));
            }

            if (choice == 2)
            {
                player.QuickSpawnItem(mod.ItemType("StarFlowerStaff"));
            }

            if (choice == 3)
            {
                player.QuickSpawnItem(mod.ItemType("FrostSlash"));
            }
        }
    }
}
