using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.BloodFang
{
    public class StaffOfTheMatyr : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 12;
            item.magic = true;
            item.mana = 3;
            item.width = 52;
            item.height = 52;
            item.useTime = 28;
            item.useAnimation = 28;
            item.useStyle = 5;
            Item.staff[item.type] = true;


            item.noMelee = true; //so the item's animation doesn't do damage
            item.knockBack = 2.25f;
            item.value = 10000;
            item.rare = 2;  
            item.UseSound = SoundID.Item72;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("BloodOfTheMatyr");
            item.shootSpeed = 11f;
        }

        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("BloodFangCore"), 15);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}