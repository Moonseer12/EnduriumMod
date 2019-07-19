using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.DragonWarrior.Shop
{
    public class DragonDualshot : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Dualshot");
            Tooltip.SetDefault("Fires 2 bullets, one after another");
        }
        public override void SetDefaults()
        {
            item.damage = 60;
            item.ranged = true;
            item.width = 72;
            item.height = 32;
            item.useAnimation = 16;
            item.useTime = 8;
            item.reuseDelay = 5;
            item.useStyle = 5;
            item.noMelee = true;
            item.knockBack = 2;
            item.value = Terraria.Item.buyPrice(0, 40, 0, 0);
            item.rare = 9;
            item.UseSound = SoundID.Item11;
            item.autoReuse = false;
            item.shoot = 10;
            item.shootSpeed = 16f;
            item.useAmmo = AmmoID.Bullet;
        }
        int num1 = 0;
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            num1 += 1;
            Vector2 muzzleOffset = Vector2.Normalize(new Vector2(speedX, speedY)) * 45f;
            if (Collision.CanHit(position, 0, 0, position + muzzleOffset, 0, 0))
            {
                position += muzzleOffset;
            }
            for (int i = 0; i < num1; i++)
            {
                Vector2 perturbedSpeed = new Vector2(speedX, speedY).RotatedByRandom(MathHelper.ToRadians(4));
                float scale = 1f - (Main.rand.NextFloat() * .3f);
                perturbedSpeed = perturbedSpeed * scale;
                Projectile.NewProjectile(position.X, position.Y, perturbedSpeed.X, perturbedSpeed.Y, type, damage, knockBack, player.whoAmI);
            }
            if (num1 >= 4)
            {
                num1 = 0;
            }
            return false;
        }
        public override Vector2? HoldoutOffset()
        {
            return new Vector2(-8, 0);
        }
    }
}