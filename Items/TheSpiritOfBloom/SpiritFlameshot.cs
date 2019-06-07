using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;


namespace EnduriumMod.Items.TheSpiritOfBloom
{
    public class SpiritFlameshot : ModItem
    {
        public override void SetDefaults()
        {
            item.damage = 24;
            item.ranged = true;
            item.width = 50;
            item.height = 20;
            item.useTime = 54;
            item.useAnimation = 54;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 3;
            item.value = Terraria.Item.buyPrice(0, 1, 30, 0);
            item.rare = 3;
            item.UseSound = SoundID.Item41;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 5f;
            item.useAmmo = AmmoID.Bullet;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Flameshot");
            Tooltip.SetDefault("'Launches bouncy projectiles'\nIf the projectile hits a target 3 times it explodes");
        }


        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            int numberProjectiles = 1;
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(3));
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("BloomBall"), damage, knockBack, player.whoAmI);
            }
            return false;
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
