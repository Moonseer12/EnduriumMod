using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ModLoader;


namespace EnduriumMod.Projectiles
{
    public class MarbleBomerangMelee : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 18;
            projectile.height = 34;
            projectile.friendly = true;
            projectile.melee = true;
            projectile.penetrate = -1;
            projectile.aiStyle = 3;
            projectile.timeLeft = 600;
            aiType = 52;
        }
		        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Rosy Gold Bomerang");
        }
    }
}