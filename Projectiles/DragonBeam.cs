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
            projectile.width = 240;
            projectile.height = 240;
            projectile.light = 0.25f;
            projectile.friendly = true;
            projectile.tileCollide = false;
            projectile.hostile = false;
            projectile.ignoreWater = true;
            projectile.penetrate = 2;
            projectile.scale = 0.25f;
            projectile.alpha = 0;
            projectile.timeLeft = 1200;
        }
        public override bool? CanHitNPC(NPC target)
        {
            if (projectile.localAI[1] == 0)
            {
                return false;
            }
            if (projectile.penetrate == 2)
            {
                return true;
            }
            return false;
        }
        float Overlayalpha = 0f;
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = Main.projectileTexture[projectile.type];
            Texture2D glowmask2 = mod.GetTexture("Projectiles/DragonBeam_Overlay");
            int glownum = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = glownum * projectile.frame;
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), -projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            Main.spriteBatch.Draw(glowmask2, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White) * Overlayalpha, projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            Main.spriteBatch.Draw(glowmask2, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White) * Overlayalpha, -projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == -1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);

            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fiery Magic");
        }
        public override void Kill(int timeLeft)
        {
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 60);
            for (int num2 = 0; num2 < 12; num2++)
            {
                int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 1f, 1f, 100, default(Color), 2.2f);
                Main.dust[num].velocity *= 1f;
                Main.dust[num].noGravity = true;
            }
        }
        public override void AI()
        {
            if (projectile.penetrate == 1 || projectile.timeLeft <= 700)
            {
                projectile.velocity *= 0.9f;
                projectile.alpha += 5;
            }
            if (projectile.alpha >= 255)
            {
                projectile.Kill();
            }
            if (projectile.localAI[0] == 0)
            {
                if (projectile.scale <= 1f)
                {
                    projectile.scale += 0.05f;
                }
                if (projectile.scale >= 1f)
                {
                    projectile.localAI[0] = 1;
                }
            }
            if (projectile.localAI[0] >= 1)
            {
                projectile.localAI[0] += 1;
                if (projectile.localAI[0] >= 60 && projectile.localAI[0] <= 200)
                {
                    Overlayalpha += 0.005f;
                    projectile.damage += 1;
                    projectile.scale += 0.005f;
                }
                if (projectile.localAI[0] == 200)
                {
                    for (int num2 = 0; num2 < 24; num2++)
                    {
                        int num = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 269, 1f, 1f, 100, default(Color), 2.2f);
                        Main.dust[num].velocity *= 4f;
                        Main.dust[num].noGravity = true;
                    }
                    Overlayalpha = 1f;
                }
                if (projectile.localAI[0] >= 260)
                {
                    Overlayalpha -= 0.025f;
                    projectile.damage -= 2;
                    projectile.scale -= 0.01f;
                }
                if (projectile.localAI[0] >= 360)
                {
                    projectile.Kill();
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
                projectile.localAI[0] = -1;
                projectile.ai[1] = 0;
                projectile.ai[0] = 0;
                projectile.localAI[1] = 2;
                Vector2 value11 = Main.screenPosition + new Vector2(Main.mouseX - (int)(Math.Cos(rad) * dist) / 2 - projectile.width / 2, Main.mouseY - (int)(Math.Sin(rad) * dist) / 2 - projectile.height / 2);
                projectile.velocity = Vector2.Normalize(value11 - projectile.Center) * 6;
            }
            if (projectile.localAI[1] == 2)
            {
                float num372 = projectile.position.X;
                float num373 = projectile.position.Y;
                float num374 = 100000f;
                bool flag10 = false;
                projectile.ai[0] += 1;
                int num3;
                for (int num375 = 0; num375 < 200; num375 = num3 + 1)
                {
                    if (Main.npc[num375].CanBeChasedBy(projectile, false))
                    {
                        float num376 = Main.npc[num375].position.X + (float)(Main.npc[num375].width / 2);
                        float num377 = Main.npc[num375].position.Y + (float)(Main.npc[num375].height / 2);
                        float num378 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num376) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num377);
                        if (num378 < 800f && num378 < num374 && Collision.CanHit(projectile.position, projectile.width, projectile.height, Main.npc[num375].position, Main.npc[num375].width, Main.npc[num375].height))
                        {
                            num374 = num378;
                            num372 = num376;
                            num373 = num377;
                            flag10 = true;
                        }
                    }
                    num3 = num375;
                }

                if (!flag10)
                {
                    num372 = projectile.position.X + (float)(projectile.width / 2) + projectile.velocity.X * 250f;
                    num373 = projectile.position.Y + (float)(projectile.height / 2) + projectile.velocity.Y * 250f;
                }
                float num379 = 7f;
                float num380 = 0.06f;
                Vector2 vector29 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num381 = num372 - vector29.X;
                float num382 = num373 - vector29.Y;
                float num383 = (float)Math.Sqrt((double)(num381 * num381 + num382 * num382));
                num383 = num379 / num383;
                num381 *= num383;
                num382 *= num383;
                if (projectile.velocity.X < num381)
                {
                    projectile.velocity.X = projectile.velocity.X + num380;
                    if (projectile.velocity.X < 0f && num381 > 0f)
                    {
                        projectile.velocity.X = projectile.velocity.X + num380 * 2f;
                    }
                }
                else
                {
                    if (projectile.velocity.X > num381)
                    {
                        projectile.velocity.X = projectile.velocity.X - num380;
                        if (projectile.velocity.X > 0f && num381 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - num380 * 2f;
                        }
                    }
                }
                if (projectile.velocity.Y < num382)
                {
                    projectile.velocity.Y = projectile.velocity.Y + num380;
                    if (projectile.velocity.Y < 0f && num382 > 0f)
                    {
                        projectile.velocity.Y = projectile.velocity.Y + num380 * 2f;
                        return;
                    }
                }
                else
                {
                    if (projectile.velocity.Y > num382)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num380;
                        if (projectile.velocity.Y > 0f && num382 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num380 * 2f;
                            return;
                        }
                    }
                }
                Lighting.AddLight(projectile.Center, ((255 - projectile.alpha) * 0.05f) / 255f, ((255 - projectile.alpha) * 0.5f) / 255f, ((255 - projectile.alpha) * 0.75f) / 255f);
                projectile.rotation += 0.08f;
                projectile.ai[1] += 0.5f;
                if (projectile.ai[1] >= 10f && projectile.ai[1] <= 25f)
                {
                    projectile.velocity.X *= 1.02f;
                    projectile.velocity.Y *= 1.02f;
                }
                if (projectile.ai[1] >= 35f)
                {
                    projectile.velocity.X *= 0.96f;
                    projectile.velocity.Y *= 0.96f;
                }
                if (projectile.ai[1] <= 35f)
                {
                    if (projectile.alpha > 40)
                    {
                        projectile.alpha -= 2;
                    }
                }
                if (projectile.ai[1] >= 10f)
                {
                    int drust = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width * (int)projectile.scale, projectile.height * (int)projectile.scale, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[drust].velocity *= 2f;
                    Main.dust[drust].fadeIn *= 1.8f;
                    Main.dust[drust].noGravity = true;

                    int drust2 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width * (int)projectile.scale, projectile.height * (int)projectile.scale, 269, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[drust2].velocity *= 4f;
                    Main.dust[drust2].fadeIn *= 2.8f;
                    Main.dust[drust2].noGravity = true;
                    projectile.rotation += 0.03f;
                }
            }
        }
    }
}

			