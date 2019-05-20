using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.Acid
{
    public class AcidReflux : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 47;
            item.melee = true;
            item.useTime = 10;
            item.useAnimation = 10;
            item.useStyle = 5;
            item.channel = true;
            item.knockBack = 2f;
            item.value = Terraria.Item.sellPrice(0, 26, 25, 0);
            item.rare = 6;
            item.autoReuse = false;
            item.shoot = mod.ProjectileType("AcidReflux");
            item.noUseGraphic = true;
            item.noMelee = true;
            item.UseSound = SoundID.Item1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Acid Reflux");
            Tooltip.SetDefault("");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("AcidCore"), 26);
            recipe.AddIngredient(null, ("DarkDust"), 6);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}
