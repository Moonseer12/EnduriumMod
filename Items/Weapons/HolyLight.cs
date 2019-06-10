using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items.Weapons
{
    public class HolyLight : ModItem
    {
        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Holy Light");
            Tooltip.SetDefault("Creates a bolt of holy energy");
        }
        public override void SetDefaults()
        {     
            item.damage = 30;                      
            item.magic = true;
            item.width = 50;
            item.height = 50;
            item.useTime = 32;
            item.useAnimation = 32;
            item.useStyle = 5;
            Item.staff[item.type] = true;
            item.noMelee = true;
            item.knockBack = 2.5f;    
            item.value = Terraria.Item.buyPrice(0, 30, 0, 0);
            item.rare = 3;
            item.mana = 20;
            item.UseSound = SoundID.Item20;
            item.autoReuse = true;
            item.shoot = mod.ProjectileType("HolyLight");
            item.shootSpeed = 9f;
        }
    }
}