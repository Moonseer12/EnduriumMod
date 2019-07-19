using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class PlanetarShard : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 50;
            item.height = 57;
            item.maxStack = 99;
            item.value = Terraria.Item.sellPrice(0, 1, 0, 0);
            item.rare = 8;
            item.value = Terraria.Item.buyPrice(0, 0, 5, 0);
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Planet Fragment");
            Tooltip.SetDefault("");
        }
    }
}
