using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class RunicHeart : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 30;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 25, 0, 0);
            item.rare = 8;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Runic Heart");
            Tooltip.SetDefault("Increases health and damage\nDamage increased by 15% if under 75% health");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("EvilHeart"));
            recipe.AddIngredient(ItemID.AvengerEmblem);
            recipe.AddIngredient(null, ("DuskSteel"), 25);
            recipe.AddIngredient(null, ("ShadowRemnants"), 25);
            recipe.AddTile(TileID.MythrilAnvil);
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            if (player.statLife <= (player.statLifeMax2 * 0.75f))
            {
                player.rangedDamage += 0.15f;
                player.magicDamage += 0.15f;
                player.meleeDamage += 0.15f;
                player.thrownDamage += 0.15f;
                player.minionDamage += 0.15f;
            }
            player.statLifeMax2 += 25;
            player.rangedDamage += 0.08f;
            player.magicDamage += 0.08f;
            player.meleeDamage += 0.08f;
            player.thrownDamage += 0.08f;
            player.minionDamage += 0.08f;
        }
    }
}
