using System.Collections.Generic;
using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles //We need this to basically indicate the folder where it is to be read from, so you the texture will load correctly
{
    public class AcidCannon : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Cannon");
        }
        public override void SetDefaults()
        {
            projectile.width = 22;
            projectile.height = 22;
            projectile.aiStyle = -1;
            projectile.timeLeft = 20;

            projectile.penetrate = -1;
            projectile.friendly = true;
            projectile.tileCollide = true;
            projectile.ignoreWater = false;
            projectile.ranged = true;
            projectile.alpha = 0;
        }
        public override void Kill(int timeLeft)
        {
            projectile.position.X = projectile.position.X + (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y + (float)(projectile.height / 2);
            projectile.width = 30;
            projectile.height = 30;
            projectile.position.X = projectile.position.X - (float)(projectile.width / 2);
            projectile.position.Y = projectile.position.Y - (float)(projectile.height / 2);
            int num3;
            if (projectile.ai[1] != 1)
            {
                for (int num731 = 0; num731 < 5; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 89, 0f, 0f, 100, default(Color), 1.25f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 2f;
                    num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 1.25f);
                    dust = Main.dust[num732];
                    dust.velocity *= 2f;
                    num3 = num731;
                }
            }
            else
            {
                for (int num731 = 0; num731 < 2; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 89, 0f, 0f, 100, default(Color), 0.75f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 1.5f;
                    num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 0.75f);
                    dust = Main.dust[num732];
                    dust.velocity *= 1.5f;
                    num3 = num731;
                }
            }
            Player player = Main.player[projectile.owner];
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            float scaleFactor2 = 20f;
            Vector2 vector3 = vector;
            int num7 = 6;
            if (projectile.ai[1] != 1)
            {
                for (int j = 0; j < 6; j++)
                {
                    vector3 = projectile.Center + new Vector2((float)Main.rand.Next(-num7, num7 + 1), (float)Main.rand.Next(-num7, num7 + 1));
                    Vector2 vector5 = Vector2.Normalize(projectile.velocity) * scaleFactor2;
                    vector5 = vector5.RotatedBy(Main.rand.NextDouble() * 0.5f - 0.5f / 2, default(Vector2));
                    if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
                    {
                        vector5 = -Vector2.UnitY;
                    }
                    Projectile.NewProjectile(vector3.X, vector3.Y, vector5.X, vector5.Y, mod.ProjectileType("AcidCannon"), projectile.damage, projectile.knockBack, projectile.owner, 0f, 1f);
                    Main.PlaySound(SoundID.Item36, projectile.position);
                }
            }
        }
        public override void AI()
        {
            if (projectile.ai[1] == 1)
            {
                projectile.scale = 0.5f;
                projectile.penetrate = 1;
                projectile.timeLeft = 10;
            }
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            Player owner = Main.player[projectile.owner]; //Makes a player variable of owner set as the player using the projectile
        }
    }
}