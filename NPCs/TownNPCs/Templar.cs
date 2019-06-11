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
    public class Templar : ModNPC
    {
        public override string Texture
        {
            get
            {
                return "EnduriumMod/NPCs/TownNPCs/Templar";
            }
        }
        public override bool Autoload(ref string name)
        {
            name = "Templar";
            return mod.Properties.Autoload;
        }

        public override void SetStaticDefaults()
        {
            NPCID.Sets.ExtraFramesCount[npc.type] = 0;
            NPCID.Sets.AttackFrameCount[npc.type] = 0;
        }
        public override void SetDefaults()
        {
            npc.townNPC = true;
            npc.friendly = true;
            npc.width = 34;
            npc.height = 50;
            npc.aiStyle = 7;
            npc.defense = 20;
            npc.lifeMax = 250;
            npc.HitSound = SoundID.NPCHit1;
            npc.DeathSound = SoundID.NPCDeath1;
            npc.knockBackResist = 0.5f;
            Main.npcFrameCount[npc.type] = 21;
            animationType = NPCID.Guide;
        }
        public override bool CanTownNPCSpawn(int numTownNPCs, int money)
        {
            if (EnduriumWorld.downedPhantasmShaman)
            {
                for (int k = 0; k < 255; k++)
                {
                    return true;
                }

            }
            return false;
        }
        public override bool CheckConditions(int left, int right, int top, int bottom)
        {
            return true;
        }

        public override string TownNPCName()
        {
            switch (WorldGen.genRand.Next(10))
            {
                case 0:
                    return "Zander";
                case 1:
                    return "Alex";
                case 2:
                    return "Eian";
                case 3:
                    return "Gavin";
                case 4:
                    return "Zur";
                case 5:
                    return "Vincent";
					                case 6:
                    return "Ben";
					                case 7:
                    return "Jackson";
					                case 8:
                    return "Zach";
					                case 9:
                    return "Riley";
                default:
                    return "Dan";
            }
        }

        public override void SetChatButtons(ref string button, ref string button2)
        {
            button = "Shop";
        }
        public override void OnChatButtonClicked(bool firstButton, ref bool openShop)
        {

            if (firstButton)
            {
                openShop = true;
            }
        }

        public override void SetupShop(Chest shop, ref int nextSlot) //GoldenAmberBattleShield
        {
            shop.item[nextSlot].SetDefaults(mod.ItemType("IronCross"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(5);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("OblivionMirror"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(50);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("TempleBadge"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(50);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("HolyLight"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(50);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("CursedKnive"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(25);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;
            shop.item[nextSlot].SetDefaults(mod.ItemType("SoulForge"));      //this defines what item to sell .
            shop.item[nextSlot].shopCustomPrice = new int?(150);  //this is the custom price, so this item will cost 20 custom Currency
            shop.item[nextSlot].shopSpecialCurrency = EnduriumMod.BronzeCoinID;  //this make so to use the CustomCurrency             
            nextSlot++;

            shop.item[nextSlot].SetDefaults(ItemID.WormholePotion);
            nextSlot++;

        }

        public override string GetChat()
        {
		            int DryadNPC = NPC.FindFirstNPC(NPCID.Dryad);
            if (DryadNPC >= 0 && Main.rand.Next(6) == 0)
            {
                return "" + Main.npc[DryadNPC].GivenName + " disqusts me, complaining about angel statues, complaining about every single thing!";
            }
			            int GuideNPC = NPC.FindFirstNPC(NPCID.Guide);
            if (GuideNPC >= 0 && Main.rand.Next(6) == 0)
            {
                return "" + Main.npc[GuideNPC].GivenName + " is the chosen, he will become the sacrifise. Dig to hell and perform the ritual!";
            }
            int SeekerNPC = NPC.FindFirstNPC(mod.NPCType("Seeker"));
            if (SeekerNPC >= 0 && Main.rand.Next(6) == 0)
            {
                return "Do you know " + Main.npc[SeekerNPC].GivenName + "? he thinks he can find the terrarian to defeat the moon lord, what a fool!";
            }
			            int GladiatorNPC = NPC.FindFirstNPC(mod.NPCType("Gladiator"));
            if (GladiatorNPC >= 0 && Main.rand.Next(6) == 0)
            {
                return "" + Main.npc[GladiatorNPC].GivenName + " is a freak, last time he tried to sell me a lolipop... for 999 platinum coins! Never!";
            }
            switch (Main.rand.Next(14))
            {
                case 0:
                    return "Bless you.";
                case 1:
                    return "I know what you are dreaming about.";
                case 2:
                    return "I know what you are thinking.";
                case 3:
                    return "There are many cults in this land. Every worships someone else, but the strangest cult i heard is the mooncult. They is so stupid!";
                case 4:
                    return "Have you seen a deep tight hole in the ground? You have to get there! Legends say that there is a legendary sword hidden there, the arkhalis. Or you will find a stupid useless sword, thats more likely.";
                               case 5:
                    return "Have you heard of a profaned god? Yeah me neither, not in this universe at least...";
                case 6:
                    return "The dungeon holds dark secret.";
                case 7:
                    return "Some say that there is a whole temple hidden in the jungle, idiots!";
                case 8:
                    return "Every night there is a chance that the blood moon will appear. It happened already, and you killed a traitor. Thats why i came!";
                case 9:
                    return "Some say that there is a grand bird hiding in the desert, perhaps there is...";
                               case 10:
                    return "You come again?";
                case 11:
                    return "Goblins! Goblins everywhere, all around us. KILL THEM!";
                case 12:
                    return "Have you seen the tropical forest? The wood from trees there is really sharp, you could make some powerfull weapons from it... And then kill everyone! What a fantastic idea right? He he...";
                case 13:
                    return "The prism arcanist, you have to save him... Craft the forgotten relic and unleash his spirit";
			   default:
                    return "You must unleash the spirits!";

            }
        }
    }
}
