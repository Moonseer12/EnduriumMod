using System;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.Audio;

using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using ReLogic.Utilities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
using System.Security.Cryptography;
using System.Text;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.Enums;
using Terraria.GameContent;
using Terraria.GameContent.Achievements;
using Terraria.GameContent.Events;
using Terraria.GameContent.Tile_Entities;
using Terraria.GameContent.UI;
using Terraria.GameInput;
using Terraria.Graphics.Capture;
using Terraria.Graphics.Effects;
using Terraria.Graphics.Shaders;
using Terraria.ID;
using Terraria.IO;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ModLoader.Exceptions;
using Terraria.ModLoader.IO;
using Terraria.ObjectData;
using Terraria.Social;
using Terraria.UI;
using Terraria.UI.Chat;
using Terraria.UI.Gamepad;
using Terraria.Utilities;
using Terraria.World.Generation;
namespace EnduriumMod.Projectiles
{
    public class FacemelterAmplifier : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 30;
            projectile.height = 30;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.timeLeft = 6000;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Face Melter");
        }
        float num006 = 0.8f;
        float num009 = 0.5f;

        float acc = 0f;
        public override bool ShouldUpdatePosition()
        {
            return false;
        }
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Facemelter")] <= 0;
            if (projectile.timeLeft >= 50)
            {
                projectile.timeLeft = 100;
            }
            if (!petProjectileNotSpawned)
            {
                projectile.ai[0] += 1f;
                if (projectile.ai[0] >= 5090)
                {
                    projectile.Kill();
                }
                if (player.dead || !player.active && !player.noItems && !player.CCed || !modPlayer.FacemelterHeld)
                {
                    projectile.Kill();
                }
                if (projectile.ai[0] <= 300)
                {
                    acc += 0.01f;
                }
                if (projectile.ai[0] >= 4730)
                {
                    acc -= 0.01f;
                }
                projectile.ai[1] += 0.25f + (acc / 4);
            }
            else
            {
                projectile.ai[1] = 0;
                acc = 0f;
            }
            int num2 = 0;
            int num3 = 21;
            int num4 = 7;
            float num007 = num006 / 10;
            float num008 = num006 / 20;

            if (num006 >= 0.08f)
            {
                num006 -= 0.0005f;
            }

            bool flag = false;
            if (projectile.ai[1] >= 6)
            {
                flag = true;
                projectile.ai[1] = 0f;
            }
            projectile.frameCounter += 1 + num2;
            if (projectile.frameCounter >= 4)
            {
                projectile.frameCounter = 0;
                projectile.frame++;
                if (projectile.frame >= 6)
                {
                    projectile.frame = 0;
                }
            }
            if (flag)
            {
                float scaleFactor = player.inventory[player.selectedItem].shootSpeed * projectile.scale;
                Vector2 vector3 = vector;
                Vector2 value2 = Main.screenPosition + new Vector2((float)Main.mouseX, (float)Main.mouseY) - vector3;
                if (player.gravDir == -1f)
                {
                    value2.Y = (float)(Main.screenHeight - Main.mouseY) + Main.screenPosition.Y - vector3.Y;
                }
                Vector2 vector4 = Vector2.Normalize(value2);
                if (float.IsNaN(vector4.X) || float.IsNaN(vector4.Y))
                {
                    vector4 = -Vector2.UnitY;
                }
                vector4 *= scaleFactor;
                if (vector4.X != projectile.velocity.X || vector4.Y != projectile.velocity.Y)
                {
                    projectile.netUpdate = true;
                }
                projectile.velocity = vector4;


                int num6 = mod.ProjectileType("FacemelterNote");
                float scaleFactor2 = 16f;
                int num7 = 6;
                float numberProjectiles = 5;
                float rotation = MathHelper.ToRadians(180);

                vector3 = projectile.Center + new Vector2((float)Main.rand.Next(-num7, num7 + 1), (float)Main.rand.Next(-num7, num7 + 1));
                Vector2 vector5 = Vector2.Normalize(projectile.velocity) * scaleFactor2;
                if (projectile.ai[0] <= 5040)
                {
                    for (int i = 0; i < numberProjectiles; i++)
                    {
                        Vector2 perturbedSpeed = new Vector2(vector5.X, vector5.Y).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))); // Watch out for dividing by 0 if there is only 1 projectile.
                        Projectile.NewProjectile(vector3.X, vector3.Y, perturbedSpeed.X, perturbedSpeed.Y, num6, projectile.damage, projectile.knockBack, player.whoAmI, player.whoAmI);
                    }
                    float num33 = 18;
                    float num44 = 6.28318548f * Main.rand.NextFloat();
                    Vector2 value = new Vector2(32f, 32f);
                    for (float num5 = 0f; num5 < num33; num5 += 1f)
                    {
                        Dust dust = Main.dust[Dust.NewDust(projectile.Center, 0, 0, 269, 0f, 0f, 0, default(Color), 1f)];
                        Vector2 vector32 = Vector2.UnitY.RotatedBy((double)(num5 * 6.28318548f / num33 + num44), default(Vector2));
                        dust.position = projectile.Center + vector32 * value / 2f;
                        dust.velocity = vector32;
                        dust.noGravity = true;
                        dust.scale = 1.5f;
                        dust.velocity *= dust.scale;
                        dust.fadeIn = Main.rand.NextFloat() * 0.6f;
                        dust.customData = player;
                    }
                }
                if (float.IsNaN(vector5.X) || float.IsNaN(vector5.Y))
                {
                    vector5 = -Vector2.UnitY;
                }
            }
            for (int num432 = 0; num432 < 1000; num432 = num3 + 1)
            {
                if (num432 != projectile.whoAmI && Main.projectile[num432].active && Main.projectile[num432].owner == projectile.owner && Main.projectile[num432].type == projectile.type && projectile.timeLeft > Main.projectile[num432].timeLeft && Main.projectile[num432].timeLeft > 30)
                {
                    Main.projectile[num432].timeLeft = 30;
                }
                num3 = num432;
            }
        }
    }
}