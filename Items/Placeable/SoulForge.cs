﻿using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Placeable
{
    public class SoulForge : ModItem
    {
        public override void SetDefaults()
        {
            item.width = 36;
            item.height = 22;
            item.useTurn = true;
            item.autoReuse = true;
            item.useAnimation = 10;
            item.useTime = 10;
            item.useStyle = 1;
            item.rare = 6;
            item.consumable = true;
            item.value = Terraria.Item.buyPrice(0, 25, 0, 0);
            item.createTile = mod.TileType("SoulForge"); //put your CustomBlock Tile name
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Soul Forge");
            Tooltip.SetDefault("Used to craft items using holy silver");
        }
    }
}