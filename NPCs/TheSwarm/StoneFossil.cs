using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.TheSwarm
{
    public class StoneFossil : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 22;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.timeLeft = 180;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Stone Fossil");
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 4; i < 31; i++)
            {
                float x = projectile.oldVelocity.X * (20f / i);
                float y = projectile.oldVelocity.Y * (20f / i);
                int newDust = Dust.NewDust(new Vector2(projectile.oldPosition.X - x, projectile.oldPosition.Y - y), 8, 8, 89, projectile.oldVelocity.X, projectile.oldVelocity.Y, 58, default(Color), 1.8f);
                Main.dust[newDust].noGravity = true;
                Main.dust[newDust].velocity *= 0.5f;
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.ai[0] += 0.1f;
            if (projectile.velocity.X != oldVelocity.X)
            {
                projectile.velocity.X = -oldVelocity.X;
            }
            if (projectile.velocity.Y != oldVelocity.Y)
            {
                projectile.velocity.Y = -oldVelocity.Y;
            }
            for (int i = 4; i < 31; i++)
            {
                float x = projectile.oldVelocity.X * (20f / i);
                float y = projectile.oldVelocity.Y * (20f / i);
                int newDust = Dust.NewDust(new Vector2(projectile.oldPosition.X - x, projectile.oldPosition.Y - y), 8, 8, 61, projectile.oldVelocity.X, projectile.oldVelocity.Y, 58, default(Color), 1.8f);
                Main.dust[newDust].noGravity = true;
                Main.dust[newDust].velocity *= 0.5f;
            }
            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 18);
            }
            projectile.ai[0] += 1f;
        }
    }
}