using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class SpiritArrow : ModItem
    {

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Arrow");
            Tooltip.SetDefault("");
        }
        public override void SetDefaults()
        {
            item.damage = 10;
            item.ranged = true;
            item.width = 22;
            item.height = 36;
            item.maxStack = 999;
            item.consumable = true;
            item.knockBack = 1.5f;
            item.value = 1200;
            item.rare = 2;
            item.shoot = mod.ProjectileType("SpiritArrow");
            item.shootSpeed = 13f;
            item.ammo = 40;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("BloomlightBar"), 1);
            recipe.AddIngredient(null, ("TrueNatureEssence"), 2);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 150);
            recipe.AddRecipe();
        }
    }
}