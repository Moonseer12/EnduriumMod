using System;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameContent.UI;

namespace EnduriumMod
{
    class EnduriumMod : Mod
    {
        internal static EnduriumMod instance;
        public static int BronzeCoinID;
        public static int ForestSpiritID;
        public static int SoulGemID;
        public static ModHotKey StarCall;
        public EnduriumMod()
        {
            Properties = new ModProperties()
            {
                Autoload = true,
                AutoloadSounds = true,
                AutoloadGores = true,
                AutoloadBackgrounds = true
            };
        }

        public override void Load()
        {
            StarCall = RegisterHotKey("Star Call", "F");
            BronzeCoinID = CustomCurrencyManager.RegisterCurrency(new BronzeCoinCurrency(ItemType<Items.BronzeCoin>(), 999L));
            SoulGemID = CustomCurrencyManager.RegisterCurrency(new SoulGemCurrency(ItemType<Items.SoulGem>(), 999L));
            ForestSpiritID = CustomCurrencyManager.RegisterCurrency(new ForestSpiritCurrency(ItemType<Items.ForestSpiritSoul>(), 999L));
            if (!Main.dedServ)
            {
                // Add certain equip textures
                AddEquipTexture(new Items.Armor.FrostHead(), null, EquipType.Head, "FrostHead", "EnduriumMod/Items/Armor/TheFrozenMoonlight_Head");
                AddEquipTexture(new Items.Armor.FrostBody(), null, EquipType.Body, "FrostBody", "EnduriumMod/Items/Armor/TheFrozenMoonlight_Body", "EnduriumMod/Items/Armor/TheFrozenMoonlight_Arms");
                AddEquipTexture(new Items.Armor.FrostLeg(), null, EquipType.Legs, "FrostLeg", "EnduriumMod/Items/Armor/TheFrozenMoonlight_Legs");
                AddEquipTexture(new Items.Accesories.DemonHead(), null, EquipType.Head, "DemonHead", "EnduriumMod/Items/Accesories/CharmofSacrifises_Head");
                AddEquipTexture(new Items.Accesories.DemonBody(), null, EquipType.Body, "DemonBody", "EnduriumMod/Items/Accesories/CharmofSacrifises_Body", "EnduriumMod/Items/Accesories/CharmofSacrifises_Arms");
                AddEquipTexture(new Items.Accesories.DemonLeg(), null, EquipType.Legs, "DemonLeg", "EnduriumMod/Items/Accesories/CharmofSacrifises_Legs");
            }
        }
        public override void PostSetupContent()
        {
            Mod LootBags = ModLoader.GetMod("LootBags");
            if (LootBags != null)
            {
                LootBags.Call(.20, ItemType("AncientMandible"), 1, 2, 1);
                LootBags.Call(.50, ItemType("PutridSpore"), 1, 5, 1);
                LootBags.Call(.50, ItemType("NatureEssence"), 1, 3, 1);
                LootBags.Call(.25, ItemType("BloodFangCore"), 1, 3, 1);
                LootBags.Call(.25, ItemType("BronzeCoin"), 1, 3, 1);

            }

            Mod yabhb = ModLoader.GetMod("FKBossHealthBar");
            if (yabhb != null)
            {
                yabhb.Call("RegisterCustomHealthBar",
                NPCType("TheSpiritOfBloom"), // This is the enemy we want to track health for
                false, // ForceSmall left as false
                "The Tyrant of Bloom", // We can set a custom display name too
                                       // The default bar textures
                GetTexture("UI/SpiritBarFill"),
                GetTexture("UI/SpiritBarLeft"),
                GetTexture("UI/SpiritBarMiddle"),
                GetTexture("UI/SpiritBarRight"),
                // Since I based the textures off the default, there is no
                // need to change any of the offsets.
                null, null, null, null, null,
                // Small textures
                null,
                null,
                null,
                null,
                // Same deal with offsets
                null, null, null
                );
            }
            Mod bossChecklist = ModLoader.GetMod("BossChecklist");
            if (bossChecklist != null)
            {
                bossChecklist.Call("AddBossWithInfo", "The Earth Elemental", 6.4f, (Func<bool>)(() => EnduriumWorld.downedBio), "Use a [i:" + this.ItemType("SuspiciousLookingCrystal") + "] In the jungle biome at day");
                bossChecklist.Call("AddBossWithInfo", "Keeper of Hollow", 8.2f, (Func<bool>)(() => EnduriumWorld.downedHollow), "Use a [i:" + this.ItemType("SigilofHollow") + "] In the hallow biome");
                bossChecklist.Call("AddBossWithInfo", "The Prism Arcanum", 5f, (Func<bool>)(() => EnduriumWorld.downedPrismArcanum), "Use a [i:" + this.ItemType("FrozenMonolith") + "] In the ice biome during night time");
                bossChecklist.Call("AddBossWithInfo", "Tyrant of Bloom", 2.8f, (Func<bool>)(() => EnduriumWorld.downedBloom), "Use [i:" + this.ItemType("TheBloomingFire") + "] Anywhere during day time");
                bossChecklist.Call("AddMiniBossWithInfo", "Bloodlight Shaman", 1.5f, (Func<bool>)(() => EnduriumWorld.downedPhantasmShaman), "Occasionally spawns during the blood moon");
                bossChecklist.Call("AddBossWithInfo", "Endurian Warlock", 10.2f, (Func<bool>)(() => EnduriumWorld.downedEndurianWarlock), "Use a [i:" + this.ItemType("PlagueSigil") + "] Anywhere during Night time");
            }
        }
        public override void UpdateMusic(ref int music)
        {
            Player player = Main.player[Main.myPlayer];

            //Don't override the songs in this list!
            int[] NoOverride = {MusicID.Dungeon, MusicID.Boss1, MusicID.Boss2, MusicID.Boss3, MusicID.Boss4, MusicID.Boss5,
                MusicID.LunarBoss, MusicID.PumpkinMoon, MusicID.TheTowers, MusicID.FrostMoon, MusicID.GoblinInvasion, MusicID.Eclipse, MusicID.MartianMadness,
                MusicID.PirateInvasion, 41};//MusicID.OldOnesArmy

            bool playMusic = true;
            foreach (int n in NoOverride)
            {
                if (music == n) playMusic = false;
            }
            if (EnduriumWorld.StarArmy && (Main.invasionX == (double)Main.spawnTileX))
            {
                music = 23;
            }
            for (int i = 0; i < Main.npc.Length; ++i)
            {
                if (Main.npc[i].boss && Main.npc[i].active)
                {
                    playMusic = false;
                }
            }
            if (EnduriumWorld.ZoneTropicalTiles >= 100 && player.active && !Main.gameMenu && playMusic && !player.ZoneUnderworldHeight)
            {
                if (!player.ZoneRockLayerHeight)
                {
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/Tropical_Rainforest"); // add surface version later
                }
                else
                {
                    music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/Tropical_Rainforest");
                }
            }
            if (EnduriumWorld.PlanetTiles >= 10 && player.active && !Main.gameMenu && playMusic && !player.ZoneUnderworldHeight)
            {
                music = this.GetSoundSlot(SoundType.Music, "Sounds/Music/The_Erosion");
            }
        }
        public override void AddRecipeGroups()
        {
            // Creates a new recipe group
            RecipeGroup group = new RecipeGroup(() => Lang.misc[37] + " " + Lang.GetItemNameValue(ItemType("DarkTulip")), new int[]
            {

                ItemType("ChorusPlant"),
                ItemType("BloodySpine"),
                ItemType("FrozenRoot"),
                ItemType("DarkTulip"),
                ItemType("FluxRose")

            });
            // Registers the new recipe group with the specified name
            RecipeGroup.RegisterGroup("EnduriumMod:AnyAdvancedPotionIngredient", group);
            group = new RecipeGroup(() => Lang.misc[37] + (" Gold Bar"), new int[]
{
                ItemID.GoldBar,
                ItemID.PlatinumBar,
});
            RecipeGroup.RegisterGroup("PlatinumBars", group);
            RecipeGroup.RegisterGroup("EnduriumMod:Tier3HMBars", group);
            group = new RecipeGroup(() => Lang.misc[37] + (" Titanium Bar"), new int[]
{
                ItemID.AdamantiteBar,
                ItemID.TitaniumBar,
});
            RecipeGroup.RegisterGroup("Tier3HMBars", group);
        }

