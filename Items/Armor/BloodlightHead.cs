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
    public class BloodlightHead : ModItem
    {

        public override void SetDefaults()
        {

            item.width = 22;
            item.height = 22;

            item.value = 12000;
            item.rare = 3;
            item.defense = 3; //42
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bloodlight Helmet");
            Tooltip.SetDefault("Increases max health\nIncreases critical strike chance and damage by 4%");
        }
		        public override bool IsArmorSet(Terraria.Item head, Terraria.Item body, Terraria.Item legs)
        {
            return body.type == mod.ItemType("BloodlightBody") && legs.type == mod.ItemType("BloodlightLegs");
        }

        public override void ArmorSetShadows(Player player)
        {
			player.armorEffectDrawOutlinesForbidden = true;
						player.armorEffectDrawShadow = true;
        }

        public override void UpdateArmorSet(Player player)
        {
            ((MyPlayer)player.GetModPlayer(mod, "MyPlayer")).BloodGem = true;
            player.setBonus = "Summons a blood gem that give you a healing effect.";
           
            {
                bool petProjectileNotSpawned1 = player.ownedProjectileCounts[mod.ProjectileType("BloodGemProj")] <= 0;
                if (petProjectileNotSpawned1 && player.whoAmI == Main.myPlayer)
                {
                    int num1 = Projectile.NewProjectile(player.position.X + (float)(player.width / 2), player.position.Y + (float)(player.height / 2), 0f, 0f, mod.ProjectileType("BloodGemProj"), 0, 0f, player.whoAmI, 0f, 0f);

                }
            }

        }
        

        public override void UpdateEquip(Player player)
        {
				            player.statLifeMax2 += 15;
			            
					            player.rangedDamage += 0.04f;
            player.meleeDamage += 0.04f;
			player.meleeCrit += 4;
			player.magicCrit += 4;
			player.rangedCrit += 4;
			player.thrownCrit += 4;
            player.magicDamage += 0.04f;
            player.thrownDamage += 0.04f;
        }
						        public override void AddRecipes()
        {
            ModRecipe recipe = new ModRecipe(mod);
			            recipe.AddIngredient(null, ("BloodlightBar"), 18);
            recipe.AddTile(TileID.Anvils);
            recipe.SetResult(this);
            recipe.AddRecipe();
        }
    }
}