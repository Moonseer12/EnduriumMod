using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class ShadowArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 52;
            projectile.friendly = true;
            projectile.ranged = true;
            projectile.tileCollide = false;
            projectile.penetrate = 5;
            projectile.aiStyle = 1;
            aiType = 1;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Shadow Arrow");
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
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("Shiver"), 700);
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 60);

            for (int num621 = 0; num621 < 15; num621++)
            {
                int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 242, 0f, 0f, 156, default(Color), 1.2f);
                Main.dust[num622].velocity *= 3f;
            }
        }
    }
}