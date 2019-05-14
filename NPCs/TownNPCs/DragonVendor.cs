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
    public class DragonVendor : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "EnduriumMod/NPCs/TownNPCs/DragonVendor";
            }
        }
        public override bool Autoload(ref string name)
        {
            name = "Dragon Vendor";
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
            Main.npcFrameCount[npc.type] = 26;
            animationType = NPCID.TravellingMerchant;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (NPC.downedPlantBoss)
            {
                return true;

            }
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            if (NPC.downedPlantBoss)
            {
                return true; //post plantera spawning add
            }
            return false;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(14))
            {
                case 0:
                    return "Casey";
                case 1:
                    return "Smaug";
                case 2:
                    return "Sam";
                case 3:
                    return "Julian";
                default:
                    return "Gavon";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
            button2 = "Flame Components";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
        {
            Player player = Main.player[Main.myPlayer];
            if (firstButton)
            {
                openShop = true;
            }
            if (!firstButton)
            {
                if (EnduriumWorld.DragonTimer == 0)
                {
                    Main.npcChatText = "Take this refined dragon flesh and blood!";
                    EnduriumWorld.DragonTimer = 72000;
                    int choice = Main.rand.Next(3);
                    if (choice == 0)
                    {
                        player.QuickSpawnItem(mod.ItemType("DragonBlood"), Main.rand.Next(4, 12));
                        player.QuickSpawnItem(mod.ItemType("DragonShard"), Main.rand.Next(2, 14));
                    }
                    if (choice == 1)
                    {
                        player.QuickSpawnItem(mod.ItemType("DragonBlood"), Main.rand.Next(3, 16));
                    }
                    if (choice == 2)
                    {
                        player.QuickSpawnItem(mod.ItemType("DragonShard"), Main.rand.Next(4, 6));
                    }
                }
				else
                {
                    Main.npcChatText = "Sorry pal, i dont have more components as of now.";
                }
            }
        }



        public override void SetupShop(Chest shop, ref int nextSlot)
        {
            if (NPC.downedPlantBoss)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("AltarofFire"));      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(5);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
                nextSlot++;
            }
            if (Main.bloodMoon)
            {
                shop.item[nextSlot].SetDefaults(mod.ItemType("Artifact"));      //this defines what item to sell .
                shop.item[nextSlot].shopCustomPrice = new int?(10);  //this is the custom price, so this item will cost 20 custom Currency
                shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
                nextSlot++;
            }
            shop.item[nextSlot].SetDefaults(mod.ItemType("DragonCataclysm"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("DragonDualshot"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("DragonFlameblast"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("DragonGreatblade"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("TheDragun"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.SoulGemID;  //this make so to use the CustomCurrency             
            nextSlot++;
        }
        public override string GetChat()
        {
            if (Main.bloodMoon)
            {
                switch (Main.rand.Next(10))
                {
                    case 0:
                        return "I can feel the wild fire.";
                    case 1:
                        return "The moon reminds me of the taste of blood!";
                    case 2:
                        return "I was born during a blood moon.";
                    case 3:
                        return "Think about this, what if the moon was actually blue instead of red, oh wait.";
                    case 4:
                        return "Ive heard that there are undead creatures running around, stop them!";
                    case 5:
                        return "I saw something outside! It had a knive.";
                    case 6:
                        return "This is fine.";
                    case 7:
                        return "Be sure to get your healing potions ready.";
                    case 8:
                        return "Theres a prophecy that if you collect 4 shards of the moon the forgotten titan will be summoned. thats just a rumor though theres no way such thing could exist!";
                    case 9:
                        return "Is it over yet?";
                }

            }
            switch (Main.rand.Next(14))
            {
                case 0:
                    return "Their flesh is tasty...";
                case 1:
                    return "I'm considering burning this house.";
                case 2:
                    return "Welcome.";
                case 3:
                    return "Slay those pesky beasts!";
                case 4:
                    return "Hello!";
                case 5:
                    return "It's suprising, you always come back from death. Tell me how, i want to know!";
                case 6:
                    return "Ive heard bad things.";
                case 7:
                    return "Get yourself a flamethrower, those work really well";
                case 8:
                    return "I wonder how those hell creatures are doing.";
                case 9:
                    return "I put a lot of time into the dragon burn elixir, buy it!";
                case 10:
                    return "There are bone merchants in the cave system! If you see bunnies running around there could be one nearby.";
                case 11:
                    return "Your gonna have to pay for the goods i provide";
                case 12:
                    return "Come everyday, i've got daily supplies of dragon blood!";
                case 13:
                    return "Those hell creatures may come back after death from time to time. They will be in their soul form, make sure to kill it before it escapes!";
                default:
                    return "I've slayed a lot of dragon souls, you have a slight chance to store them in a vessel.";

            }
        }
    }
}
