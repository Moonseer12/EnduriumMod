using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.DataStructures;
using Terraria.ModLoader;
namespace EnduriumMod.Items.Accesories
{
    [AutoloadEquip(EquipType.Wings)]

    public class ElementalBooster : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 20;
            item.height = 26;

            item.value = Terraria.Item.buyPrice(0, 20, 0, 0);
            item.rare = 9;
            item.accessory = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Element Booster");
            Tooltip.SetDefault("Allows flight\nYou leave a trail of energy in your wake");
        }
        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            player.moveSpeed *= 1.1f;
            player.wingTimeMax = 500;
        }

        public override void VerticalWingSpeeds(Player player, ref float ascentWhenFalling, ref float ascentWhenRising, ref float maxCanAscendMultiplier, ref float maxAscentMultiplier, ref float constantAscend)
        {
            ascentWhenFalling = 1f;
            ascentWhenRising = 0.2f;
            maxCanAscendMultiplier = 1f;
            maxAscentMultiplier = 2f;
            constantAscend = 0.7f;
        }


        public override void HorizontalWingSpeeds(Player player, ref float speed, ref float acceleration)
        {
            speed = 7.5f;
            acceleration *= 2.3f;
        }
        public override bool WingUpdate(Player player, bool inUse)
        {
            if (inUse)
            {
                Dust.NewDust(player.position, player.width, player.height, 242, 0, 0, 0, default(Color), 0.95f);
                Dust.NewDust(player.position, player.width, player.height, 156, 0, 0, 0, default(Color), 0.95f);
                Dust.NewDust(player.position, player.width, player.height, 221, 0, 0, 0, default(Color), 0.95f);
                Dust.NewDust(player.position, player.width, player.height, 158, 0, 0, 0, default(Color), 0.95f);
                base.WingUpdate(player, inUse);

            }
            return false;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.LunarBar, 20);
			recipe.AddIngredient(ItemID.FragmentSolar, 20);
			recipe.AddIngredient(ItemID.FragmentNebula, 20);
			recipe.AddIngredient(ItemID.FragmentStardust, 20);
			 recipe.AddIngredient(ItemID.FragmentVortex, 20);
            recipe.AddIngredient(null, ("CoreofCreation"));
            recipe.AddTile(TileID.LunarCraftingStation);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
