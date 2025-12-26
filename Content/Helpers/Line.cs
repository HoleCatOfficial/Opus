using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace OpusLib.Content.Helpers
{
    /// <summary>
    /// This is a line!
    /// <br/> This class is a Linear Value from one Vector2 to another, meaning it wont bend or ignore any obstacles and will stretch as long as needed to span the gap!
    /// </summary>
    public abstract class Line
    {
        public Vector2 Start;
        public Vector2 End;

        public virtual bool ShouldUpdate()
        {
            return true;
        }

        public bool VisualiseLineWithDust = false;

        public float GetLineLength => LineLength();
        public float GetLineRotation => LineRotation();

        protected float LineLength()
        {
            return Start.Distance(End);
        }

        public float LineRotation()
        {
            Vector2 Dir = End - Start;
            return Dir.ToRotation();
        }

        public virtual bool PreUpdateLine()
        {
            if (ShouldUpdate())
            {
                if (VisualiseLineWithDust)
                {
                    Vector2[] points = GetPointsAlongLine(10);
                    if (points != null)
                    {
                        foreach (Vector2 point in points)
                        {
                            Dust.NewDust(point, 2, 2, DustID.TintableDustLighted, 0, 0, 0, Color.Green, 1f);
                        }
                    }
                    
                }
                return true;
            }
            else
            {
                return false;
            }

            
        }

        public void UpdateLine()
        {
            if (!PreUpdateLine() || !ShouldUpdate())
            {
                return;
            }

            PostUpdateLine();
        }

        public void PostUpdateLine()
        {

        }

        public Vector2[]? GetPointsAlongLine(int Divisions = 1)
        {
            if (Divisions < 1)
            {
                return null;
            }
            Vector2[] points = new Vector2[Divisions + 1];
            
            for (int i = 0; i <= Divisions; i++)
            {
                float t = i / (float)Divisions;
                points[i] = Vector2.Lerp(Start, End, t);
            }
            
            return points;
        }

        /// <summary>
        /// Returns true if the line crosses an entity hitbox or if an entity hitbox is within Width. 
        /// <br/> <b>Width</b> is used as a diameter, with half of the width extending from either side of the line perpendicularly.
        /// <br/> <b>Type</b> is used to determine what to check collision for. 1 is for NPCs, 2 is for Players, and 3 is for Projectiles.
        /// <br/> Using an invalid value for Type will cause this to return false no matter what.
        /// </summary>
        /// <param name="Width"></param>
        /// <param name="Type"></param>
        /// <returns></returns>
        public bool Collision(float Width, int Type)
        {
            float halfWidth = Width / 2f;

            if (Type < 1 || Type > 3)
            {
                return false;
            }
            
            foreach (var npc in Main.npc)
            {
                if (!npc.active) continue;
                
                Rectangle hitbox = npc.Hitbox;
                Vector2 lineDir = (End - Start).SafeNormalize(Vector2.Zero);
                Vector2 closestPoint = ClosestPointOnLineSegment(new Vector2(hitbox.Center.X, hitbox.Center.Y), Start, End);
                
                float distanceToLine = Vector2.Distance(new Vector2(hitbox.Center.X, hitbox.Center.Y), closestPoint);
                
                if (distanceToLine <= halfWidth + hitbox.Width / 2f && Type == 1)
                {
                    return true;
                }
            }

            foreach (var plr in Main.player)
            {
                if (!plr.active) continue;
                
                Rectangle hitbox = plr.Hitbox;
                Vector2 lineDir = (End - Start).SafeNormalize(Vector2.Zero);
                Vector2 closestPoint = ClosestPointOnLineSegment(new Vector2(hitbox.Center.X, hitbox.Center.Y), Start, End);
                
                float distanceToLine = Vector2.Distance(new Vector2(hitbox.Center.X, hitbox.Center.Y), closestPoint);
                
                if (distanceToLine <= halfWidth + hitbox.Width / 2f && Type == 2)
                {
                    return true;
                }
            }

            foreach (Projectile proj in Main.projectile)
            {
                if (!proj.active) continue;
                
                Rectangle hitbox = proj.Hitbox;
                Vector2 lineDir = (End - Start).SafeNormalize(Vector2.Zero);
                Vector2 closestPoint = ClosestPointOnLineSegment(new Vector2(hitbox.Center.X, hitbox.Center.Y), Start, End);
                
                float distanceToLine = Vector2.Distance(new Vector2(hitbox.Center.X, hitbox.Center.Y), closestPoint);
                
                if (distanceToLine <= halfWidth + hitbox.Width / 2f && Type == 3)
                {
                    return true;
                }
            }
            
            return false;
        }
        
        private static Vector2 ClosestPointOnLineSegment(Vector2 point, Vector2 lineStart, Vector2 lineEnd)
        {
            Vector2 lineVec = lineEnd - lineStart;
            float lineLen = lineVec.Length();
            float t = Math.Max(0, Math.Min(1, Vector2.Dot(point - lineStart, lineVec) / (lineLen * lineLen)));
            return lineStart + lineVec * t;
        }
    }
}