using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BloodOfTheMatyr : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Of The Matyr");
        }
        public override void SetDefaults()
        {
            projectile.width = 20;
            projectile.height = 20;
            projectile.friendly = true;
            projectile.alpha = 255;
            projectile.penetrate = 5;
            projectile.timeLeft = 180;
            projectile.magic = true;
        }

        public override void AI()
        {
            projectile.velocity.X *= 0.985f;
            projectile.velocity.Y *= 0.985f;
            int a = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 90, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Main.dust[a].noGravity = true;
            Main.dust[a].velocity *= 0.5f;
            Main.dust[a].scale *= 1.25f;
        }
    }
}