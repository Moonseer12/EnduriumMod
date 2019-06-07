using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BiologicalEnergy : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.penetrate = 3;
            projectile.timeLeft = 8000;
            projectile.light = 0.0f;
            projectile.aiStyle = 1;
            aiType = 1;
            projectile.ignoreWater = true;
        }
        public override void AI()
        {
            int num3;
            for (int num20 = 0; num20 < 2; num20 = num3 + 1)
            {
                float num21 = projectile.velocity.X / 4f * (float)num20;
                float num22 = projectile.velocity.Y / 4f * (float)num20;
                int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 89, 0f, 0f, 0, default(Color), 1.6f);
                Main.dust[num23].position.X = projectile.Center.X - num21;
                Main.dust[num23].position.Y = projectile.Center.Y - num22;
                Dust dust3 = Main.dust[num23];
                dust3.velocity *= 0f;
                dust3.noGravity = true;
                Main.dust[num23].scale = 0.7f;
                num3 = num20;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
    }
}