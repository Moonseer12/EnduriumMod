using System;
using System.IO;
using System.Linq;
using System.Collections.Generic;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using Terraria.ModLoader.IO;
using Terraria.World.Generation;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Generation;
using EnduriumMod;

public class EnduriumWorld : ModWorld
{
    private const int saveVersion = 0;

    public static bool downedNatureWarMage;

    public static bool downedPhantasmShaman;

    public static bool downedEndurianWarlock;

    public static bool SeekerFirst;

    public static bool downedPrismArcanum;

    public static bool downedFluxQueen;

    public static bool downedBio;

    public static bool downedBloom;

    public static bool EndurianLegion;

    public static int LegionRemainingPoints;

    public static int ZoneTropicalTiles = 0;

    public static int PlanetTiles;

    public static bool downedHollow;

    public static bool StarArmy;

    public static bool StarArmyUp;

    public static bool downedStar;

    public static int DragonTimer;

    public override void ResetNearbyTileEffects()
    {
        PlanetTiles = 0;
        ZoneTropicalTiles = 0;
        MyPlayer modPlayer = Main.LocalPlayer.GetModPlayer<MyPlayer>(mod);
        modPlayer.TropicalCandle = false;
    }

    public override void TileCountsAvailable(int[] tileCounts)
    {
        ZoneTropicalTiles = tileCounts[base.mod.TileType("TropicalSoil")] + tileCounts[base.mod.TileType("ElementBloomOre")] + tileCounts[base.mod.TileType("TropicalTrunk")] + tileCounts[base.mod.TileType("TropicStoneTest")];
        PlanetTiles = tileCounts[base.mod.TileType("PlanetarShard")];
    }

    public override void Initialize()
    {
        Main.invasionSize = 0;
        downedNatureWarMage = false;
        downedHollow = false;
        downedPhantasmShaman = false;
        downedFluxQueen = false;
        downedBloom = false;
        downedBio = false;
        downedEndurianWarlock = false;
        SeekerFirst = false;
        downedPrismArcanum = false;
        StarArmy = false;
        StarArmyUp = false;
        downedStar = false;
    }
   
    public override void PostUpdate()
    {
      
        if (StarArmyUp)
        {
            if (Main.invasionX == (double)Main.spawnTileX)
            {
                StarInvasion.CheckDungeonInvasionProgress();
            }
            StarInvasion.UpdateDungeonInvasion();
        }
        if (DragonTimer >= 1)
        {
            DragonTimer--;
        }
    }

    public override TagCompound Save()
    {
        List<string> list = new List<string>();
        if (downedNatureWarMage)
        {
            list.Add("NatureWarMage");
        }
        if (downedBio)
        {
            list.Add("TheSwarm");
        }
        if (downedFluxQueen)
        {
            list.Add("FluxQueen");
        }
        if (downedPhantasmShaman)
        {
            list.Add("NecroMaster");
        }
        if (downedPrismArcanum)
        {
            list.Add("ThePrismArcanum");
        }
        if (downedEndurianWarlock)
        {
            list.Add("EndurianWarlock");
        }
        if (downedHollow)
        {
            list.Add("TheKeeperofHollow2");
        }
        if (downedStar)
        {
            list.Add("StarPortal");
        }
        TagCompound tagCompound = new TagCompound();
        tagCompound.Add("downed", list);
        return tagCompound;
    }

    public override void Load(TagCompound tag)
    {
        IList<string> list = tag.GetList<string>("downed");
        downedNatureWarMage = list.Contains("NatureWarMage");
        downedFluxQueen = list.Contains("FluxQueen");
        downedBio = list.Contains("TheSwarm");
        downedEndurianWarlock = list.Contains("EndurianWarlock");
        downedPrismArcanum = list.Contains("ThePrismArcanum");
        downedPhantasmShaman = list.Contains("NecroMaster");
        downedBloom = list.Contains("TheSpiritOfBloom");
        downedStar = list.Contains("StarPortal");
        downedHollow = list.Contains("TheKeeperofHollow2");
    }

