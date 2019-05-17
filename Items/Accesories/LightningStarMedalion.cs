using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class LightningStarMedalion : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 2, 50, 0);
            item.rare = 2;
            item.accessory = true;
            item.defense = 2;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Lightning Star Emblem");
            Tooltip.SetDefault("Hitting enemies occasionally lets you run like the wind for a short time.");
        }


        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).Lightning = true;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.Diamond, 5);
            recipe.AddIngredient(ItemID.FallenStar, 5);
            recipe.AddIngredient(null, ("SkyGlazeAlloy"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
