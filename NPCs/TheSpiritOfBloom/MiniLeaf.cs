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
    public class MiniLeaf : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 24;
            projectile.hostile = true;
            projectile.penetrate = -1;
            projectile.CloneDefaults(38);
            projectile.timeLeft = 180;
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
        public override void AI()
        {
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 25f)       //how much time the projectile can travel before landing
            {  // projectile fall velocity
                projectile.velocity.Y = projectile.velocity.X / 6f;    // projectile velocity
            }
        }
        public override void OnHitPlayer(Player target, int damage, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 10; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 89, projectile.oldVelocity.X * 0.15f, projectile.oldVelocity.Y * 0.15f);
            }
        }
    }
}