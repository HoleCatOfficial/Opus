using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Opus.Content.Helpers;
using ReLogic.Content;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;
using InnoVault.PRT;
using Terraria.Audio;

namespace Opus
{
	// Please read https://github.com/tModLoader/tModLoader/wiki/Basic-tModLoader-Modding-Guide#mod-skeleton-contents for more information about the various files in a mod.
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

		public void DustsWhileItemIsInWorld(Rectangle itemRect, int DustType = -1, int ChancePerTick = 3, float DustScale = 1f, float DustVelX = 0f, float DustVelY = 0f, Color DustColor = default)
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

		public void RadialSpreadProjectile(int ID, int Amount, Vector2 CTR, int Dmg = 0, int KB = 0, int Speed = 2)
		{
			float rotationStep = MathHelper.TwoPi / Amount;

			for (int i = 0; i < Amount; i++)
			{
				Vector2 velocity = new Vector2(Speed, 0f).RotatedBy(rotationStep * i);
				Projectile.NewProjectile(
					Projectile.GetSource_None(),
					CTR,
					velocity,
					ID,
					Dmg,
					KB
				);
			}
		}

		/// <summary>
		/// Easy-to-call method for drawing a point glow over the center of a projectile.
		/// </summary>
		/// <param name="projectile"></param>
		/// <param name="color"></param>
		/// <param name="RotateWithProj"></param>
		/// <param name="Rot"></param>
		public void DrawGlowOnProj(Projectile projectile, Color color, bool RotateWithProj, float Rot = 0)
		{
			if (RotateWithProj)
			{
				Rot = projectile.rotation;
			}

			Main.EntitySpriteDraw(
				OpusAssets.PointGlow.Value,
				projectile.Center - Main.screenPosition,
				null,
				color,
				Rot,
				OpusAssets.PointGlow.Value.Size() / 2,
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
		public void DrawTextureOnProj(Asset<Texture2D> Tex, Projectile projectile, Color color, bool RotateWithProj, float Rot = 0, float ScaleX = 1f, float ScaleY = 1f)
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

		public void StartSpriteBatchWithBlending(SpriteBatch spriteBatch, BlendState blendState, SpriteSortMode ssm)
		{
			spriteBatch.End();
			spriteBatch.Begin(ssm, blendState, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
		}

		public void ReturnToDefaultDrawing(SpriteBatch spriteBatch)
		{
			spriteBatch.End();
			spriteBatch.Begin(SpriteSortMode.Deferred, BlendState.AlphaBlend, SamplerState.AnisotropicClamp, DepthStencilState.None, RasterizerState.CullNone, null, Main.GameViewMatrix.TransformationMatrix);
		}
	}
	
	public class OpusEnterWorldPlayer : ModPlayer
	{
		public override void OnEnterWorld()
		{
			Main.NewText("Thank you for using Opus!", Color.LightGreen);
			if (!Opus.HasJingled)
			{
				SoundEngine.PlaySound(OpusAssets.Jingle);
				Opus.HasJingled = true;
			}
		}
	}
}
