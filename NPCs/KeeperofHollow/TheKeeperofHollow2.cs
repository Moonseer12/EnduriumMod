using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.World.Generation;
using Terraria.GameContent.Generation;

namespace EnduriumMod.NPCs.KeeperofHollow
{
    [AutoloadBossHead]
    public class TheKeeperofHollow2 : ModNPC
    {
        public override void SetDefaults()
        {

            npc.damage = 100;
            npc.npcSlots = 1f;
            npc.width = 144; //324
            npc.height = 126; //216
            npc.defense = 20;
            npc.lifeMax = 40000;
            npc.aiStyle = -1; //new
            bossBag = mod.ItemType("HollowBag");
            npc.knockBackResist = 0f;
            Main.npcFrameCount[npc.type] = 4;
            npc.noGravity = true;
            npc.value = 80000;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.boss = true;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Hallowed_Skies");
            npc.DeathSound = SoundID.NPCDeath6;
        }
        public override void FindFrame(int frameHeight)
        {
            npc.frameCounter += 0.16f;
            npc.frameCounter %= Main.npcFrameCount[npc.type];
            int frame = (int)npc.frameCounter;
            npc.frame.Y = frame * frameHeight;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 115;
            npc.defense = 40;
            npc.lifeMax = (int)(npc.lifeMax * 0.6f * bossLifeScale); //double health in expert mode and stacked with amount of players.
        }
        int First = 0;
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn a gore
            {
                if (!EnduriumWorld.downedHollow)
                {
                    EnduriumWorld.downedHollow = true;
                }
            }
            for (int num623 = 0; num623 < 12; num623++)
            {
                int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("HollowBurn"), 0f, 0f, 100, default(Color), 1f);
                Main.dust[num624].noGravity = true;
                Main.dust[num624].velocity *= 4f;
            }
            if (npc.life <= 0)
            {
                for (int num623 = 0; num623 < 25; num623++)
                {
                    int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("HollowBurn"), 0f, 0f, 100, default(Color), 2f);
                    Main.dust[num624].noGravity = true;
                    Main.dust[num624].velocity *= 4f;
                }
            }
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Keeper of Hollow");
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("GleamingCrag"), Main.rand.Next(12, 28));
            }
        }
        public override bool CanHitPlayer(Player target, ref int cooldownSlot)
        {
            if (Spawn <= 200 || npc.ai[3] == 2  )
            {
                return false;
            }
            return true;
        }
        int Spawn = 0;
        public override void AI()
        {
            Player player = Main.player[npc.target];
            int num414 = (int)(npc.Center.X);
            int num415 = (int)(npc.Center.Y);
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 150 && Main.netMode != 1)
                    {
                        npc.timeLeft = 150;
                        npc.netUpdate = true;
                    }
                    return;
                }
            }
            Spawn += 1;
            if (Spawn <= 275 && Main.netMode != 1)
            {
                npc.dontTakeDamage = true;
                if (Spawn <= 5)
                {
                    npc.alpha = 200;
                }
                if (Spawn >= 50)
                {
                    npc.alpha -= 2;
                    npc.velocity = new Vector2(0f, -0.5f);
                }
                if (Spawn == 250)
                {
                    for (int num623 = 0; num623 < 100; num623++)
                    {
                        int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, mod.DustType("HollowBurn"), 0f, 0f, 100, default(Color), 2f);
                        Main.dust[num624].noGravity = true;
                        Main.dust[num624].velocity *= 4f;
                    }
                }
                    if (Spawn == 275)
                {
                    npc.alpha = 0;
                    npc.ai[3] = 0;
                }
            }
            else
            {


                if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
                {
                    npc.TargetClosest(true);
                }
                float maxSpeed = 20f;
                if ((float)(player.position.X - npc.Center.X) >= 550f || (float)(player.position.Y - npc.Center.Y) >= 550f)
                {
                    maxSpeed = 35f;
                }
                else
                {
                    maxSpeed = 20f;
                }
                    if (npc.ai[0] == 1)
                {
                    int amount = 175 - Main.rand.Next(150);
                    int amount2 = 35 - Main.rand.Next(10);
                    int amount3 = 35 - Main.rand.Next(10);
                    float positionX = player.position.X - amount;
                    float positionY = player.position.Y - amount;
                    if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X + amount + amount3;
                        positionY = player.position.Y - amount + amount2;
                    }
                    else if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X + amount + amount2;
                        positionY = player.position.Y + amount + amount3;
                    }
                    else if (Main.rand.Next(3) == 0 && Main.netMode != 1)
                    {
                        positionX = player.position.X - amount + amount3;
                        positionY = player.position.Y + amount + amount2;
                    }
                    npc.ai[0] = 2;
                    if (npc.position.X == positionX)
                    {
                        npc.velocity = new Vector2(0f, 0f);
                    }
                    if (npc.position.Y == positionY)
                    {
                        npc.velocity = new Vector2(0f, 0f);
                    }
                    Vector2 toTarget = new Vector2(positionX - npc.Center.X, positionY - npc.Center.Y);
                    toTarget = new Vector2(positionX - npc.Center.X, positionY - npc.Center.Y);
                    toTarget.Normalize();
                    npc.velocity = toTarget * maxSpeed;
                    npc.netUpdate = true;
                }
                if (npc.ai[0] >= 2 && Main.netMode != 1)
                {
                    npc.velocity.X *= 0.999f;
                    npc.velocity.Y *= 0.999f;
                    npc.ai[0] += 1;
                }
                if (npc.ai[0] >= 40 && Main.netMode != 1)
                {
                    npc.velocity.X *= 0.7f;
                    npc.velocity.Y *= 0.7f;
                }
                if (npc.ai[0] == 90 && Main.netMode != 1)
                {
                    npc.ai[0] = 0;
                }
                if (npc.ai[3] == 0) //default attack pattern
                {
                    int num002 = 4;
                    int num001 = 65;
                    int num003 = 135;
                    int num004 = 190;
                    int num005 = 60;

                    int num006 = num005 *= 2;
                    if ((double)npc.life < (double)npc.lifeMax * 0.90 && Main.netMode != 1)
                    {
                        num001 = 60;
                        num002 = 5;
                        num003 = 125;
                        num004 = 175;
                        num005 = 56;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.65 && Main.netMode != 1)
                    {
                        if (First == 0 && npc.ai[3] == 0)
                        {
                            npc.ai[3] = 1;
                            npc.ai[1] = 0;
                            npc.ai[2] = 0;
                            npc.ai[0] = 1;
                            First = 1;
                        }
                        num001 = 45;
                        num002 = 7;
                        num003 = 110;
                        num004 = 150;
                        num005 = 48;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.35 && Main.netMode != 1)
                    {
                        num001 = 35;
                        num003 = 75;
                        num004 = 125;
                        num005 = 38;
                    }
                    if ((double)npc.life < (double)npc.lifeMax * 0.14 && Main.netMode != 1)
                    {
                        num001 = 15;
                        num002 = 8;
                        num003 = 45;
                        num004 = 65;
                        num005 = 25;
                    }
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[0] = 1;
                        npc.ai[2] = 1;
                    }
                    if (npc.ai[2] >= 1 && npc.ai[2] <= 4)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] >= num001 && Main.netMode != 1)
                        {
                            npc.ai[2] += 1;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            Vector2 vector23 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num147 = 12f;
                            float num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                            float num149 = Math.Abs(num148) * 0.1f;
                            float num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y - num149;
                            float num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                            npc.netUpdate = true;
                            num151 = num147 / num151;
                            num148 *= num151;
                            num150 *= num151;
                            int num152 = 35;
                            int num25;
                            for (int num154 = 0; num154 < 5; num154 = num25 + 1)
                            {
                                num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                                num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
                                num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                                num151 = 12f / num151;
                                num148 += (float)Main.rand.Next(-100, 101);
                                num150 += (float)Main.rand.Next(-100, 101);
                                num148 *= num151;
                                num150 *= num151;
                                Projectile.NewProjectile(vector23.X, vector23.Y, num148, num150, mod.ProjectileType("HollowBoomBoomBoom"), 42, 0f, Main.myPlayer, 0f, 0f);
                                num25 = num154;
                            }
                        }
                    }
                    if (npc.ai[2] == 5)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 25)
                        {
                            npc.ai[0] = 1;
                        }
                        float Speed = 2f;
                        if (npc.ai[1] == num003)
                        {
                            if ((double)npc.life < (double)npc.lifeMax * 0.65 && Main.netMode != 1)
                            {
                                Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, mod.ProjectileType("NovaBoomBoom"), 25, 0f, Main.myPlayer, 0f, 0f);
                            }
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, -Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, -Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, Speed, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, Speed, 0f, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -Speed, 0f, mod.ProjectileType("HollowBoomBoomSpawn2"), 35, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (npc.ai[1] >= num004)
                        {
                            if ((double)npc.life < (double)npc.lifeMax * 0.90 && Main.netMode != 1)
                            {
                                npc.ai[2] = 6;
                            }
                            else
                            {
                                npc.ai[2] = 0;
                            }
                            npc.netUpdate = true;
                            npc.ai[1] = 0;
                        }
                    }
                    if (npc.ai[2] >= 6 && npc.ai[2] <= 8)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 10 && Main.netMode != 1)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] >= 75 && Main.netMode != 1)
                        {
                            npc.ai[2] += 1;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            Vector2 vector23 = new Vector2(npc.position.X + (float)npc.width * 0.5f, npc.position.Y + (float)npc.height * 0.5f);
                            float num147 = 11f;
                            float num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                            float num149 = Math.Abs(num148) * 0.1f;
                            float num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y - num149;
                            float num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                            npc.netUpdate = true;
                            num151 = num147 / num151;
                            num148 *= num151;
                            num150 *= num151;
                            int num152 = 35;
                            int num25;
                            for (int num154 = 0; num154 < 2; num154 = num25 + 1)
                            {
                                num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                                num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
                                num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                                num151 = 12f / num151;
                                num148 += (float)Main.rand.Next(-60, 61);
                                num150 += (float)Main.rand.Next(-60, 61);
                                num148 *= num151;
                                num150 *= num151;
                                Projectile.NewProjectile(vector23.X, vector23.Y, num148, num150, mod.ProjectileType("HollowBoomBoom"), 32, 0f, Main.myPlayer, 0f, 0f);
                                num25 = num154;
                            }
                        }
                    }
                    if (npc.ai[2] == 9)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == num005 && Main.netMode != 1)
                        {
                            float Speed = 0.5f;  // projectile speed
                            npc.netUpdate = true;
                            Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, mod.ProjectileType("HollowFire"), 32, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (npc.ai[1] == num006 && Main.netMode != 1)
                        {
                            npc.ai[2] = 11;
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                        }
                    }
                    if (npc.ai[2] == 11)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 25 && Main.netMode != 1)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        float Speed = 2f;
                        if (npc.ai[1] == 135 && Main.netMode != 1)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, 3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, 6f, -6f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                            Projectile.NewProjectile((float)num414, (float)num415, -6f, -6f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] == 195)
                        {
                            if ((double)npc.life < (double)npc.lifeMax * 0.35 && Main.netMode != 1)
                            {
                                npc.ai[2] = 12;
                            }
                            else
                            {
                                npc.ai[2] = 0;
                            }
                            npc.netUpdate = true;
                            npc.ai[1] = 0;
                        }
                    }
                    if (npc.ai[2] == 12)
                    {
                        npc.ai[1] += 1;
                        if (npc.ai[1] == 5)
                        {
                            npc.ai[0] = 1;
                            npc.netUpdate = true;
                        }
                        if (npc.ai[1] == num005 && Main.netMode != 1)
                        {
                            if (Main.expertMode)
                            {
                                npc.netUpdate = true;
                                NPC.NewNPC((int)(npc.position.X -= 160), (int)(npc.position.Y -= 160), mod.NPCType("KeepersBlade"));
                                NPC.NewNPC((int)(npc.position.X += 160), (int)(npc.position.Y += 160), mod.NPCType("KeepersBlade"));
                                Main.PlaySound(2, (int)npc.position.X, (int)npc.position.Y, 20);
                                NPC.NewNPC((int)(npc.position.X -= 160), (int)(npc.position.Y += 160), mod.NPCType("KeepersBlade"));
                                NPC.NewNPC((int)(npc.position.X += 160), (int)(npc.position.Y -= 160), mod.NPCType("KeepersBlade"));
                            }
                        }
                        if (npc.ai[1] == num006)
                        {
                            npc.ai[1] = 0;
                            npc.netUpdate = true;
                            npc.ai[2] = 0;
                            npc.ai[0] = 0;
                            npc.ai[3] = 0;
                        }
                    }
                    npc.dontTakeDamage = false;
                }
                if (npc.ai[3] == 1) //dissapears into clouds, spawning a bunch of projectiles anywhere around him that don't deal contact damage, the projectiles explode after a random time but always before the boss reapears.
                {
                    npc.dontTakeDamage = true;
                    npc.ai[1] += 1;
                    int num5 = 4;
                    if (npc.ai[1] == 50 && Main.netMode != 1 || npc.ai[1] == 100 && Main.netMode != 1 || npc.ai[1] == 150 && Main.netMode != 1)
                    {
                        npc.netUpdate = true;
                        float Speed = 12f;  // projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 32;  // projectile damage
                        int type = mod.ProjectileType("HollowBlast");  //put your projectile
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                    }
                    if (npc.ai[1] >= 200 && Main.netMode != 1)
                    {
                        npc.ai[1] = 0;
                        float Speed = 0.5f;  // projectile speed
                        npc.netUpdate = true;
                        Projectile.NewProjectile((float)num414, (float)num415, 0f, -Speed, mod.ProjectileType("HollowFire"), 32, 0f, Main.myPlayer, 0f, 0f);
                        npc.ai[2] = 0;
                        npc.ai[0] = 0;
                        npc.ai[3] = 2;
                        Projectile.NewProjectile((float)num414, (float)num415, 3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 34, 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile((float)num414, (float)num415, -3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 34, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
                if (npc.ai[3] == 2) //dissapears into clouds, spawning a bunch of projectiles anywhere around him that don't deal contact damage, the projectiles explode after a random time but always before the boss reapears.
                {
                    npc.ai[1] += 1;
                    if (npc.ai[1] <= 450 && Main.netMode != 1)
                    {
                        npc.alpha = 200;
                        npc.ai[2] += 1;
                        npc.netUpdate = true;
                    }
                    else
                    {
                        npc.alpha -= 4;
                    }
                    if (npc.ai[2] >= 45 && npc.ai[1] <= 450 && Main.netMode != 1)
                    {
                        npc.ai[0] = 1;
                        npc.netUpdate = true;
                        npc.ai[2] = 0;
                        Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, mod.ProjectileType("HollowSpawn"), 34, 0f, Main.myPlayer, 0f, 0f);
                    }
                    if (npc.ai[1] >= 500 && Main.netMode != 1)
                    {
                        npc.ai[2] = 0;
                        npc.ai[1] = 0;
                        npc.ai[3] = 0;
                        npc.netUpdate = true;
                        npc.alpha = 0;
                        Projectile.NewProjectile((float)num414, (float)num415, 3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                        Projectile.NewProjectile((float)num414, (float)num415, -3f, -3f, mod.ProjectileType("HollowBoomBoomSpawn"), 36, 0f, Main.myPlayer, 0f, 0f);
                    }
                }
            }
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead)
            {
                npc.TargetClosest(true);
            }
        }
    }
}