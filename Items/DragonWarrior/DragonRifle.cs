using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.DragonWarrior
{
    public class DragonRifle : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 60;
            item.ranged = true;
            item.width = 62;
            item.height = 26;
            item.useTime = 11;

            item.useAnimation = 11;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 8f;
            item.value = 125000;
            item.rare = 8;
            item.UseSound = SoundID.Item36;
            item.autoReuse = true;
            item.shoot = 10;
            item.shootSpeed = 30f;
            item.useAmmo = AmmoID.Bullet;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("DragonShard"), 18);
            recipe.AddIngredient(null, ("DragonBlood"), 18);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddTile(null, "AltarofFire");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Rifle");
            Tooltip.SetDefault("Rapidly hitting enemies overloads the gun\nWhen the gun is overloaded a burst of bullets will be released");
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-6, 0);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (modPlayer.DragonRifle >= 1f)
            {
                for (int i = 0; i < 15; i++)
                {
                    Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 10);
                    Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedBy(MathHelper.ToRadians(45)); // 30 degree spread.                                                                                            // perturbedSpeed = perturbedSpeed * scale; 
                    Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
                }
                modPlayer.DragonRifle = -0.25f;
            }
            return true; // return false because we don't want tmodloader to shoot projectile
        }
    }
}