    public override void LoadLegacy(BinaryReader reader)
    {
        int num = reader.ReadInt32();
        if (num == 0)
        {
            BitsByte bitsByte = reader.ReadByte();
            downedNatureWarMage = bitsByte[0];
            downedFluxQueen = bitsByte[1];
            downedPrismArcanum = bitsByte[2];
            downedPhantasmShaman = bitsByte[3];
            downedBloom = bitsByte[4];
            downedEndurianWarlock = bitsByte[5];
            downedStar = bitsByte[6];
            downedBio = bitsByte[7];
            downedHollow = bitsByte[8];
        }
        else
        {
            ErrorLogger.Log("EnduriumMod: Unknown loadVersion: " + num);
        }
    }

    public override void NetSend(BinaryWriter writer)
    {
        BitsByte bb = default(BitsByte);
        bb[0] = downedNatureWarMage;
        bb[1] = downedFluxQueen;
        bb[2] = downedPrismArcanum;
        bb[3] = downedPhantasmShaman;
        bb[4] = downedBloom;
        bb[5] = downedEndurianWarlock;
        bb[6] = downedStar;
        bb[7] = downedBio;
        bb[8] = downedHollow;
        writer.Write(bb);
    }

    public override void NetReceive(BinaryReader reader)
    {
        BitsByte bitsByte = reader.ReadByte();
        downedNatureWarMage = bitsByte[0];
        downedFluxQueen = bitsByte[1];
        downedPrismArcanum = bitsByte[2];
        downedPhantasmShaman = bitsByte[3];
        downedBloom = bitsByte[4];
        downedEndurianWarlock = bitsByte[5];
        downedStar = bitsByte[6];
        downedBio = bitsByte[7];
        downedHollow = bitsByte[8];
    }

