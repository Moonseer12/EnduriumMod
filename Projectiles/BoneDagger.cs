using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BoneDagger : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 12;
            projectile.height = 12;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 2;
            projectile.timeLeft = 1200;
            aiType = 48;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Dagger");
        }

        public override void AI()
        {
            projectile.ai[0] += 1f;
            projectile.rotation *= 1.005f;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {                                                           // sound that the projectile make when hiting the terrain
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 10);
            Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AOE"), (int)((double)projectile.damage * 0.75f), projectile.knockBack, projectile.owner);
            Main.PlaySound(SoundID.Item62, projectile.position);
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 24;
            projectile.height = 24;
            int num3;
            for (int num731 = 0; num731 < 8; num731 = num3 + 1)
            {
                int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 121, 0f, 0f, 100, default(Color), 1.5f);
                Main.dust[num732].noGravity = true;
                Dust dust = Main.dust[num732];
                dust.velocity *= 1.5f;
                num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 2.5f);
                dust = Main.dust[num732];
                Main.dust[num732].noGravity = true;
                dust.velocity *= 2f;
                num3 = num731;
            }
        }
    }
}