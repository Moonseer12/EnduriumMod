using System;
using System.IO;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.GameInput;
using Terraria.ModLoader.IO;

namespace EnduriumMod
{
    public class MyPlayer : ModPlayer
    {
        private const int saveVersion = 0;
        public static bool hasProjectile;
        public int bonusHealth;
        public int bonusMana;
        public bool CharmofLuna;
        public bool BloodGemHurt;
        public bool CharmHide;
        public bool CharmAccPrevious;
        public bool CharmForce;
        public bool CharmEffect;
        public bool BloodGem;
        public bool CharmofSacrifises;
        public bool CharmSHide;
        public bool CharmSAccPrevious;
        public bool CharmSForce;
        public bool CharmSEffect;
        public bool ZonePlanet = false;
        public bool ZoneTropical = false;
        //weapon specific
        public bool UsedPrismStaff = false;
        public bool UsedSpiritOrb = false;
        public float ThunderStrike = 1f;
        public float StarExcalibur = 1f;
        public int SanguineGoliathOffSet = -10;
        public int SanguineGoliathDirection = -1;
        public bool FacemelterLethal = false;
        public bool FacemelterHeld = false;
        public int SpearBoom = 0;
        /*
        		public override Texture2D GetMapBackgroundImage()
		{
			if (ZoneExample)
			{
				return mod.GetTexture("ExampleBiomeMapBackground");
			}
			return null;
		}
        */
        public override void Initialize()
        {

            bonusHealth = 0;
            bonusMana = 0;
        }

        public override TagCompound Save()
        {
            string bonusLife = bonusHealth.ToString();
            string bonusMagic = bonusMana.ToString();

            return new TagCompound {
                {"bonusLife", bonusLife},
                {"bonusMagic",bonusMagic},
            };
        }

        public override void Load(TagCompound tag)
        {
            var bonusLife = tag.GetString("bonusLife");
            var bonusMagic = tag.GetString("bonusMagic");
            bonusHealth = int.Parse(bonusLife);
            bonusMana = int.Parse(bonusMagic);
        }

        public bool DragonRifleHeld = false;

        public float DragonRifle = 0f;

        public bool Voidwalker = false;

        public bool TropicalBlushV2 = false;

        public bool EyeofTheStorm = false;

        public bool StarlightOrbit = false;

        public bool QuantumRain = false;

        public bool SnowflakeMinionBuff = false;

        public bool ThrowingSpeedBuff = false;

        public float ThrowingSpeed = 1f;

        public bool Slimy = false;

        public bool BiologicalCrit = false;

        public bool ShadowTouch = false;

        public bool Death = false;

        public bool BloodScourgeSet = false;

        public bool TropicEruption = false;

        public bool ErodedSet = false;

        public bool Book = false;

        public bool EatersBreath = false;

        public bool BloodMedalion = false;

        public bool BloomingTyrant = false;

        public bool NeoniteWazp = false;

        public bool DimensionalRift = false;

        public bool EtherArmorBonus = false;

        public bool Eye = false;

        public bool TheOneThingThatCreatesEnergyOnGettingHitAndStuff = false;

        public bool SwiftDodge = false;

        public bool SwiftEmblem = false;

        public bool GraniteShatter = false;

        public bool GrandBlob = false;

        public bool SwiftEmblemV2 = false;

        public bool PoseidonsGuard = false;

        public bool GoblinMinionBuff = false;

        public bool BloomlightRanged = false;

        public bool BloomlightMagic = false;

        public bool PoisonRune = false;

        public bool FireRune = false;

        public bool EthernalHeal = false;

        public bool FrostRune = false;

        public bool FD = false;

        public bool Voidflame = false;

        public bool EarthTurret = false;

        public bool VoidflameImbue = false;

        public bool BloomlightThrowing = false;

        public bool BloodFangBuff = false;

        public bool GraniteMinionBuff = false;

        public bool SpaceMinionBuff = false;

        public bool Overgrowth1 = false;

        public bool flaggo = false;

        public bool Spikeball = false;

        public bool AncientCurse = false;

        public bool TropicalAura = false;

        public bool TropicalBlush = false;

        public bool Overgrowth = false;

        public bool VampireHeal = false;

        public bool Overgrowth2 = false;

        public bool NatureMagic = false;

        public bool TropicalCandle = false;

        public bool Overgrowth3 = false;


        public bool SoulCrow = false;

        public bool MiniHeal = false;

        public bool RedDust = false;

        public bool BlueDust = false;

        public bool BunnyPet = false;

        public bool EyassesCrystal = false;

        public bool BloodFang = false;

        public bool Lightning = false;

        public bool HealingSpirit = false;

        public bool AncientCurse1 = false;

        public bool DragonPet = false;

        public bool SandSentry = false;

        public bool MinionShiver = false;

        public bool Wyvern = false;

        public bool HeartOfTheForest = false;

        public bool HeartOfTheForestCooldown = false;

        public bool ElementalRune = false;

        public bool ReaperNature = false;

        public bool Tyrant = false;

        public bool NuclearHurt = false;

        public bool ChaosCrit = false;

        public bool ManaRefill = false;

        public bool RoyalQuiver = false;

        public bool EthernalNecklace = false;

        public bool DemonicHurt = false;

        public bool StarSetRanged = false;

        public bool StarSetMelee = false;

        public bool StarSetMagic = false;

        public bool StarSetThrown = false;

        public bool StarSetSummon = false;

        public bool StarSet = false;

        public bool StarSetEffect = false;

        public bool StarCooldown = false;

        public bool RoyalCurseInflict = false;

        public bool DragonBuff = false;

        public float OutburstSpeed = 1f;

        public bool StormShield = false;

        public float StormShieldCharge = 1f;

