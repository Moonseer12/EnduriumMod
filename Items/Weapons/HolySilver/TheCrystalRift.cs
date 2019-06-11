using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.HolySilver  
{
    public class TheCrystalRift : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 72;
            item.noMelee = true;
            item.ranged = true;
            item.width = 50;
            item.height = 68;
            item.useTime = 42;
            item.useAnimation = 42;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 1;
            item.value = 100000;
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 32f;

        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(502, 25);
            recipe.AddIngredient(1225, 16);
            recipe.AddIngredient(null, ("StarCrystal"), 5);
            recipe.AddIngredient(null, ("HolySilver"), 5);
            recipe.AddTile(null, "SoulForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Crystal Riftbow");
            Tooltip.SetDefault("Fires arrows and 2 rift shards");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            float num82 = (float)Main.mouseX + Main.screenPosition.X - position.X;
            float num83 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;

            float num121 = 0.314159274f;
            int num122 = 2;
            Vector2 vector15 = new Vector2(num82, num83);
            vector15.Normalize();
            vector15 *= 50f;
            bool flag13 = Collision.CanHit(position, 0, 0, position + vector15, 0, 0);
            int num2;
            int num002;
            for (int num123 = 0; num123 < num122; num123 = num2 + 1)
            {
                float num124 = (float)num123 - ((float)num122 - 1f) / 2f;
                Vector2 vector16 = vector15.RotatedBy((double)(num121 * num124), default(Vector2));
                if (!flag13)
                {
                    vector16 -= vector15;
                }
                int num125 = Projectile.NewProjectile(position.X + vector16.X, position.Y + vector16.Y, num82, num83, mod.ProjectileType("TheCrystalRift"), damage, knockBack, player.whoAmI);
                Main.projectile[num125].noDropItem = true;
                num2 = num123;
            }
            return true; //Makes sure to not fire the original projectile
        }
    }
}