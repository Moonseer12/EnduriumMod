using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class BlessedSpiritArrow : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 8;
            projectile.height = 8;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 2;
            projectile.timeLeft = 800;
            projectile.light = 0.25f;
            projectile.extraUpdates = 2;
            ProjectileID.Sets.TrailCacheLength[projectile.type] = 2;
            ProjectileID.Sets.TrailingMode[projectile.type] = 5;
            projectile.aiStyle = 2;
            aiType = 48;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Blessed Spirit Arrow");
        }
        public override bool PreAI()
        {
            int num;
            for (int num143 = 0; num143 < 2; num143 = num + 1)
            {
                int num144 = Utils.SelectRandom<int>(Main.rand, new int[]
                {
                        89,
                        107,
                        61
                });
                Dust dust23 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, num144, projectile.velocity.X, projectile.velocity.Y, 100, default(Color), 1f)];
                dust23.velocity = dust23.velocity / 4f + projectile.velocity / 2f;
                dust23.noGravity = true;
                dust23.scale = 1.1f;
                if (num144 != 107)
                {
                    dust23.velocity *= 0.1f;
                }
                dust23.noLight = true;
                num = num143;
            }

            float num166 = (float)Math.Sqrt((double)(projectile.velocity.X * projectile.velocity.X + projectile.velocity.Y * projectile.velocity.Y));
            float num167 = projectile.localAI[0];
            if (num167 == 0f)
            {
                projectile.localAI[0] = num166;
                num167 = num166;
            }
            if (projectile.alpha > 0)
            {
                projectile.alpha -= 25;
            }
            if (projectile.alpha < 0)
            {
                projectile.alpha = 0;
            }
            float num168 = projectile.position.X;
            float num169 = projectile.position.Y;
            float num170 = 300f;
            bool flag4 = false;
            int num171 = 0;
            if (projectile.ai[1] == 0f)
            {
                for (int num172 = 0; num172 < 200; num172 = num + 1)
                {
                    if (Main.npc[num172].CanBeChasedBy(projectile, false) && (projectile.ai[1] == 0f || projectile.ai[1] == (float)(num172 + 1)))
                    {
                        float num173 = Main.npc[num172].position.X + (float)(Main.npc[num172].width / 2);
                        float num174 = Main.npc[num172].position.Y + (float)(Main.npc[num172].height / 2);
                        float num175 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num173) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num174);
                        if (num175 < num170 && Collision.CanHit(new Vector2(projectile.position.X + (float)(projectile.width / 2), projectile.position.Y + (float)(projectile.height / 2)), 1, 1, Main.npc[num172].position, Main.npc[num172].width, Main.npc[num172].height))
                        {
                            num170 = num175;
                            num168 = num173;
                            num169 = num174;
                            flag4 = true;
                            num171 = num172;
                        }
                    }
                    num = num172;
                }
                if (flag4)
                {
                    projectile.ai[1] = (float)(num171 + 1);
                }
                flag4 = false;
            }
            if (projectile.ai[1] > 0f)
            {
                int num176 = (int)(projectile.ai[1] - 1f);
                if (Main.npc[num176].active && Main.npc[num176].CanBeChasedBy(projectile, true) && !Main.npc[num176].dontTakeDamage)
                {
                    float num177 = Main.npc[num176].position.X + (float)(Main.npc[num176].width / 2);
                    float num178 = Main.npc[num176].position.Y + (float)(Main.npc[num176].height / 2);
                    float num179 = Math.Abs(projectile.position.X + (float)(projectile.width / 2) - num177) + Math.Abs(projectile.position.Y + (float)(projectile.height / 2) - num178);
                    if (num179 < 1000f)
                    {
                        flag4 = true;
                        num168 = Main.npc[num176].position.X + (float)(Main.npc[num176].width / 2);
                        num169 = Main.npc[num176].position.Y + (float)(Main.npc[num176].height / 2);
                    }
                }
                else
                {
                    projectile.ai[1] = 0f;
                }
            }
            if (!projectile.friendly)
            {
                flag4 = false;
            }
            if (flag4)
            {
                float num180 = num167;
                Vector2 vector19 = new Vector2(projectile.position.X + (float)projectile.width * 0.5f, projectile.position.Y + (float)projectile.height * 0.5f);
                float num181 = num168 - vector19.X;
                float num182 = num169 - vector19.Y;
                float num183 = (float)Math.Sqrt((double)(num181 * num181 + num182 * num182));
                num183 = num180 / num183;
                num181 *= num183;
                num182 *= num183;
                int num184 = 8;
                projectile.velocity.X = (projectile.velocity.X * (float)(num184 - 1) + num181) / (float)num184;
                projectile.velocity.Y = (projectile.velocity.Y * (float)(num184 - 1) + num182) / (float)num184;
            }
            return false;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
            target.immune[projectile.owner] = 3;
        }
        public override void Kill(int timeLeft)
        {
            for (int k = 0; k < 5; k++)
            {
                Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 2, projectile.oldVelocity.X * 0.5f, projectile.oldVelocity.Y * 0.3f);
            }
        }
        public override bool OnTileCollide(Vector2 oldVelocity)
        {
            projectile.Kill();
            return true;
        }
    }
}
