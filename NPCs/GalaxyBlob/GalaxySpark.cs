﻿using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.GalaxyBlob
{
    public class GalaxySpark : ModProjectile
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Galaxy Spark");
        }
        public override void SetDefaults()
        {
            projectile.width = 16;
            projectile.height = 16;
            projectile.hostile = true;
            projectile.alpha = 255;
            projectile.penetrate = 1;
            projectile.timeLeft = 200;
        }

        public override void AI()
        {
            int num3;
            for (int num93 = 0; num93 < 5; num93 = num3 + 1)
            {
                float num94 = projectile.velocity.X / 3f * (float)num93;
                float num95 = projectile.velocity.Y / 3f * (float)num93;
                int num96 = 4;
                int num97 = Dust.NewDust(new Vector2(projectile.position.X + (float)num96, projectile.position.Y + (float)num96), projectile.width - num96 * 2, projectile.height - num96 * 2, 267, 0f, 0f, 100, new Color(Main.DiscoR, Main.DiscoG, Main.DiscoB), 1.2f);
                Main.dust[num97].noGravity = true;
                Dust dust3 = Main.dust[num97];
                dust3.velocity *= 0.1f;
                dust3 = Main.dust[num97];
                dust3.velocity += projectile.velocity * 0.1f;
                Dust dust6 = Main.dust[num97];
                dust6.position.X = dust6.position.X - num94;
                Dust dust7 = Main.dust[num97];
                dust7.position.Y = dust7.position.Y - num95;
                num3 = num93;
            }
        }
    }
}