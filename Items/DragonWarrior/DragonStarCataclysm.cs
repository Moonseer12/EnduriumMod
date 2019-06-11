using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.DragonWarrior
{
    public class DragonStarCataclysm : ModItem
    {
        public override void SetDefaults()
        {

            item.damage = 78;
            item.magic = true;
            item.mana = 20;
            item.width = 28;
            item.height = 30;
            item.useTime = 15;
            item.useAnimation = 15;
            item.useStyle = 5;
            item.crit = 20;
            item.noMelee = true;
            item.knockBack = 3.25f;
            item.value = 80000;
            item.rare = 7;
            item.UseSound = SoundID.Item43;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("DragonBeam");
            item.shootSpeed = 8f;
        }
        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
            recipe.AddIngredient(null, ("DragonShard"), 12);
            recipe.AddIngredient(null, ("DragonBlood"), 12);
            recipe.AddIngredient(ItemID.ChlorophyteBar, 25);
            recipe.AddTile(null, "AltarofFire");
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dragon Fire Cataclysm");
            Tooltip.SetDefault("Creates orbiting fiery stars around the player\nProjectiles do more damage overtime");
        }
        public override bool CanUseItem(Player player)
        {
            if (player.altFunctionUse == 2)
            {
                item.useStyle = 5;
                Item.staff[item.type] = true;
                item.useTime = 40;
                item.useAnimation = 40;
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
        public override bool AltFunctionUse(Player player)
        {
            return true;
        }
        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            MyPlayer modPlayer = (MyPlayer)player.GetModPlayer(mod, "MyPlayer");
            if (player.altFunctionUse != 2)
            {
                modPlayer.UsedSpiritOrb = false;
                Projectile.NewProjectile(position.X, position.Y, 0f, 0f, type, damage, knockBack, player.whoAmI, Main.rand.NextFloat(0f, 360f), Main.rand.NextFloat(80f, 180f));
            }
            if (player.altFunctionUse == 2)
            {
                modPlayer.UsedSpiritOrb = true;
            }
            return false;
        }
    }
}