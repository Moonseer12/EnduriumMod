using Terraria.ID;
using Terraria.ModLoader;

namespace EnduriumMod.Items
{
	public class AccretionBar : ModItem
	{
        public override void SetDefaults()
		{
			item.width = 30;
			item.height = 30;
			item.maxStack = 999;
			item.value = 255000;
            item.rare = 10;
		}

        public override void SetStaticDefaults()
        {
            DisplayName.SetDefault("Accretion Bar");
            Tooltip.SetDefault("'The hardest substance in the Galaxy, having the power of a black hole.'");
        }
    }
}
