using System.Collections.Generic;

namespace ConsoleApp2
{
    public class SphereCollider : Component, ICollider
    {
        private double _radius;
        private bool _isStatic;
        private bool _isTrigger;
        
        public SphereCollider()
        {
            Radius = 0;
            Static = false;
        }

        public SphereCollider(double radius,bool isStatic) : this()
        {
            Radius = radius;
            Static = isStatic;
        }
        public SphereCollider(double radius, bool isStatic, bool isTrigger) : this()
        {
            Radius = radius;
            Static = isStatic;
            Trigger = isTrigger;
        }
        public double Radius { get => _radius; set => _radius = value; }
        public bool Static { get => _isStatic; set => _isStatic = value; }
        public bool Trigger { get => _isTrigger; set => _isTrigger = value; }

        public bool IsCollided(SphereCollider collider)
        {
            if (Static && collider.Static)
                return false;

            double d = Parent.transform.Position.GetDistance(collider.Parent.transform.Position);

            if(d > Radius + collider.Radius)
            {
                return false;
            }else if(d <= Radius + collider.Radius)
            {
                if (!Static && !Trigger && !collider.Trigger)
                {
                    Parent.transform.Position = Parent.transform.Position - ((collider.Parent.transform.Position - Parent.transform.Position).Normalize()) * ((Radius + collider.Radius) - d);
                }
                if (Trigger)
                {
                    Parent.CollisionHappened = true;
                    Parent.CollidedWith.Add(collider.Parent);
                }
                return true;
            }

            return false;
        }

        public bool IsCollided(RectangleCollider collider)
        {
            if (Static && collider.Static || !collider.Active)
                return false;

            Vector2D mtd = Vector2D.Zero();
            List<Vector2D> axes = new List<Vector2D>();

            collider.Position = collider.Parent.transform.Position;
            int i, j, l,axisZ;
            Vector2D tmp, proj, voffset;
            double dp, amin, amax, bmin, bmax, d1, d2, foffset, minD;

            minD = double.MinValue;
            axisZ = 0;

            // Offset berechnen
            voffset = collider.Position - this.Parent.transform.Position;
            // A - alle Projektionsgeraden ermitteln und projezieren
            for (i = 0; i < collider.Points.Length; i++)
            {
                l = i + 1;
                if (l > collider.Points.Length - 1)
                {
                    l = 0;
                }
                // Berechnung der Seitenfläche
                tmp = collider.Points[l] - collider.Points[i];
                // Berechnet die Normale der Seitenfläche
                proj = tmp.Orthogonal().Normalize();
                axes.Add(proj);
                // Projeziert den ersten Wert
                amin = collider.Points[0].Dot(proj);
                amax = amin;
                // Findet den kleinsten und größten projezierten Wert für die Gerade für A
                for (j = 1; j < collider.Points.Length; j++)
                {
                    //projezieren
                    dp = collider.Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }
                // s.o.
                bmax = (proj * Radius).Dot(proj);
                bmin = -bmax;

                // 1D Kollision
                foffset = voffset.Dot(proj);
                amin = amin + foffset;
                amax = amax + foffset;
                d1 = amin - bmax;
                d2 = bmin - amax;
                // Wenn es keine Überschneidung gibt, abbrechen -> keine Kollision
                if (d1 > 0 || d2 > 0)
                {
                    return false;
                }
                else if (d1 > minD || d2 > minD)
                {
                    if (d1 > minD)
                    {
                        axisZ = i;
                        minD = d1;
                    }
                    if (d2 > minD)
                    {
                        axisZ = i;
                        minD = d2;
                    }
                }
                // C - Alle Projektionsgeraden ermitteln und projezieren
                proj = (this.Parent.transform.Position - (collider.Points[i] + collider.Parent.transform.Position)).Normalize();
                axes.Add(tmp.Normalize());
                //s.o.
                amin = collider.Points[0].Dot(proj);
                amax = amin;
                for (j = 1; j < collider.Points.Length; j++)
                {
                    //projezieren
                    dp = collider.Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }

                bmax = (proj * Radius).Dot(proj);
                bmin = -bmax;

                // 1D Kollision
                foffset = voffset.Dot(proj);
                amin = amin + foffset;
                amax = amax + foffset;
                d1 = amin - bmax;
                d2 = bmin - amax;
                // Wenn es keine Überschneidung gibt, abbrechen -> keine Kollision
                if (d1 > 0 || d2 > 0)
                {
                    return false;
                }
                else if (d1 > minD || d2 > minD)
                {
                    if (d1 > minD)
                    {
                        axisZ = i + 4;
                        minD = d1;
                    }
                    if (d2 > minD)
                    {
                        axisZ = i + 4;
                        minD = d2;
                    }
                }

            }

            mtd = axes[axisZ];
            /*for (int k = 1; k < axes.Count; k++)
            {
                if (axes[k].GetLength() < mtd.GetLength())
                {
                    mtd = axes[k];
                }
            }*/
            if (voffset.Dot(mtd) < 0)
                mtd = mtd * -1;
            mtd = mtd * (-minD + 1);

            if (!Static && !Trigger && !collider.Trigger)
                Parent.transform.Position = Parent.transform.Position - mtd;

            if (Trigger)
            {
                Parent.CollisionHappened = true;
                Parent.CollidedWith.Add(collider.Parent);
            }

            return true;
        }

        public override void Update(double elapsedTime)
        {
            
        }
    }
}