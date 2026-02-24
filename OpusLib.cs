using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using OpusLib.Content.Helpers;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InnoVault.PRT;
using Terraria.Audio;
using Terraria.ModLoader.Config;
using MonoMod.Cil;
using Mono.Cecil.Cil;
using Terraria.GameContent;

namespace OpusLib
{
	public class Opus : Mod
	{
		public override void Load()
		{
			IL_Player.ItemCheck_Inner += IL_Player_ItemCheck_Inner;
		}

		// Thank you to Ramona on the modding forum for helping with this!
		public static void IL_Player_ItemCheck_Inner(ILContext il)
		{
			ILCursor cur = new ILCursor(il);
			var invertLocal = il.Body.Variables.Count;
			il.Body.Variables.Add(new VariableDefinition(il.Import(typeof(bool))));
		
			// moves to after the "bool allowChannel = this.controlUseItem" statement
			cur.GotoNext(MoveType.After,
				// Matches to "this"
				i => i.MatchLdarg0(),
				// Matches to ".controlUseItem"
				i => i.MatchLdfld<Player>("controlUseItem"),
				// Matches to "allowChannel = "
				i => i.MatchStloc(6)
			);
		
			// Pushes "this"
			cur.EmitLdarg0();
			// Pushes "currentHeldItem" (an already existing local variable in
			//   Player.ItemCheck_Inner() that stores the palyer's held item)
			cur.EmitLdloc1();
			cur.EmitLdloc(invertLocal);
			// Pushes "ref allowChannel"
			cur.EmitLdloca(6);
			// Calls MyLoadingClass.ModifyAllowChannel() with the last 3 things pushed/on_the_stack as the arguments
			cur.EmitCall(typeof(Opus).GetMethod("ModifyAllowChannel"));
		}

		/*
		Something to note is that player.controlUseItem keeps track if the player is left clicking, and player.controlUseTile keep track of right clicking. 
		This means that whatever the MountID.Drill is, it's an example of channeling with right click. 
		Also note that, even without this modification, it is still to possible to channel with right click if the player begins holding left click at the perfect moment!
		- Ramona
		*/

		/// <summary>
        /// Allows the changing of whether an item channels in left or right clicking.
		/// <para/> By default, this will allow channeling on right click but not on left click, Invert does the opposite of that. 
		/// <para/>This only affects items in ItemChannel_RightChannel_LeftNot and ItemChannel_LeftChannel_RightNot.
        /// </summary>
        /// <param name="player"></param>
        /// <param name="heldItem"></param>
        /// <param name="Invert"></param>
        /// <param name="allowChannel"></param>
		public static void ModifyAllowChannel(Player player, Item ItemToModify, bool Invert, ref bool allowChannel)
		{
			if (!Invert)
			{
				if(ItemChannel_RightChannel_LeftNot.Contains(ItemToModify.type) && player.HeldItem == ItemToModify)
				{
					// Disable channeling for left click:
					if(player.altFunctionUse == 0)
						allowChannel = false;
				
					// Enable channeling for right click:
					if(player.altFunctionUse != 0)
						allowChannel = player.controlUseTile;
				}
			}
			if (Invert)
			{
				if(ItemChannel_LeftChannel_RightNot.Contains(ItemToModify.type) && player.HeldItem == ItemToModify)
				{
					// Disable channeling for left click:
					if(player.altFunctionUse == 0)
						allowChannel = player.controlUseItem;
				
					// Enable channeling for right click:
					if(player.altFunctionUse != 0)
						allowChannel = false;
				}
			}
		}

		public static List<int> ItemChannel_LeftChannel_RightNot = new List<int>()
		{
			-1
		};

		public static List<int> ItemChannel_RightChannel_LeftNot = new List<int>()
		{
			-1
		};

		public static bool HasJingled = false;
        public override void Unload()
        {
			HasJingled = false;
        }
		
		public static void BuffDust(int DustType, Player target, int ChancePerTick = 5, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
		{
			if (DustType == -1)
				DustType = DustID.TintableDustLighted;

			if (target.width <= 0 || target.height <= 0)
				return;

			if (Main.rand.NextBool(ChancePerTick))
			{
				Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustType, DustVelX, DustVelY, 100, DustColor, DustScale);
			}
		}
		public static void BuffDust(int DustType, NPC target, int ChancePerTick = 5, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
		{
			if (DustType == -1)
				DustType = DustID.TintableDustLighted;

			if (target.width <= 0 || target.height <= 0)
				return;

			if (Main.rand.NextBool(ChancePerTick))
			{
				Dust.NewDust(new Vector2(target.position.X, target.position.Y), target.width, target.height, DustType, DustVelX, DustVelY, 100, DustColor, DustScale);
			}
		}
		public static void BuffParticle(int PRTType, Player target, int ChancePerTick = 5, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
		{
			if (PRTType == -1)
				PRTType = DustID.TintableDustLighted;

			if (target.width <= 0 || target.height <= 0)
				return;

			if (Main.rand.NextBool(ChancePerTick))
			{
				PRTLoader.NewParticle(PRTType, target.Center, new Vector2(DustVelX, DustVelY), DustColor, DustScale);
			}
		}
		public static void BuffParticle(int PRTType, NPC target, int ChancePerTick = 5, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
		{
			if (PRTType == -1)
				PRTType = DustID.TintableDustLighted;

			if (target.width <= 0 || target.height <= 0)
				return;

			if (Main.rand.NextBool(ChancePerTick))
			{
				PRTLoader.NewParticle(PRTType, target.Center, new Vector2(DustVelX, DustVelY), DustColor, DustScale);
			}
		}

		


		#region Projectile Utils

		//Evenly Spaced, Starts at the Center. Moves out.
		public static void RadialSpreadProjectile(int ID, int Amount, Vector2 CTR, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, bool RandomOffset = false)
        {
            float rotationStep = MathHelper.TwoPi / Amount;
            float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + baseRotation;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

                Projectile.NewProjectile(
                    Projectile.GetSource_None(),
                    CTR,
                    velocity,
                    ID,
                    Dmg,
                    KB,
                    ai0: AI0,
                    ai1: AI1,
                    ai2: AI2
                );
            }
        }

		public static void RadialSpreadProjectile(int ID, int Amount, Vector2 CTR, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, float offset = 0f)
        {
            float rotationStep = MathHelper.TwoPi / Amount;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + offset;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

                Projectile.NewProjectile(
                    Projectile.GetSource_None(),
                    CTR,
                    velocity,
                    ID,
                    Dmg,
                    KB,
                    ai0: AI0,
                    ai1: AI1,
                    ai2: AI2
                );
            }
        }

		//Randomly Spaced, Starts at the Center. Moves out.
		public static void RadialProjectileRandomDir(int ID, int Amount, Vector2 CTR, int Dmg = 0, int KB = 0, float Speed = 2f, float AI0 = 0, float AI1 = 0, float AI2 = 0, bool friendly = false, bool hostile = false)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 velocity = new Vector2(Speed, 0f).RotatedByRandom(MathHelper.TwoPi);
				Projectile Pr = Projectile.NewProjectileDirect(
					Projectile.GetSource_None(),
					CTR,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
				Pr.friendly = friendly;
				Pr.hostile = hostile;
			}
		}
		
