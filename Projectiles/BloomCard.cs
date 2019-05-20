using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BloomCard : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloom Card");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.aiStyle = 1;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 300;
            projectile.extraUpdates = 1;
            aiType = ProjectileID.Bullet;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int num3;
            int a = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 89, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
            Main.dust[a].noGravity = true;
            Main.dust[a].velocity *= 0.25f;
            Main.dust[a].scale *= 0.5f;


            if (projectile.penetrate == 1)
            {
                float num372 = projectile.position.X;
                float num373 = projectile.position.Y;
                float num374 = 100000f;
                bool flag10 = false;

                for (int num375 = 0; num375 < 200; num375 = num3 + 1)
                {
                    if (Main.npc[num375].CanBeChasedBy(projectile, false) && (!Main.npc[num375].wet || projectile.type == 307))
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
                if (flag10)
                {
                    float num379 = 8f;
                    float num380 = 0.08f;
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
                    else if (projectile.velocity.X > num381)
                    {
                        projectile.velocity.X = projectile.velocity.X - num380;
                        if (projectile.velocity.X > 0f && num381 < 0f)
                        {
                            projectile.velocity.X = projectile.velocity.X - num380 * 2f;
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
                    else if (projectile.velocity.Y > num382)
                    {
                        projectile.velocity.Y = projectile.velocity.Y - num380;
                        if (projectile.velocity.Y > 0f && num382 < 0f)
                        {
                            projectile.velocity.Y = projectile.velocity.Y - num380 * 2f;
                            return;
                        }
                    }
                }
            }
        }
    }
}