        public override void ResetEffects()
        {
            if (player.statLifeMax2 < player.statLifeMax + bonusHealth)
                player.statLifeMax2 = player.statLifeMax + bonusHealth;
            if (player.statManaMax2 < player.statManaMax + bonusMana)
                player.statManaMax2 = player.statManaMax + bonusMana;

            CharmAccPrevious = CharmofLuna;
            CharmofLuna = CharmHide = CharmForce = CharmEffect = false;
            CharmSAccPrevious = CharmofSacrifises;
            CharmofSacrifises = CharmSHide = CharmSForce = CharmSEffect = false;
            Voidwalker = false;
            DemonicHurt = false;
            RoyalCurseInflict = false;
            StormShield = false;
            ChaosCrit = false;
            NeoniteWazp = false;
            ManaRefill = false;
            DragonBuff = false;
            RoyalQuiver = false;
            EthernalNecklace = false;
            EarthTurret = false;
            flaggo = false;
            BloodGem = false;
            SnowflakeMinionBuff = false;
            BloodFang = false;
            ShadowTouch = false;
            TropicalCandle = false;
            NuclearHurt = false;
            ElementalRune = false;
            EyeofTheStorm = false;
            EtherArmorBonus = false;
            ErodedSet = false;
            SwiftDodge = false;
            SwiftEmblem = false;
            ThrowingSpeedBuff = false;
            Tyrant = false;
            BloodMedalion = false;
            Slimy = false;
            BiologicalCrit = false;
            SwiftEmblemV2 = false;
            TropicalBlushV2 = false;
            BloomlightMagic = false;
            BloomlightRanged = false;
            Eye = false;
            BloomlightThrowing = false;
            GrandBlob = false;
            FireRune = false;
            EthernalHeal = false;
            FrostRune = false;
            DimensionalRift = false;
            PoisonRune = false;
            Voidflame = false;
            VoidflameImbue = false;
            HeartOfTheForest = false;
            HeartOfTheForestCooldown = false;
            Book = false;
            EatersBreath = false;
            PoseidonsGuard = false;
            Death = false;
            Wyvern = false;
            MinionShiver = false;
            TheOneThingThatCreatesEnergyOnGettingHitAndStuff = false;
            TropicalAura = false;
            SandSentry = false;
            BloomingTyrant = false;
            FD = false;
            TropicEruption = false;
            QuantumRain = false;
            DragonPet = false;
            HealingSpirit = false;
            Lightning = false;
            SpaceMinionBuff = false;
            EyassesCrystal = false;
            GoblinMinionBuff = false;
            Overgrowth1 = false;
            Overgrowth2 = false;
            GraniteShatter = false;
            BloodFangBuff = false;
            NatureMagic = false;
            Overgrowth3 = false;
            BloodScourgeSet = false;
            Overgrowth = false;
            VampireHeal = false;
            Spikeball = false;
            AncientCurse = false;
            SoulCrow = false;
            MiniHeal = false;
            RedDust = false;
            BlueDust = false;
            BunnyPet = false;
            GraniteMinionBuff = false;
            ChaosCrit = false;
            AncientCurse = false;
            TropicalBlush = false;
            StarSet = false;
            ReaperNature = false;
            StarSetRanged = false;
            StarSetMelee = false;
            StarSetMagic = false;
            StarSetThrown = false;
            StarSetEffect = false;
            StarCooldown = false;
            StarSetSummon = false;
            StarlightOrbit = false;
        }
        public override void PreUpdate()
        {
            StarlightOrbit = false;
            ChaosCrit = false;
            ManaRefill = false;
            DragonBuff = false;
            TropicalCandle = false;
            StarCooldown = false;
            EthernalNecklace = false;
            NeoniteWazp = false;
            AncientCurse = false;
            RoyalCurseInflict = false;
            StarSetRanged = false;
            StarSetMelee = false;
            StarSetEffect = false;
            StarSetMagic = false;
            StarSetThrown = false;
            StormShield = false;
            StarSetSummon = false;
            SwiftDodge = false;
            EyeofTheStorm = false;
            ThrowingSpeedBuff = false;
            VoidflameImbue = false;
            Slimy = false;
            Voidflame = false;
            ReaperNature = false;
            EarthTurret = false;
            SwiftEmblem = false;
            BiologicalCrit = false;
            FD = false;
            SwiftEmblemV2 = false;
            PoisonRune = false;
            Eye = false;
            flaggo = false;
            BloodMedalion = false;
            FireRune = false;
            FrostRune = false;
            EatersBreath = false;
            ErodedSet = false;
            Tyrant = false;
            PoseidonsGuard = false;
            EtherArmorBonus = false;
            GrandBlob = false;
            RoyalQuiver = false;
            ElementalRune = false;
            TropicEruption = false;
            DemonicHurt = false;
            DimensionalRift = false;
            SnowflakeMinionBuff = false;
            TropicalBlush = false;
            BloomlightMagic = false;
            BloomlightRanged = false;
            BloomlightThrowing = false;
            NuclearHurt = false;
            Spikeball = false;
            HeartOfTheForest = false;
            EthernalHeal = false;
            Book = false;
            TropicalBlushV2 = false;
            HeartOfTheForestCooldown = false;
            QuantumRain = false;
            SandSentry = false;
            GraniteMinionBuff = false;
            BunnyPet = false;
            Overgrowth = false;
            MiniHeal = false;
            VampireHeal = false;
            BloomingTyrant = false;
            TheOneThingThatCreatesEnergyOnGettingHitAndStuff = false;
            TropicalAura = false;
            GraniteShatter = false;
            SpaceMinionBuff = false;
            BloodFangBuff = false;
            AncientCurse = false;
            NatureMagic = false;
            MinionShiver = false;
            SoulCrow = false;
            DragonPet = false;
            HealingSpirit = false;
            RedDust = false;
            BlueDust = false;
            EyassesCrystal = false;
            Wyvern = false;
            BloodFang = false;
            BloodScourgeSet = false;
            Lightning = false;
            Death = false;
            ChaosCrit = false;
            StarSet = false;
            ShadowTouch = false;
        }
        public override void UpdateBiomes()
        {
            ZoneTropical = (EnduriumWorld.ZoneTropicalTiles > 50);
            ZonePlanet = (EnduriumWorld.PlanetTiles > 25);
        }


