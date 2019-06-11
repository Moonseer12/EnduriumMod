using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
namespace EnduriumMod.Items.Weapons.HolySilver
{
    public class AurelianRifle : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aurelian Rifle");
            Tooltip.SetDefault("Fires light piercing bolts of energy");
        }
        public override void SetDefaults()
        {
            item.damage = 50;
            item.ranged = true;
            item.width = 66;
            item.height = 34;
            item.useAnimation = 24;
            item.useTime = 8;
            item.UseSound = SoundID.Item36;
            item.reuseDelay = 10;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4;
            item.value = 10000;
            item.useTurn = false;
            item.rare = 5;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("NeoniteRifle");
            item.shootSpeed = 12f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(197);
            recipe.AddIngredient(null, ("StarCrystal"), 5);
            recipe.AddIngredient(null, ("HolySilver"), 5);
            recipe.AddTile(null, "SoulForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(0, 3);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            return true;
        }
    }
}
