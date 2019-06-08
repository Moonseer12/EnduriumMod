using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;

namespace EnduriumMod.Projectiles
{
    public class BloodGemProj : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blood Gem");
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
        }

        public override bool CanHitPvp(Player target)
        {
            return false;
        }
        public override bool? CanHitNPC(NPC target)
        {
            return false;
        }

        public override void SetDefaults()
        {
            projectile.netImportant = true;
            projectile.friendly = true;
            projectile.height = 22;
            projectile.width = 30;
            projectile.alpha = 0;
            projectile.penetrate = -1;
            projectile.timeLeft = -1;
            Main.projFrames[projectile.type] = 4;
            projectile.tileCollide = false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (projectile.ai[1] >= 600)
            {
                int num3;
                for (int num731 = 0; num731 < 6; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("TakenBalls"), 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 2f;
                    num732 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, mod.DustType("TakenBalls"), 0f, 0f, 100, default(Color), 0.5f);
                    dust = Main.dust[num732];
                    dust.velocity *= 1f;
                    num3 = num731;

                }
                projectile.ai[1] = 0;
                projectile.frameCounter++;
            }
            


            if (projectile.frameCounter == 1)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
            if (projectile.frame == 0)
            {
                if (modPlayer.BloodGemHurt)
                {
                    
                    {

                        int num492 = (int)projectile.owner;
                        int num497 = 40;
                        Main.player[num492].HealEffect(num497, false);
                        player.statLife += num497;
                        NetMessage.SendData(66, -1, -1, null, num492, (float)num497, 0f, 0f, 0, 0, 0);
                        modPlayer.BloodGemHurt = false;
                        projectile.frame++;
                    }
                }
            }
            else
            {
                modPlayer.BloodGemHurt = false;
                projectile.ai[1] += 1;
            }
            //Making player variable "p" set as the projectile's owner
            bool flag64 = projectile.type == mod.ProjectileType("BloodGem");

            if (player.dead)
            {
                modPlayer.BloodGem = false;
            }
            if (modPlayer.BloodGem)
            {
                projectile.timeLeft = 2;
            }
            if (!modPlayer.BloodGem)
            {
                projectile.Kill();
            }

            float num24 = 0.6f;
            float num25 = 7f;
            projectile.tileCollide = false;
            Vector2 vector4 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
            float num26 = Main.player[projectile.owner].position.X + (float)(Main.player[projectile.owner].width / 2) - vector4.X;
            float num27 = Main.player[projectile.owner].position.Y + Main.player[projectile.owner].gfxOffY + (float)(Main.player[projectile.owner].height / 2) - vector4.Y;
            if (Main.player[projectile.owner].direction == -1)
            {
                
            }
            else if (Main.player[projectile.owner].direction == 1)
            {
                
                projectile.direction = 1;
            }
            num27 -= 50f;
            float num28 = (float)Math.Sqrt((double)(num26 * num26 + num27 * num27));
            projectile.position.X = projectile.position.X + num26;
            projectile.position.Y = projectile.position.Y + num27;

        }
    }
}