using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class DragonBeam : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.light = 0.25f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 1;
            projectile.scale = 0.25f;
            projectile.alpha = 0;
            projectile.timeLeft = 600;
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = Main.projectileTexture[projectile.type];
            int glownum = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = glownum * projectile.frame;
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), -projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fiery Magic");
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.localAI[1] == 0)
            {
                return false;
            }
            return true;
        }
        public override void Kill(int timeLeft)
        {
            int num3;
            for (int num20 = 0; num20 < 12; num20 = num3 + 1)
            {
                float num21 = projectile.velocity.X / 4f * (float)num20;
                float num22 = projectile.velocity.Y / 4f * (float)num20;
                int num23 = Dust.NewDust(projectile.position, projectile.width, projectile.height, 6, 0f, 0f, 0, default(Color), 1.3f);
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
            if (projectile.localAI[0] == 0)
            {
                if (projectile.scale <= 1f)
                {
                    projectile.scale += 0.05f;
                }
            }
                projectile.rotation += 0.04f;
            double deg = (double)projectile.ai[0];
            double rad = deg * (Math.PI / 180);
            double dist = (double)projectile.ai[1];
            if (projectile.localAI[1] == 0)
            {
                Player player = Main.player[projectile.owner];
                MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
                Vector2 direction = Main.player[projectile.owner].Center - projectile.Center;
                projectile.rotation = direction.ToRotation();  //To make this i modified a projectile that orbits around the player, modified it and got it working.
                projectile.position.X = player.Center.X - (int)(Math.Cos(rad) * dist) - projectile.width / 2;
                projectile.position.Y = player.Center.Y - (int)(Math.Sin(rad) * dist) - projectile.height / 2;
                projectile.ai[0] += 1;
                projectile.ai[0] += Main.rand.NextFloat(0.5f, 2f);
                if (modPlayer.UsedSpiritOrb)
                {
                    projectile.localAI[1] = 1;
                }
                Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX, Main.mouseY);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 12;
            }
            if (projectile.localAI[1] == 1)
            {
                projectile.localAI[1] = 2;
                Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX - (int)(Math.Cos(rad) * dist) - projectile.width / 2, Main.mouseY - (int)(Math.Sin(rad) * dist) - projectile.height / 2);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 20;
            }
        }
    }
}

			