        public override void CopyCustomBiomesTo(Player other)
        {
            MyPlayer modOther = other.GetModPlayer<MyPlayer>(mod);
            modOther.ZonePlanet = ZonePlanet;
            modOther.ZoneTropical = ZoneTropical;
        }

        public override void SendCustomBiomes(BinaryWriter writer)
        {
            BitsByte flags = new BitsByte();
            flags[0] = ZonePlanet;
            flags[1] = ZoneTropical;
            writer.Write(flags);
        }

        public override void ReceiveCustomBiomes(BinaryReader reader)
        {
            BitsByte flags = reader.ReadByte();
            ZonePlanet = flags[0];
            ZoneTropical = flags[1];
        }
        /*public override Texture2D GetMapBackgroundImage()
        {
            if (ZoneTropical)
            {
                return mod.GetTexture("ExampleBiomeMapBackground");
            }
            return null;
        }*/
        public override void UpdateVanityAccessories()
        {
            for (int n = 13; n < 18 + player.extraAccessorySlots; n++)
            {
                Item item = player.armor[n];
                if (item.type == mod.ItemType<Items.Armor.TheFrozenMoonlight>())
                {
                    CharmHide = false;
                    CharmForce = true;
                }
                if (item.type == mod.ItemType<Items.Accesories.CharmofSacrifises>())
                {
                    CharmSHide = false;
                    CharmSForce = true;
                }
            }
        }
        public override void FrameEffects()
        {
            if ((CharmEffect || CharmForce) && !CharmHide)
            {
                player.legs = mod.GetEquipSlot("FrostLeg", EquipType.Legs);
                player.body = mod.GetEquipSlot("FrostBody", EquipType.Body);
                player.head = mod.GetEquipSlot("FrostHead", EquipType.Head);
            }
            if ((CharmSEffect || CharmSForce) && !CharmSHide)
            {
                player.legs = mod.GetEquipSlot("DemonLeg", EquipType.Legs);
                player.body = mod.GetEquipSlot("DemonBody", EquipType.Body);
                player.head = mod.GetEquipSlot("DemonHead", EquipType.Head);
            }
        }
        public override void UpdateEquips(ref bool wallSpeedBuff, ref bool tileSpeedBuff, ref bool tileRangeBuff)
        {
            // Make sure this condition is the same as the condition in the Buff to remove itself. We do this here instead of in ModItem.UpdateAccessory in case we want future upgraded items to set blockyAccessory
            if (CharmofLuna)
            {
                player.AddBuff(mod.BuffType<Buffs.CharmofLuna>(), 60, true);
            }
            if (CharmofSacrifises)
            {
                player.AddBuff(mod.BuffType<Buffs.CharmofSacrifises>(), 60, true);
            }
        }
        public override void ProcessTriggers(TriggersSet triggersSet)
        {
            int num666 = 200;
            if (EnduriumMod.StarCall.JustPressed)
            {
                if (!StarCooldown && !player.dead && StarSet)
                {
                    player.AddBuff(mod.BuffType("StarSet"), num666);
                    player.AddBuff(mod.BuffType("StarCooldown"), 5000);
                }
            }
        
            /*if (StarSetMelee && EnduriumMod.StarCall.JustPressed && player.FindBuffIndex(mod.BuffType("StarCooldown")) == -1 && !player.dead)
            {

                player.AddBuff(mod.BuffType("StarSetMelee"), 100);
                player.AddBuff(mod.BuffType("StarSetMeleeDefese"), 900);
                player.AddBuff(mod.BuffType("StarCooldown"), 12000);


            }
            if (StarSetThrown && EnduriumMod.StarCall.JustPressed && player.FindBuffIndex(mod.BuffType("StarCooldown")) == -1 && !player.dead)
            {

                player.AddBuff(mod.BuffType("StarSetThrown"), 100);
                player.AddBuff(mod.BuffType("StarCooldown"), 12000);


            }
            if (StarSetMagic && EnduriumMod.StarCall.JustPressed && player.FindBuffIndex(mod.BuffType("StarCooldown")) == -1 && !player.dead)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, 2f, mod.ProjectileType("StarSetMagic"), 100, 0f, 0);
                player.AddBuff(mod.BuffType("StarCooldown"), 12000);
            }*/
        }
        public override bool PreHurt(bool pvp, bool quiet, ref int damage, ref int hitDirection, ref bool crit, ref bool customDamage, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            if (OutburstSpeed >= 1f)
            {
                OutburstSpeed -= 0.5f;
            }
            if (ThrowingSpeed >= 1f)
            {
                ThrowingSpeed = 1f;
            }
            if (QuantumRain)
            {
                player.AddBuff(mod.BuffType("QuantumRain"), 80);
            }
            if (EatersBreath && Main.myPlayer == player.whoAmI)
            {
                for (int m = 0; m < 200; m++)
                {
                    if (Main.npc[m].active && !Main.npc[m].friendly)
                    {
                        int num2 = 4;
                        int num9 = 600;
                        num9 += (int)num2 * 2;
                        if (Main.rand.Next(500) < num9)
                        {
                            float num10 = (Main.npc[m].Center - player.Center).Length();
                            float num11 = (float)Main.rand.Next(200 + (int)num2 / 2, 301 + (int)num2 * 2);
                            if (num11 > 500f)
                            {
                                num11 = 500f + (num11 - 500f) * 0.75f;
                            }
                            if (num11 > 700f)
                            {
                                num11 = 700f + (num11 - 700f) * 0.5f;
                            }
                            if (num11 > 900f)
                            {
                                num11 = 900f + (num11 - 900f) * 0.25f;
                            }
                            if (num10 < num11)
                            {
                                float num12 = (float)Main.rand.Next(300 + (int)num2 / 2, 600 + (int)num2 / 2);
                                Main.npc[m].AddBuff(39, (int)num12, false);
                            }
                        }
                    }
                }
            }
            if (BloodMedalion && Main.myPlayer == player.whoAmI)
            {
                for (int m = 0; m < 200; m++)
                {
                    if (Main.npc[m].active && !Main.npc[m].friendly)
                    {
                        int num2 = 4;
                        int num9 = 600;
                        num9 += (int)num2 * 2;
                        if (Main.rand.Next(500) < num9)
                        {
                            float num10 = (Main.npc[m].Center - player.Center).Length();
                            float num11 = (float)Main.rand.Next(200 + (int)num2 / 2, 301 + (int)num2 * 2);
                            if (num11 > 500f)
                            {
                                num11 = 500f + (num11 - 500f) * 0.75f;
                            }
                            if (num11 > 700f)
                            {
                                num11 = 700f + (num11 - 700f) * 0.5f;
                            }
                            if (num11 > 900f)
                            {
                                num11 = 900f + (num11 - 900f) * 0.25f;
                            }
                            if (num10 < num11)
                            {
                                float num12 = (float)Main.rand.Next(300 + (int)num2 / 2, 600 + (int)num2 / 2);
                                Main.npc[m].AddBuff(69, (int)num12, false);
                            }
                        }
                    }
                }
            }
            if (TropicalBlush && Main.rand.Next(4) == 0)
            {
                int num3;
                for (int num731 = 0; num731 < 12; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 89, 0f, 0f, 100, default(Color), 1.6f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    num3 = num731;
                }
                player.AddBuff(mod.BuffType("TropicalAura"), 80);
                Main.PlaySound(25, (int)player.position.X, (int)player.position.Y, 0);
            }
            if (TropicalBlushV2 && Main.rand.Next(3) == 0)
            {
                int num3;
                for (int num731 = 0; num731 < 12; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 89, 0f, 0f, 100, default(Color), 1.6f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 3f;
                    num3 = num731;
                }
                player.AddBuff(mod.BuffType("TropicalAura"), 120);
                Main.PlaySound(25, (int)player.position.X, (int)player.position.Y, 0);
            }
            if (SwiftDodge)
            {
                return false;
            }
            if (SwiftEmblem && Main.rand.Next(2) == 0)
            {
                int num3;
                for (int num731 = 0; num731 < 12; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 133, 0f, 0f, 100, default(Color), 1.6f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 3f;
                    num3 = num731;
                }
                player.AddBuff(mod.BuffType("SwiftDodge"), 30);
                player.immune = true;
                player.immuneTime = player.longInvince ? 80 : 60;
                for (int k = 0; k < player.hurtCooldowns.Length; k++)
                {
                    player.hurtCooldowns[k] = player.longInvince ? 80 : 60;
                }
                Main.PlaySound(25, (int)player.position.X, (int)player.position.Y, 0);
            }
            if (SwiftEmblemV2)
            {
                int num3;
                for (int num731 = 0; num731 < 12; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 133, 0f, 0f, 100, default(Color), 1.6f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 3f;
                    num3 = num731;
                }
                player.AddBuff(mod.BuffType("SwiftDodge"), 40);
                player.immune = true;
                player.immuneTime = player.longInvince ? 80 : 60;
                for (int k = 0; k < player.hurtCooldowns.Length; k++)
                {
                    player.hurtCooldowns[k] = player.longInvince ? 80 : 60;
                }
                player.AddBuff(mod.BuffType("TropicalAura"), Main.rand.Next(120, 160));
                Main.PlaySound(25, (int)player.position.X, (int)player.position.Y, 0);
            }
            return base.PreHurt(pvp, quiet, ref damage, ref hitDirection, ref crit, ref customDamage, ref playSound, ref genGore, ref damageSource);
        }
        public override void PreUpdateMovement()
        {
            if (player.justJumped && CharmofLuna)
            {
                Main.PlaySound(SoundID.Item62, player.position);
                int num3;
                for (int num731 = 0; num731 < 12; num731 = num3 + 1)
                {
                    int num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 132, 0f, 0f, 100, default(Color), 1.6f);
                    Main.dust[num732].noGravity = true;
                    Dust dust = Main.dust[num732];
                    dust.velocity *= 3f;
                    num732 = Dust.NewDust(new Vector2(player.position.X, player.position.Y), player.width, player.height, 132, 0f, 0f, 100, default(Color), 1.2f);
                    dust = Main.dust[num732];
                    dust.velocity *= 2f;
                    num3 = num731;
                }
                if (Main.myPlayer == player.whoAmI)
                {
                    for (int m = 0; m < 200; m++)
                    {
                        if (Main.npc[m].active && !Main.npc[m].friendly)
                        {
                            int num2 = 4;
                            int num9 = 600;
                            num9 += (int)num2 * 2;
                            if (Main.rand.Next(500) < num9)
                            {
                                float num10 = (Main.npc[m].Center - player.Center).Length();
                                float num11 = (float)Main.rand.Next(200 + (int)num2 / 2, 301 + (int)num2 * 2);
                                if (num11 > 500f)
                                {
                                    num11 = 500f + (num11 - 500f) * 0.75f;
                                }
                                if (num11 > 700f)
                                {
                                    num11 = 700f + (num11 - 700f) * 0.5f;
                                }
                                if (num11 > 900f)
                                {
                                    num11 = 900f + (num11 - 900f) * 0.25f;
                                }
                                if (num10 < num11)
                                {
                                    float num12 = (float)Main.rand.Next(100 + (int)num2 / 2, 200 + (int)num2 / 2);
                                    Main.npc[m].AddBuff(mod.BuffType("Shiver"), (int)num12, false);
                                }
                            }
                        }
                    }
                }
            }
        }
        float dustpos1 = 180;
        float dist = 40;
        float dustpos2 = 0;
        public override void PreUpdateBuffs()
        {
            if (TropicalCandle)
            {
                player.AddBuff(mod.BuffType("FireflyThing"), 20);
            }
        }
        public override void UpdateBadLifeRegen()
        {

            if (Voidwalker)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num38 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 2, 2, 21, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1.5f);
                    Main.dust[num38].noGravity = true;
                    Main.dust[num38].noLight = true;
                    Dust dust2 = Main.dust[num38];
                    dust2.velocity *= 0f;
                }
                if (Main.rand.Next(3) < 2)
                {
                    int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 21, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].velocity.Y += 0.25f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }
            if (SpearBoom > 0)
            {
                SpearBoom -= 1;
            }
            if (player.HeldItem.type == mod.ItemType("Tyrfing"))
            {
                player.armorPenetration += 9999999;
            }
            if (player.HeldItem.type == mod.ItemType("HeavensGalore"))
            {
                if (player.statLife >= player.statLifeMax2)
                {
                    player.meleeDamage *= 2f;
                }
            }
            if (player.HeldItem.type != mod.ItemType("Facemelter"))
            {
                FacemelterHeld = false;
            }
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Facemelter")] <= 0;
            if (petProjectileNotSpawned)
            {
                if (FacemelterLethal)
                {
                    FacemelterLethal = false;
                    player.KillMe(PlayerDeathReason.ByOther(10), 10.0, 0, false);
                }
            }
            if (StormShield)
            {
                double deg1 = (double)dustpos1;
                double deg2 = (double)dustpos2;
                double rad1 = deg1 * (Math.PI / 180);
                double rad2 = deg2 * (Math.PI / 180);
                int a = Dust.NewDust(player.Center, player.width, player.height, 156, player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
                Main.dust[a].noGravity = true;
                Main.dust[a].velocity = player.velocity;
                Main.dust[a].scale *= StormShieldCharge;
                Main.dust[a].position.X = player.Center.X - (int)(Math.Cos(rad1) * dist);
                Main.dust[a].position.Y = player.Center.Y - (int)(Math.Sin(rad1) * dist);

                int b = Dust.NewDust(player.Center, player.width, player.height, 156, player.velocity.X * 0.5f, player.velocity.Y * 0.5f);
                Main.dust[b].noGravity = true;
                Main.dust[b].velocity = player.velocity;
                Main.dust[b].scale *= StormShieldCharge;
                Main.dust[b].position.X = player.Center.X - (int)(Math.Cos(rad2) * dist);
                Main.dust[b].position.Y = player.Center.Y - (int)(Math.Sin(rad2) * dist);

                dustpos1 += 4;
                dustpos2 += 4;
            }

            if (SanguineGoliathOffSet <= -10)
            {
                SanguineGoliathDirection = -1;
            }
            if (SanguineGoliathOffSet >= 10)
            {
                SanguineGoliathDirection = 1;
            }
            if (StarExcalibur >= 1.75f)
            {
                StarExcalibur = 1.75f;
            }
            if (StarExcalibur <= 1f)
            {
                StarExcalibur = 1f;
            }
            if (StarExcalibur > 1f)
            {
                player.statDefense += 25;
            }
            if (StormShield)
            {
                if (StormShieldCharge > 0.50f)
                {
                    StormShieldCharge = 0.50f;
                }
            }
            if (player.statLife >= (player.statLifeMax + player.statLifeMax2) / 2)
            {
                ThunderStrike = 1f;
            }
            if (player.statLife <= (player.statLifeMax + player.statLifeMax2) / 2)
            {
                ThunderStrike = 1.25f;
            }

            if (player.statLife <= (player.statLifeMax + player.statLifeMax2) / 4)
            {
                ThunderStrike = 1.5f;
            }

            if (Slimy)
            {
                if (player.accRunSpeed >= 0.7f)
                {
                    player.accRunSpeed = 0.7f;
                }
                if (player.moveSpeed >= 2f)
                {
                    player.moveSpeed = 2f;
                }
                player.accRunSpeed = 0f;
                player.eocDash = 0;
            }
            if (GoblinMinionBuff)
            {
                player.minionDamage += 0.05f;
                player.moveSpeed += 0.1f;
            }
            if (ShadowTouch)
            {
                player.lifeRegen += 5;
            }
            if (GraniteShatter)
            {
                player.statDefense -= 15;
            }
            if (HeartOfTheForestCooldown)
            {
                player.lifeRegen = -2;
            }
            if (Voidflame)
            {
                player.lifeRegen = -20;
            }
            if (ReaperNature)
            {
                player.lifeRegen = -3;
                player.statDefense--;
                player.statDefense--;
                player.statDefense--;
                player.statDefense--;
                player.statDefense--;
            }
            if (SwiftDodge)
            {
                player.moveSpeed += 0.25f;
            }
            if (AncientCurse1)
            {
                player.lifeRegen = -6;
            }
            if (TropicalAura)
            {
                player.lifeRegen += 5;
            }
            if (BloodFangBuff)
            {
            }
            if (Overgrowth3)
            {
                player.endurance += 0.15f;
            }
            if (Overgrowth2)
            {
                player.thrownDamage += 0.15f;
                player.meleeDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.rangedDamage += 0.15f;
            }
            if (DimensionalRift)
            {
                player.thrownDamage -= 0.15f;
                player.meleeDamage -= 0.15f;
                player.magicDamage -= 0.15f;
                player.rangedDamage -= 0.15f;
            }
            if (Tyrant)
            {
                player.thrownDamage += 0.25f;
            }
            if (Overgrowth1)
            {
                player.lifeRegen += 3;
            }
            if (BloodScourgeSet)
            {
                if (Main.rand.Next(2) == 0)
                {
                    int num38 = Dust.NewDust(new Vector2(player.position.X - 2f, player.position.Y + (float)player.height - 2f), player.width + 2, 2, 12, 0f, 0f, 100, default(Microsoft.Xna.Framework.Color), 1.5f);
                    Main.dust[num38].noGravity = true;
                    Main.dust[num38].noLight = true;
                    Dust dust2 = Main.dust[num38];
                    dust2.velocity *= 0f;
                }
                if (Main.rand.Next(3) < 2)
                {
                    int dust = Dust.NewDust(player.position - new Vector2(2f, 2f), player.width + 4, player.height + 4, 12, player.velocity.X * 0.4f, player.velocity.Y * 0.4f, 100, default(Color), 1.5f);
                    Main.dust[dust].noGravity = true;
                    Main.dust[dust].velocity *= 0f;
                    Main.dust[dust].velocity.Y += 0.25f;
                    if (Main.rand.Next(2) == 0)
                    {
                        Main.dust[dust].scale *= 0.5f;
                    }
                }
            }

            if (player.HeldItem.type == mod.ItemType("DragonRifle"))
            {
                DragonRifleHeld = true;
                if (DragonRifle > 0f && DragonRifle < 1f)
                {
                    DragonRifle -= 0.01f;
                }
                if (DragonRifle <= 0f)
                {
                    DragonRifle += 0.01f;
                }
            }
            else
            {
                DragonRifleHeld = false;
            }
        }
        public override void OnHitNPCWithProj(Projectile proj, NPC target, int damage, float knockback, bool crit)
        {
            if (DragonRifleHeld)
            {
                if (DragonRifle > 0f)
                {
                    if (DragonRifle < 1f)
                    {
                        DragonRifle += 0.18f;
                    }
                }
            }
            if (StormShield)
            {
                StormShieldCharge += 0.01f;
            }
            if (proj.type == mod.ProjectileType("OutburstOutbreak"))
            {
                if (OutburstSpeed <= 1.75f)
                {
                    OutburstSpeed += 0.05f;
                }

            }
            if (ThrowingSpeedBuff)
            {
                if (proj.thrown)
                {
                    if (ThrowingSpeed <= 1.2f)
                    {
                        ThrowingSpeed += 0.02f;
                    }
                }
            }
            if (ChaosCrit && crit)
            {
                damage = (int)(damage * 3f);
            }
            if (BiologicalCrit && crit)
            {
                player.AddBuff(mod.BuffType("SpiritEnergy"), 60);
            }
            if (BiologicalCrit)
            {
                target.AddBuff(mod.BuffType("ReaperNature"), 60);
            }
            if (ErodedSet && proj.thrown)
            {
                if (Main.rand.Next(25) == 0)
                {

                    for (int num53 = 0; num53 < 15; num53++)
                    {
                        Dust dust = Main.dust[Dust.NewDust(target.position + target.velocity * 5f, target.width, target.height, 21, 0f, 0f, 100, Color.Transparent, 2.5f)];
                        dust.noGravity = true;
                        dust.velocity *= 2f;
                        dust.fadeIn = 1.5f;
                    }

                    int num11 = 1;
                    for (int j = 0; j < num11; j++)
                    {
                        Vector2 vector = new Vector2((float)Main.rand.Next(-300, 301), (float)Main.rand.Next(-300, 301));
                        vector += target.velocity * 12f;
                        vector.Normalize();
                        vector *= (float)Main.rand.Next(35, 39) * 0.3f;
                        Projectile.NewProjectile(target.Center.X, target.Center.Y, vector.X, vector.Y, mod.ProjectileType("ErodedFire"), damage, 0f, player.whoAmI);

                    }
                }
                if (crit)
                {
                    if (Main.rand.Next(15) == 0)
                    {
                        for (int num53 = 0; num53 < 15; num53++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(target.position + target.velocity * 5f, target.width, target.height, 21, 0f, 0f, 100, Color.Transparent, 2.5f)];
                            dust.noGravity = true;
                            dust.velocity *= 2f;
                            dust.fadeIn = 1.5f;
                        }

                        int num11 = 2;
                        for (int j = 0; j < num11; j++)
                        {
                            Vector2 vector = new Vector2((float)Main.rand.Next(-300, 301), (float)Main.rand.Next(-300, 301));
                            vector += target.velocity * 12f;
                            vector.Normalize();
                            vector *= (float)Main.rand.Next(35, 39) * 0.3f;
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, vector.X, vector.Y, mod.ProjectileType("ErodedFire"), damage, 0f, player.whoAmI);

                        }
                    }
                }
                if (target.life <= 0)
                {
                    if (Main.rand.Next(2) == 0)
                    {
                        for (int num53 = 0; num53 < 15; num53++)
                        {
                            Dust dust = Main.dust[Dust.NewDust(target.position + target.velocity * 5f, target.width, target.height, 21, 0f, 0f, 100, Color.Transparent, 2.5f)];
                            dust.noGravity = true;
                            dust.velocity *= 2f;
                            dust.fadeIn = 1.5f;
                        }

                        int num11 = 3;
                        for (int j = 0; j < num11; j++)
                        {
                            Vector2 vector = new Vector2((float)Main.rand.Next(-300, 301), (float)Main.rand.Next(-300, 301));
                            vector += target.velocity * 12f;
                            vector.Normalize();
                            vector *= (float)Main.rand.Next(35, 39) * 0.3f;
                            Projectile.NewProjectile(target.Center.X, target.Center.Y, vector.X, vector.Y, mod.ProjectileType("ErodedFire"), damage, 0f, player.whoAmI);

                        }
                    }
                }
            }
            if (VoidflameImbue)
            {
                target.AddBuff(mod.BuffType("Voidflame"), 90);

            }
            if (EthernalNecklace)
            {
                target.AddBuff(BuffID.OnFire, 100);
            }
            if (SoulCrow & proj.thrown)
            {
                if (Main.rand.Next(5) == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("SoulCrowOrbit"), 50, 1f, 0);
                }

            }
            if (TropicEruption)
            {
                if (crit && proj.thrown)
                {
                    int num3;
                    if (proj.owner == Main.myPlayer)
                    {
                        int num626 = 2;
                        if (Main.rand.Next(5) == 0)
                        {
                            num3 = num626;
                            num626 = num3 + 1;
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            num3 = num626;
                            num626 = num3 + 1;
                        }
                        if (Main.rand.Next(5) == 0)
                        {
                            num3 = num626;
                            num626 = num3 + 1;
                        }
                        for (int num627 = 0; num627 < num626; num627 = num3 + 4)
                        {
                            float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                            float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                            num628 *= 20f;
                            num629 *= 20f;
                            Projectile.NewProjectile(proj.position.X, proj.position.Y, num628, num629, mod.ProjectileType("BloomBolt"), (int)((double)proj.damage * 0.75), (float)((int)((double)proj.knockBack * 0.35)), Main.myPlayer, 0f, 0f);
                            num3 = num627;
                        }
                        proj.Kill();
                    }
                }
            }
            if (Overgrowth)
            {
                if (Main.rand.Next(30) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth1"), Main.rand.Next(80, 120));
                }
                if (Main.rand.Next(30) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth2"), Main.rand.Next(80, 100));
                }
                if (Main.rand.Next(30) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth3"), Main.rand.Next(80, 100));
                }
            }
            if (BloomlightMagic && proj.magic)
            {
                if (Main.rand.Next(10) == 0)
                {
                    player.AddBuff(mod.BuffType("BloomlightBuffMagic"), Main.rand.Next(80, 100));
                }
            }
            if (BloomlightThrowing && proj.thrown)
            {
                if (Main.rand.Next(10) == 0)
                {
                    player.AddBuff(mod.BuffType("BloomlightBuffThrowing"), Main.rand.Next(80, 100));
                }
            }
            if (BloomlightRanged && proj.ranged)
            {
                if (Main.rand.Next(10) == 0)
                {
                    player.AddBuff(mod.BuffType("BloomlightBuffRanged"), Main.rand.Next(80, 100));
                }
            }
            if (AncientCurse && proj.minion)
            {
                if (Main.rand.Next(7) == 0)
                {
                    target.AddBuff(mod.BuffType("AncientCurse"), 20);
                }
            }
            if (MinionShiver && proj.minion)
            {
                if (Main.rand.Next(7) == 0)
                {
                    target.AddBuff(mod.BuffType("Shiver"), 25);
                }
            }
            if (NatureMagic && proj.magic)
            {
                target.AddBuff(mod.BuffType("NatureSting"), 20);
            }

            if (BloodFangBuff)
            {
                if (crit)
                {
                    Player player = Main.player[proj.owner];
                    player.AddBuff(mod.BuffType("BloodFangFlame"), 0);
                    int healAmount = 5;
                    player.HealEffect(healAmount);
                    player.statLife += healAmount;
                }
            }
            if (BloodFang && !BloodFangBuff)
            {
                if (Main.rand.Next(20) == 0)
                {
                    player.AddBuff(mod.BuffType("BloodFangFlame"), 60);
                }
            }
            if (Lightning)
            {
                if (Main.rand.Next(25) == 0)
                {
                    player.AddBuff(mod.BuffType("Lightning"), Main.rand.Next(180, 200));
                }
            }
            //Healing Spirit
            if (HealingSpirit && proj.minion)
            {
                if (Main.rand.Next(50) == 0)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 0f, 0f, mod.ProjectileType("HealingSpirit"), 1, 1f, 0);
                }

            }
            //            target.AddBuff(BuffID.OnFire, 300);
            //Throwing Healing
            ///Throwing Healing
            ///Throwing Healing
            ///Throwing Healing
            if (VampireHeal && proj.thrown)
            {
                if (Main.rand.Next(4) == 0)
                {
                    Player player = Main.player[proj.owner];
                    int healAmount = (Main.rand.Next(2) + 3);
                    player.statLife += healAmount;
                    player.HealEffect(healAmount);
                }
            }
            if (MiniHeal && proj.thrown)
            {
                if (Main.rand.Next(3) == 0)
                {
                    Player player = Main.player[proj.owner];
                    int healAmount = (Main.rand.Next(1) + 2);
                    player.statLife += healAmount;
                    player.HealEffect(healAmount);
                }
                if (EthernalHeal && proj.thrown)
                {
                    if (Main.rand.Next(4) == 0)
                    {
                        Player player = Main.player[proj.owner];
                        int healAmount = (Main.rand.Next(1) + 3);
                        player.statLife += healAmount;
                        player.HealEffect(healAmount);
                    }
                }
            }
            if (CharmofSacrifises && proj.type != mod.ProjectileType("BloodConsumption"))
            {
                if (Main.rand.Next(4) == 0)
                {
                    int num414 = (int)(proj.position.X + (float)proj.width);
                    int num415 = (int)(proj.position.Y + (float)proj.height);
                    Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, mod.ProjectileType("BloodConsumption"), proj.damage, 0f, proj.owner, (float)proj.owner);
                }
            }
            if (StarSetEffect && proj.type != mod.ProjectileType("StarShard"))
            {
                int num414 = (int)(proj.position.X + (float)proj.width);
                int num415 = (int)(proj.position.Y + (float)proj.height);
                Projectile.NewProjectile((float)num414, (float)num415, 0f, 0f, mod.ProjectileType("StarShard"), 200, 0f, proj.owner);
            }
        }

        public override void OnHitNPC(Item item, NPC target, int damage, float knockback, bool crit)
        {
            if (BiologicalCrit && crit)
            {
                player.AddBuff(mod.BuffType("SpiritEnergy"), 60);
            }
            if (BiologicalCrit)
            {
                target.AddBuff(mod.BuffType("ReaperNature"), 60);
            }
            if (StormShield)
            {
                StormShieldCharge += 0.015f;
            }
            if (item.type == mod.ItemType("StarClaymore"))
            {
                if (StarExcalibur <= 1.75f)
                {
                    StarExcalibur += 0.25f;
                }
                
            }
            if (PoisonRune)
            {
                target.AddBuff(BuffID.Poisoned, 100);

            }
            if (FireRune)
            {
                target.AddBuff(BuffID.OnFire, 100);

            }
            if (FrostRune)
            {
                target.AddBuff(BuffID.Frostburn, 100);


            }
            if (ElementalRune)
            {
                target.AddBuff(BuffID.Poisoned, 200);
                target.AddBuff(BuffID.OnFire, 200);
                target.AddBuff(BuffID.Frostburn, 200);

            }
            if (Overgrowth)
            {
                if (Main.rand.Next(30) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth1"), Main.rand.Next(80, 120));
                }
                if (Main.rand.Next(28) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth2"), Main.rand.Next(80, 100));
                }
                if (Main.rand.Next(30) == 0)
                {
                    player.AddBuff(mod.BuffType("Overgrowth3"), Main.rand.Next(80, 100));
                }
            }
            if (BloodFangBuff)
            {
                if (crit)
                {
                    player.AddBuff(mod.BuffType("BloodFangFlame"), 0);
                    int healAmount = 5;
                    player.HealEffect(healAmount);
                    player.statLife += healAmount;

                }
            }
            if (BloodFang && !BloodFangBuff)
            {
                if (Main.rand.Next(20) == 0)
                {
                    player.AddBuff(mod.BuffType("BloodFangFlame"), 60);
                }
            }
            if (Lightning)
            {
                if (Main.rand.Next(25) == 0)
                {
                    player.AddBuff(mod.BuffType("Lightning"), Main.rand.Next(180, 200));
                }
            }
            if (ChaosCrit && crit)
            {
                damage = (int)(damage * 3f);
            }
            if (RoyalCurseInflict && item.melee)
            {
                if (Main.rand.Next(4) == 0)
                {
                    target.AddBuff(mod.BuffType("RoyalCurse"), 600);
                }
            }
            if (Lightning)
            {
                player.AddBuff(mod.BuffType("Lightning"), 300);

            }
            if (EthernalNecklace)
            {
                target.AddBuff(BuffID.OnFire, 100);
            }
            if (VoidflameImbue)
            {
                target.AddBuff(mod.BuffType("Voidflame"), 90);

            }
        }


        public override void Hurt(bool pvp, bool quiet, double damage, int hitDirection, bool crit)
        {
            if (BloodGem)
            {
                BloodGemHurt = true;
               
            }
            
            if (StormShield)
            {
                StormShieldCharge = 0f;
            }
            if (TheOneThingThatCreatesEnergyOnGettingHitAndStuff)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, 2f, mod.ProjectileType("SpectreOrbit"), 50, 0f, 0);
            }
            if (NuclearHurt)
            {
                int Damage = 60;
                float num628 = (float)Main.rand.Next(-35, 36) * 0.02f;
                float num629 = (float)Main.rand.Next(-35, 36) * 0.02f;
                num628 *= 8f;
                num629 *= 8f;
                int num1 = 2;
                if (Main.rand.Next(3) == 0)
                {
                    num1 += 1;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num1 += 1;
                }
                for (int i = 0; i < num1; i++)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, num628, num629, mod.ProjectileType("NuclearPlague"), Damage, 0.5f, Main.myPlayer, 0f, 0f);
                }
            }
            if (EtherArmorBonus)
            {
                int Damage = 30;
                int num1 = 2;
                if (Main.rand.Next(3) == 0)
                {
                    num1 += 1;
                }
                if (Main.rand.Next(3) == 0)
                {
                    num1 += 1;
                }
                for (int i = 0; i < num1; i++)
                {
                    Projectile.NewProjectile(player.Center.X, player.Center.Y, 2f, 2f, mod.ProjectileType("EtherBolt"), Damage, 0.5f, Main.myPlayer, 0f, 0f);
                }
            }
            if (DemonicHurt)
            {
                Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("DemonOrbit"), 50, 0f, 0);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("DemonOrbit"), 50, 0f, 0);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("DemonOrbit"), 50, 0f, 0);
                Projectile.NewProjectile(player.Center.X, player.Center.Y, -2f, -2f, mod.ProjectileType("DemonOrbit"), 50, 0f, 0);
            }

            if (ManaRefill)
            {
                player.statMana = player.statManaMax2;
            }
        }
        public override bool PreKill(double damage, int hitDirection, bool pvp, ref bool playSound, ref bool genGore, ref PlayerDeathReason damageSource)
        {
            int num20 = 36;
            if (HeartOfTheForest)
            {
                if (!HeartOfTheForestCooldown)
                {
                    player.statLife = 200;
                    player.HealEffect(200);

                    player.AddBuff(mod.BuffType("HeartOfNatureCooldown"), 5000);
                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    for (int k = 0; k < player.hurtCooldowns.Length; k++)
                    {
                        player.hurtCooldowns[k] = player.longInvince ? 180 : 120;
                    }
                    Main.PlaySound(SoundID.Item29, player.position);
                    for (int i = 0; i < num20; i++)
                    {
                        Vector2 vector2 = Vector2.Normalize(player.velocity) * new Vector2((float)player.width / 2f, (float)player.height) * 0.75f * 0.5f;
                        vector2 = vector2.RotatedBy((double)((float)(i - (num20 / 2 - 1)) * 6.28318548f / (float)num20), default(Vector2)) + player.Center;
                        Vector2 vector3 = vector2 - player.Center;
                        int num21 = Dust.NewDust(vector2 + vector3, 0, 0, 89, vector3.X * 2f, vector3.Y * 2f, 100, default(Color), 1.4f);
                        Main.dust[num21].noGravity = true;
                        Main.dust[num21].noLight = true;
                        Main.dust[num21].fadeIn = 1.4f;
                        Main.dust[num21].velocity = Vector2.Normalize(vector3) * 3f;
                    }
                    return false;
                }
            }
            bool petProjectileNotSpawned = player.ownedProjectileCounts[mod.ProjectileType("Facemelter")] <= 0;
            if (!petProjectileNotSpawned)
            {
                if (FacemelterHeld)
                {
                    player.statLife = 1;
                    player.HealEffect(1);

                    player.immune = true;
                    player.immuneTime = player.longInvince ? 180 : 120;
                    for (int k = 0; k < player.hurtCooldowns.Length; k++)
                    {
                        player.hurtCooldowns[k] = player.longInvince ? 180 : 120;
                    }
                    Main.PlaySound(SoundID.Item29, player.position);

                    FacemelterLethal = true;
                    return false;
                }
            }
            if (Death)
            {
                player.KillMeForGood();
                player.ghost = true;
            }
            return true;
        }
    }
}