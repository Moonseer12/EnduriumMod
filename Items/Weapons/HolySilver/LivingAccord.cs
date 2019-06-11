using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.HolySilver
{
    public class LivingAccord : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 50;
            item.melee = true;
            item.useTime = 54;
            item.useAnimation = 54;
            item.useStyle = 5;
            item.channel = true;
            item.knockBack = 5f;
            item.value = Terraria.Item.sellPrice(0, 10, 25, 0);
            item.rare = 8;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("LivingAccord");
            item.noUseGraphic = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item1;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(175, 20);
            recipe.AddIngredient(117, 25);
            recipe.AddIngredient(522, 12);
            recipe.AddIngredient(null, ("StarCrystal"), 5);
            recipe.AddIngredient(null, ("HolySilver"), 5);
            recipe.AddTile(null, "SoulForge");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Living Accord");
            Tooltip.SetDefault("");
        }
    }
}
