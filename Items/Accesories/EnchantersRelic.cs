using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class EnchantersRelic : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 3, 0, 0);
            item.rare = 5;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Relic of Enchantment");
            Tooltip.SetDefault("'Your soul is sealed within'\nIncreases damage by 18% at the cost of vitality");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("ErodedPrism"));
            recipe.AddIngredient(null, ("EvilHeart"));
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.lifeRegen = -3;
            player.statLifeMax2 -= 50;
            player.minionDamage *= 1.18f;
            player.thrownDamage *= 1.18f;
            player.magicDamage *= 1.18f;
            player.rangedDamage *= 1.18f;
            player.meleeDamage *= 1.18f;
        }
    }
}
