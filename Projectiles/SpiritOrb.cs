using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class SpiritOrb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 18;
            projectile.light = 0.25f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.alpha = 0;
            projectile.timeLeft = 200;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Spirit Orb");
        }

        public override void Kill(int timeLeft)
        {
            int num3;
            for (int num20 = 0; num20 < 8; num20 = num3 + 1)
            {
                float num21 = projectile.velocity.X / 4f * (float)num20;
                float num22 = projectile.velocity.Y / 4f * (float)num20;
                int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 89, 0f, 0f, 0, default(Color), 1.3f);
                Main.dust[num23].position.X = projectile.Center.X - num21;
                Main.dust[num23].position.Y = projectile.Center.Y - num22;
                Dust dust3 = Main.dust[num23];
                dust3.velocity *= 0.6f;
                Main.dust[num23].scale = 0.6f;
                Main.dust[num23].fadeIn = 0.4f;
                num3 = num20;
            }
        }
        public override void AI()
        {
            double deg = (double)projectile.ai[0];
            double rad = deg * (Math.PI / 180);
            double dist = 50;
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            if (projectile.ai[1] == 0)
            {
                Player player = Main.player[projectile.owner];
                MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
                Vector2 direction = Main.player[projectile.owner].Center - projectile.Center;
                projectile.rotation = direction.ToRotation();  //To make this i modified a projectile that orbits around the player, modified it and got it working.
                projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
                projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
                projectile.ai[0] += 2;
                projectile.ai[0] += Main.rand.NextFloat(0.15f, 0.45f);
                if (modPlayer.UsedSpiritOrb)
                {
                    projectile.ai[1] = 1;
                }
                Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 12;
            }
            if (projectile.ai[1] == 1)
            {
                projectile.ai[1] = 2;
                Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX - (int)(Math.Cos(rad) * dist) - projectile.width / 2, Main.mouseY - (int)(Math.Sin(rad) * dist) - projectile.height / 2);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 20;
            }
        }
    }
}