      /*  public override void UpdateMusic(ref int music)
        {
            if (Main.myPlayer != -1 && !Main.gameMenu)
            {
                if (Main.LocalPlayer.active && Main.LocalPlayer.GetModPlayer<MyPlayer>(this).ZonePlanet)
                {
                    music = GetSoundSlot(SoundType.Music, "Sounds/Music/DriveMusic");
                }
            }
        }*/

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.UnicornHorn, 15);
            recipe.AddIngredient(ItemID.PixieDust, 30);
            recipe.AddIngredient(ItemID.SoulofLight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.HallowedKey);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.CursedFlame, 15);
            recipe.AddIngredient(ItemID.RottenChunk, 30);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.CorruptionKey);
            recipe.AddRecipe();

            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.Ichor, 15);
            recipe.AddIngredient(ItemID.Vertebrae, 30);
            recipe.AddIngredient(ItemID.SoulofNight, 10);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.CrimsonKey);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.JungleSpores, 15);
            recipe.AddIngredient(ItemID.Stinger, 10);
            recipe.AddIngredient(ItemID.Vine, 5);
            recipe.AddIngredient(ItemID.TurtleShell, 3);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.JungleKey);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.ShadowKey);
            recipe.AddIngredient(ItemID.IceFeather, 2);
            recipe.AddIngredient(ItemID.FrostCore, 5);
            recipe.AddIngredient(ItemID.SoulofSight, 15);
            recipe.AddIngredient(ItemID.SoulofMight, 15);
            recipe.AddIngredient(ItemID.SoulofFright, 15);
            recipe.AddTile(TileID.MythrilAnvil);
            recipe.SetResult(ItemID.FrozenKey);
            recipe.AddRecipe();
            recipe = new ModRecipe(this);
            recipe.AddIngredient(ItemID.Vertebrae, 5);
            recipe.AddTile(TileID.WorkBenches);
            recipe.SetResult(ItemID.Leather);
            recipe.AddRecipe();
        }

        class SpawnRateMultiplierGlobalNPC : GlobalNPC
        {
            public override void EditSpawnRate(Player player, ref int spawnRate, ref int maxSpawns)
            {
                if (EnduriumWorld.EndurianLegion)
                {
                    spawnRate = (int)(spawnRate / 5f);
                    maxSpawns = (int)(maxSpawns * 5f);
                }
            }
        }
    }
}