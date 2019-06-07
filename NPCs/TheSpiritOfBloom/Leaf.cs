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

namespace EnduriumMod.NPCs.TheSpiritOfBloom
{
    public class Leaf : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;
            projectile.hostile = true;
            projectile.penetrate = -1;
            Main.projFrames[projectile.type] = 5;
            projectile.CloneDefaults(38);
            projectile.timeLeft = 280;
            projectile.alpha = 100;
            aiType = 38;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blooming leaf");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            Texture2D glowmask = Main.projectileTexture[projectile.type];
            int glownum = Main.projectileTexture[projectile.type].Height / Main.projFrames[projectile.type];
            int y7 = glownum * projectile.frame;
            Main.spriteBatch.Draw(glowmask, projectile.Center - Main.screenPosition + new Vector2(0f, projectile.gfxOffY), new Microsoft.Xna.Framework.Rectangle?(new Microsoft.Xna.Framework.Rectangle(0, y7, glowmask.Width, glownum)), projectile.GetAlpha(Color.White), projectile.rotation, new Vector2((float)glowmask.Width / 2f, (float)glownum / 2f), projectile.scale, projectile.spriteDirection == 1 ? SpriteEffects.None : SpriteEffects.FlipHorizontally, 0f);
            return false;
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();

            return false;
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
        public override void AI()
        {
            int num;
            projectile.frameCounter++;
            if (projectile.frameCounter > 5)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 4)
            {
                projectile.frame = 0;
            }
            projectile.ai[0] += 1f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] >= 24f)
            {
                projectile.localAI[0] = 0f;
                for (int l = 0; l < 12; l = num + 1)
                {
                    Vector2 vector3 = Vector2.UnitX * -(float)projectile.width / 2f;
                    vector3 += -Vector2.UnitY.RotatedBy((double)((float)l * 3.14159274f / 6f), default(Vector2)) * new Vector2(8f, 16f);
                    vector3 = vector3.RotatedBy((double)(projectile.rotation - 1.57079637f), default(Vector2));
                    int num9 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 89, 0f, 0f, projectile.alpha, default(Color), 1.5f);

                    Main.dust[num9].scale = 1.4f;
                    Main.dust[num9].noGravity = true;
                    Main.dust[num9].position = projectile.Center + vector3;
                    Main.dust[num9].velocity = projectile.velocity * 0.25f;
                    Main.dust[num9].velocity = Vector2.Normalize(projectile.Center - projectile.velocity * 3f - Main.dust[num9].position) * 1.25f;
                    num = l;
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 89, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.5f);
            }
        }
    }
}