using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace EnduriumMod.NPCs.TheGatekeeper
{
    public class FlameBomb : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 36;
            projectile.hostile = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.alpha = 110;
            projectile.penetrate = 5;      //this is how many enemy this projectile penetrate before desapear 
            projectile.extraUpdates = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Flame Blast");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = Main.projectileTexture[projectile.type];
            int glownum = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = glownum * projectile.frame;
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override void Kill(int timeLeft)
        {
            for (int num623 = 0; num623 < 25; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 2f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 4f;
            }
        }
        public override void AI()
        {
            int num4324;
            int num;
            projectile.spriteDirection = projectile.direction;
            if (projectile.ai[1] == 0f)
            {
                projectile.ai[1] = 1f;
                Player player = Main.player[Main.myPlayer];
                float closestDist = 10000;
                int chosenPlayer = Main.myPlayer;
                for (int i = 0; i < 255; i++)
                {
                    if (i == 0) closestDist = Vector2.Distance(Main.player[i].Center, projectile.Center);
                    else if (Vector2.Distance(Main.player[i].Center, projectile.Center) < closestDist)
                    {
                        closestDist = Vector2.Distance(Main.player[i].Center, projectile.Center);
                        chosenPlayer = i;
                    }
                }
                player = Main.player[chosenPlayer];
                projectile.velocity = Vector2.Normalize(player.Center - projectile.Center) * 13;
                Main.PlaySound(SoundID.Item91, projectile.position);
                for (int num623 = 0; num623 < 25; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 4f;
                }
            }
            else if (projectile.ai[1] == 1f && Main.netMode != 1)
            {
                int num3 = -1;
                float num4 = 2000f;
                for (int k = 0; k < 255; k = num + 1)
                {
                    if (Main.player[k].active && !Main.player[k].dead)
                    {
                        Vector2 center = Main.player[k].Center;
                        float num5 = Vector2.Distance(center, projectile.Center);
                        if ((num5 < num4 || num3 == -1) && Collision.CanHit(projectile.Center, 1, 1, center, 1, 1))
                        {
                            num4 = num5;
                            num3 = k;
                        }
                    }
                    num = k;
                }
                if (num4 < 20f)
                {
                    projectile.Kill();
                    return;
                }
                if (num3 != -1)
                {
                    projectile.ai[1] = 21f;
                    projectile.ai[0] = (float)num3;
                    projectile.netUpdate = true;
                }
            }
            else if (projectile.ai[1] > 20f && projectile.ai[1] < 80f)
            {
                projectile.ai[1] += 1f;
                int num6 = (int)projectile.ai[0];
                if (!Main.player[num6].active || Main.player[num6].dead)
                {
                    projectile.ai[1] = 1f;
                    projectile.ai[0] = 0f;
                    projectile.netUpdate = true;
                }
                else
                {
                    float num7 = projectile.velocity.ToRotation();
                    Vector2 vector2 = Main.player[num6].Center - projectile.Center;
                    if (vector2.Length() < 20f)
                    {
                        projectile.Kill();
                        return;
                    }
                    float targetAngle = vector2.ToRotation();
                    if (vector2 == Vector2.Zero)
                    {
                        targetAngle = num7;
                    }
                    float num8 = num7.AngleLerp(targetAngle, 0.01f);
                    projectile.velocity = new Vector2(projectile.velocity.Length(), 0f).RotatedBy((double)num8, default(Vector2));
                }
            }
            if (projectile.ai[1] >= 1f && projectile.ai[1] < 40f)
            {
                projectile.ai[1] += 1f;
                if (projectile.ai[1] == 40f)
                {
                    projectile.ai[1] = 1f;
                }
            }
            int num444;
            for (int num93 = 0; num93 < 5; num93 = num444 + 1)
            {
                float num94 = projectile.velocity.X / 3f * (float)num93;
                float num95 = projectile.velocity.Y / 3f * (float)num93;
                int num96 = 4;
                int num97 = Dust.NewDust(new Vector2(projectile.position.X + (float)num96, projectile.position.Y + (float)num96), projectile.width - num96 * 2, projectile.height - num96 * 2, 127, 0f, 0f, 100, default(Color), 1.4f);
                Main.dust[num97].noGravity = true;
                Dust dust3 = Main.dust[num97];
                dust3.velocity *= 0f;
                dust3 = Main.dust[num97];
                dust3.velocity += projectile.velocity * 0f;
                Dust dust6 = Main.dust[num97];
                dust6.position.X = dust6.position.X - num94;
                Dust dust7 = Main.dust[num97];
                dust7.position.Y = dust7.position.Y - num95;
                num444 = num93;
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ImperialInferno"), 160);
        }
    }
}