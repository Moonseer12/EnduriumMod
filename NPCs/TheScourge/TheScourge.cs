using System;
using System.Collections.Generic;
using System.IO;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.TheScourge
{
    public class TheScourge : ModNPC
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("The Scourge");
        }
        public override void SetDefaults()
        {
            npc.lifeMax = 100000;
            npc.width = 82;
            npc.height = 116;
            npc.damage = 60;
            npc.defense = 0;
            npc.HitSound = SoundID.NPCHit1;
            Main.npcFrameCount[npc.type] = 6;
            npc.DeathSound = SoundID.NPCDeath30;
            npc.knockBackResist = 0f;
            music = mod.GetSoundSlot(SoundType.Music, "Sounds/Music/Fury_of_the_Jungle");
            npc.noGravity = true;
            bossBag = mod.ItemType("JungleBag");
            npc.noTileCollide = true;
            npc.aiStyle = -1; //new
            aiType = -1; //new
        }
        public override void ScaleExpertStats(int numPlayers, float bossLifeScale)
        {
            npc.lifeMax = (int)(npc.lifeMax * 0.5f * bossLifeScale);
            npc.damage = 75;
        }

        public override void FindFrame(int frameHeight)
        {
            if (npc.ai[3] == 0)
            {
                npc.frame.Y = 0 * frameHeight;
            }
            if (npc.ai[3] == 4 || npc.ai[3] == 5)
            {
                if (npc.ai[0] >= 60)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
            if (npc.ai[3] == 6)
            {
                if (npc.ai[0] >= 10)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
            if (npc.ai[3] == 10 || npc.ai[3] == 12)
            {
                npc.frame.Y = 1 * frameHeight;
            }
            if (npc.ai[3] == 11)
            {
                npc.frame.Y = 2 * frameHeight;
            }
            if (npc.ai[3] >= 1 && npc.ai[3] <= 2)
            {
                if (npc.ai[1] > 22 && npc.ai[1] < 327)
                {
                    npc.frame.Y = 5 * frameHeight;
                }
                if ((npc.ai[1] < 22 && npc.ai[1] > 11) || (npc.ai[1] > 327 && npc.ai[1] < 338))
                {
                    npc.frame.Y = 4 * frameHeight;
                }
                if (npc.ai[1] < 11 || npc.ai[1] > 349)
                {
                    npc.frame.Y = 3 * frameHeight;
                }
            }
        }
        float movementACCX = 0.12f;
        float movementACCY = 0.23f;
        float movementCAPX = 11f;
        float movementCAPY = 14f;

        float numberProjectiles = 28; // 3, 4, or 5 shots
        int i = 0;
        public override void AI()
        {
            Player player = Main.player[npc.target];
            if (!player.active || player.dead)
            {
                npc.TargetClosest(false);
                player = Main.player[npc.target];
                if (!player.active || player.dead)
                {
                    npc.velocity = new Vector2(0f, -10f);
                    if (npc.timeLeft > 150)
                    {
                        npc.timeLeft = 150;
                    }
                    return;
                }
            }
            else if (npc.timeLeft > 1800)
            {
                npc.timeLeft = 1800;
            }
            if (npc.ai[3] == 0) //movement, shooting basic 2 projectiles, charging at the player and floating away to a position where they can do attack.
            {
                if (npc.velocity.Y >= movementCAPY)
                {
                    npc.velocity.Y = movementCAPY;
                }
                if (npc.velocity.Y <= -movementCAPY)
                {
                    npc.velocity.Y = -movementCAPY;
                }
                if (npc.velocity.X >= movementCAPX)
                {
                    npc.velocity.X = movementCAPX;
                }
                if (npc.velocity.X <= -movementCAPX)
                {
                    npc.velocity.X = -movementCAPX;
                }
                else if (npc.position.Y + (float)(npc.height / 2) > player.position.Y + (float)(player.height / 2) + 50f)
                {
                    npc.velocity.Y = npc.velocity.Y -= movementACCY;
                }
                else if (npc.position.Y + (float)(npc.height / 2) < player.position.Y + (float)(player.height / 2) - 50f)
                {
                    npc.velocity.Y = npc.velocity.Y += movementACCY;
                }
                if (npc.position.X + (float)(npc.width / 2) > (player.position.X + (float)(player.width / 2)) + 50f)
                {
                    npc.velocity.X = npc.velocity.X -= movementACCX;
                }
                if (npc.position.X + (float)(npc.width / 2) < (player.position.X + (float)(player.width / 2)) - 50f)
                {
                    npc.velocity.X = npc.velocity.X += movementACCX;
                }


                npc.ai[0] += 1f;
                npc.ai[1] += 1f;
                if (npc.ai[0] >= 340)
                {
                    npc.velocity *= 0.4f;
                }
                else
                {

                    if (npc.ai[1] >= 40)
                    {
                        Main.PlaySound(SoundID.Item17, npc.position);
                        float Speed = 12f;  // projectile speed
                        Vector2 vector8 = new Vector2(npc.position.X + (npc.width / 4), npc.position.Y + (npc.height / 4));
                        int damage = 20;  // projectile damage
                        int type = mod.ProjectileType("ScourgeBoltTiny");  //put your projectile
                        float rotation = (float)Math.Atan2(vector8.Y - (player.position.Y + (player.height * 0.5f)), vector8.X - (player.position.X + (player.width * 0.5f)));
                        int num54 = Projectile.NewProjectile(vector8.X, vector8.Y, (float)((Math.Cos(rotation) * Speed) * -1), (float)((Math.Sin(rotation) * Speed) * -1), type, damage, 0f, Main.myPlayer);
                        npc.netUpdate = true;
                        npc.ai[1] = 0f;
                        npc.ai[2] += 1f;
                    }

                    if (npc.ai[2] >= 10)
                    {
                        npc.ai[0] = 0f;
                        npc.ai[1] = 0f;
                        npc.ai[2] = 0f;
                        npc.ai[3] = 10f;
                    }
                }
                if (npc.ai[0] >= 380)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = Main.rand.Next(1, 2);
                    npc.ai[3] = 6;
                }
            } //normal shit

            if (npc.ai[3] >= 1 && npc.ai[3] <= 2) //1-3 depending on these he launches a different amount of projectiles at a time
            {
                npc.ai[0] += 1f;
                npc.ai[1] += 1f;
                if (npc.ai[0] >= 70) //shoot projectile
                {
                    npc.ai[0] = 0;
                    for (int k = 0; k < npc.ai[3]; k++)
                    {
                        NPC.NewNPC((int)(npc.position.X + (float)(npc.width / 2)), (int)(npc.position.Y - 60 + (float)(npc.height / 2)), mod.NPCType("ScourgeSpitNonOrbit"), npc.damage, 0f, Main.myPlayer, npc.whoAmI);
                    }
                }
                if (npc.ai[1] >= 360)
                {
                    npc.ai[3] = 0;
                    npc.ai[1] = 0;
                    npc.ai[0] = 0;
                }
            } //normal shit but animated

            if (npc.ai[3] == 3) //chooses between 4 and 5
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 80)
                {
                    npc.ai[2] = 0;
                    npc.ai[0] = 0;
                    npc.ai[1] = Main.rand.Next(4, 5);
                    npc.ai[3] = 6;
                }
            }
            if (npc.ai[3] == 4) //surronds player with rotating vine projectiles that randomly go towards player
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 120) //summons a single rune
                {
                    int num1 = NPC.NewNPC((int)npc.Center.X, (int)npc.Center.Y, mod.NPCType("ScourgeRune"));
                    Main.npc[num1].ai[0] = player.whoAmI;
                    Main.npc[num1].netUpdate = true;

                    npc.ai[0] = 112;
                    npc.ai[1] += 1;
                }
                if (npc.ai[1] >= 20)
                {
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 6;
                }
            }
            if (npc.ai[3] == 5)
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] == 150)
                {
                    float rotation = MathHelper.ToRadians(360);
                    if (i <= numberProjectiles)
                    {
                        i += 1;
                        if (npc.ai[2] >= 10)
                        {
                            npc.ai[2] = 0;
                            Vector2 perturbedSpeed = new Vector2(5f, 5f).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed.X, perturbedSpeed.Y, mod.ProjectileType("ScourgeBoltTiny"), npc.damage, 1f, Main.myPlayer);

                            Vector2 perturbedSpeed2 = new Vector2(15f, 15f).RotatedBy(MathHelper.Lerp(-rotation, rotation, i / (numberProjectiles - 1))) * .2f; // Watch out for dividing by 0 if there is only 1 projectile.
                            Projectile.NewProjectile(npc.Center.X, npc.Center.Y, perturbedSpeed2.X, perturbedSpeed2.Y, mod.ProjectileType("ScourgeBoltTiny"), npc.damage, 1f, Main.myPlayer);
                        }
                    }
                }
                if (npc.ai[0] >= 180) //summons homing projectiles
                {
                    i = 0;
                    numberProjectiles = 28;
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                    npc.ai[2] = 0;
                    npc.ai[3] = 6;
                }
            }
            if (npc.ai[3] != 6) //teleport cancel
            {
                if (npc.alpha >= 0)
                {
                    npc.alpha -= 10;
                }
            }
            if (npc.ai[3] == 6) //teleport
            {
                npc.velocity *= 0.9f;
                npc.ai[0] += 1;
                npc.alpha += 5;
                if (npc.ai[0] >= 50)
                {
                    npc.position.Y = player.position.Y + (float)Main.rand.Next(-200, 201);
                    npc.position.X = player.position.X + (float)Main.rand.Next(-200, 201);
                    npc.ai[3] = npc.ai[1];
                    npc.ai[0] = 0;
                    npc.ai[1] = 0;
                }
            }

            if (npc.ai[3] == 10)
            {
                npc.velocity *= 0.9f;
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 60)
                {
                    npc.ai[0] = 0f;
                    npc.ai[1] = 0f;
                    npc.ai[2] = 0f;
                    npc.ai[3] = 11;

                    for (int k = 0; k < 20; k++)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, 3, 1f, 1f, 0, default(Color), 1f);
                        Dust.NewDust(npc.position, npc.width, npc.height, 24, 1f, 1f, 0, default(Color), 1f);
                    }
                    for (int g = 0; g < 5; g++)
                    {
                        Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TheScourgeCloak_1"), 1f);
                        Gore.NewGore(npc.position, npc.velocity, mod.GetGoreSlot("Gores/TheScourgeCloak_2"), 1f);
                    }
                }
            }
            if (npc.ai[3] == 11)
            {
                if (npc.ai[1] >= 30)
                {
                    npc.ai[2] += 0.004f;
                }
                npc.ai[0] += 1;
                if (npc.ai[1] >= 1)
                {
                    npc.ai[1] += 1f;
                }
                if (npc.ai[1] >= 620)
                {
                    npc.ai[3] = 12;
                    npc.ai[0] = 0;
                    npc.ai[2] = 0;
                }
                if (npc.ai[0] >= 60)
                {
                    npc.ai[0] = 0;
                    Vector2 PlayerPosition = new Vector2(player.Center.X + Main.rand.Next(-50, 51) - npc.Center.X, player.Center.Y + Main.rand.Next(-50, 51) - npc.Center.Y);
                    PlayerPosition.Normalize();
                    float velocityX = PlayerPosition.X *= 6f;
                    float velocityY = PlayerPosition.Y *= 6f;
                    Vector2 spinningpoint = new Vector2(velocityX, velocityY);
                    Vector2 center = npc.Center;
                    float num = 0.7853982f;
                    int num2 = 8;
                    float num3 = -(num * 2f) / (float)(num2 - 1);
                    for (int i = 0; i < num2; i++)
                    {
                        int num4 = Projectile.NewProjectile(center, spinningpoint.RotatedBy((double)(num + num3 * (float)i), default(Vector2)), mod.ProjectileType("ScourgeBoltTiny"), npc.damage, 1f, Main.myPlayer);
                    }
                }
                if (npc.ai[1] == 0)
                {
                    npc.ai[1] = 1;
                    for (int num624 = 0; num624 < 15; num624++) //todo, basically make it so instead of doing the random code in ScourgeVine, do it here and then spawn 2 projectiles, one will basically act like a preview of where the vine will appear.
                    {
                        float RandomNum = Main.rand.NextFloat(1f, 361f);

                        int preview = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ScourgeVinePreview"), npc.damage, 3f, Main.myPlayer, npc.whoAmI);
                        int vine = Projectile.NewProjectile(npc.Center.X, npc.Center.Y, 0f, 0f, mod.ProjectileType("ScourgeVine"), npc.damage, 3f, Main.myPlayer, npc.whoAmI);
                        Main.projectile[preview].ai[1] = RandomNum;
                        Main.projectile[vine].ai[1] = RandomNum;
                    }
                }
            }
            if (npc.ai[3] == 12)
            {
                npc.ai[0] += 1f;
                if (npc.ai[0] >= 30)
                {
                    npc.ai[0] = 0f;
                    npc.ai[3] = 3;
                    for (int k = 0; k < 20; k++)
                    {
                        Dust.NewDust(npc.position, npc.width, npc.height, 3, 1f, 1f, 0, default(Color), 1f);
                        Dust.NewDust(npc.position, npc.width, npc.height, 24, 1f, 1f, 0, default(Color), 1f);
                    }
                }
            }
        }

        public override void NPCLoot()
        {
            if (Main.expertMode)
            {
                npc.DropBossBags();
            }
            else
            {
                Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TropicTooth"), Main.rand.Next(15, 25));
                int choice = Main.rand.Next(2);
                if (Main.rand.Next(3) == 0)
                {
                    if (choice == 0)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("JungleHatchet"));
                    }

                    if (choice == 1)
                    {
                        Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("RazorShot"));
                    }
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("SapSpray"));
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("ThornBlade"));
                }
                if (Main.rand.Next(4) == 0)
                {
                    Item.NewItem((int)npc.position.X, (int)npc.position.Y, npc.width, npc.height, mod.ItemType("TheScourge"));
                }
            }
        }
        public override void HitEffect(int hitDirection, double damage)
        {
            for (int k = 0; k < 10; k++)
            {
                int dust1 = Dust.NewDust(npc.position, npc.width, npc.height, mod.DustType("Plaugue"), hitDirection, -1f, 0, default(Color), 1f);
                Main.dust[dust1].noGravity = true;
            }
        }
    }
}