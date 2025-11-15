using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace OpusLib.Content.Helpers
{
    public class OpusNPCDropHelper : GlobalNPC
    {
        public override bool InstancePerEntity => true;

        /// <summary>
        /// NPCs that should be ignored for drop purposes when doing global drops.
        /// <para/> Most included are extensions like Moon Lord parts, single-use NPCs like the unconscious bartender, and projectile NPCs like Vile Spit.
        /// </summary>
        public static int[] IgnoreEnemies = new int[]
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
        public static int[] DiabolicFactionEnemiesExclusive = new int[]
        {
            NPCID.DiabolistRed,
            NPCID.DiabolistWhite
        };
        
        /// <summary>
        /// Contains members of the Molten Legion.
        /// </summary>
        public static int[] MoltenLegionEnemiesExclusive = new int[]
        {
            NPCID.HellArmoredBones,
            NPCID.HellArmoredBonesMace,
            NPCID.HellArmoredBonesSpikeShield,
            NPCID.HellArmoredBonesSword
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Tiled Walls.
        /// </summary>
        public static int[] TiledWallsEnemies = new int[]
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
        public static int[] RaggedBrotherhoodEnemiesExclusive = new int[]
        {
            NPCID.RaggedCaster,
            NPCID.RaggedCasterOpenCoat
        };

        /// <summary>
        /// Contains members of the Rusted Company.
        /// </summary>
        public static int[] RustedCompanyEnemiesExclusive = new int[]
        {
            NPCID.RustyArmoredBonesAxe,
            NPCID.RustyArmoredBonesFlail,
            NPCID.RustyArmoredBonesSword,
            NPCID.RustyArmoredBonesSwordNoArmor
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Slab Walls.
        /// </summary>
        public static int[] SlabWallsDungeonEnemies = new int[]
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
        public static int[] NecromanticFactionEnemies = new int[]
        {
            NPCID.Necromancer,
            NPCID.NecromancerArmored
        };

        /// <summary>
        /// Contains members of the Marching Bones.
        /// </summary>
        public static int[] MarchingBonesFactionEnemies = new int[]
        {
            NPCID.BlueArmoredBones,
            NPCID.BlueArmoredBonesMace,
            NPCID.BlueArmoredBonesNoPants,
            NPCID.BlueArmoredBonesSword
        };

        /// <summary>
        /// Contains all enemies that spawn in Dungeon Areas with Brick Walls.
        /// </summary>
        public static int[] BrickWallsDungeonEnemies = new int[]
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
        public static int[] PumpkinMoonEnemies = new int[]
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
        public static int[] PumpkinMoveGetWaveEnemies(int Wave)
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

        public static int[] Zombies = [
            -65,
            -64,
            -63,
            -62,
            -61,
            -60,
            -59,
            -58,
            -57,
            -55,
            -54,
            -52,
            -48,
            -47,
            -46,
            -45,
            -44,
            -43,
            -42,
            -41,
            -40,
            -39,
            -38,
            -37,
            -36,
            -35,
            -34,
            -33,
            -32,
            -31];


        public static int[] Skeletons = [
            21,
            499,
            -46,
            -47,
            201,
            450,
            -48,
            -49,
            202,
            451,
            -50,
            -51,
            203,
            452,
            -52,
            -53,
            322,
            323,
            324,
            ..TiledWallsEnemies,
            ..SlabWallsDungeonEnemies,
            ..BrickWallsDungeonEnemies,
            NPCID.BoneLee,
            NPCID.SkeletonCommando,
            NPCID.TacticalSkeleton,
            NPCID.SporeSkeleton,
            481,
            NPCID.SkeletonArcher,
            NPCID.Tim,
            NPCID.UndeadMiner,
            NPCID.UndeadViking,
            NPCID.ArmoredViking,
            NPCID.ArmoredSkeleton,
            NPCID.SkeletonSniper,
            NPCID.RuneWizard
        ];
        public static int[] DemonEyes = new int[]
        {
            2, -43, 190, -38, 191, -39, 192, -40, 193, -41, 194, -42, 317, 318
        };

        public int[] TundraEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.IceSlime,
                    NPCID.ZombieEskimo,
                    NPCID.ArmedZombieEskimo,
                    NPCID.CorruptPenguin,
                    NPCID.CrimsonPenguin,
                    NPCID.IceBat,
                    NPCID.SpikedIceSlime,
                    NPCID.UndeadViking,
                    NPCID.SnowFlinx,
                    NPCID.CyanBeetle
                ];
            }
            if (Hardmode)
            {
                return [
                    NPCID.IceSlime,
                    NPCID.ZombieEskimo,
                    NPCID.ArmedZombieEskimo,
                    NPCID.CorruptPenguin,
                    NPCID.CrimsonPenguin,
                    NPCID.IceBat,
                    NPCID.SpikedIceSlime,
                    NPCID.UndeadViking,
                    NPCID.SnowFlinx,
                    NPCID.CyanBeetle,
                    NPCID.IceElemental,
                    NPCID.Wolf,
                    NPCID.IceGolem,
                    NPCID.ArmoredViking,
                    NPCID.IceTortoise,
                    NPCID.IcyMerman,
                    NPCID.IceMimic,
                    NPCID.PigronCorruption,
                    NPCID.PigronCrimson,
                    NPCID.PigronHallow
                ];
            }
            return [NPCID.IceSlime];
        }

        public int[] JungleEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.JungleSlime,
                    NPCID.JungleBat,
                    NPCID.Piranha,
                    NPCID.Snatcher,
                ];
            }
            if (Hardmode)
            {
                return[
                
                    NPCID.JungleSlime,
                    NPCID.JungleBat,
                    NPCID.Piranha,
                    NPCID.Snatcher,
                    NPCID.Derpling,
                    NPCID.GiantTortoise,
                    NPCID.GiantFlyingFox,
                    NPCID.AngryTrapper,
                    NPCID.Arapaima,
                    NPCID.MossHornet
                ];
            }
            return [-1];
        }


        public int[] DesertEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return[
                    NPCID.Vulture,
                    NPCID.Antlion,
                    NPCID.LarvaeAntlion,
                    NPCID.WalkingAntlion,
                    NPCID.GiantFlyingAntlion,
                    NPCID.TombCrawlerHead,
                    NPCID.SandShark,
                    NPCID.GiantWalkingAntlion
                ];
            }
            if (Hardmode)
            {
                return[

                    NPCID.Vulture,
                    NPCID.Antlion,
                    NPCID.LarvaeAntlion,
                    NPCID.WalkingAntlion,
                    NPCID.GiantFlyingAntlion,
                    NPCID.TombCrawlerHead,
                    NPCID.SandShark,
                    NPCID.GiantWalkingAntlion,
                    NPCID.Mummy,
                    NPCID.DarkMummy,
                    NPCID.BloodMummy,
                    NPCID.DesertLamiaDark,
                    NPCID.DesertLamiaLight,
                    NPCID.DesertGhoul,
                    NPCID.DesertGhoulCorruption,
                    NPCID.DesertGhoulCrimson,
                    NPCID.DesertGhoulHallow,
                    NPCID.DesertScorpionWalk,
                    NPCID.DesertScorpionWall,
                    NPCID.DesertBeast,
                    NPCID.DuneSplicerHead,
                    NPCID.SandElemental,
                    NPCID.DesertDjinn
                ];
            }
            return [-1];
        }


        public int[] ForestEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.SlimedZombie,
                    NPCID.BigSlimedZombie,
                    NPCID.BlueSlime,
                    NPCID.GreenSlime,
                    NPCID.PurpleSlime,
                    NPCID.YellowSlime,
                    NPCID.Zombie,
                    NPCID.ZombieRaincoat,
                    NPCID.DemonEye,
                    NPCID.Raven,
                    NPCID.GoblinScout,
                    ..Zombies,
                    ..DemonEyes,
                    NPCID.Pinky
                ];
            }
            if (Hardmode)
            {
                return[
                    NPCID.SlimedZombie,
                    NPCID.BigSlimedZombie,
                    NPCID.BlueSlime,
                    NPCID.GreenSlime,
                    NPCID.PurpleSlime,
                    NPCID.YellowSlime,
                    NPCID.Zombie,
                    NPCID.ZombieRaincoat,
                    NPCID.DemonEye,
                    NPCID.Raven,
                    NPCID.GoblinScout,
                    ..Zombies,
                    ..DemonEyes,
                    NPCID.PossessedArmor,
                    NPCID.WanderingEye,
                    NPCID.Wraith,
                    NPCID.Werewolf,
                    NPCID.HoppinJack,
                    NPCID.Pinky
                ];
            }
            return [-1];
        }

        public int[] CaveAndCavernEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.GiantWormHead,
                    NPCID.BlueSlime,
                    NPCID.RedSlime,
                    NPCID.YellowSlime,
                    NPCID.BlueJellyfish,
                    NPCID.Pinky
                ];
            }
            if (Hardmode)
            {
                return[
                    NPCID.GiantWormHead,
                    NPCID.DiggerHead,
                    NPCID.ToxicSludge,
                    NPCID.GreenJellyfish,
                    NPCID.BlueSlime,
                    NPCID.RedSlime,
                    NPCID.YellowSlime,
                    NPCID.BlueJellyfish,
                    NPCID.Pinky
                ];
            }
            return [-1];
        }

        public int[] HallowEnemies = new int[]
        {
            NPCID.Pixie,
            NPCID.Gastropod,
            NPCID.Unicorn,
            NPCID.RainbowSlime,
            NPCID.LightMummy,
            NPCID.IlluminantBat,
            NPCID.IlluminantSlime,
            NPCID.ChaosElemental,
            NPCID.EnchantedSword,
            NPCID.BigMimicHallow
        };

        public int[] CrimsonEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.BloodCrawler,
                    NPCID.BloodCrawlerWall,
                    NPCID.CrimsonGoldfish,
                    NPCID.FaceMonster,
                    NPCID.Crimera,
                    NPCID.BigCrimera
                ];
            }
            if (Hardmode)
            {
                return[
                    NPCID.BloodCrawler,
                    NPCID.BloodCrawlerWall,
                    NPCID.CrimsonGoldfish,
                    NPCID.FaceMonster,
                    NPCID.Crimera,
                    NPCID.BigCrimera,
                    NPCID.Crimslime,
                    NPCID.Herpling,
                    NPCID.BloodJelly,
                    NPCID.BloodFeeder,
                    NPCID.BloodMummy
                ];
            }
            return [-1];
        }

        public int[] CorruptEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.EaterofSouls,
                    NPCID.DevourerHead,
                    NPCID.CorruptGoldfish,
                ];
            }
            if (Hardmode)
            {
                return[
                    NPCID.EaterofSouls,
                    NPCID.DevourerHead,
                    NPCID.CorruptGoldfish,
                    NPCID.DiggerHead,
                    NPCID.Corruptor,
                    NPCID.CorruptSlime,
                    NPCID.Slimer,
                    NPCID.Slimer2,
                    NPCID.DarkMummy
                ];
            }
            return [-1];
        }

        public int[] HellEnemies(bool Hardmode, bool MechBoss)
        {
            if (!MechBoss && !Hardmode)
            {
                return [
                    NPCID.Hellbat,
                    NPCID.LavaSlime,
                    NPCID.FireImp,
                    NPCID.Demon,
                    NPCID.VoodooDemon,
                    NPCID.BoneSerpentHead
                ];
            }
            if (Hardmode)
            {
                return [
                    NPCID.Hellbat,
                    NPCID.LavaSlime,
                    NPCID.FireImp,
                    NPCID.Demon,
                    NPCID.VoodooDemon,
                    NPCID.BoneSerpentHead,
                    NPCID.DemonTaxCollector,
                    NPCID.Mimic
                ];
            }
            if (MechBoss)
            {
                return[
                    NPCID.Hellbat,
                    NPCID.LavaSlime,
                    NPCID.FireImp,
                    NPCID.Demon,
                    NPCID.VoodooDemon,
                    NPCID.BoneSerpentHead,
                    NPCID.DemonTaxCollector,
                    NPCID.Mimic,
                    NPCID.Lavabat,
                    NPCID.RedDevil
                ];
            }
            return [-1];
        }

        public int[] SkyEnemies(bool Hardmode)
        {
            if (!Hardmode)
            {
                return [
                    NPCID.Harpy,
                ];
            }
            if (Hardmode)
            {
                return[
                    NPCID.Harpy,
                    NPCID.WyvernHead
                ];
            }
            return [-1];
        }
    }
}