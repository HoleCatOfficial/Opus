using System;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;

namespace OpusLib.Content.Helpers
{

    public class SimpleLine
    {
        public Vector2 Start;
        public Vector2 End;

        public SimpleLine(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

        protected float LineLength()
        {
            return Start.Distance(End);
        }

        public float LineRotation()
        {
            Vector2 Dir = End - Start;
            return Dir.ToRotation();
        }

        public float GetLineLength => LineLength();
        public float GetLineRotation => LineRotation();

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
    }
    /// <summary>
    /// This is a line!
    /// <br/> This class is a Linear Value from one Vector2 to another, meaning it wont bend or ignore any obstacles and will stretch as long as needed to span the gap!
    /// </summary>
    public class Line
    {
        public Vector2 Start;
        public Vector2 End;

        public Line(Vector2 start, Vector2 end)
        {
            Start = start;
            End = end;
        }

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
            float ColPT = 0f;

            if (Type < 1 || Type > 3)
            {
                return false;
            }
            
            foreach (var npc in Main.npc)
            {
                if (!npc.active) continue;
                
                Rectangle hitbox = npc.Hitbox;
                if (Terraria.Collision.CheckAABBvLineCollision(
                    hitbox.TopLeft(),
                    hitbox.Size(),
                    Start,
                    End,
                    Width,
                    ref ColPT
                ))
                {
                    return true;
                }
            }

            foreach (var plr in Main.player)
            {
                if (!plr.active) continue;
                
                Rectangle hitbox = plr.Hitbox;
                if (Terraria.Collision.CheckAABBvLineCollision(
                    hitbox.TopLeft(),
                    hitbox.Size(),
                    Start,
                    End,
                    Width,
                    ref ColPT
                ))
                {
                    return true;
                }
            }

            foreach (Projectile proj in Main.projectile)
            {
                if (!proj.active) continue;
                
                Rectangle hitbox = proj.Hitbox;
                if (Terraria.Collision.CheckAABBvLineCollision(
                    hitbox.TopLeft(),
                    hitbox.Size(),
                    Start,
                    End,
                    Width,
                    ref ColPT
                ))
                {
                    return true;
                }
            }
            
            return false;
        }
    }
}