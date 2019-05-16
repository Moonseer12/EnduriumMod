using Terraria.ModLoader;

namespace EnduriumMod.Items
{
    public class AngelFeather : ModItem
    {
        public override void SetDefaults()
        {

            item.width = 50;
            item.height = 57;
            item.maxStack = 99;
            item.value = Terraria.Item.sellPrice(0, 0, 25, 0);
            item.rare = 5;
        }

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Angel Feather");
            Tooltip.SetDefault("'A rare form of material'");
        }
    }
}
