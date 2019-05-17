using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class MechanicalChip : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 24;

            item.value = Terraria.Item.buyPrice(0, 1, 0, 0);
            item.rare = 3;
            item.accessory = true;

        }

    public override void SetStaticDefaults()
    {
      DisplayName.SetDefault("Mechanical Chip");
      Tooltip.SetDefault("Increases melee damage and speed by 8%\n'It increases your muscle strength'");
    }

        public override void UpdateAccessory(Player player, bool hideVisual)  //this is so when the item is equipped will give this stats to the player
        {
            player.meleeDamage += 0.08f;
			player.meleeSpeed += 0.08f;
        }
    }
}
