using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons
{
    public class HeavensGalore : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 112;
            item.melee = true;
            item.width = 58;
            item.height = 58;
            item.useTime = 26;
            item.useAnimation = 26;
            item.useStyle = 1;

            item.knockBack = 5;
            item.value = 105500;
            item.rare = 6;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HeavensGalore");
            item.shootSpeed = 5f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Aerial Judgement");
            Tooltip.SetDefault("When at full health this weapon does double damage");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Excalibur);
            //recipe.AddIngredient(null, ("GrailKatana"));
            recipe.AddIngredient(ItemID.SoulofLight, 15);
            recipe.AddIngredient(ItemID.CrystalShard, 15);
            recipe.AddIngredient(null, ("GemofHollow"), 12);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool Shoot(Player player, ref Microsoft.Xna.Framework.Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("HeavensGalore"), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false;
        }
    }
}