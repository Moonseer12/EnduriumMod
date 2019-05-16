using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class CoreofaForestTyrant : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 10, 50, 0);
            item.rare = -12;
            item.accessory = true;
            item.expert = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Core of the Forest Tyrant");
            Tooltip.SetDefault("Attacks inflict Reaper Nature\nCritical strikes imbue you with spirit energy\nSummons two earthen crystals to protect you\nIncreases all stats\nThe rage of the tyrant is released upon getting hit\nTaking lethal damage will instead bring you back to 200\nThis effect has a cooldown of 2 minutes, during that time life regeneration is slightly lowered");
        }

		/*public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("TheCrownOfNature"));
            recipe.AddIngredient(null, ("EndurianHeart"));
            recipe.AddIngredient(null, ("PlagueCore"));
            recipe.AddIngredient(null, ("SoulofDusk"), 25);
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }*/
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
		            if (player.immune)
            {
                if (Main.rand.Next(16) == 0)
                {
                    for (int n = 0; n < 3; n++)
                    {
                        float x = player.position.X + (float)Main.rand.Next(-400, 400);
                        float y = player.position.Y - (float)Main.rand.Next(500, 800);
                        Vector2 vector = new Vector2(x, y);
                        float num13 = player.position.X + (float)(player.width / 2) - vector.X;
                        float num14 = player.position.Y + (float)(player.height / 2) - vector.Y;
                        num13 += (float)Main.rand.Next(-100, 101);
                        int num15 = 23;
                        float num16 = (float)Math.Sqrt((double)(num13 * num13 + num14 * num14));
                        num16 = (float)num15 / num16;
                        num13 *= num16;
                        num14 *= num16;
                        int num17 = Projectile.NewProjectile(x, y, num13, num14, mod.ProjectileType("MiniLeafFriendly"), 18, 5f, player.whoAmI, 0f, 0f);
                        Main.projectile[num17].ai[1] = player.position.Y;
                    }
                }
            }
            player.moveSpeed += 0.1f;
            player.manaCost *= 0.95f;
            player.minionDamage += 0.05f;
            player.rangedDamage += 0.05f;
            player.meleeDamage += 0.05f;
            player.magicDamage += 0.05f;
            player.thrownDamage += 0.05f;
            player.accRunSpeed += 0.5f;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).HeartOfTheForest = true;
			            player.magicCrit += 10;
            player.AddBuff(mod.BuffType("EarthTurret"), 1);
            player.meleeCrit += 10;
            player.rangedCrit += 10;
            player.thrownCrit += 10;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).BiologicalCrit = true;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).EarthTurret = true;
        }
    }
}
