using System;
using Microsoft.Xna.Framework;
using Terraria;

namespace OpusLib.Content.Helpers
{
    public static class OpusColorUtils
    {
        /// <summary>
        /// Returns a copy of the color with a different alpha.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="alpha">Alpha as a float from 0f–1f.</param>
        public static Color WithAlpha(this Color color, float alpha)
        {
            return new Color(color.R, color.G, color.B, (byte)(MathHelper.Clamp(alpha, 0f, 1f) * 255));
        }

        /// <summary>
        /// Returns a copy of the color with a different alpha.
        /// </summary>
        /// <param name="color">The original color.</param>
        /// <param name="alpha">Alpha as a byte (0–255).</param>
        public static Color WithAlpha(this Color color, byte alpha)
        {
            return new Color(color.R, color.G, color.B, alpha);
        }

        /// <summary>
        /// Returns the input color that is tinted <i>percentage</i>% white, with 1 being fully white.
        /// </summary>
        /// <param name="inputColor"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static Color Pastel(this Color inputColor, float percentage)
        {
            percentage = MathHelper.Clamp(percentage, 0f, 1f);

            return new Color(
                (byte)MathHelper.Lerp(inputColor.R, 255, percentage),
                (byte)MathHelper.Lerp(inputColor.G, 255, percentage),
                (byte)MathHelper.Lerp(inputColor.B, 255, percentage),
                inputColor.A
            );
        }

        /// <summary>
        /// Returns the input color that is tinted <i>percentage</i>% black, with 1 being fully black.
        /// </summary>
        /// <param name="inputColor"></param>
        /// <param name="percentage"></param>
        /// <returns></returns>
        public static Color Darken(this Color inputColor, float percentage)
        {
            percentage = MathHelper.Clamp(percentage, 0f, 1f);

            return new Color(
                (byte)MathHelper.Lerp(inputColor.R, 0, percentage),
                (byte)MathHelper.Lerp(inputColor.G, 0, percentage),
                (byte)MathHelper.Lerp(inputColor.B, 0, percentage),
                inputColor.A
            );
        }

        public static Color MultiLerp(float progress, params Color[] colors)
        {
            if (colors == null || colors.Length == 0)
                return Color.White;

            if (colors.Length == 1)
                return colors[0];

            progress = MathHelper.Clamp(progress, 0f, 1f);

            int segmentCount = colors.Length - 1;
            float scaled = progress * segmentCount;

            int index = (int)scaled;

            if (index >= segmentCount)
                return colors[^1];

            float localProgress = scaled - index;

            return Color.Lerp(
                colors[index],
                colors[index + 1],
                localProgress
            );
        }

        public static Color FromHex(string hex)
        {
            System.Drawing.Color c = System.Drawing.ColorTranslator.FromHtml(hex);
            return new Color(c.R, c.G, c.B, c.A);
        }
    }
}