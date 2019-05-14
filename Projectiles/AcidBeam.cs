using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class AcidBeam : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Beam");
        }
        public override void SetDefaults()
        {
            projectile.width = 4;
            projectile.height = 4;
            projectile.friendly = true;
            projectile.magic = true;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
            projectile.extraUpdates = 100;
            projectile.ignoreWater = true;
        }

        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            Player player = Main.player[projectile.owner];
            target.immune[projectile.owner] = 7;
            for (int num623 = 0; num623 < 4; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 100, default(Color), 0.4f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 4f;
            }
            if (projectile.ai[1] == 0)
            {
                Projectile.NewProjectile(player.position.X + Main.rand.Next(-50, 51), player.position.Y + Main.rand.Next(-50, 51), 0f, 0f, mod.ProjectileType("AcidBeam"), projectile.damage, 0f, projectile.owner, target.whoAmI, 1f);
            }
        }

        public override void AI()
        {
            if (projectile.ai[1] == 1)
            {
                projectile.tileCollide = false;
                for (int num623 = 0; num623 < 12; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(projectile.position.X, projectile.position.Y), projectile.width, projectile.height, 107, 0f, 0f, 25, default(Color), 1f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 4f;
                }
                projectile.ai[1] = 2;
                projectile.velocity = Vector2.Normalize(Main.npc[(int)projectile.ai[0]].Center - projectile.Center) * 8;
            }
            int num144 = Utils.SelectRandom<int>(Main.rand, new int[]
            {
                        107,
                        89
            });
            Dust dust23 = Main.dust[Dust.NewDust(projectile.position, projectile.width, projectile.height, num144, projectile.velocity.X, projectile.velocity.Y, 25, default(Color), 1f)];
            dust23.velocity = dust23.velocity / 4f + projectile.velocity / 2f;
            dust23.noGravity = true;
            dust23.scale = 0.95f;
            dust23.position = projectile.Center;
        }
    }
}

			