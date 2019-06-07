using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    public class LeafStorm : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Leaf Storm");
        }
        public override void SetDefaults()
        {
            projectile.width = 14;
            projectile.height = 22;
            projectile.friendly = true;
            projectile.penetrate = 2;
            Main.projFrames[projectile.type] = 4;
            projectile.timeLeft = 100;
            projectile.aiStyle = 1;
            aiType = 1;
            projectile.magic = true;
        }
        public override void OnHitNPC(NPC target, int damage, float knockback, bool crit)
        {
            target.AddBuff(mod.BuffType("ReaperNature"), 200);
            target.immune[projectile.owner] = 5;
        }
        public override void AI()
        {
            projectile.frameCounter++;
            if (projectile.frameCounter > 4)
            {
                projectile.frame++;
                projectile.frameCounter = 0;
            }
            if (projectile.frame > 3)
            {
                projectile.frame = 0;
            }
        }
    }
}