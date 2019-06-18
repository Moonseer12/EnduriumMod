using System;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.PrismMonolith
{
    [AutoloadBossHead]
    public class ThePrismArcanum : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Prism Arcanum");
        }

        public override void SetDefaults()
        {

            npc.lifeMax = 9000;
            npc.damage = 65;
            npc.knockBackResist = 0f;
            npc.width = 96;
            npc.height = 100;
            npc.value = Item.buyPrice(0, 5, 0, 0);
            npc.npcSlots = 1f;
            npc.boss = true;
            npc.lavaImmune = true;
            npc.noGravity = true;
            npc.aiStyle = -1;
            npc.noTileCollide = true;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath2;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/First_Prism");
            npc.netAlways = true;
            bossBag = mod.ItemType("PrismBag");
            npc.defense = 8;
            npc.scale = 0.9f;
        }
        public override Color? GetAlpha(Color lightColor)
        {
            return Color.White;
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.damage = 70;
            npc.defense = 24;
            npc.lifeMax = 11000;
        }
        public override void BossLoot(ref string name, ref int potionType)
        {
            potionType = ItemID.HealingPotion;   //boss drops
        }
        float Speed = 0f;
        public override void HitEffect(int hitDirection, double damage)
        {
            if (npc.life <= 0)          //this make so when the npc has 0 life(dead) he will spawn a gore
            {
                if (!EnduriumWorld.downedPrismArcanum)
                {
                    EnduriumWorld.downedPrismArcanum = true;   //so the message and the ore spawn does not proc(show) when you kill EoC/npc again
                                                               //Red  Green Blue
                    Main.NewText("A bright blue light shines from the dark caverns", 1, 50, 255);  //this is the message that will appear when the npc is killed  , 200, 200, 55 is the text color
                    for (int k = 0; k < (int)((double)(Main.maxTilesX * Main.maxTilesY) * 6E-05); k++)
                    {
                        int i2 = WorldGen.genRand.Next(0, Main.maxTilesX);
                        int j2 = WorldGen.genRand.Next((int)(Main.maxTilesY * .3f), (int)(Main.maxTilesY * .9f));
                        if (Main.tile[i2, j2].type == 203 || Main.tile[i2, j2].type == 117 || Main.tile[i2, j2].type == 1 || Main.tile[i2, j2].type == 25)
                            WorldGen.OreRunner(i2, j2, (double)WorldGen.genRand.Next(4, 12), WorldGen.genRand.Next(4, 12), (ushort)mod.TileType("Aquamarine"));
                    }
                }
            }
        }
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (player.Center.X > npc.Center.X)
            {
                npc.spriteDirection = 1;
            }
            else
            {
                npc.spriteDirection = -1;
            }
            if (npc.ai[3] == 0)
            {
                npc.ai[0] += 1f;
                if (Main.expertMode)
                {
                    npc.ai[0] += 0.2f;
                }
                Vector2 vector102 = new Vector2(npc.Center.X, npc.Center.Y);
                float num791 = Main.player[npc.target].Center.X - vector102.X;
                float num792 = Main.player[npc.target].Center.Y - vector102.Y;
                float num793 = (float)Math.Sqrt((double)(num791 * num791 + num792 * num792));
                float num794 = 8f;
                num793 = num794 / num793;
                num791 *= num793;
                num792 *= num793;
                npc.velocity.X = (npc.velocity.X * 100f + num791) / 101f;
                npc.velocity.Y = (npc.velocity.Y * 100f + num792) / 101f;
                if (npc.ai[0] >= 60f && npc.ai[0] <= 160f)
                {
                    int chance = 7;
                    if (Main.expertMode)
                    {
                        chance = 6;
                    }
                    if (Main.rand.Next(chance) == 0)
                    {
                        npc.netUpdate = true;
                        Vector2 vector23 = new Vector2(npc.position.X + 29 + (float)(npc.width / 2), (int)(npc.position.Y - 16 + (float)(npc.height / 2)));
                        if (player.Center.X > npc.Center.X)
                        {
                            vector23 = new Vector2(npc.position.X + 29 + (float)(npc.width / 2), (int)(npc.position.Y - 16 + (float)(npc.height / 2)));
                        }
                        else
                        {
                            vector23 = new Vector2(npc.position.X - 29 + (float)(npc.width / 2), (int)(npc.position.Y - 16 + (float)(npc.height / 2)));
                        }
                        float num147 = 12f;
                        float num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                        float num149 = Math.Abs(num148) * 0.1f;
                        float num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y - num149;
                        float num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                        npc.netUpdate = true;
                        num151 = num147 / num151;
                        num148 *= num151;
                        num150 *= num151;
                        int num25;
                        for (int num154 = 0; num154 < 1; num154 = num25 + 1)
                        {
                            num148 = Main.player[npc.target].position.X + (float)Main.player[npc.target].width * 0.5f - vector23.X;
                            num150 = Main.player[npc.target].position.Y + (float)Main.player[npc.target].height * 0.5f - vector23.Y;
                            num151 = (float)Math.Sqrt((double)(num148 * num148 + num150 * num150));
                            num151 = 12f / num151;
                            num148 += (float)Main.rand.Next(-100, 101);
                            num150 += (float)Main.rand.Next(-100, 101);
                            num148 *= num151;
                            num150 *= num151;
                            Projectile.NewProjectile(vector23.X, vector23.Y, num148, num150, mod.ProjectileType("Knoive"), 27, 0f, Main.myPlayer, 0f, 0f);
                            num25 = num154;
                        }
                    }
                }
                if (npc.ai[0] >= 210f)
                {
                    npc.ai[1] += 1;
                    npc.ai[0] = 20f;
                }
                if (npc.ai[1] == 3)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[3] = 1; // 5 projectiles in a line
                    }
                    else
                    {
                        npc.ai[3] = 2; // cold dbomb
                    }
                }
            }
            if (npc.ai[3] == 1)
            {
                npc.ai[0] += 1f;
                if (Main.expertMode)
                {
                    npc.ai[0] += 0.5f;
                }
                if (npc.ai[1] == 5)
                {
                    npc.ai[3] = 3;
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                }
                if (npc.ai[0] >= 120f)
                {
                    npc.ai[1] += 1;
                    npc.ai[0] = 0;
                    float Speed = 7f;  // projectile speed
                    for (int num623 = 0; num623 < 15; num623++)
                    {
                        int num624 = Dust.NewDust(new Vector2(npc.position.X, npc.position.Y), npc.width, npc.height, 132, 0f, 0f, 100, default(Color), 1.6f);
                        Main.dust[num624].noGravity = true;
                        Main.dust[num624].velocity *= 4f;
                    }
                    Vector2 vector8 = new Vector2(npc.position.X + 29 + (npc.width / 2), npc.position.Y - 16 + (npc.height / 2));
                    if (player.Center.X > npc.Center.X)
                    {
                        vector8 = new Vector2(npc.position.X + 29 + (npc.width / 2), npc.position.Y - 16 + (npc.height / 2));
                    }
                    else
                    {
                        vector8 = new Vector2(npc.position.X - 29 + (npc.width / 2), npc.position.Y - 16 + (npc.height / 2));
                    }
                    int num25;
                    int amount = 5;
                    if (Main.expertMode)
                    {
                        amount = 7;
                    }
                    for (int num154 = 0; num154 < amount; num154 = num25 + 1)
                    {
                        Speed += 2.5f;
                        int damage = 23;  // projectile damage
                        int type = mod.ProjectileType("Knoive");  //put your projectile
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, 0);
                        num25 = num154;
                    }
                }
            }
            if (npc.ai[3] == 2)
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 40)
                {
                    npc.ai[1] += 1;
                    npc.ai[0] = 0;
                    Main.PlaySound(SoundID.Item8, npc.position);
                    if (Main.netMode != 1)
                    {
                        NPC.NewNPC((int)(player.position.X + Main.rand.Next(-50, 51) + npc.velocity.X), (int)(player.position.Y + Main.rand.Next(-50, 51) + npc.velocity.Y), mod.NPCType("ColdBomb"), 0, 0f, 1f, 0f, 0f, 255);
                    }
                    npc.netUpdate = true;
                }
                if (npc.ai[1] >= 5)
                {
                    npc.ai[3] = 3;
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                }
                npc.netUpdate = true;
                npc.velocity *= 0.9f;
               
            }
            if (npc.ai[3] == 3)
            {
                npc.velocity *= 0.9f;
                npc.ai[0] += 1;
                if (npc.ai[0] >= 120)
                {
                    if (npc.ai[2] == 0)
                    {
                        npc.ai[3] = 5; //glaive
                    }
                    else
                    {
                        npc.ai[3] = 4; // circel
                    }
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                }
            }
            if (npc.ai[3] == 4)
            {
                if (Main.expertMode)
                {
                    npc.ai[0] += 0.5f;
                }
                npc.ai[0] += 1f;
                if (npc.ai[1] == 5)
                {
                    npc.ai[3] = 5;
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                }
                npc.velocity *= 0.95f;
                if (npc.ai[0] >= 75f)
                {
                    npc.ai[1] += 1;
                    npc.ai[0] = 0f;
                    if (player.Center.X > npc.Center.X)
                    {
                        NPC.NewNPC((int)(npc.position.X + 29 + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y - 16 + (float)(npc.height / 2) + npc.velocity.Y), mod.NPCType("PrismSpell2"), 0, 0f, 1f, 0f, 0f, 255);
                    }
                    else
                    {
                        NPC.NewNPC((int)(npc.position.X - 29 + (float)(npc.width / 2) + npc.velocity.X), (int)(npc.position.Y - 16 + (float)(npc.height / 2) + npc.velocity.Y), mod.NPCType("PrismSpell2"), 0, 0f, 1f, 0f, 0f, 255);
                    }
                }
            }
            if (npc.ai[3] == 5)
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 80)
                {
                    npc.ai[1] += 1;
                    npc.ai[0] = 0;
                    Main.PlaySound(SoundID.Item8, npc.position);
                    if (Main.netMode != 1)
                    {
                        int num414 = (int)(player.position.X + Main.rand.Next(-100, 101));
                        int num415 = (int)(player.position.Y - 900);
                        int velocityChoice = Main.rand.Next(4);
                        if (velocityChoice == 0)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, 3f, 0f, mod.ProjectileType("IceFall"), 24, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (velocityChoice == 1)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, 6f, 0f, mod.ProjectileType("IceFall"), 24, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (velocityChoice == 2)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, -3f, 0f, mod.ProjectileType("IceFall"), 24, 0f, Main.myPlayer, 0f, 0f);
                        }
                        if (velocityChoice == 3)
                        {
                            Projectile.NewProjectile((float)num414, (float)num415, -6f, 0f, mod.ProjectileType("IceFall"), 24, 0f, Main.myPlayer, 0f, 0f);
                        }
                    }

                    npc.netUpdate = true;
                }
                if (npc.ai[1] >= 3)
                {
                    npc.ai[2] = 1;
                    npc.ai[3] = 0;
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                }
                npc.netUpdate = true;
            }
            Main.NewText(npc.ai[3]);
            if (npc.ai[3] == 1 || npc.ai[3] == 5)
            {
                npc.noGravity = true;
                npc.noTileCollide = true;
                npc.knockBackResist = 0f;
                if (npc.localAI[0] == 0f)
                {
                    npc.TargetClosest(true);
                    npc.localAI[0] = 1f;
                    npc.localAI[1] = (float)npc.direction;
                }
                else if (npc.localAI[0] == 1f)
                {
                    npc.TargetClosest(true);
                    float num1289 = 0.22f;
                    float num1290 = 5.8f;
                    float num1291 = 3.5f;
                    float num1292 = 510f;
                    float num1293 = 4.8f;
                    npc.velocity.X = npc.velocity.X + npc.localAI[1] * num1289;
                    if (npc.velocity.X > num1290)
                    {
                        npc.velocity.X = num1290;
                    }
                    if (npc.velocity.X < -num1290)
                    {
                        npc.velocity.X = -num1290;
                    }
                    float num1294 = Main.player[npc.target].Center.Y - npc.Center.Y;
                    if (Math.Abs(num1294) > num1291)
                    {
                        num1293 = 15f;
                    }
                    if (num1294 > num1291)
                    {
                        num1294 = num1291;
                    }
                    else if (num1294 < -num1291)
                    {
                        num1294 = -num1291;
                    }
                    npc.velocity.Y = (npc.velocity.Y * (num1293 - 1f) + num1294) / num1293;
                    if ((npc.localAI[1] > 0f && Main.player[npc.target].Center.X - npc.Center.X < -num1292) || (npc.localAI[1] < 0f && Main.player[npc.target].Center.X - npc.Center.X > num1292))
                    {
                        npc.localAI[0] = 2f;
                        npc.localAI[1] = 0f;
                        if (npc.Center.Y + 20f > Main.player[npc.target].Center.Y)
                        {
                            npc.localAI[1] = -1f;
                        }
                        else
                        {
                            npc.localAI[1] = 1f;
                        }
                    }
                }
                else if (npc.localAI[0] == 2f)
                {
                    float num1295 = 0.3f;
                    float scaleFactor13 = 0.85f;
                    float num1296 = 4.8f;
                    npc.velocity.Y = npc.velocity.Y + npc.localAI[1] * num1295;
                    if (npc.velocity.Length() > num1296)
                    {
                        npc.velocity *= scaleFactor13;
                    }
                    if (npc.velocity.X > -1f && npc.velocity.X < 1f)
                    {
                        npc.TargetClosest(true);
                        npc.localAI[0] = 3f;
                        npc.localAI[1] = (float)npc.direction;
                    }
                }
                else if (npc.localAI[0] == 3f)
                {
                    float num1297 = 0.38f;
                    float num1298 = 0.22f;
                    float num1299 = 4.8f;
                    float scaleFactor14 = 0.95f;
                    npc.velocity.X = npc.velocity.X + npc.localAI[1] * num1297;
                    if (npc.Center.Y > Main.player[npc.target].Center.Y)
                    {
                        npc.velocity.Y = npc.velocity.Y - num1298;
                    }
                    else
                    {
                        npc.velocity.Y = npc.velocity.Y + num1298;
                    }
                    if (npc.velocity.Length() > num1299)
                    {
                        npc.velocity *= scaleFactor14;
                    }
                    if (npc.velocity.Y > -1f && npc.velocity.Y < 1f)
                    {
                        npc.TargetClosest(true);
                        npc.localAI[0] = 0f;
                        npc.localAI[1] = (float)npc.direction;
                    }
                }
            }
                npc.dontTakeDamage = false;
            
            if (npc.target < 0 || npc.target == 255 || Main.player[npc.target].dead || !Main.player[npc.target].active)
            {
                npc.TargetClosest(true);
            }
            if (Main.player[npc.target].dead)
            {
                npc.velocity.Y -= 0.8f;
                npc.velocity.X *= 0.9f;
                npc.netUpdate = true;
            }
            if (!Main.player[npc.target].dead && Main.netMode != 1)
            {
                npc.timeLeft = 100;
                npc.netUpdate = true;
            }
        }
        public override bool? DrawHealthBar(byte hbPosition, ref float scale, ref Vector2 position)
        {
            scale = 1.5f;
            return null;
        }
        public override void NPCLoot()
        {
            Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("CrypticPowerCell"), Main.rand.Next(15, 25));
            if (Main.rand.Next(10) == 0)
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("PrismHood"));
            }
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                int itemChoice = Main.rand.Next(4);
                if (itemChoice == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ArcanumPike"));
                }
                else if (itemChoice == 1)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("FrozenCleaver"));
                }
                else if (itemChoice == 2)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("IceClasper"));
                }
                else if (itemChoice == 3)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheArcticWind"));
                }
            }
        }
    }
}