		//Evenly Spaced, but does not start at the center. Moves out.
		public static void RingProjectileOutward(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					position,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
			}
		}

		public static void RingProjectileOutward(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					position,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
			}
		}
		
		//Evenly Spaced, but does not start at the center. Moves in.
		public static void RingProjectileInward(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					position,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
			}
		}

		public static void RingProjectileInward(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					position,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
			}
		}

		//Randomly Spaced, but does not start at the center. Moves out.
		public static void RingProjectileOutwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2f, float AI0 = 0f, float AI1 = 0f, float AI2 = 0f)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 spawnPos = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);

				Vector2 direction = Vector2.Normalize(spawnPos - CTR);
				Vector2 velocity = direction * Speed;

				Projectile.NewProjectile(Entity.GetSource_None(), spawnPos, velocity, ID, Dmg, KB, -1, AI0, AI1, AI2);
			}
		}

		//Randomly Spaced, but does not start at the center. Moves in.
		public static void RingProjectileInwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, int Dmg = 0, int KB = 0, float Speed = 2, float AI0 = 0, float AI1 = 0, float AI2 = 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR - position) * Speed;

				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					position,
					velocity,
					ID,
					Dmg,
					KB,
					ai0: AI0,
					ai1: AI1,
					ai2: AI2
				);
			}
		}

		#endregion

		#region Dust Utils

		//Evenly Spaced, Starts at the Center. Moves out.
		public static void RadialSpreadDust(int ID, int Amount, Vector2 CTR,  int Alpha, Color CLR, float Scale = 1f, float Speed = 2, bool RandomOffset = false)
        {
            float rotationStep = MathHelper.TwoPi / Amount;
            float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + baseRotation;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Dust.NewDustPerfect(CTR, ID, velocity, Alpha, CLR, Scale);
            }
        }

		public static void RadialSpreadDust(int ID, int Amount, Vector2 CTR,  int Alpha, Color CLR, float Scale = 1f, float Speed = 2, float offset = 0f)
        {
            float rotationStep = MathHelper.TwoPi / Amount;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + offset;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Dust.NewDustPerfect(CTR, ID, velocity, Alpha, CLR, Scale);
            }
        }
		
		//Randomly Spaced, Starts at the Center. Moves out.
		public static void RadialDustRandomDir(int ID, int Amount, Vector2 CTR, int Alpha, Color CLR, float Scale = 1f, float Speed = 2f)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 velocity = new Vector2(Speed, 0f).RotatedByRandom(MathHelper.TwoPi);
				Dust.NewDustPerfect(CTR, ID, velocity, Alpha, CLR, Scale);
			}
		}
		
		//Evenly Spaced, but does not start at the center. Moves out.
		public static void RingDustOutward(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Scale = 1f, float Speed = 2, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		public static void RingDustOutward(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Scale = 1f, float Speed = 2, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		//Randomly Spaced, but does not start at the center. Moves out.
		public static void RingDustOutwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Speed = 2, float Scale = 1f)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR + position) * Speed;

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		//Evenly Spaced, but does not start at the center. Moves in.
		public static void RingDustInward(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Scale = 1f, float Speed = 2, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		public static void RingDustInward(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Scale = 1f, float Speed = 2, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		//Randomly Spaced, but does not start at the center. Moves in.
		public static void RingDustInwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, int Alpha, Color CLR, float Speed = 2, float Scale = 1f)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR - position) * Speed;

				Dust.NewDustPerfect(position, ID, velocity, Alpha, CLR, Scale);
			}
		}

		public static void DustsWhileItemIsInWorld(Rectangle itemRect, int DustType = -1, int ChancePerTick = 3, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
        {
            if (DustType == -1)
                DustType = DustID.TintableDustLighted;


            if (itemRect.Width <= 0 || itemRect.Height <= 0)
                return;

            if (Main.rand.NextBool(ChancePerTick))
            {
                Dust.NewDust(new Vector2(itemRect.Width / 2, itemRect.Height / 2), itemRect.Width, itemRect.Height, DustType, 0f, 0f, 100, DustColor, DustScale);
            }
        }
		#endregion

		#region Particle Utils

		//All particle functions, if applicable, will have float ai[] slots.

		//Calls PRTLoader.NewParticle (well, one of its overloads), but with floats passed into the ai[] slots, instead of integers.
		public static BasePRT NewParticleFloatAI(int ID, Vector2 position, Vector2 velocity, Color color = default, float scale = 1f, float ai0 = 0, float ai1 = 0, float ai2 = 0)
        {
            BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
        	basePRT.Position = position;
            basePRT.Velocity = velocity;
            basePRT.Scale = scale;
            basePRT.Color = color;
            basePRT.ai[0] = ai0;
            basePRT.ai[1] = ai1;
            basePRT.ai[2] = ai2;
            PRTLoader.AddParticle(basePRT);
            return basePRT;
        }

		//Evenly Spaced, Starts at the Center. Moves out.
		public static void RadialSpreadParticle(int ID, int Amount, Vector2 CTR,  float Alpha = 1, Color CLR= default, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, bool RandomOffset = false)
        {
            float rotationStep = MathHelper.TwoPi / Amount;
            float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + baseRotation;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = CTR;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
            }
        }

		public static void RadialSpreadParticle(int ID, int Amount, Vector2 CTR,  float Alpha = 1, Color CLR= default, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, float offset = 0f)
        {
            float rotationStep = MathHelper.TwoPi / Amount;

            for (int i = 0; i < Amount; i++)
            {
                float angle = rotationStep * i + offset;
                Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = CTR;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
            }
        }
		
		//Randomly Spaced, Starts at the Center. Moves out.
		public static void RadialParticleRandomDir(int ID, int Amount, Vector2 CTR, float Alpha = 1, Color CLR = default, float Scale = 1f, float Speed = 2f, float ai0 = 0, float ai1 = 0, float ai2 = 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 velocity = new Vector2(Speed, 0f).RotatedByRandom(MathHelper.TwoPi);

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = CTR;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}
		
		//Evenly Spaced, but does not start at the center. Moves out.
		public static void RingParticleOutward(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}

		public static void RingParticleOutward(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(angle);

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}
		
		//Randomly Spaced, but does not start at the center. Moves out.
		public static void RingParticleOutwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Speed = 2, float Scale = 1f, float ai0 = 0, float ai1 = 0, float ai2 = 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR + position) * Speed;

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}
		
		//Evenly Spaced, but does not start at the center. Moves in.
		public static void RingParticleInward(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, bool RandomOffset = false)
		{
			float rotationStep = MathHelper.TwoPi / Amount;
			float baseRotation = RandomOffset ? Main.rand.NextFloat(MathHelper.TwoPi) : 0f;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + baseRotation;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}

		public static void RingParticleInward(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Scale = 1f, float Speed = 2, float ai0 = 0, float ai1 = 0, float ai2 = 0, float offset = 0f)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				float angle = rotationStep * i + offset;
				Vector2 position = CTR + new Vector2(Radius, 0f).RotatedBy(angle);

				// Direction from ring point toward center
				Vector2 direction = (CTR - position).SafeNormalize(Vector2.Zero);

				Vector2 velocity = direction * Speed;

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}

		//Randomly Spaced, but does not start at the center. Moves in.
		public static void RingParticleInwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius, float Alpha, Color CLR, float Speed = 2, float Scale = 1f, float ai0 = 0, float ai1 = 0, float ai2 = 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR - position) * Speed;

				BasePRT basePRT = PRTLoader.PRT_IDToInstances[ID].Clone();
				basePRT.Position = position;
				basePRT.Velocity = velocity;
				basePRT.Scale = Scale;
				basePRT.Color = CLR * Alpha;
				basePRT.ai[0] = ai0;
				basePRT.ai[1] = ai1;
				basePRT.ai[2] = ai2;
				PRTLoader.AddParticle(basePRT);
			}
		}

		#endregion

		#region Temp

		
		#endregion
		/// <summary>
        /// Returns an array of Vectors that all are <i> radius </i> away from <i> center </i>, equidistantly spaced.
		/// <para/> The array is <i> numVectors </i> in length.
        /// </summary>
        /// <param name="numVectors"></param>
        /// <param name="center"></param>
        /// <param name="rotationSpeed"></param>
        /// <param name="radius"></param>
        /// <returns></returns>
		public static Vector2[] GetEquidistantVectors(int numVectors, Vector2 center, float radius)
		{
			Vector2[] vectors = new Vector2[numVectors];
			float angleStep = MathHelper.TwoPi / numVectors;

			for (int i = 0; i < numVectors; i++)
			{
				float angle = angleStep * i;
				vectors[i] = center + new Vector2(radius, 0f).RotatedBy(angle);
			}

			return vectors;
		}

		/// <summary>
		/// Returns an array of Vectors that all are <i> radius </i> away from <i> center </i>, equidistantly spaced, rotating at <i> rotationSpeed </i>.
		/// <para/> The array is <i> numVectors </i> in length.
		/// </summary>
		/// <param name="numVectors"></param>
		/// <param name="center"></param>
		/// <param name="rotationSpeed"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static Vector2[] GetEquidistantOrbitVectors(int numVectors, Vector2 center, float rotationSpeed, float radius)
		{
			Vector2[] vectors = new Vector2[numVectors];
			float angleStep = MathHelper.TwoPi / numVectors;
			float rotationOffset = Main.GameUpdateCount * rotationSpeed;

			for (int i = 0; i < numVectors; i++)
			{
				float angle = rotationOffset + angleStep * i;
				vectors[i] = center + new Vector2(radius, 0f).RotatedBy(angle);
			}

			return vectors;
		}

		/// <summary>
		/// Returns an array of tuples that all are <i> radius </i> away from <i> center </i>, equidistantly spaced, rotating at <i> rotationSpeed </i>.
		/// <para/> The array is <i> numVectors </i> in length.
		/// <para/> The rotation is in the direction of travel.
		/// </summary>
		/// <param name="numEntities"></param>
		/// <param name="center"></param>
		/// <param name="rotationSpeed"></param>
		/// <param name="radius"></param>
		/// <returns></returns>
		public static (Vector2 Position, float Rotation)[] GetEquidistantOrbitVectorsAndRots(int numEntities, Vector2 center, float rotationSpeed, float radius)
		{
			var results = new (Vector2, float)[numEntities];
			float angleStep = MathHelper.TwoPi / numEntities;
			float rotationOffset = Main.GameUpdateCount * rotationSpeed;

			for (int i = 0; i < numEntities; i++)
			{
				float angle = rotationOffset + angleStep * i;
				Vector2 position = center + new Vector2(radius, 0f).RotatedBy(angle);
				results[i] = (position, angle);
			}

			return results;
		}

		public static void SetEquidistantProjectilesWithRotation(Projectile[] projectiles, Vector2 center, float rotationSpeed, float radius, bool alignToTangent, out Vector2[] centers, out float[] rotations)
		{
			int n = projectiles.Length;
			centers = new Vector2[n];
			rotations = new float[n];

			if (n == 0)
				return;

			float angleStep = MathHelper.TwoPi / n;
			float baseRotation = Main.GameUpdateCount * rotationSpeed;

			for (int i = 0; i < n; i++)
			{
				float angle = baseRotation + angleStep * i;
				rotations[i] = angle;

				// compute the center position for this index
				Vector2 c = center + new Vector2(radius, 0f).RotatedBy(angle);
				centers[i] = c;

				// assign to entity if present
				if (projectiles[i] != null)
				{
					// position is top-left, so subtract half-size to place center correctly
					projectiles[i].position = c - new Vector2(projectiles[i].width / 2f, projectiles[i].height / 2f);

					// choose how to orient the entity:
					// - radial: rotation == angle (points away from circle center along radial)
					// - tangent: rotation == angle + 90deg (points along orbit direction)
					projectiles[i].rotation = alignToTangent ? angle + MathHelper.PiOver2 : angle;
				}
			}
		}

		public static void SetEquidistantNPCsWithRotation(NPC[] NPCs, Vector2 center, float rotationSpeed, float radius, bool alignToTangent, out Vector2[] centers, out float[] rotations)
		{
			int n = NPCs.Length;
			centers = new Vector2[n];
			rotations = new float[n];

			if (n == 0)
				return;

			float angleStep = MathHelper.TwoPi / n;
			float baseRotation = Main.GameUpdateCount * rotationSpeed;

			for (int i = 0; i < n; i++)
			{
				float angle = baseRotation + angleStep * i;
				rotations[i] = angle;

				// compute the center position for this index
				Vector2 c = center + new Vector2(radius, 0f).RotatedBy(angle);
				centers[i] = c;

				// assign to entity if present
				if (NPCs[i] != null)
				{
					// position is top-left, so subtract half-size to place center correctly
					NPCs[i].position = c - new Vector2(NPCs[i].width / 2f, NPCs[i].height / 2f);

					// choose how to orient the entity:
					// - radial: rotation == angle (points away from circle center along radial)
					// - tangent: rotation == angle + 90deg (points along orbit direction)
					NPCs[i].rotation = alignToTangent ? angle + MathHelper.PiOver2 : angle;
				}
			}
		}
		
		public static void SetEquidistantPlayersWithRotation(Player[] players, Vector2 center, float rotationSpeed, float radius, bool alignToTangent, out Vector2[] centers, out float[] rotations)
		{
			int n = players.Length;
			centers = new Vector2[n];
			rotations = new float[n];

			if (n == 0)
				return;

			float angleStep = MathHelper.TwoPi / n;
			float baseRotation = Main.GameUpdateCount * rotationSpeed;

			for (int i = 0; i < n; i++)
			{
				float angle = baseRotation + angleStep * i;
				rotations[i] = angle;

				// compute the center position for this index
				Vector2 c = center + new Vector2(radius, 0f).RotatedBy(angle);
				centers[i] = c;

				// assign to entity if present
				if (players[i] != null)
				{
					// position is top-left, so subtract half-size to place center correctly
					players[i].position = c - new Vector2(players[i].width / 2f, players[i].height / 2f);

					// choose how to orient the entity:
					// - radial: rotation == angle (points away from circle center along radial)
					// - tangent: rotation == angle + 90deg (points along orbit direction)
					players[i].bodyRotation = alignToTangent ? angle + MathHelper.PiOver2 : angle;
				}
			}
		}

		public static Asset<Texture2D> PointGlow = ModContent.Request<Texture2D>("OpusLib/Assets/Textures/PointGlow");


        /// <summary>
        /// Easy-to-call method for drawing a point glow over the center of a projectile.
        /// </summary>
        /// <param name="projectile"></param>
        /// <param name="color"></param>
        /// <param name="RotateWithProj"></param>
        /// <param name="Rot"></param>
        public static void DrawGlowOnProj(Projectile projectile, Color color, bool RotateWithProj, float Rot = 0)
        {
            if (RotateWithProj)
            {
                Rot = projectile.rotation;
            }

            Main.EntitySpriteDraw(
                PointGlow.Value,
                projectile.Center - Main.screenPosition,
                null,
                color,
                Rot,
                PointGlow.Value.Size() / 2,
                projectile.scale,
                SpriteEffects.None,
                0
            );
        }

        /// <summary>
        /// Easy-to-call method for drawing any texture over the center of a projectile.
        /// </summary>
        /// <param name="Tex"></param>
        /// <param name="projectile"></param>
        /// <param name="color"></param>
        /// <param name="RotateWithProj"></param>
        /// <param name="Rot"></param>
        public static void DrawTextureOnProj(Asset<Texture2D> Tex, Projectile projectile, Color color, bool RotateWithProj, float Rot = 0, float ScaleX = 1f, float ScaleY = 1f)
        {
            if (RotateWithProj)
            {
                Rot = projectile.rotation;
            }

            Main.EntitySpriteDraw(
                Tex.Value,
                projectile.Center - Main.screenPosition,
                null,
                color,
                Rot,
                Tex.Value.Size() / 2,
                new Vector2(ScaleX, ScaleY),
                SpriteEffects.None,
                0
            );
        }

        public static bool BossNearby()
        {
            foreach (NPC boss in Main.npc)
            {
                if (boss.active && boss.boss)
                {
                    return true;
                }
            }
            return false;
        }

        

        public static void StartSpriteBatchWithBlending(SpriteBatch spriteBatch, BlendState blendState, SpriteSortMode ssm)
        {
            spriteBatch.End();
            spriteBatch.Begin(ssm, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }

		public static void StartSpriteBatchForTrails(SpriteBatch spriteBatch, BlendState blendState, SpriteSortMode ssm)
        {
			spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Immediate, BlendState.Additive, SamplerState.PointWrap, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
		}

        public static void ReturnToDefaultDrawing(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }

		public static int GetCrateByChestID(ChestID chest, bool Hard)
        {
			if (chest.TileType == ChestID.Wooden.TileType && chest.Style == ChestID.Wooden.Style)
			{
				return !Hard ? ItemID.WoodenCrate : ItemID.WoodenCrateHard;
			}
			if (chest.TileType == ChestID.Gold.TileType && chest.Style == ChestID.Gold.Style)
			{
				return !Hard ? ItemID.DungeonFishingCrate : ItemID.DungeonFishingCrateHard;
			}
			if (chest.TileType == ChestID.Ivy.TileType && chest.Style == ChestID.Ivy.Style)
			{
				return !Hard ? ItemID.JungleFishingCrate: ItemID.JungleFishingCrateHard;
			}
			if (chest.TileType == ChestID.SkyChest.TileType && chest.Style == ChestID.SkyChest.Style)
			{
				return !Hard ? ItemID.FloatingIslandFishingCrate : ItemID.FloatingIslandFishingCrateHard;
			}
			if (chest.TileType == ChestID.Frozen.TileType && chest.Style == ChestID.Frozen.Style)
			{
				return !Hard ? ItemID.FrozenCrate : ItemID.FrozenCrateHard;
			}
			if (chest.TileType == ChestID.Sandstone.TileType && chest.Style == ChestID.Sandstone.Style)
			{
				return !Hard ? ItemID.OasisCrate : ItemID.OasisCrateHard;
			}
			if ((chest.TileType == ChestID.ShadowLocked.TileType && chest.Style == ChestID.ShadowLocked.Style) || (chest.TileType == ChestID.Shadow.TileType && chest.Style == ChestID.Shadow.Style))
			{
				return !Hard ? ItemID.LavaCrate : ItemID.LavaCrateHard;
			}
			if (chest.TileType == ChestID.Water.TileType && chest.Style == ChestID.Water.Style)
			{
				return !Hard ? ItemID.OceanCrate : ItemID.OceanCrateHard;
			}
			if (chest.TileType == ChestID.Sandstone.TileType && chest.Style == ChestID.Sandstone.Style)
			{
				return !Hard ? ItemID.OasisCrate : ItemID.OasisCrateHard;
			}
			return -1;
        }

		public static List<WeightedLootEntry> CommonPotion = new List<WeightedLootEntry>()
		{
			new WeightedLootEntry(ItemID.LesserHealingPotion, 2, 17, 0.75f),
			new WeightedLootEntry(ItemID.HealingPotion, 2, 8, 0.5f),
			new WeightedLootEntry(ItemID.LesserManaPotion, 2, 17, 0.75f),
			new WeightedLootEntry(ItemID.ManaPotion, 2, 8, 0.5f),
			new WeightedLootEntry(ItemID.RecallPotion, 2, 8, 0.3f),
			new WeightedLootEntry(GetRandomCombatPotion(), 1, 6, 0.5f),
			new WeightedLootEntry(GetRandomUtilityPotion(), 1, 6, 0.5f),
		};

		public static int GetRandomCombatPotion()
        {
            List<int> options = new List<int> { ItemID.TitanPotion, 
			ItemID.ThornsPotion, ItemID.TrapsightPotion, ItemID.HunterPotion, 
			ItemID.BattlePotion, ItemID.MagicPowerPotion, ItemID.ArcheryPotion,
			ItemID.AmmoReservationPotion, ItemID.IronskinPotion, ItemID.EndurancePotion,
			ItemID.SummoningPotion, ItemID.RestorationPotion};
			return options[Main.rand.Next(options.Count)];
        }

		public static int GetRandomUtilityPotion()
        {
			List<int> options = new List<int> { ItemID.SwiftnessPotion, 
			ItemID.PotionOfReturn, ItemID.RedPotion, ItemID.LovePotion, 
			ItemID.LuckPotionLesser, ItemID.CratePotion, ItemID.GillsPotion, 
			ItemID.ShinePotion, ItemID.SonarPotion, ItemID.StinkPotion, 
			ItemID.MiningPotion, ItemID.WarmthPotion, ItemID.BuilderPotion, 
			ItemID.CalmingPotion, ItemID.FishingPotion, ItemID.FlipperPotion, 
			ItemID.NightOwlPotion, ItemID.WormholePotion, ItemID.LifeforcePotion,
			ItemID.BiomeSightPotion, ItemID.FeatherfallPotion, ItemID.GenderChangePotion,
			ItemID.InvisibilityPotion, ItemID.ObsidianSkinPotion, ItemID.WaterWalkingPotion,
			ItemID.TeleportationPotion, ItemID.SpelunkerPotion };
            return options[Main.rand.Next(options.Count)];
        }

		public static float Sine(float Value1, float Value2, float Speed = 0.05f)
		{
			return MathHelper.Lerp(Value1, Value2, (float)Math.Sin(Main.GameUpdateCount * Speed) * 0.5f + 0.5f);
		}

		public static double Sine(double Value1, double Value2, float Speed = 0.05f)
		{
			return Double.Lerp(Value1, Value2, Math.Sin(Main.GameUpdateCount * Speed) * 0.5f + 0.5f);
		}

		public static int Sine(int Value1, int Value2, float Speed = 0.05f)
		{
			return (int)MathHelper.Lerp(Value1, Value2, (float)Math.Sin(Main.GameUpdateCount * Speed) * 0.5f + 0.5f);
		}

		public static Vector2 Sine(Vector2 Value1, Vector2 Value2, float Speed = 0.05f)
		{
			return new Vector2(Sine(Value1.X, Value2.X, Speed), Sine(Value1.Y, Value2.Y, Speed));
		}

		public static Vector3 Sine(Vector3 Value1, Vector3 Value2, float Speed = 0.05f)
		{
			return new Vector3(Sine(Value1.X, Value2.X, Speed), Sine(Value1.Y, Value2.Y, Speed), Sine(Value1.Z, Value2.Z, Speed));
		}

		public static Color Sine(Color color1, Color color2, float Speed = 0.05f)
		{
			return new Color(Sine(color1.R, color2.R, Speed), Sine(color1.G, color2.G, Speed), Sine(color1.B, color2.B, Speed));
		}

		public static void DrawProjectileShadow(Projectile proj, Vector2 offset, Color color, float rotationOffset = 0f)
		{
			Texture2D tex = TextureAssets.Projectile[proj.type].Value;

			Main.EntitySpriteDraw(
				tex,
				proj.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * 0.5f,
				proj.rotation,
				tex.Size() / 2f,
				proj.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawProjectileShadow(Projectile proj, Vector2 offset, Color color, float rotationOffset = 0f, float Opacity = 0.5f)
		{
			Texture2D tex = TextureAssets.Projectile[proj.type].Value;

			Main.EntitySpriteDraw(
				tex,
				proj.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * Opacity,
				proj.rotation,
				tex.Size() / 2f,
				proj.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawProjectileShadow(Projectile proj, Rectangle frame, Vector2 offset, Color color, float rotationOffset = 0f)
		{
			Texture2D tex = TextureAssets.Projectile[proj.type].Value;

			Main.EntitySpriteDraw(
				tex,
				proj.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				frame,
				color * 0.5f,
				proj.rotation,
				tex.Size() / 2f,
				proj.scale,
				SpriteEffects.None,
				0
			);
		}

		

		public static void DrawProjectileShadow(Projectile proj, Rectangle frame, Vector2 offset, Color color, float rotationOffset = 0f, float Opacity = 0.5f)
		{
			Texture2D tex = TextureAssets.Projectile[proj.type].Value;

			Main.EntitySpriteDraw(
				tex,
				proj.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				frame,
				color * Opacity,
				proj.rotation,
				tex.Size() / 2f,
				proj.scale,
				SpriteEffects.None,
				0
			);
		}


		public static void DrawProjectileShadowsStatic(Projectile proj, float dist)
		{
			DrawProjectileShadowsStatic(proj, dist, Color.White);
		}

		public static void DrawProjectileShadowsStatic(Projectile proj, float dist, Color drawColor)
		{
			DrawProjectileShadow(proj, new Vector2(0,  dist), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, new Vector2(0, -dist), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, new Vector2( dist, 0), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, new Vector2(-dist, 0), drawColor, rotationOffset: 0);
		}

		public static void DrawProjectileShadowsStatic(Projectile proj, float dist, Color drawColor, float Opacity = 0.5f)
		{
			DrawProjectileShadow(proj, new Vector2(0,  dist), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, new Vector2(0, -dist), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, new Vector2( dist, 0), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, new Vector2(-dist, 0), drawColor, Opacity: Opacity);
		}

		public static void DrawProjectileShadowsStatic(Projectile proj, Rectangle frame, float dist, Color drawColor)
		{
			DrawProjectileShadow(proj, frame, new Vector2(0,  dist), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, frame, new Vector2(0, -dist), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, frame, new Vector2( dist, 0), drawColor, rotationOffset: 0);
			DrawProjectileShadow(proj, frame, new Vector2(-dist, 0), drawColor, rotationOffset: 0);
		}

		public static void DrawProjectileShadowsStatic(Projectile proj, Rectangle frame, float dist, Color drawColor, float Opacity = 0.5f)
		{
			DrawProjectileShadow(proj, frame, new Vector2(0,  dist), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, frame, new Vector2(0, -dist), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, frame, new Vector2( dist, 0), drawColor, Opacity: Opacity);
			DrawProjectileShadow(proj, frame, new Vector2(-dist, 0), drawColor, Opacity: Opacity);
		}

		public static void DrawProjectileShadowsRotating(Projectile proj, float dist, Color drawColor, float speed = 0.2f)
		{
			float rot = speed * proj.direction;

			DrawProjectileShadow(proj, new Vector2(0,  dist), drawColor, rot);
			DrawProjectileShadow(proj, new Vector2(0, -dist), drawColor, rot);
			DrawProjectileShadow(proj, new Vector2( dist, 0), drawColor, rot);
			DrawProjectileShadow(proj, new Vector2(-dist, 0), drawColor, rot);
		}

		public static void DrawProjectileShadowsRotating(Projectile proj, float dist, Color drawColor, float speed = 0.2f, float Opacity = 0.5f)
		{
			float rot = speed * proj.direction;

			DrawProjectileShadow(proj, new Vector2(0,  dist), drawColor, rot, Opacity);
			DrawProjectileShadow(proj, new Vector2(0, -dist), drawColor, rot, Opacity);
			DrawProjectileShadow(proj, new Vector2( dist, 0), drawColor, rot, Opacity);
			DrawProjectileShadow(proj, new Vector2(-dist, 0), drawColor, rot, Opacity);
		}

		public static void DrawItemShadow(Item item, Vector2 offset, Color color, float rotationOffset = 0f)
		{
			Texture2D tex = TextureAssets.Item[item.type].Value;

			Main.EntitySpriteDraw(
				tex,
				item.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * 0.5f,
				0f,
				tex.Size() / 2f,
				item.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawItemShadow(Item item, Vector2 offset, Color color, float rotationOffset = 0f, float Opacity = 0.5f)
		{
			Texture2D tex = TextureAssets.Item[item.type].Value;

			Main.EntitySpriteDraw(
				tex,
				item.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * 0.5f,
				0f,
				tex.Size() / 2f,
				item.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawItemShadowsStatic(Item item, float dist)
		{
			DrawItemShadowsStatic(item, dist, Color.White);
		}

		public static void DrawItemShadowsStatic(Item item, float dist, Color drawColor)
		{
			DrawItemShadow(item, new Vector2(0,  dist), drawColor, rotationOffset: 0);
			DrawItemShadow(item, new Vector2(0, -dist), drawColor, rotationOffset: 0);
			DrawItemShadow(item, new Vector2( dist, 0), drawColor, rotationOffset: 0);
			DrawItemShadow(item, new Vector2(-dist, 0), drawColor, rotationOffset: 0);
		}

		public static void DrawItemShadowsStatic(Item item, float dist, Color drawColor, float Opacity = 0.5f)
		{
			DrawItemShadow(item, new Vector2(0,  dist), drawColor, Opacity: Opacity);
			DrawItemShadow(item, new Vector2(0, -dist), drawColor, Opacity: Opacity);
			DrawItemShadow(item, new Vector2( dist, 0), drawColor, Opacity: Opacity);
			DrawItemShadow(item, new Vector2(-dist, 0), drawColor, Opacity: Opacity);
		}

		public static void DrawItemShadowsRotating(Item item, float dist, Color drawColor, float speed = 0.2f)
		{
			float rot = speed * item.direction;

			DrawItemShadow(item, new Vector2(0,  dist), drawColor, rot);
			DrawItemShadow(item, new Vector2(0, -dist), drawColor, rot);
			DrawItemShadow(item, new Vector2( dist, 0), drawColor, rot);
			DrawItemShadow(item, new Vector2(-dist, 0), drawColor, rot);
		}

		public static void DrawItemShadowsRotating(Item item, float dist, Color drawColor, float speed = 0.2f, float Opacity = 0.5f)
		{
			float rot = speed * item.direction;

			DrawItemShadow(item, new Vector2(0,  dist), drawColor, rot, Opacity);
			DrawItemShadow(item, new Vector2(0, -dist), drawColor, rot, Opacity);
			DrawItemShadow(item, new Vector2( dist, 0), drawColor, rot, Opacity);
			DrawItemShadow(item, new Vector2(-dist, 0), drawColor, rot, Opacity);
		}

		public static void DrawNPCShadow(NPC npc, Vector2 offset, Color color, float rotationOffset = 0f)
		{
			Texture2D tex = TextureAssets.Npc[npc.type].Value;

			

			Main.EntitySpriteDraw(
				tex,
				npc.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * 0.5f,
				npc.rotation,
				tex.Size() / 2f,
				npc.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawNPCShadow(NPC npc, Vector2 offset, Color color, float rotationOffset = 0f, float Opacity = 0.5f)
		{
			Texture2D tex = TextureAssets.Npc[npc.type].Value;

			Main.EntitySpriteDraw(
				tex,
				npc.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				null,
				color * Opacity,
				npc.rotation,
				tex.Size() / 2f,
				npc.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawNPCShadow(NPC npc, Rectangle frame, Vector2 offset, Color color, float rotationOffset = 0f)
		{
			Texture2D tex = TextureAssets.Npc[npc.type].Value;

			Rectangle? source = frame;
			Vector2 origin = source.HasValue 
				? source.Value.Size() / 2f 
				: tex.Size() / 2f;

			Main.EntitySpriteDraw(
				tex,
				npc.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				frame,
				color * 0.5f,
				npc.rotation,
				origin,
				npc.scale,
				SpriteEffects.None,
				0
			);
		}

		public static void DrawNPCShadow(NPC npc, Rectangle frame, Vector2 offset, Color color, float rotationOffset = 0f, float Opacity = 0.5f)
		{
			Texture2D tex = TextureAssets.Npc[npc.type].Value;

			Rectangle? source = frame;
			Vector2 origin = source.HasValue 
				? source.Value.Size() / 2f 
				: tex.Size() / 2f;

			Main.EntitySpriteDraw(
				tex,
				npc.Center + offset.RotatedBy(rotationOffset) - Main.screenPosition,
				frame,
				color * Opacity,
				npc.rotation,
				origin,
				npc.scale,
				SpriteEffects.None,
				0
			);
		}
		
		public static void DrawNPCShadowsStatic(NPC npc, float dist)
		{
			DrawNPCShadowsStatic(npc, dist, Color.White);
		}

		public static void DrawNPCShadowsStatic(NPC npc, Rectangle frame, float dist)
		{
			DrawNPCShadowsStatic(npc, frame, dist, Color.White);
		}

		public static void DrawNPCShadowsStatic(NPC npc, float dist, Color drawColor)
		{
			DrawNPCShadow(npc, new Vector2(0,  dist), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, new Vector2(0, -dist), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, new Vector2( dist, 0), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, new Vector2(-dist, 0), drawColor, rotationOffset: 0);
		}

		public static void DrawNPCShadowsStatic(NPC npc, float dist, Color drawColor, float Opacity = 0.5f)
		{
			DrawNPCShadow(npc, new Vector2(0,  dist), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, new Vector2(0, -dist), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, new Vector2( dist, 0), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, new Vector2(-dist, 0), drawColor, Opacity: Opacity);
		}

		public static void DrawNPCShadowsStatic(NPC npc, Rectangle frame, float dist, Color drawColor)
		{
			DrawNPCShadow(npc, frame, new Vector2(0,  dist), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, frame, new Vector2(0, -dist), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, frame, new Vector2( dist, 0), drawColor, rotationOffset: 0);
			DrawNPCShadow(npc, frame, new Vector2(-dist, 0), drawColor, rotationOffset: 0);
		}

		public static void DrawNPCShadowsStatic(NPC npc, Rectangle frame, float dist, Color drawColor, float Opacity = 0.5f)
		{
			DrawNPCShadow(npc, frame, new Vector2(0,  dist), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, frame, new Vector2(0, -dist), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, frame, new Vector2( dist, 0), drawColor, Opacity: Opacity);
			DrawNPCShadow(npc, frame, new Vector2(-dist, 0), drawColor, Opacity: Opacity);
		}

		public static void DrawNPCShadowsRotating(NPC npc, float dist, Color drawColor, float speed = 0.2f)
		{
			float rot = speed * npc.direction;

			DrawNPCShadow(npc, new Vector2(0,  dist), drawColor, rot);
			DrawNPCShadow(npc, new Vector2(0, -dist), drawColor, rot);
			DrawNPCShadow(npc, new Vector2( dist, 0), drawColor, rot);
			DrawNPCShadow(npc, new Vector2(-dist, 0), drawColor, rot);
		}

		public static void DrawNPCShadowsRotating(NPC npc, float dist, Color drawColor, float speed = 0.2f, float Opacity = 0.5f)
		{
			float rot = speed * npc.direction;

			DrawNPCShadow(npc, new Vector2(0,  dist), drawColor, rot, Opacity);
			DrawNPCShadow(npc, new Vector2(0, -dist), drawColor, rot, Opacity);
			DrawNPCShadow(npc, new Vector2( dist, 0), drawColor, rot, Opacity);
			DrawNPCShadow(npc, new Vector2(-dist, 0), drawColor, rot, Opacity);
		}

		public static void DrawNPCShadowsRotating(NPC npc, Rectangle frame, float dist, Color drawColor, float speed = 0.2f)
		{
			float rot = speed * npc.direction;

			DrawNPCShadow(npc, frame, new Vector2(0,  dist), drawColor, rot);
			DrawNPCShadow(npc, frame, new Vector2(0, -dist), drawColor, rot);
			DrawNPCShadow(npc, frame, new Vector2( dist, 0), drawColor, rot);
			DrawNPCShadow(npc, frame, new Vector2(-dist, 0), drawColor, rot);
		}

		public static void DrawNPCShadowsRotating(NPC npc, Rectangle frame, float dist, Color drawColor, float speed = 0.2f, float Opacity = 0f)
		{
			float rot = speed * npc.direction;

			DrawNPCShadow(npc, frame, new Vector2(0,  dist), drawColor, rot, Opacity);
			DrawNPCShadow(npc, frame, new Vector2(0, -dist), drawColor, rot, Opacity);
			DrawNPCShadow(npc, frame, new Vector2( dist, 0), drawColor, rot, Opacity);
			DrawNPCShadow(npc, frame, new Vector2(-dist, 0), drawColor, rot, Opacity);
		}

		public static void RectDustRandom(int ID, Rectangle Rect, Color color, float scale, int amount = 10)
		{
			for (int i = 0; i < amount; i++)
			{
				int edge = Main.rand.Next(4);
				Vector2 pos;

				switch (edge)
				{
					case 0: // top
						pos = new Vector2(
							Main.rand.NextFloat(Rect.Left, Rect.Right),
							Rect.Top
						);
						break;

					case 1: // right
						pos = new Vector2(
							Rect.Right,
							Main.rand.NextFloat(Rect.Top, Rect.Bottom)
						);
						break;

					case 2: // bottom
						pos = new Vector2(
							Main.rand.NextFloat(Rect.Left, Rect.Right),
							Rect.Bottom
						);
						break;

					default: // left
						pos = new Vector2(
							Rect.Left,
							Main.rand.NextFloat(Rect.Top, Rect.Bottom)
						);
						break;
				}

				Dust d = Dust.NewDustPerfect(pos, ID, Vector2.Zero);
				d.color = color;
				d.scale = scale;
				d.noGravity = true;
			}
		}


		public static void RectDustLooping(int ID, Rectangle Rect, Color color, float scale, int amount = 10, int direction = 1, float Speed = 2)
		{

			Vector2[] corners =
			{
				new(Rect.Left,  Rect.Top),
				new(Rect.Right, Rect.Top),
				new(Rect.Right, Rect.Bottom),
				new(Rect.Left,  Rect.Bottom)
			};

			if (direction < 0)
				Array.Reverse(corners);

			for (int e = 0; e < 4; e++)
			{
				Vector2 start = corners[e];
				Vector2 end   = corners[(e + 1) % 4];

				Vector2 dir = end - start;
				float length = dir.Length();
				dir.Normalize();

				float spacing = length / amount;

				for (float i = 0; i < length; i += spacing)
				{
					Vector2 pos = start + dir * (i + (Speed * direction));

					Dust d = Dust.NewDustPerfect(pos, ID, Vector2.Zero);
					d.color = color;
					d.scale = scale;
					d.noGravity = true;
				}
			}
		}

	}

	public static class OpusExtensions
	{
		/// <summary>
		/// Rotates this vector around a local origin point.
		/// </summary>
		public static Vector2 LocalRotatedBy(this Vector2 position, Vector2 origin, float radians)
		{
			return origin + (position - origin).RotatedBy(radians);
		}
	}
	
	public class OpusEnterWorldPlayer : ModPlayer
	{
		public override void OnEnterWorld()
		{
			Main.NewText("Thank you for using Opus!", Color.LightGreen);
			if (!Opus.HasJingled && ModContent.GetInstance<OpusConfig>().PlayJingleOnEnterWorldFirstTime)
			{
				SoundEngine.PlaySound(new SoundStyle("OpusLib/Assets/Audio/OpusJingle"));
				Opus.HasJingled = true;
			}
		}
	}
}
