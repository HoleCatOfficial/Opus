
using Microsoft.CodeAnalysis.Operations;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OpusLib.Content.Items
{
	public class PlaceablePot : ModItem
	{
		public override void SetDefaults() {
			Item.DefaultToPlaceableTile(TileID.Pots, Main.rand.Next(4));
            Item.consumable = false;
            Item.maxStack = 1;
			Item.width = 34;
			Item.height = 32;
			Item.value = 10;
		}
		public override void AddRecipes() {
			CreateRecipe()
				.AddIngredient(ItemID.ClayBlock, 5)
                .AddTile(TileID.Furnaces)
				.Register();
		}
	}
}