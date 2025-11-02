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

namespace OpusLib
{
	public class Opus : Mod
	{
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

		public static void RingProjectileOutward(int ID, int Amount, Vector2 CTR, float Radius,
			int Dmg = 0, int KB = 0, int Speed = 2,
			float AI0 = 0, float AI1 = 0, float AI2 = 0,
			bool RandomOffset = false)
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

		public static void RingProjectileInward(int ID, int Amount, Vector2 CTR, float Radius,
			int Dmg = 0, int KB = 0, int Speed = 2,
			float AI0 = 0, float AI1 = 0, float AI2 = 0,
			bool RandomOffset = false)
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

		public static void RingProjectileOutwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius,
			int Dmg = 0, int KB = 0, int Speed = 2,
			float AI0 = 0, float AI1 = 0, float AI2 = 0)
		{
			for (int i = 0; i < Amount; i++)
			{
				// Random direction around the circle
				Vector2 position = CTR + Main.rand.NextVector2CircularEdge(Radius, Radius);
				Vector2 velocity = (CTR + position) * Speed;

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

		public static void RingProjectileInwardRandomDir(int ID, int Amount, Vector2 CTR, float Radius,
			int Dmg = 0, int KB = 0, int Speed = 2,
			float AI0 = 0, float AI1 = 0, float AI2 = 0)
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

		public static void RadialSpreadProjectile(
            int ID, int Amount, Vector2 CTR,
            int Dmg = 0, int KB = 0, int Speed = 2,
            float AI0 = 0, float AI1 = 0, float AI2 = 0,
            bool RandomOffset = false)
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

        public static BasePRT NewParticleFloatAI(int prtID, Vector2 position, Vector2 velocity, Color color = default(Color), float scale = 1f, float ai0 = 0, float ai1 = 0, float ai2 = 0)
        {
            BasePRT basePRT = PRTLoader.PRT_IDToInstances[prtID].Clone();
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

        public static void StartSpriteBatchWithBlending(SpriteBatch spriteBatch, BlendState blendState, SpriteSortMode ssm)
        {
            spriteBatch.End();
            spriteBatch.Begin(ssm, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public static void ReturnToDefaultDrawing(SpriteBatch spriteBatch)
        {
            spriteBatch.End();
            spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
        }

        public static void BurstParticle(int type, int Amount, Vector2 Center, Color color, float Scale = 1f, float Radius = 6f)
		{
			for (int i = 0; i < Amount; i++)
			{
				PRTLoader.NewParticle(type, Center, Vector2.Zero, color, Scale);
			}
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
