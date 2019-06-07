using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Microsoft.Xna.Framework.Graphics;

namespace EnduriumMod.Projectiles
{
    public class WrathOfTheForest05 : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = 1;
            projectile.aiStyle = 27;
            projectile.alpha = 125;
            projectile.timeLeft = 50;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Wrath of the Natura");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            //Redraw the projectile with the color not influenced by light
            Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
            for (int k = 0; k < projectile.oldPos.Length; k++)
            {
                Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 100);
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            int num;
            float[] arg_56E6_0 = projectile.localAI;
            num = projectile.frameCounter + 1;
            projectile.alpha = 0;
            projectile.scale = 1f;
            projectile.frameCounter = num;
            if (num >= 40)
            {
                projectile.frameCounter = 0;
            }
            int num0004;
            num0004 = projectile.frameCounter + 1;
            projectile.frameCounter = num0004;
            if (num0004 >= 40)
            {
                projectile.frameCounter = 0;
            }
            projectile.frame = projectile.frameCounter / 5;
            Player owner = Main.player[projectile.owner]; //Makes a player variable of owner set as the player using the projectile
            if (projectile.alpha < 170)
            {
                Vector2 vector15 = new Vector2(0f, (float)Math.Cos((double)((float)projectile.frameCounter * 6.28318548f / 40f - 1.57079637f))) * 16f;
                vector15 = vector15.RotatedBy((double)projectile.rotation, default(Vector2));
                Vector2 value16 = projectile.velocity.SafeNormalize(Vector2.Zero);
                for (int num115 = 0; num115 < 1; num115 = num + 1)
                {
                    Dust dust14 = Dust.NewDustDirect(projectile.Center, projectile.width / 2, projectile.height / 2, 89, 0f, 0f, 125, default(Color), 1f);
                    dust14.noGravity = true;
                    dust14.position = projectile.Center + vector15;
                    Dust dust3 = dust14;
                    dust3.velocity *= 0f;
                    dust14.fadeIn = 0.9f;
                    dust14.scale = 1.15f;
                    dust3 = dust14;
                    dust3.position += projectile.velocity * 1.2f;
                    dust3 = dust14;
                    dust3.velocity += value16 * 2f;
                    dust14 = Dust.NewDustDirect(projectile.Center, projectile.width / 2, projectile.height / 2, 89, 0f, 0f, 125, default(Color), 1f);
                    dust14.noGravity = true;
                    dust14.position = projectile.Center + vector15;
                    dust3 = dust14;
                    dust3.velocity *= 0f;
                    dust14.fadeIn = 0.9f;
                    dust14.scale = 1.15f;
                    dust3 = dust14;
                    dust3.position += projectile.velocity * 0.5f;
                    dust3 = dust14;
                    dust3.position += projectile.velocity * 1.2f;
                    dust3 = dust14;
                    dust3.velocity += value16 * 2f;
                    num = num115;
                }

            }
        }

        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int i = 4; i < 12; i++)
            {
                float x = projectile.oldVelocity.X * (25f / i);
                float y = projectile.oldVelocity.Y * (25f / i);
                int newDust = Dust.NewDust(new Vector2(projectile.oldPosition.X - x, projectile.oldPosition.Y - y), 8, 8, 89, projectile.oldVelocity.X, projectile.oldVelocity.Y, 125, default(Color), 1.8f);
                Main.dust[newDust].noGravity = true;
                Main.dust[newDust].velocity *= 0.5f;
            }
        }
    }
}