using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.HolySilver
{
    public class Archeon45 : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Archeon-45");
            Tooltip.SetDefault("Fires red projectiles\nProperties of the projectiles might fluctuate");
        }
        public override void SetDefaults()
        {
            item.damage = 46;
            item.ranged = true;
            item.width = 72;
            item.height = 32;
            item.useAnimation = 12;
            item.useTime = 6;
            item.reuseDelay = 11;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Terraria.Item.buyPrice(0, 25, 0, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item11;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(175, 20);
            recipe.AddIngredient(117, 25);
            recipe.AddIngredient(1332, 12);
            recipe.AddIngredient(null, ("StarCrystal"), 5);
            recipe.AddIngredient(null, ("HolySilver"), 5);
            recipe.AddTile(null, "SoulForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public int Skiddadle = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            Skiddadle += 1;
            if (Skiddadle >= 2)
            {
                Skiddadle = 0;
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X * 0.5f, perturbedSpeed.Y * 0.5f, mod.ProjectileType("Archeon45"), damage, knockBack, player.whoAmI);

                return false;
            }
            return true;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-3, 0);
        }
    }
}