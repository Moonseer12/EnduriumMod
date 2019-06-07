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
            DisplayName.SetDefault("The Thornvile");
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
                projectile.localAI[0] += 1f;
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
                    if (projectile.localAI[0] >= 30)
                    {
                        Main.npc[num987].HitEffect(0, 10);
                        projectile.localAI[0] = 0;
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
            target.AddBuff(1, 300);
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
                int num3;
                for (int num731 = 0; num731 < 22; num731 = num3 + 1)
                {
                    float x = projectile.velocity.X * (20f / num731);
                    float y = projectile.velocity.Y * (20f / num731);
                    int num732 = Dust.NewDust(new Vector2(projectile.oldPosition.X - x, projectile.oldPosition.Y - y), projectile.width, projectile.height, 6, 0f, 0f, 100, default(Color), 1.5f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity.X *= 0.8f;
                    dust.velocity.Y *= 1.2f;
                    num732 = Dust.NewDust(new Vector2(projectile.oldPosition.X - x, projectile.oldPosition.Y - y), projectile.width, projectile.height, 266, 0f, 0f, 100, default(Color), 0.8f);
                    dust = Main.dust[num732];
                    dust.velocity.X *= 0.8f;
                    dust.noGravity = true;
                    dust.velocity.Y *= 1.2f;
                    num3 = num731;
                }
            }
        }
    }
}
