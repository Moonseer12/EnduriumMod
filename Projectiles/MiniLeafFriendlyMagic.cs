using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class MiniLeafFriendlyMagic : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;
            projectile.hostile = false;
            projectile.penetrate = -1;
            projectile.magic = true;
            projectile.friendly = true;
            aiType = ProjectileID.ThrowingKnife;
            projectile.timeLeft = 180;
            projectile.aiStyle = 1;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blooming leaf");
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
            return false;
        }
        public override void AI()
        {
            if (projectile.ai[0] == 0f)
            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 18);
            }
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)       //how much time the projectile can travel before landing
            {  // projectile fall velocity
                projectile.velocity.Y = projectile.velocity.X / 6f;    // projectile velocity
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 22, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 1.5f);
            }
        }
    }
}