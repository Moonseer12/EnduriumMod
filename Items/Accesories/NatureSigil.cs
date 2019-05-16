using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class NatureSigil : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 5, 50, 0);
            item.rare = 4;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nature Sigil");
            Tooltip.SetDefault("Regenerates and increases health\nGetting hit has a 33% chance to boost your life regeneration for a short time.");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.statLifeMax2 += 25;
            player.lifeRegen = +2;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).TropicalBlushV2 = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("BloomlightBar"), 15);
            recipe.AddIngredient(null, ("CharmofLive"));
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
