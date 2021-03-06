using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Armor
{
    [AutoloadEquip(EquipType.Head)]
    public class OvergrowthHead1 : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 26;
            item.height = 26;

            item.value = 100000;
            item.rare = 7;
            item.defense = 10; //42
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Endurian Headgear");
            Tooltip.SetDefault("18% increased ranged damage and critical strike chance!");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("AcidCore"), 21);
            recipe.AddIngredient(null, ("DarkDust"), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool IsArmorSet(Terraria.Item head, Terraria.Item body, Terraria.Item legs)
        {
            return body.type == mod.ItemType("OvergrowthChest") && legs.type == mod.ItemType("OvergrowthLegs");
        }

        public override void ArmorSetShadows(Player player)
        {
            player.armorEffectDrawOutlines = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            player.setBonus = "Increases ammo and ranged damage by 10%\nHitting enemies gives you several different buffs";
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).Overgrowth = true;
            player.rocketDamage += 0.1f;
            player.arrowDamage += 0.1f;
            player.bulletDamage += 0.1f;
            player.rangedDamage += 0.1f;
        }

        public override void UpdateEquip(Player player)
        {
            player.rangedDamage += 0.18f;
            player.rangedCrit += 18;
        }
    }
}