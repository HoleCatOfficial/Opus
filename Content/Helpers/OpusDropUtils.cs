using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace Opus.Content.Helpers
{
    public class OpusNPCDropHelper : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        /// <summary>
        /// NPCs that should be ignored for drop purposes when doing global drops.
        /// <para/> Most included are extensions like Moon Lord parts, single-use NPCs like the unconscious bartender, and projectile NPCs like Vile Spit.
        /// </summary>
        public int[] IgnoreEnemies = new int[]
        {
            NPCID.MoonLordFreeEye,
            NPCID.MoonLordHand,
            NPCID.MoonLordHead,
            NPCID.MoonLordLeechBlob,
            NPCID.AncientCultistSquidhead,
            NPCID.AncientDoom,
            NPCID.AncientLight,
            NPCID.BartenderUnconscious,
            NPCID.BloodEelBody,
            NPCID.BloodEelTail,
            NPCID.VileSpit,
            NPCID.VileSpitEaterOfWorlds,
            NPCID.WaterSphere,
            NPCID.ChaosBall,
            NPCID.ChaosBallTim,
            NPCID.RedDragonfly,
            NPCID.BlueDragonfly,
            NPCID.GoldDragonfly,
            NPCID.BlackDragonfly,
            NPCID.GreenDragonfly,
            NPCID.OrangeDragonfly,
            NPCID.YellowDragonfly,
            NPCID.Butterfly,
            NPCID.GoldButterfly,
            NPCID.HellButterfly,
            NPCID.EmpressButterfly,
            NPCID.Bunny,
            NPCID.BunnySlimed,
            NPCID.BunnyXmas,
            NPCID.GemBunnyAmber,
            NPCID.GemBunnyAmethyst,
            NPCID.GemBunnyDiamond,
            NPCID.GemBunnyEmerald,
            NPCID.GemBunnyRuby,
            NPCID.GemBunnySapphire,
            NPCID.GemBunnyTopaz,
            NPCID.GemSquirrelAmber,
            NPCID.GemSquirrelAmethyst,
            NPCID.GemSquirrelDiamond,
            NPCID.GemSquirrelEmerald,
            NPCID.GemSquirrelRuby,
            NPCID.GemSquirrelSapphire,
            NPCID.GemSquirrelTopaz,
            NPCID.GolemFistLeft,
            NPCID.GolemFistRight,
            NPCID.GolemHeadFree,
            NPCID.Firefly,
            NPCID.Stinkbug
        };

        /// <summary>
        /// Contains members of the Diabolic Faction.
        /// </summary>
        public int[] DiabolicFactionEnemiesExclusive = new int[]
        {
            NPCID.DiabolistRed,
            NPCID.DiabolistWhite
        };
        
        /// <summary>
        /// Contains members of the Molten Legion.
        /// </summary>
        public int[] MoltenLegionEnemiesExclusive = new int[]
        {
            NPCID.HellArmoredBones,
            NPCID.HellArmoredBonesMace,
            NPCID.HellArmoredBonesSpikeShield,
            NPCID.HellArmoredBonesSword
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Tiled Walls.
        /// </summary>
        public int[] TiledWallsEnemies = new int[]
        {
            NPCID.DiabolistRed,
            NPCID.DiabolistWhite,
            NPCID.HellArmoredBones,
            NPCID.HellArmoredBonesMace,
            NPCID.HellArmoredBonesSpikeShield,
            NPCID.HellArmoredBonesSword
        };

        /// <summary>
        /// Contains members of the Ragged Brotherhood.
        /// </summary>
        public int[] RaggedBrotherhoodEnemiesExclusive = new int[]
        {
            NPCID.RaggedCaster,
            NPCID.RaggedCasterOpenCoat
        };

        /// <summary>
        /// Contains members of the Rusted Company.
        /// </summary>
        public int[] RustedCompanyEnemiesExclusive = new int[]
        {
            NPCID.RustyArmoredBonesAxe,
            NPCID.RustyArmoredBonesFlail,
            NPCID.RustyArmoredBonesSword,
            NPCID.RustyArmoredBonesSwordNoArmor
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Slab Walls.
        /// </summary>
        public int[] SlabWallsDungeonEnemies = new int[]
        {
            NPCID.RaggedCaster,
            NPCID.RaggedCasterOpenCoat,
            NPCID.RustyArmoredBonesAxe,
            NPCID.RustyArmoredBonesFlail,
            NPCID.RustyArmoredBonesSword,
            NPCID.RustyArmoredBonesSwordNoArmor
        };

        /// <summary>
        /// Contains members of the Necromantic Faction.
        /// </summary>
        public int[] NecromanticFactionEnemies = new int[]
        {
            NPCID.Necromancer,
            NPCID.NecromancerArmored
        };

        /// <summary>
        /// Contains members of the Marching Bones.
        /// </summary>
        public int[] MarchingBonesFactionEnemies = new int[]
        {
            NPCID.BlueArmoredBones,
            NPCID.BlueArmoredBonesMace,
            NPCID.BlueArmoredBonesNoPants,
            NPCID.BlueArmoredBonesSword
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Brick Walls.
        /// </summary>
        public int[] BrickWallsDungeonEnemies = new int[]
        {
            NPCID.Necromancer,
            NPCID.NecromancerArmored,
            NPCID.BlueArmoredBones,
            NPCID.BlueArmoredBonesMace,
            NPCID.BlueArmoredBonesNoPants,
            NPCID.BlueArmoredBonesSword
        };

        /// <summary>
        /// Contains all enemies encountered during the Pumpkin Moon Event
        /// </summary>
        public int[] PumpkinMoonEnemies = new int[]
        {
            NPCID.Scarecrow1,
            NPCID.Scarecrow2,
            NPCID.Scarecrow3,
            NPCID.Scarecrow4,
            NPCID.Scarecrow5,
            NPCID.Scarecrow6,
            NPCID.Scarecrow7,
            NPCID.Scarecrow8,
            NPCID.Scarecrow9,
            NPCID.Scarecrow10,
            NPCID.Hellhound,
            NPCID.Poltergeist,
            NPCID.Splinterling,
            NPCID.HeadlessHorseman
        };

        /// <summary>
        /// Gets the Enemy list for the specified wave of the Pumpkin Moon
        /// </summary>
        /// <param name="Wave"></param>
        /// <returns></returns>
        public int[] PumpkinMoveGetWaveEnemies(int Wave)
        {
            if (Wave < 0)
            {
                Wave = 1;
            }

            if (Wave == 1)
            {
                return [NPCID.Scarecrow1,
                NPCID.Scarecrow2,
                NPCID.Scarecrow3,
                NPCID.Scarecrow4,
                NPCID.Scarecrow5,
                NPCID.Scarecrow6,
                NPCID.Scarecrow7,
                NPCID.Scarecrow8,
                NPCID.Scarecrow9,
                NPCID.Scarecrow10];
            }
            if (Wave == 2)
            {
                return [NPCID.Scarecrow1,
                NPCID.Scarecrow2,
                NPCID.Scarecrow3,
                NPCID.Scarecrow4,
                NPCID.Scarecrow5,
                NPCID.Scarecrow6,
                NPCID.Scarecrow7,
                NPCID.Scarecrow8,
                NPCID.Scarecrow9,
                NPCID.Scarecrow10,
                NPCID.Splinterling];
            }
            if (Wave == 3)
            {
                return [NPCID.Hellhound,
                NPCID.Splinterling];
            }
            if (Wave == 4)
            {
                return [NPCID.Scarecrow1,
                NPCID.Scarecrow2,
                NPCID.Scarecrow3,
                NPCID.Scarecrow4,
                NPCID.Scarecrow5,
                NPCID.Scarecrow6,
                NPCID.Scarecrow7,
                NPCID.Scarecrow8,
                NPCID.Scarecrow9,
                NPCID.Scarecrow10,
                NPCID.Splinterling,
                NPCID.Poltergeist];
            }
            if (Wave == 5)
            {
                return [NPCID.HeadlessHorseman, NPCID.Hellhound];
            }
            if (Wave == 6)
            {
                return [NPCID.Scarecrow1,
                NPCID.Scarecrow2,
                NPCID.Scarecrow3,
                NPCID.Scarecrow4,
                NPCID.Scarecrow5,
                NPCID.Scarecrow6,
                NPCID.Scarecrow7,
                NPCID.Scarecrow8,
                NPCID.Scarecrow9,
                NPCID.Scarecrow10,
                NPCID.Splinterling,
                NPCID.MourningWood];
            }
            if (Wave == 7)
            {
                return [NPCID.MourningWood, NPCID.Poltergeist, NPCID.Hellhound];
            }
            if (Wave == 8)
            {
                return [NPCID.HeadlessHorseman, NPCID.Poltergeist, NPCID.Hellhound];
            }
            if (Wave == 9)
            {
                return [NPCID.MourningWood,
                NPCID.Poltergeist,
                NPCID.Hellhound,
                NPCID.Splinterling,
                NPCID.Scarecrow2,
                NPCID.Scarecrow3,
                NPCID.Scarecrow4,
                NPCID.Scarecrow5,
                NPCID.Scarecrow6,
                NPCID.Scarecrow7,
                NPCID.Scarecrow8,
                NPCID.Scarecrow9,
                NPCID.Scarecrow10];
            }
            if (Wave == 7)
            {
                return [NPCID.Pumpking, NPCID.Splinterling, NPCID.Hellhound];
            }
            


            return [NPCID.Scarecrow1,
            NPCID.Scarecrow2,
            NPCID.Scarecrow3,
            NPCID.Scarecrow4,
            NPCID.Scarecrow5,
            NPCID.Scarecrow6,
            NPCID.Scarecrow7,
            NPCID.Scarecrow8,
            NPCID.Scarecrow9,
            NPCID.Scarecrow10];
        }
    }
}