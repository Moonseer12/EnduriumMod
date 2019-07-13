using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class GleamingCrag : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 26;
            item.height = 26;
            item.maxStack = 99;
            item.value = Terraria.Item.sellPrice(0, 0, 5, 0);
            item.rare = 6;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Gleaming Crag");
            Tooltip.SetDefault("'Contains Energy from the keeper of hollow'");
        }
    }
}