using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class BoneScrap : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 30;
            item.height = 30;
            item.maxStack = 99;

            item.value = Terraria.Item.sellPrice(0, 0, 2, 50);
            item.rare = 3;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Bone Scrap");
        }    
    }
}