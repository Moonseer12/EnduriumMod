using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class DragonRipper : ModProjectile
    {

        public override void SetDefaults()
        {
            projectile.width = 24;
            projectile.height = 24;
            projectile.friendly = true;
            projectile.thrown = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 300;
        }

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 5;
            ProjectileID.Sets.TrailingMode[projectile.type] = 0;
            DisplayName.SetDefault("Dragon Ripper");
        }
        public override bool PreDraw(SpriteBatch spriteBatch, Color lightColor)
        {
            if (projectile.ai[0] != 1)
            {
                //Redraw the projectile with the color not influenced by light
                Vector2 drawOrigin = new Vector2(Main.projectileTexture[projectile.type].Width * 0.5f, projectile.height * 0.5f);
                for (int k = 0; k < projectile.oldPos.Length; k++)
                {
                    Vector2 drawPos = projectile.oldPos[k] - Main.screenPosition + drawOrigin + new Vector2(0f, projectile.gfxOffY);
                    Color color = projectile.GetAlpha(lightColor) * ((float)(projectile.oldPos.Length - k) / (float)projectile.oldPos.Length);
                    spriteBatch.Draw(Main.projectileTexture[projectile.type], drawPos, null, color, projectile.rotation, drawOrigin, projectile.scale, SpriteEffects.None, 0f);
                }
            }
            return true;
        }
        public override void AI()
        {
            projectile.rotation = projectile.velocity.ToRotation() + 1.57079637f;
            int num981 = 25;
            if (projectile.alpha > 0)
            {
                projectile.alpha -= num981;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            if (projectile.ai[0] == 1f)
            {
                projectile.ignoreWater = true;
                projectile.tileCollide = false;
                int num986 = 15;
                bool flag53 = false;
                bool flag54 = false;
                projectile.localAI[0] += 1f;
                if (projectile.localAI[0] % 30f == 0f)
                {
                    flag54 = true;
                }
                int num987 = (int)projectile.ai[1];
                if (projectile.localAI[0] >= (float)(60 * num986))
                {
                    flag53 = true;
                }
                else if (num987 < 0 || num987 >= 200)
                {
                    flag53 = true;
                }
                else if (Main.npc[num987].active && !Main.npc[num987].dontTakeDamage)
                {
                    projectile.Center = Main.npc[num987].Center - projectile.velocity * 1.7f;
                    projectile.gfxOffY = Main.npc[num987].gfxOffY;
                    if (flag54)
                    {
                        float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                        num628 *= 5f;
                        num629 *= 5f;
                        int num1111 = Projectile.NewProjectile(projectile.Center.X, projectile.Center.Y, num628, num629, mod.ProjectileType("FireBlast"), 20, 2f, Main.myPlayer, 0f, 0f);
                        Main.projectile[num1111].extraUpdates = 1;
                        Main.npc[num987].HitEffect(0, 1.0);
                    }
                }
                else
                {
                    flag53 = true;
                }
                if (flag53)
                {
                    projectile.Kill();
                }
            }
            if (projectile.velocity.X > 0f)
            {
                projectile.direction = 1;
            }
            if (projectile.velocity.X < 0f)
            {
                projectile.direction = -1;
            }
            if (projectile.ai[0] == 0)
            {
                int a = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 6, projectile.velocity.X * 0.5f, projectile.velocity.Y * 0.5f);
                Main.dust[a].noGravity = true;
                Main.dust[a].velocity *= 0.15f;
                Main.dust[a].scale *= 0.8f;
            }
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            Main.PlaySound(0, (int)projectile.position.X, (int)projectile.position.Y, 10);
            return true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
         //   target.AddBuff(24, 300);
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            projectile.ai[0] = 1;
            projectile.ai[1] = target.whoAmI;
            projectile.damage = 0;
            int num27 = 6;
            Point[] array2 = new Point[num27];
            int num28 = 0;
            for (int i = 0; i < 200; i++)
            {
                for (int l = 0; l < 1000; l++)
                {
                    if (l != projectile.whoAmI && Main.projectile[l].active && Main.projectile[l].owner == Main.myPlayer && Main.projectile[l].type == projectile.type && Main.projectile[l].ai[0] == 1f && Main.projectile[l].ai[1] == (float)i)
                    {
                        array2[num28++] = new Point(l, Main.projectile[l].timeLeft);
                        if (num28 >= array2.Length)
                        {
                            modPlayer.SpearBoom = 8;
                            target.immune[projectile.owner] = 1;
                            break;
                        }
                    }
                }
                if (num28 >= array2.Length)
                {
                    int num29 = 0;
                    for (int m = 1; m < array2.Length; m++)
                    {
                        if (array2[m].Y < array2[num29].Y)
                        {
                            num29 = m;
                        }
                    }
                    Main.projectile[array2[num29].X].Kill();
                }
            }
        }
        public override void Kill(int timeLeft)
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (modPlayer.SpearBoom == 0)
            {
                for (int num621 = 0; num621 < 15; num621++)
                {
                    int num622 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.2f);
                    Main.dust[num622].velocity *= 3f;
                }
            }
        }
    }
}
