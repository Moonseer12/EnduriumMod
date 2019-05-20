using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons.ForestChest
{
    public class PurityPrismStaff : ModItem
    {
        public override void SetStaticDefaults()
        {
            Tooltip.SetDefault("Right click to create a purity prism\nFiring while the prism is active causes it to fire aswell!");
        }

        public override void SetDefaults()
        {
            item.damage = 12;
            item.magic = true;
            item.width = 48;
            item.height = 22;
            item.mana = 4;
            item.useTime = 24;
            item.useAnimation = 24;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.knockBack = 3;
            item.value = 10000;
            item.rare = 6;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shootSpeed = 11f;
            item.shoot = mod.ProjectileType("BloomCard");
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("NatureEssence"), 15);
            recipe.AddTile(mod.TileType("AncientAltar"));
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }

        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 5;
                Item.staff[item.type] = true;
                item.useTime = 32;
                item.useAnimation = 32;
            }
            else
            {
                item.useStyle = 5;
                Item.staff[item.type] = true;
                item.useTime = 24;
                item.useAnimation = 24;
            }
            return base.CanUseItem(player);
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.altFunctionUse != 2)
            {
                modPlayer.UsedPrismStaff = true;
            }
            if (player.altFunctionUse == 2)
            {
                int p = Projectile.NewProjectile(Main.MouseWorld.X, Main.MouseWorld.Y, 0f, 0f, mod.ProjectileType("PurityPrism"), item.damage, item.knockBack, Main.myPlayer);
                return false;
            }
          return true;
        }
    }
}