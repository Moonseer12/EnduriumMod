using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.DragonWarrior
{
    public class DragonTalonShot : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 100;
            item.ranged = true;
            item.width = 30;
            item.height = 62;
            item.useTime = 18;

            item.useAnimation = 18;
            item.useStyle = 5;
            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 4f;
            item.value = 125000;
            item.rare = 8;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shoot = 19; //idk why but all the guns in the vanilla source have this
            item.shootSpeed = 20f;
            item.useAmmo = 40;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-4, 0);
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
            DisplayName.SetDefault("Dragon Talon Shot");
            Tooltip.SetDefault("Every other a piercing projectilee is fired");
        }
        public int Skiddadle = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Skiddadle += 1;
            if (Skiddadle >= 2)
            {
                Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("DragonTalon"), damage, knockBack, player.whoAmI, 1.25f, 1.25f); //This is spawning a projectile of type FrostburnArrow using the original stats
                Skiddadle = 0;
                return true; //Makes sure to not fire the original projectile
            }
            return true;
        }
    }
}