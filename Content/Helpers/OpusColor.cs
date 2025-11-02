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
    }
}