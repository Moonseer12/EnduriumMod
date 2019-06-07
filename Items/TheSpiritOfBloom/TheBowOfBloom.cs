using Terraria;
using System;
using Terraria.ID;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using Terraria.ModLoader;

namespace EnduriumMod.Items.TheSpiritOfBloom
{
    public class TheBowOfBloom : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 18;
            item.noMelee = true;
            item.ranged = true;
            item.width = 24;
            item.height = 54;
            item.useTime = 49;
            item.useAnimation = 49;
            item.useStyle = 5;
            item.shoot = 3;
            item.useAmmo = AmmoID.Arrow;
            item.knockBack = 1;
            item.value = 100000;
            item.rare = 7;
            item.UseSound = SoundID.Item5;
            item.autoReuse = true;
            item.shootSpeed = 8f;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blessed Spirit Bow");
            Tooltip.SetDefault("Turns arrows into spirit bolts that home onto enemies\nInflicts nature reaper");
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            Projectile.NewProjectile(position.X, position.Y, speedX, speedY, mod.ProjectileType("BlessedSpiritArrow"), damage, knockBack, player.whoAmI, 0f, 0f); //This is spawning a projectile of type FrostburnArrow using the original stats
            return false; //Makes sure to not fire the original projectile
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("BloomlightBar"), 15);
            recipe.AddIngredient(null, ("TrueNatureEssence"), 12);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}