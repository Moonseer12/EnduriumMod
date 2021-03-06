using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class GigantAmber : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 50;
            item.height = 57;
            item.maxStack = 999;
            item.value = Terraria.Item.sellPrice(0, 0, 0, 0);
            item.rare = -12;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gigant Amber");
            Tooltip.SetDefault("'The seeker might be interested in this'");
        }
    }
}