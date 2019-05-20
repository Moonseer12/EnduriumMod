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
    public class Facemelter : ModProjectile
    {
        public override void SetDefaults()
        {
            projectile.width = 60;
            projectile.height = 60;
            projectile.friendly = true;
            projectile.penetrate = -1;
            projectile.tileCollide = false;
            projectile.magic = true;
            projectile.ignoreWater = true;
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Face Melter");
        }
        float num006 = 0.8f;
        float num009 = 0.5f;

        float acc = 0f;
        public override void AI()
        {
            Player player = Main.player[projectile.owner];
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            float num = 1.57079637f;
            Vector2 vector = player.RotatedRelativePoint(player.MountedCenter, true);
            projectile.ai[0] += 1f;
            if (projectile.ai[0] >= 5090)
            {
                modPlayer.FacemelterHeld = false;
                projectile.Kill();
            }
            if (player.dead || !player.active && !player.noItems && !player.CCed)
            {
                modPlayer.FacemelterHeld = false;
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
            modPlayer.FacemelterHeld = true;
            int num2 = 0;
            int num3 = 21;
            int num4 = 7;
            float num007 = num006 / 10;
            float num008 = num006 / 20;
            projectile.ai[1] += 0.25f + (acc / 4);

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
            projectile.position = player.RotatedRelativePoint(player.MountedCenter, true) - projectile.Size / 2f;
            projectile.rotation = projectile.velocity.ToRotation() + num;
            projectile.spriteDirection = projectile.direction;
            projectile.timeLeft = 2;
            player.ChangeDir(projectile.direction);
            player.heldProj = projectile.whoAmI;
            player.itemTime = 2;
            player.itemAnimation = 2;
            player.itemRotation = (float)Math.Atan2((double)(projectile.velocity.Y * (float)projectile.direction), (double)(projectile.velocity.X * (float)projectile.direction));
        }
    }
}