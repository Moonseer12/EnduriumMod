using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class AcidBane : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 10;
            projectile.height = 10;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.penetrate = -1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Bane");
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(SoundID.Item10, projectile.position);
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            projectile.damage = projectile.damage / 2;
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
        public override void AI()
        {
            if (projectile.damage <= 0)
            {
                projectile.Kill();
            }
            projectile.ai[1] += 1;
            if (projectile.ai[1] >= 4)
            {
                projectile.ai[1] = 0;
                Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, 0f, 0f, mod.ProjectileType("AcidBaneBoom"), projectile.damage / 2, 0f, Main.myPlayer, 0f, 0f);
            }
            if (projectile.ai[1] <= 3)
            {
                int a = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 107, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[a].scale = 0.9f;
                Main.dust[a].position = projectile.Center;
                Main.dust[a].velocity *= 0f;
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
        }
    }
}