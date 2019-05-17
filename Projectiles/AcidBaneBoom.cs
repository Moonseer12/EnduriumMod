using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Projectiles
{
    class AcidBaneBoom : ModProjectile
    {
        public override void SetDefaults()
        { 
            projectile.width = 18; //324
            projectile.height = 18; //216
            projectile.aiStyle = -1; //new
            projectile.friendly = true;
            projectile.alpha = 25;
            projectile.tileCollide = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Bane");
        }
        float rotationR = 0f;
        float scaleR = 0f;
        public override bool PreAI()
        {
            if (projectile.ai[0] == 0)
            {
                rotationR += Main.rand.NextFloat(0.01f, 0.08f);
                scaleR += Main.rand.NextFloat(0.01f, 0.03f);
                projectile.ai[0] = 1;
            }
            projectile.rotation += rotationR;
            if (projectile.ai[1] >= 20)
            {
                if (projectile.alpha <= 255)
                {
                    projectile.alpha += 4;
                }
            }
            if (projectile.ai[1] <= 15)
            {
                projectile.scale += scaleR;
            }
            else
            {
                if (projectile.scale >= 0.25f + scaleR)
                {
                    projectile.scale -= scaleR;
                }
            }
            projectile.ai[1] += 1;
            if (projectile.ai[1] >= 120 || projectile.alpha == 225)
            {
                projectile.Kill();
            }
            return false;
        }
    }
}
