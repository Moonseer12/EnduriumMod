﻿using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.BloodFang
{
    public class BloodKnive : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 16;
            item.thrown = true;
            item.noMelee = true;
            item.width = 14;
            item.height = 36;
            item.useTime = 26;
            item.crit = 35;
            item.useAnimation = 26;
            item.useStyle = 1;
            item.knockBack = 9;
            item.value = Terraria.Item.buyPrice(0, 0, 4, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BloodKnive");
            item.shootSpeed = 12f;
            item.useTurn = true;
            item.maxStack = 999;
            item.consumable = true;
            item.noUseGraphic = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Clot Knive");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("BloodFangCore"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 75);
            recipe.AddRecipe();
        }
    }
}
