using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons
{
    public class VeinCutter : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 21;
            item.thrown = true;
            item.noMelee = true;
            item.width = 14;
            item.height = 36;
            item.useTime = 25;
            item.crit = 13;
            item.useAnimation = 25;
            item.useStyle = 1;
            item.knockBack = 9;
            item.value = Terraria.Item.buyPrice(0, 0, 10, 0);
            item.rare = 4;
            item.UseSound = SoundID.Item1;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("VeinCutter");
            item.shootSpeed = 13f;
            item.useTurn = true;
            item.maxStack = 999;
            item.consumable = true;
            item.noUseGraphic = true;

        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Vein Cutter");
            Tooltip.SetDefault("");
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(ItemID.CrimtaneBar, 5);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this, 125);
            recipe.AddRecipe();
        }
    }
}
