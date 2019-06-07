using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.TheSpiritOfBloom
{
    public class BloomingEmblem : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 2, 50, 0);
            item.rare = 3;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Band");
            Tooltip.SetDefault("While you are poisoned or ");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        { 
            if (player.poisoned)
            {
                player.magicDamage += 0.1f;
                player.meleeDamage += 0.1f;
                player.minionDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.thrownDamage += 0.1f;
            }
            if (((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).ReaperNature)
            {
                player.magicDamage += 0.1f;
                player.meleeDamage += 0.1f;
                player.minionDamage += 0.1f;
                player.rangedDamage += 0.1f;
                player.thrownDamage += 0.1f;
            }
        }
    }
}
