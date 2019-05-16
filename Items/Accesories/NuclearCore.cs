using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class NuclearCore : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 10, 50, 0);
            item.rare = 8;
            item.accessory = true;
            item.defense = 4;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Nuclear Plating");
            Tooltip.SetDefault("Grants immunity to Cursed Inferno, Slow and the Acid Plague\nGetting hit releases acid");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("AcidCore"), 18);
            recipe.AddIngredient(null, ("DarkDust"), 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.buffImmune[BuffID.CursedInferno] = true;
            player.buffImmune[BuffID.Slow] = true;
            player.buffImmune[mod.BuffType("AcidPlague")] = true;
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).NuclearHurt = true;
        }
    }
}
