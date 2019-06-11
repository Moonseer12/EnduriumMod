using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;


namespace EnduriumMod.Items.Weapons.HolySilver
{
    public class RottenSpire : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 76;
            item.crit += 25;
            item.magic = true;
            item.mana = 21;
            item.width = 62;
            item.height = 62;
            item.useTime = 34;
            item.useAnimation = 34;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = 900000;
            item.rare = 11;
            item.UseSound = SoundID.Item15;
            Item.staff[item.type] = true;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("RottenFlame");
            item.shootSpeed = 12f;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y - 70, 0f, 0f, mod.ProjectileType("RottenFlame"), item.damage, item.knockBack, item.owner);
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(522, 12);
            recipe.AddIngredient(null, ("StarCrystal"), 5);
            recipe.AddIngredient(null, ("HolySilver"), 5);
            recipe.AddTile(null, "SoulForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rotten Spire");
            Tooltip.SetDefault("Creates a rotten flame at the cursors position\nThe Flame hits the enemy twice");
        }
    }
}