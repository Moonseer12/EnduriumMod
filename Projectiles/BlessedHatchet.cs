using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BlessedHatchet : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.aiStyle = 2;
            projectile.thrown = true;
            projectile.penetrate = 1;
            aiType = ProjectileID.ThrowingKnife;
            projectile.timeLeft = 180;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flacon Hatchet");
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
        public override void Kill(int timeLeft)
        {
            float numberProjectiles = 2; // 3, 4, or 5 shots
            float rotation = MathHelper.ToRadians(45);
            for (int i = 0; i < numberProjectiles; i++)
            {
                Vector2 perturbedSpeed = new Vector2(projectile.velocity.X, projectile.velocity.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .9f; // Watch out for dividing by 0 if there is only 1 projectile.
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("MiniLeafFriendly"), (int)((double)projectile.damage * 0.75f), projectile.knockBack, projectile.owner, 0f, 0f);
            }
            for (int k = 0; k < 8; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 89, projectile.oldVelocity.X * 0.1f, projectile.oldVelocity.Y * 0.1f);
            }
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
    }
}