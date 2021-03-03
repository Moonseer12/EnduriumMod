using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class MagmaCore : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 14;
            item.height = 14;
            item.maxStack = 99;
            item.value = Terraria.Item.sellPrice(0, 0, 33, 0);
            item.rare = 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Magma Crystal");
            Tooltip.SetDefault("'A crystal of solidified magma'\n'These crystals are fed upon by Core Trenchers, who search for these to sustain themselves'");
        }
    }
}
