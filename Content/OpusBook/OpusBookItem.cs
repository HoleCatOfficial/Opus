using OpusLib.Content.Helpers;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;

namespace OpusLib.Content.OpusBook
{
    public abstract class OpusBookItem : ModItem
    {
        /// <summary>
        /// The registry key for this book. Must match what was passed into OpusReader.RegisterBook.
        /// </summary>
        public abstract string GetBookKey();
        public virtual SoundStyle OpenSound => new SoundStyle("OpusLib/Assets/Audio/BookOpen");

        public override void SetDefaults()
        {
            Item.width = 34;
            Item.height = 38;
            Item.useStyle = ItemUseStyleID.HoldUp;
            Item.useTime = 20;
            Item.useAnimation = 20;
            Item.useTurn = true;
            Item.noUseGraphic = true;
            Item.UseSound = OpenSound;
            Item.rare = ItemRarityID.Blue;
            Item.maxStack = 1;
        }

        public override bool? UseItem(Player player)
        {
            // If the book UI is already open, just close it
            if (OpusBookUI.Visible)
            {
                OpusBookUI.Visible = false;
                return true;
            }

            // Otherwise, open the book associated with this item
            var reader = ModContent.GetInstance<OpusReader>();
            reader.OpenBook(GetBookKey());
            return true;
        }
    }
}
