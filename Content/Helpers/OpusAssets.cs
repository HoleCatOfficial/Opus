
using System;
using System.Media;
using System.Reflection;
using System.Runtime.CompilerServices;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;
using Mono.Cecil.Cil;
using rail;
using ReLogic.Content;
using Terraria;
using Terraria.Audio;
using Terraria.GameContent;
using Terraria.ID;
using Terraria.ModLoader;

namespace Opus.Content.Helpers
{
    /// <summary>
    /// A central repository of useful textures to use for draw calls.
    /// </summary>
    public class OpusAssets
    {
        public const string TexturePath = "Opus/Assets/Textures";
        public const string AudioPath = "Opus/Assets/Audio";
        //
        //Practical, Every-Day VFX Textures
        //
        public static Asset<Texture2D> Square = TextureAssets.MagicPixel;
        public static Asset<Texture2D> PointGlow = ModContent.Request<Texture2D>($"{TexturePath}/PointGlow");
        public static Asset<Texture2D> AreaGlow = ModContent.Request<Texture2D>($"{TexturePath}/Glow");
        public static Asset<Texture2D> FadeLine = ModContent.Request<Texture2D>($"{TexturePath}/FadeLine");
        public static Asset<Texture2D> Sparkle(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Shine{Variant}");
        }

        public static Asset<Texture2D> Star(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Star{Variant}");
        }

        public static Asset<Texture2D> Cyclone(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Cyclone{Variant}");
        }
        public static Asset<Texture2D> Warning = ModContent.Request<Texture2D>($"{TexturePath}/WarningTriangle");
        public static Asset<Texture2D> Trail(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Trail{Variant}");
        }
        public static Asset<Texture2D> Line(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Line{Variant}");
        }
        public static Asset<Texture2D> TilableNoise(int Variant)
        {
            if (Variant <= 0)
            {
                Variant = 1;
            }
            return ModContent.Request<Texture2D>($"{TexturePath}/Noise{Variant}");
        }
        //
        // Sounds
        //
        public static SoundStyle Jingle = new SoundStyle($"{AudioPath}/OpusJingle");
        public static SoundStyle PageTurn = new SoundStyle($"{AudioPath}/Pageturn");
        public static SoundStyle BookOpen = new SoundStyle($"{AudioPath}/BookOpen");
    }
}