using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons
{
    public class MementoMori : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 73;
            item.magic = true;
            item.mana = 18;
            item.width = 46;
            item.height = 46;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            Item.staff[item.type] = true;


            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 8.25f;
            item.value = 650000;
            item.rare = 9;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("AstralDazeBlue");
            item.shootSpeed = 5f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.SoulofNight, 15);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(null, ("GleamingCrag"), 20);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Astral Glaze");
            Tooltip.SetDefault("'Memento mori'");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int i = Main.myPlayer;

            float num82 = (float)Main.mouseX + Main.screenPosition.X - position.X;
            float num83 = (float)Main.mouseY + Main.screenPosition.Y - position.Y;
            Vector2 vector23 = new Vector2(num82, num83);
            int num77 = (int)(((float)item.damage * player.magicDamage) * 0.7f);
            vector23 *= 0.8f;
            Vector2 value4 = vector23.SafeNormalize(-Vector2.UnitY);
            float num201 = 0.0174532924f * -(float)player.direction;
            for (float num202 = -5.5f; num202 < 5f; num202 += 5f)
            {
                Projectile.NewProjectile(position, (vector23 + value4 * num202 * 0.5f).RotatedBy((double)(num202 * num201), default(Vector2)), mod.ProjectileType("AstralDazeBlue"), num77, 2, i, 0f, 0f);
                Projectile.NewProjectile(position, (vector23 + value4 * num202 * 0.5f).RotatedBy((double)(num202 * num201), default(Vector2)), mod.ProjectileType("AstralDazePurple"), num77, 2, i, 0f, 0f);
            }
            return false;
        }
    }
}