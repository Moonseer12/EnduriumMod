using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BloomBall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.friendly = true;
            projectile.timeLeft = 120;
            projectile.aiStyle = 1;
            projectile.ranged = true;
            projectile.penetrate = 4;      //this is how many enemy this projectile penetrate before desapear 
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloom Ball");
        }

        public override void Kill(int timeLeft)
        {
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AOE"), (int)((double)projectile.damage * 0.75f), projectile.knockBack, projectile.owner);
            Main.PlaySound(SoundID.Item62, projectile.position);
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 24;
            projectile.height = 24;
            int num3;
            for (int num731 = 0; num731 < 8; num731 = num3 + 1)
            {
                int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num732].noGravity = true;
                Dust dust = Main.dust[num732];
                dust.velocity *= 1.5f;
                num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 61, 0f, 0f, 100, default(Color), 2.5f);
                dust = Main.dust[num732];
                Main.dust[num732].noGravity = true;
                dust.velocity *= 2f;
                num3 = num731;
            }
        }

        public override void AI()
        {
            int a = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 44, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Main.dust[a].velocity *= 0.1f;
            Main.dust[a].noGravity = true;
            int b = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Main.dust[b].velocity *= 0.5f;
            Main.dust[b].noGravity = true;
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 20f)       //how much time the projectile can travel before landing
            {
                projectile.velocity.Y = projectile.velocity.Y + 3.15f;    // projectile fall velocity
                projectile.velocity.X = projectile.velocity.X * 2f;    // projectile velocity
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            //If collide with tile, reduce the penetrate.
            //So the projectile can reflect at most 5 times
            
            projectile.velocity.Y += 5f;
            if (projectile.penetrate <= 0)
            {
                projectile.Kill();
            }
            else
            {
                if (projectile.velocity.X != oldVelocity.X)
                {
                    projectile.velocity.X = -oldVelocity.X;
                }
                if (projectile.velocity.Y != oldVelocity.Y)
                {
                    projectile.velocity.Y = -oldVelocity.Y;
                }
                Main.PlaySound(SoundID.Item10, projectile.position);
            }
            return false;
        }
    }
}