using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class AncientMandible : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 16;
            item.height = 16;
            item.maxStack = 999;
            item.value = Terraria.Item.sellPrice(0, 0, 5, 0);
            item.rare = 1;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Dunefang");
            Tooltip.SetDefault("A fang of an Amber Warden\nAmber Wardens use these to dig through the sands, hunting for robbers and thieves");
        }
    }
}
