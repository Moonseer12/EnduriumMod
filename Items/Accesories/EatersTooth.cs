using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class EatersTooth : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 2, 50, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Eater's Tooth");
            Tooltip.SetDefault("Getting struck releases cursed inferno");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
          ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).EatersBreath = true;
        }
    }
}
