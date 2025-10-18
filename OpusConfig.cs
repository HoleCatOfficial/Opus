using System.ComponentModel;
using Terraria.Localization;
using Terraria.ModLoader.Config;

namespace Opus
{
    public class OpusConfig : ModConfig
    {
        public override ConfigScope Mode => ConfigScope.ClientSide;

        [DefaultValue(true)]
        [DisplayName("Play Opus Jingle on first world entry.")]
        public bool PlayJingleOnEnterWorldFirstTime { get; set; }
        
    }
}
