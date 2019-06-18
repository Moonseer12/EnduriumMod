using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.PrismMonolith
{
    public class IceFall : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 40;
            projectile.height = 40;
            projectile.aiStyle = 1;
            projectile.timeLeft = 100;
            aiType = ProjectileID.Bullet;
            projectile.tileCollide = false;
            projectile.penetrate = -1;      //this is how many enemy this projectile penetrate before desapear 
        }
        public override bool CanHitPlayer(Player target)
        {
            return false;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Ice Fall");
        }
        public override void AI()
        {
            int dust = Dust.NewDust(projectile.position + projectile.velocity, projectile.width, projectile.height, 132, 0, 0);
            Main.dust[dust].noGravity = true;
            Main.dust[dust].velocity *= 2f;
            projectile.localAI[0] += 1f;
            if (projectile.localAI[0] > 10f)
            {
                projectile.localAI[0] = 0f;
                int num414 = (int)(projectile.position.X + (float)Main.rand.Next(projectile.width - 6));
                int num415 = (int)(projectile.position.Y + (float)projectile.height + 6);
                Projectile.NewProjectile((float)num414, (float)num415, 0f, 9f, mod.ProjectileType("Knoive"), 30, 0f, projectile.owner, 0f, 0f);

            }
        }
    }
}
