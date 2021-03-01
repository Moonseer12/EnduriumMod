﻿using System;
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
    public class BurningBallofFire : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 38;
            projectile.height = 80;
            projectile.hostile = true;
            projectile.aiStyle = 1;
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.melee = true;
            projectile.alpha = 80;
            projectile.timeLeft = 80;
            projectile.penetrate = 5;      //this is how many enemy this projectile penetrate before desapear 
            projectile.extraUpdates = 1;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ImperialInferno"), 160);
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Fire Bolt");
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
            Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 74);
            for (int num623 = 0; num623 < 25; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 100, default(Color), 1.6f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 4f;
            }

            {
                Main.PlaySound(2, (int)projectile.position.X, (int)projectile.position.Y, 14);
                int num3;
                int num4;
                for (int num624 = 0; num624 < 50; num624 = num3 + 1)
                {
                    int num625 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 127, 0f, 0f, 0, default(Color), 2f);
                    Dust dust = Main.dust[num625];
                    dust.velocity *= 2.5f;
                    dust = Main.dust[num625];
                    dust.scale *= 0.9f;
                    Main.dust[num625].noGravity = true;
                    num3 = num624;
                }
                int num11 = Main.rand.Next(2, 4);
                for (int j = 0; j < num11; j++)
                {
                    Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector5 += projectile.velocity * 3f;
                    vector5.Normalize();
                    vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                    int dab = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector5.X, vector5.Y, 15, 40, 0f, 0);
                    Main.projectile[dab].hostile = true;
                    Main.projectile[dab].friendly = false;
                }

                int num12 = Main.rand.Next(2, 4);
                for (int g = 0; g < num12; g++)
                {
                    Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector5 += projectile.velocity * 3f;
                    vector5.Normalize();
                    vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                    int dab = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector5.X, vector5.Y, 328, 40, 0f, 0);
                    Main.projectile[dab].hostile = true;
                    Main.projectile[dab].friendly = false;
                }
                int num13 = Main.rand.Next(2, 4);
                for (int g = 0; g < num13; g++)
                {
                    Vector2 vector5 = new Vector2((float)Main.rand.Next(-100, 101), (float)Main.rand.Next(-100, 101));
                    vector5 += projectile.velocity * 3f;
                    vector5.Normalize();
                    vector5 *= (float)Main.rand.Next(35, 39) * 0.1f;

                    int dab = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, vector5.X, vector5.Y, mod.ProjectileType("FlameBomb"), 40, 0f, 0);
                    Main.projectile[dab].hostile = true;
                    Main.projectile[dab].friendly = false;
                }
            }
        }
        public override void AI()
        {
            projectile.spriteDirection = projectile.direction;
            int num3;
            for (int num93 = 0; num93 < 5; num93 = num3 + 1)
            {
                float num94 = projectile.velocity.X / 3f * (float)num93;
                float num95 = projectile.velocity.Y / 3f * (float)num93;
                int num96 = 4;
                int num97 = Dust.NewDust(new Vector2(projectile.position.X + (float)num96, projectile.position.Y + (float)num96), projectile.width - num96 * 2, projectile.height - num96 * 2, 127, 0f, 0f, 100, default(Color), 0.8f);
                Main.dust[num97].noGravity = true;
                Dust dust3 = Main.dust[num97];
                dust3.velocity *= 0f;
                dust3 = Main.dust[num97];
                dust3.velocity += projectile.velocity * 0f;
                Dust dust6 = Main.dust[num97];
                dust6.position.X = dust6.position.X - num94;
                Dust dust7 = Main.dust[num97];
                dust7.position.Y = dust7.position.Y - num95;
                num3 = num93;
            }
        }
    }
}