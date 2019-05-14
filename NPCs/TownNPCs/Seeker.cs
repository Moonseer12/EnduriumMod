using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria.ID;
using Terraria;
using Terraria.ModLoader;

namespace EnduriumMod.NPCs.TownNPCs
{
    [AutoloadHead]
    public class Seeker : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "EnduriumMod/NPCs/TownNPCs/Seeker";
            }
        }
        public override bool Autoload(ref string name)
        {
            name = "Seeker";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ExtraFramesCount[npc.type] = 0;
            NPCID.Sets.AttackFrameCount[npc.type] = 5;
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 28;
            npc.height = 46;
            npc.aiStyle = 7;
            npc.defense = 20;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            Main.npcFrameCount[npc.type] = 25;
            animationType = NPCID.ArmsDealer;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            return true;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(6))
            {
                case 0:
                    return "Devon";
                case 1:
                    return "Darwin";
                case 2:
                    return "Mark";
                case 3:
                    return "Vincent";
                case 4:
                    return "James";
                case 5:
                    return "David";
                default:
                    return "Max";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Herbs";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
        {
            Player player = Main.player[Main.myPlayer];
            int WeirdSludge = player.FindItem(mod.ItemType("WeirdSludge"));
            int OddGemstone = player.FindItem(mod.ItemType("OddGemstone"));
            int MagicCherry = player.FindItem(mod.ItemType("MagicCherry"));
            int GoldCarrot = player.FindItem(mod.ItemType("GoldCarrot"));
            int GigantAmber = player.FindItem(mod.ItemType("GigantAmber"));

            if (firstButton)
            {
                openShop = true;
            }
            if (!firstButton)
            {
                if (player.HasItem(mod.ItemType("WeirdSludge")))
                {
                    Main.npcChatText = "This type of gel is very precious to me, i will gladly take it";
                    player.inventory[WeirdSludge].stack -= 1;
                    player.QuickSpawnItem(mod.ItemType("GreenBaggy"));
                }

                else if (player.HasItem(mod.ItemType("OddGemstone")))
                {
                    Main.npcChatText = "Shards of the prismatic crystal!? Where did you get those, i will gladly take it";
                    player.inventory[OddGemstone].stack -= 1;
                    player.QuickSpawnItem(mod.ItemType("GreenBaggy"));
                }
                else if (player.HasItem(mod.ItemType("MagicCherry")))
                {
                    player.inventory[MagicCherry].stack -= 1;
                    Main.npcChatText = "This is my favourite food, thank you! I will gladly take it";
                    player.QuickSpawnItem(mod.ItemType("GreenBaggy"));
                }
                else if (player.HasItem(mod.ItemType("GigantAmber")))
                {
                    player.inventory[GigantAmber].stack -= 1;
                    Main.npcChatText = "I have never seen an amber so big in my entire live! I will gladly take it.";
                    player.QuickSpawnItem(mod.ItemType("GreenBaggy"));
                }
                else if (player.HasItem(mod.ItemType("GreenBaggy")))
                {
                    Main.npcChatText = "I gave you the reward, open it and see whats inside!";
                }
                else if (!EnduriumWorld.SeekerFirst)
                {
                    Main.npcChatText = "Doesn't look like you have anything precious for me, look for these things. Don't lose this list as i wont give it again!";
                    player.QuickSpawnItem(mod.ItemType("SeekersList"));
                    EnduriumWorld.SeekerFirst = true;
                }
                else if (EnduriumWorld.SeekerFirst)
                {
                    Main.npcChatText = "Doesn't look like you have anything precious for me.";
                }
            }
        }



        public override void SetupShop(Chest shop, ref int nextSlot) //GoldenAmberBattleShield
        {
            shop.item[nextSlot].SetDefaults(mod.ItemType("WaterEye"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(5);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(ItemID.SlimeStaff);      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(10);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("MarbleMedallion"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(10);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("EvilHeart"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(10);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            if (NPC.downedBoss1)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("MechanicalChip"));      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(20);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
                nextSlot++;
                shop.item[nextSlot].SetDefaults(mod.ItemType("BlankTab"));      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(50);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
                nextSlot++;
            }
            if (NPC.downedBoss2)
            {
                shop.item[nextSlot].SetDefaults(982);      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
                nextSlot++;
            }
            if (NPC.downedBoss3)
            {
            }
            if (NPC.downedQueenBee)
            {
            }

            if (NPC.downedMoonlord)
            {
                shop.item[nextSlot].SetDefaults(ItemID.AnkhCharm);      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(100);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
                nextSlot++;
            }
            if (NPC.downedBoss3)
            {
          
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType("EmberCactusFruit"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(10);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
        }

        public override string GetChat()
        {
            if (Main.bloodMoon)
            {
                switch (Main.rand.Next(3))
                {
                    case 0:
                        return "The moon has a strange colour this night, and these bloody zombies can open doors!? What is going on!";
                    case 1:
                        return "I heard that there might be something lurking in the darkness, whats that behind you with that creepy smile?";
                    case 2:
                        return "This is going to be a terreible night...";
                    case 3:
                        return "This is not good, it's terreible like a nightmare! Oh hello i did not see you there.";
                }

            }
            switch (Main.rand.Next(9))
            {
                case 0:
                    return "I will sell almost everything you need, for a price of course!";
                case 1:
                    return "Bring something interesting and worth my time and i will reward you with something special.";
                case 2:
                    return "Cherries! Keep that in mind, it might be important.";
                case 3:
                    return "My clothes are all dirty.";
                case 4:
                    return "I came from a far away land. When i was little my dream was to seek the fortune and help people. I heard many legends about the mysterious moon cult and i know that they must be stopped. Now im traveling across cities to find the chosen terrarian who will slaughter the ancient evlis.";
                case 5:
                    return "The Blank runes and tabs are really expensive, but im not a greedy person so im gonna tell you how to get them. Fight underground enemies, they might have something you need to obtain these items, but you can always buy it from me if you want.";
                case 6:
                    return "Don't deal with the devil... Don't make the same mistakes some did.";

                default:
                    return "I traveled the whole tropical forest. The beauty of that area amazes me and fills me with happines. You can't be sad there.";

            }
        }
    }
}
