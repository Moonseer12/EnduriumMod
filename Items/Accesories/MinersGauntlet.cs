using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
namespace EnduriumMod.Items.Accesories
{
    public class MinersGauntlet : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 26;

            item.value = Terraria.Item.buyPrice(0, 1, 0, 0);
            item.rare = 3;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Miner's Gauntlet");
            Tooltip.SetDefault("Provides the necessary skils for a miner");
        }

        public override void UpdateAccessory(Player player, bool hideVisual)  //this is so when the item is equipped will give this stats to the player
        {
            player.AddBuff(BuffID.Shine, 2);
            Player.tileRangeX += 10;
            player.pickSpeed -= 0.4f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("MinersRing"), 1);
            recipe.AddIngredient(null, ("MinersLantern"), 1);
            recipe.AddIngredient(null, ("MinersGlove"), 1);
            recipe.AddIngredient(null, ("MinersBelt"), 1);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
