using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Accesories
{
    public class BlightBaneEmblem : ModItem
    {
        public override void SetDefaults()
        {


            item.width = 32;
            item.height = 32;
            item.value = Terraria.Item.buyPrice(0, 10, 0, 0);
            item.rare = -12;
			item.expert = true;
            item.accessory = true;
        }

        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Increases damage resistance by 14% and damage by 10%\nGetting struck releases cursed inferno\nIncreases life regeneration");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("EatersTooth"));
            recipe.AddIngredient(ItemID.WormScarf);
            recipe.AddIngredient(null, ("ErodedPrism"), 1);
            recipe.AddIngredient(ItemID.DemoniteBar, 15);
            recipe.AddIngredient(ItemID.ShadowScale, 45);
            recipe.AddIngredient(ItemID.BeeWax, 10);
            recipe.AddTile(TileID.TinkerersWorkbench);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }

        public override void UpdateAccessory(Player player, bool hideVisual)
        {
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).EatersBreath = true;
            player.endurance += 0.14f;
            player.magicDamage += 0.10f;
            player.thrownDamage += 0.10f;
            player.rangedDamage += 0.10f;
            player.meleeDamage += 0.10f;
            player.minionDamage += 0.10f;
            player.lifeRegen = +2;
        }
    }
}