    public override void ModifyWorldGenTasks(List<GenPass> tasks, ref float totalWeight)
    {
        int num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Stalac"));
        if (num != -1)
        {
            tasks.Insert(num + 1, new PassLegacy("TropicalForestBiomeGen", delegate (GenerationProgress progress)
            {
                progress.Message = "Hiding the Tropical Paradise";
                int num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + 60) : (Main.spawnTileX - 60);
                int num3 = Raycast(num2, 200);
                if (Main.maxTilesY == 1800)
                {
                    num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + 90) : (Main.spawnTileX - 90);
                }
                if (Main.maxTilesY == 2400)
                {
                    num2 = (WorldGen.dungeonX > Main.spawnTileX) ? (Main.spawnTileX + 110) : (Main.spawnTileX - 110);
                }
            
                if (num3 < (int)Main.worldSurface - 50)
                {
                    num3 = Raycast(num2, (int)Main.worldSurface - 100) + 260;
                }
                if (Main.tile[num2, Raycast(num2, num3)].type == 147)
                {
                    num2 += ((num2 < Main.spawnTileX) ? (-460) : 460);
                }
                if (Main.tile[num2, Raycast(num2, num3)].type == 41 || (Main.tile[num2, Raycast(num2, num3)].type == 43 || (Main.tile[num2, Raycast(num2, num3)].type == 44)))
                {
                    num2 += ((num2 < Main.spawnTileX) ? (-150) : 150);
                }
                if (Main.tile[num2, Raycast(num2, num3)].type == 60)
                {
                    num2 += ((num2 < Main.spawnTileX) ? (-200) : 200);
                }
                if (Main.tile[num2, Raycast(num2, num3)].type == 226)
                {
                    num2 += ((num2 < Main.spawnTileX) ? (-100) : 100);
                }
                if (Main.tile[num2, Raycast(num2, num3)].type == 404)
                {
                    num2 += ((num2 < Main.spawnTileX) ? (-100) : 100);
                }
                int maxValue = 0;
                int num4 = WorldGen.genRand.Next(5, 8);
                for (int i = 0; i < num4; i++)
                {
                    int num5 = num3 + WorldGen.genRand.Next(50, 70) * i;
                    for (int j = 0; j < 20; j++)
                    {
                        int num6 = WorldGen.genRand.Next(-120, 120);
                        int num7 = WorldGen.genRand.Next(0, 120);
                        for (int k = -50; k < 50; k++)
                        {
                            if (num6 + k >= 20 && num6 + k <= Main.maxTilesX - 20)
                            {
                                for (int l = -50; l < 50; l++)
                                {
                                    if (num7 + l >= 20 && num7 + l <= Main.maxTilesY - 20)
                                    {
                                        ushort type = Main.tile[num6 + k, num7 + l].type;
                                    }
                                }
                            }
                        }
                        int num8 = WorldGen.genRand.Next(70, 100);
                        WorldGen.TileRunner(num2 - num6, num5 - num7, (double)num8, 10, base.mod.TileType("TropicalSoil"), false, 9f, 9f, false, true);
                        if (num5 - num7 < Main.maxTilesY - 200)
                        {
                            SmoothWallRunner(new Point(num2 - num6, num5 - num7), num8, 63);
                        }
                    }
                    maxValue = num3 + i * 60;
                }
                for (int i = 0; i < Main.maxTilesX; ++i) //Adds trees.
                {
                    for (int k = 0; k < Main.maxTilesY; ++k)
                    {
                        if (Main.tile[i, k].active() && Main.tile[i, k].type == (ushort)mod.TileType("TropicalSoil") && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(10) == 0) //Checks values
                        {
                            WorldGen.PlaceTile(i, k - 1, mod.TileType("TropicalSapling")); //Places sapling
                            WorldGen.GrowTree(i, k); //Tries to grow sapling. 
                        }
                    }
                }
                MakeTropicalTree(new Vector2((float)num2, (float)(num3 - 240)));
                CleanUpTree(new Point(num2, num3 - 240));
                for (int m = 0; m < 50; m++)
                {
                    WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-120, 120), WorldGen.genRand.Next(num3, maxValue), (double)WorldGen.genRand.Next(6, 9), 10, base.mod.TileType("ElementBloomOre"), false, 0f, 0f, false, true);
                }
                for (int n = 0; n < 100; n++)
                {
                    WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-120, 120), WorldGen.genRand.Next(num3, maxValue), (double)WorldGen.genRand.Next(15, 16), 10, base.mod.TileType("TropicStoneTest"), false, 0f, 0f, false, true);
                }
                for (int num9 = 0; num9 < 40; num9++)
                {
                    WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-60, 60), WorldGen.genRand.Next(num3, maxValue), (double)WorldGen.genRand.Next(3, 6), 10, base.mod.TileType("LivingStone"), false, 0f, 0f, false, true);
                }
                for (int num10 = 0; num10 < 100; num10++)
                {
                    WorldGen.TileRunner(num2 + WorldGen.genRand.Next(-30, 30), WorldGen.genRand.Next(num3, maxValue), (double)WorldGen.genRand.Next(10, 19), 10, base.mod.TileType("TropicalTrunk"), false, 0f, 0f, false, true);
                }
                for (int i = 0; i < Main.maxTilesX; ++i) //Adds trees.
                {
                    for (int k = 0; k < Main.maxTilesY; ++k)
                    {
                        if (Main.tile[i, k].active() && Main.tile[i, k].type == (ushort)mod.TileType("TropicalSoil") && !Main.tile[i, k - 1].active() && !Main.tile[i, k - 2].active() && WorldGen.genRand.Next(10) == 0) //Checks values
                        {
                            if (WorldGen.genRand.Next(2) == 0)
                            {
                                WorldGen.PlaceTile(i, k - 1, mod.TileType("GemCluster1")); //Places gemstone
                            }
                            else
                            {
                                WorldGen.PlaceTile(i, k - 1, mod.TileType("GemCluster2")); //Places gemstone
                            }
                        }
                    }
                }

                PlaceAltar(new Point(num2, num3));
                PlaceDungeons(new Point(num2, num3));
            }));
            num = tasks.FindIndex((GenPass genpass) => genpass.Name.Equals("Guide"));
            if (num != -1)
            {
                tasks.Insert(num + 1, new PassLegacy("BiomeChest", delegate (GenerationProgress progress)
                {
                    progress.Message = "Placing chest";
                    PlaceBiomeChest();
                }));
            }
            int LivingTreesIndex = tasks.FindIndex(genpass => genpass.Name.Equals("Living Trees"));
            if (LivingTreesIndex != -1)
            {
                tasks.Insert(LivingTreesIndex + 1, new PassLegacy("Post Terrain", delegate (GenerationProgress progress)
                {
                    // We can inline the world generation code like this, but if exceptions happen within this code 
                    // the error messages are difficult to read, so making methods is better. This is called an anonymous method.
                    progress.Message = "Secrets Secrets";
                }));
            }
        }
    }

    public void PlaceBiomeChest()
    {
        int num = (WorldGen.dungeonX < Main.spawnTileX) ? 1 : 2;
        int i;
        int k;
        do
        {
            i = WorldGen.genRand.Next(50, Main.maxTilesX / 2 * num - 50);
            k = WorldGen.genRand.Next((int)Main.worldSurface, Main.maxTilesY - 200);
        }
        while (!CheckPlaceChest(i, k));
    }

    public bool CheckPlaceChest(int i, int k)
    {
        bool flag = (Main.tile[i, k].type == 41 && Main.tile[i, k].type == 41) || (Main.tile[i, k].type == 44 && Main.tile[i, k].type == 44) || (Main.tile[i, k].type == 43 && Main.tile[i, k].type == 43);
        bool flag2 = !Main.tile[i, k - 1].active() && !Main.tile[i + 1, k - 1].active() && !Main.tile[i, k - 2].active() && !Main.tile[i + 1, k - 2].active();
        bool flag3 = Main.tile[i, k].active() && Main.tile[i + 1, k].active();
        int[] source = new int[9]
        {
            7,
            94,
            95,
            98,
            99,
            8,
            96,
            97,
            9
        };
        bool flag4 = source.Any((int x) => x == Main.tile[i, k - 1].wall) && source.Any((int x) => x == Main.tile[i + 1, k - 1].wall);
        if (flag && flag2 && flag3 && flag4)
        {
           // WorldGen.PlaceChestDirect(i, k - 1, (ushort)base.mod.TileType("BloomChest"), 0, 2);
            return true;
        }
        return false;
    }

    public void CleanUpTree(Point pos)
    {
        for (int i = -130; i < 130; ++i)
        {
            if (pos.X + i < 20) continue;
            if (pos.X + i > Main.maxTilesX - 20) continue; //"Hey you know I really need some exception cases." - Gabe
            for (int k = -200; k < 200; ++k)
            {
                if (pos.Y + k < 20) continue;
                if (pos.Y + k > Main.maxTilesY - 20) continue; // E X C E P T I O N    C A S E S.   -   G A B E
                if (Main.tile[pos.X + i, pos.Y + k].type == TileID.LeafBlock)
                {
                    if (Main.tile[pos.X + i, pos.Y + k].active()) //Kills any leaf tiles that aren't connected by at least two sides - Gabe
                    {
                        try
                        {
                            int tilesConnected = 0;
                            if (Main.tile[pos.X + i, pos.Y + k + 1].active()) tilesConnected++;
                            if (Main.tile[pos.X + i, pos.Y + k - 1].active()) tilesConnected++;
                            if (Main.tile[pos.X + i + 1, pos.Y + k].active()) tilesConnected++;
                            if (Main.tile[pos.X + i - 1, pos.Y + k].active()) tilesConnected++;
                            if (tilesConnected < 2)
                                WorldGen.KillTile(pos.X + i, pos.Y + k);
                        }
                        catch
                        {
                            continue; //Catches error. Might not clean up tree fully but it'll continue the generation without crashing. - Gabe
                        }
                    }
                }
            }
        }
    }
    private void PlaceAltar(Point centre)
    {
        int num = 1;
        for (int i = 0; i < num; i++)
        {
            Point pos = new Point(centre.X + WorldGen.genRand.Next(-120, 120), centre.Y + WorldGen.genRand.Next(-120, 220));
            MakeAltar(pos, -1, -1, WorldGen.genRand.Next(2) == 0);
        }
    }
    private void PlaceDungeons(Point centre)
    {
        int num = 8;
        if (Main.maxTilesY == 1800)
        {
            num = 12;
        }
        if (Main.maxTilesY == 2400)
        {
            num = 16;
        }
        for (int i = 0; i < num; i++)
        {
            Point pos = new Point(centre.X + WorldGen.genRand.Next(-120, 120), centre.Y + WorldGen.genRand.Next(-120, 220));
            MakeRuin(pos, -1, -1, WorldGen.genRand.Next(2) == 0);
        }
    }
    public Point MakeAltar(Point pos, int type = -1, int wType = -1, bool doRep = true)
    {
        int num = 24;
        int num2 = num - 14;
        int type2 = (type != -1) ? type : base.mod.TileType("RuinBrick");
        int type3 = (wType != -1) ? wType : 147;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num2; j++)
            {
                WorldGen.KillTile(pos.X + i, pos.Y + j, false, false, false);
                WorldGen.KillTile(pos.X + i, pos.Y - 1 + j, false, false, false);
                WorldGen.KillTile(pos.X + i, pos.Y + 1 + j, false, false, false);
                WorldGen.KillWall(pos.X + i, pos.Y + j, false);
                WorldGen.PlaceWall(pos.X + i, pos.Y + j, type3, false);
            }
        }
        for (int k = 0; k < num; k++)
        {
            KillPlaceTile(pos.X + k, pos.Y, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y - 1, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y + num2, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y + 1 + num2, type2, 0);
        }
        for (int l = 0; l < num2; l++)
        {
            KillPlaceTile(pos.X, pos.Y + l, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
        }
        KillPlaceTile(pos.X + num, pos.Y + num2, type2, 0);
        KillPlaceTile(pos.X + num, pos.Y + num2 + 1, type2, 0);
        KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
        int num3 = pos.Y + (num2 - 3);
        int num4 = pos.X + num / 2;

        WorldGen.PlaceTile(num4, num3 + 2, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 + 1, num3 + 2, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 - 1, num3 + 2, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 + 2, num3 + 2, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 - 2, num3 + 2, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4, num3 + 2, type2, true, false, -1, 23);

        WorldGen.PlaceTile(num4, num3 + 1, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 + 1, num3 + 1, type2, true, false, -1, 23);
        WorldGen.PlaceTile(num4 - 1, num3 + 1, type2, true, false, -1, 23);

        WorldGen.PlaceChest(num4 - 5, num3, (ushort)base.mod.TileType("AncientTotem"), false, 0);
        WorldGen.PlaceChest(num4 + 6, num3, (ushort)base.mod.TileType("AncientTotem"), false, 0);
        WorldGen.PlaceChest(num4, num3, (ushort)base.mod.TileType("AncientAltar"), false, 0);
        return new Point(pos.X + num, pos.Y + num2);
    }
    public Point MakeRuin(Point pos, int type = -1, int wType = -1, bool doRep = true)
    {
        int num = WorldGen.genRand.Next(12, 16);
        int num2 = num - 5;
        int type2 = (type != -1) ? type : base.mod.TileType("RuinBrick");
        int type3 = (wType != -1) ? wType : 147;
        for (int i = 0; i < num; i++)
        {
            for (int j = 0; j < num2; j++)
            {
                WorldGen.KillTile(pos.X + i, pos.Y + j, false, false, false);
                WorldGen.KillTile(pos.X + i, pos.Y - 1 + j, false, false, false);
                WorldGen.KillTile(pos.X + i, pos.Y + 1 + j, false, false, false);
                WorldGen.KillWall(pos.X + i, pos.Y + j, false);
                WorldGen.PlaceWall(pos.X + i, pos.Y + j, type3, false);
            }
        }
        for (int k = 0; k < num; k++)
        {
            KillPlaceTile(pos.X + k, pos.Y, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y - 1, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y + num2, type2, 0);
            KillPlaceTile(pos.X + k, pos.Y + 1 + num2, type2, 0);
        }
        for (int l = 0; l < num2; l++)
        {
            KillPlaceTile(pos.X, pos.Y + l, type2, 0);
            KillPlaceTile(pos.X + num, pos.Y + l, type2, 0);
        }
        KillPlaceTile(pos.X + num, pos.Y + num2, type2, 0);
        KillPlaceTile(pos.X + num, pos.Y + num2 + 1, type2, 0);
        KillPlaceTile(pos.X + num, pos.Y - 1, type2, 0);
        int num3 = pos.Y + (num2 - 1);
        num3 -= ((WorldGen.genRand.Next(2) == 0) ? 3 : 0);
        int num4 = pos.X + WorldGen.genRand.Next(3, num - 3);
        if (num3 != pos.Y + (num2 - 1))
        {
            WorldGen.PlaceTile(num4, num3 + 1, 19, true, false, -1, 23);
            WorldGen.PlaceTile(num4 + 1, num3 + 1, 19, true, false, -1, 23);
        }
        WorldGen.PlaceChest(num4, num3, (ushort)base.mod.TileType("RuinChest"), false, 0);
        return new Point(pos.X + num, pos.Y + num2);
    }

    public void KillPlaceTile(int x, int y, int type, int style = 0)
    {
        WorldGen.KillTile(x, y, false, false, true);
        WorldGen.PlaceTile(x, y, type, true, true, -1, style);
    }

    public void MakeTropicalTree(Vector2 pos)
    {
        Point leafMiddle = pos.ToPoint();
        for (int i = 0; i < 10; ++i) //Places leaves w/ walls. - Gabe
        {
            int size = WorldGen.genRand.Next(27, 35);
            Point position = (pos - new Vector2(WorldGen.genRand.Next(-30, 30), WorldGen.genRand.Next(-30, 30))).ToPoint();
            WorldGen.TileRunner(position.X, position.Y, size * 3, 8, TileID.LeafBlock, true, 0, 0, false, true);
            SmoothWallRunnerActive(position, size - 1, WallID.LivingLeaf);
        }
        int trunkSize = 10;
        int lastY = 0;
        for (int i = 0; i < 20; ++i) //Places wood w/ walls. - Gabe
        {
            WorldGen.TileRunner(leafMiddle.X, leafMiddle.Y + (i * 10), trunkSize * 3, 10, mod.TileType("TropicalTrunk"), true, 0, 0, false, true); //Change TileID.LivingWood to mod.TileType("TileTrunk")
            SmoothWallRunnerActive(new Point(leafMiddle.X, leafMiddle.Y + (i * 10)), trunkSize, WallID.LivingWood); //Change WallID.LivingWood to mod.WallType("WallOfTheTreesTrunk") - Gabe
            if (i < 7)
            {
                if (i % 3 == 0)
                    trunkSize++;
            }
            else if (i < 12)
                trunkSize++;
            else if (i < 15)
                trunkSize += WorldGen.genRand.Next(1, 3);
            else
                trunkSize += 3;
            lastY = leafMiddle.Y + (i * 10) + trunkSize;
            if (i >= 15)
                WorldGen.TileRunner(leafMiddle.X, leafMiddle.Y + (i * 10) + 15, 25, 10, -1);
        }
        int xSize = 7;
        for (int i = leafMiddle.Y; i < lastY + 5; ++i) //Digs out trunk. - Gabe
        {
            LineTunnel(new Point(leafMiddle.X, i), xSize);
            int rand = WorldGen.genRand.Next(3);
            if (rand == 0)
                xSize++;
            if (rand == 1)
                xSize--;
            if (xSize < 6)
                xSize++;
            if (xSize > 12)
                xSize--;
        }
        int dirTunnel = WorldGen.genRand.Next(2) == 0 ? -1 : 1;
        for (int i = 0; i < 10; ++i) //Platforms! - Gabe
        {
            if (Main.tile[leafMiddle.X - 5 + i, leafMiddle.Y + 9].active()) continue; //Checks if there's a tile in the way. If there is, skip it. - Gabe
            KillPlaceTile(leafMiddle.X - 5 + i, leafMiddle.Y + 9, TileID.Platforms, 23); //Places platform. - Gabe
        }
        int roomSide = WorldGen.genRand.Next(2) == 0 ? -1 : 1; //Left or right of the trunk, respectively. ~ Gabe
        int roomX = roomSide == 1 ? 10 : -20; //Added X pos of the room. ~ Gabe
        int roomY = leafMiddle.Y + WorldGen.genRand.Next(10, 15); //Y pos of the room. ~ Gabe
        Point bottomCorner = MakeRuin(new Point(leafMiddle.X + roomX, roomY), mod.TileType("TropicalTrunk"), WallID.LivingWood, false); //Makes the room. ~ Gabe
        //ChangeSpawn(leafMiddle.X + (5 * roomSide), bottomCorner.Y - 3);

        for (int i = 0; i < Math.Abs(roomX + 2); ++i) //X repeats. ~ Gabe
        {
            for (int k = 0; k < 3; ++k) //Y repeats. ~ Gabe
            {
                WorldGen.KillTile(leafMiddle.X + (i * roomSide), bottomCorner.Y - k - 1); //Makes tunnel. ~ Gabe
                if (WorldGen.genRand.Next(3) == 0)
                    WorldGen.PlaceTile(leafMiddle.X + (i * roomSide), bottomCorner.Y - k - 1, TileID.Cobweb); //Adds some cobwebs.
            }
        }
    }
    /*public bool ChangeSpawn(int i, int j)
    {
        for (int playerIndex = 0; playerIndex < 255; playerIndex++)
        {
            if (Main.player[playerIndex].active)
            {
                Player player = Main.player[playerIndex];
                MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
                player.SpawnX = i;
                player.SpawnY = j;
            }
        }
        return true;
    }*/
    public void SmoothWallRunner(Point position, int size, int wallID)
    { 
        for (int i = position.X - size; i <= position.X + size; i++)
        {
            for (int j = position.Y - size; j <= position.Y + size; j++)
            {
                bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null)
                {
                    WorldGen.KillWall(i, j, false);
                    WorldGen.PlaceWall(i, j, (ushort)wallID, true);
                }
            }
        }
    }

    public void SmoothWallRunnerActive(Point position, int size, int wallID)
    {
        for (int i = position.X - size; i <= position.X + size; i++)
        {
            for (int j = position.Y - size; j <= position.Y + size; j++)
            {
                bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null && Main.tile[i, j].active())
                {
                    WorldGen.KillWall(i, j, false);
                    WorldGen.PlaceWall(i, j, (ushort)wallID, true);
                }
            }
        }
    }

    public void SmoothTileRunner(Point position, int size, int type)
    {
        for (int i = position.X - size; i <= position.X + size; i++)
        {
            for (int j = position.Y - size; j <= position.Y + size; j++)
            {
                if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && Main.tile[i, j] != null)
                {
                    WorldGen.KillTile(i, j, false, false, true);
                    WorldGen.PlaceTile(i, j, (ushort)type, true, true, -1, 0);
                }
            }
        }
    }

    public void SmoothTunnel(Point position, int size)
    {
        for (int i = position.X - size; i <= position.X + size; i++)
        {
            for (int j = position.Y - size; j <= position.Y + size; j++)
            {
                bool flag = i > 10 && i < Main.maxTilesX - 10 && j > 10 && j < Main.maxTilesY - 10;
                if (Vector2.Distance(new Vector2((float)position.X, (float)position.Y), new Vector2((float)i, (float)j)) <= (float)size && flag && Main.tile[i, j] != null)
                {
                    WorldGen.KillTile(i, j, false, false, true);
                }
            }
        }
    }

    public void SquareRunner(Point position, int size, int type)
    {
        for (int i = position.X - size; i <= position.X + size; i++)
        {
            for (int j = position.Y - size; j <= position.Y + size; j++)
            {
                KillPlaceTile(i, j, (ushort)type, 0);
            }
        }
    }

    public void LineTunnel(Point position, int xReps)
    {
        for (int i = position.X - xReps / 2; i <= position.X + xReps / 2; i++)
        {
            WorldGen.KillTile(i, position.Y, false, false, true);
        }
    }

    public int Raycast(int x, int y)
    {
        if (x < 2 || x > Main.maxTilesX - 2)
        {
            ErrorLogger.Log("X is dead.");
            return 0;
        }
        if (y < 2 || y > Main.maxTilesY - 2)
        {
            ErrorLogger.Log("Y is not alive");
            return 0;
        }
        while (!Main.tile[x, y].active())
        {
            y++;
        }
        return y;
    }

    public override void PostWorldGen()
    {
        int[] array = new int[8]
        {
            base.mod.ItemType("NatureBless"),
            base.mod.ItemType("RuinCore"),
            base.mod.ItemType("GlowingScale"),
            base.mod.ItemType("PurityPrismStaff"),
            base.mod.ItemType("LeafStorm"),
            base.mod.ItemType("Chameleon"),
            base.mod.ItemType("CharmofLive"),
            base.mod.ItemType("BlankTab")
        };
        int[] array2 = new int[11]
        {
            313,
            314,
            315,
            316,
            279,
            2351,
            292,
            289,
            0,
            0,
            0
        };
        array2[8] = base.mod.ItemType("LiveBloomPotion");
        array2[9] = base.mod.ItemType("JadePotion");
        array2[10] = base.mod.ItemType("NatureEssence");
        int[] array3 = array2;
        int num = 0;
        for (int i = 0; i < 1000; i++)
        {
            Chest chest = Main.chest[i];
            if (chest != null && Main.tile[chest.x, chest.y].type == base.mod.TileType("RuinChest"))
            {
                num = WorldGen.genRand.Next(array.Length);
                chest.item[0].SetDefaults(array[num], false);
                int num2 = WorldGen.genRand.Next(5, 9);
                for (int j = 1; j < num2; j++)
                {
                    int num3 = WorldGen.genRand.Next(array3.Length);
                    chest.item[j].SetDefaults(array3[num3], false);
                    chest.item[j].stack = WorldGen.genRand.Next(2, 5);
                }
            }
        }
        int[] array4 = new int[1]
        {
            base.mod.ItemType("TheTyrant")
        };
        int[] array5 = new int[11]
        {
            313,
            314,
            315,
            316,
            279,
            2351,
            292,
            289,
            0,
            0,
            0
        };
        array5[8] = base.mod.ItemType("LiveBloomPotion");
        array5[9] = base.mod.ItemType("JadePotion");
        array5[10] = base.mod.ItemType("NatureEssence");
        int[] array6 = array5;
        int num4 = 0;
        for (int k = 0; k < 1000; k++)
        {
            Chest chest2 = Main.chest[k];
            if (chest2 != null && Main.tile[chest2.x, chest2.y].type == base.mod.TileType("BloomChest"))
            {
                num4 = WorldGen.genRand.Next(array4.Length);
                chest2.item[0].SetDefaults(array4[num4], false);
                int num5 = WorldGen.genRand.Next(5, 9);
                for (int l = 1; l < num5; l++)
                {
                    int num6 = WorldGen.genRand.Next(array6.Length);
                    chest2.item[l].SetDefaults(array6[num6], false);
                    chest2.item[l].stack = WorldGen.genRand.Next(2, 5);
                }
            }
        }
    }
}
