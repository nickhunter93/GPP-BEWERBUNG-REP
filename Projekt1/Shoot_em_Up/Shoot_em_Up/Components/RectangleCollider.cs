using System;
using System.Collections.Generic;

namespace ConsoleApp2
{
    public class RectangleCollider : Component, ICollider
    {
        private Vector2D _position;
        private Vector2D _size;
        private Vector2D[] _points;
        private double _degree = 0;
        private bool _isStatic;
        private bool _active;
        private double _activeTimer;

        public RectangleCollider()
        {
            Position = Vector2D.Zero();
            Size = Vector2D.One();
            _points = new Vector2D[4];
            Static = false;
            Active = true;
            _activeTimer = 0;
            RecalculatePoints();
        }

        public RectangleCollider(Vector2D size,bool isStatic) : this()
        {
            Static = isStatic;
            Size = size;
            RecalculatePoints();
        }

        public RectangleCollider(Vector2D size,bool isStatic,int activeTimer) : this(size, isStatic)
        {
            Active = false;
            _activeTimer = activeTimer;
        }

        public Vector2D Size { get => _size; set => _size = value; }
        public Vector2D Position { get => _position; set => _position = value; }
        public Vector2D[] Points { get => _points;}
        public bool Static { get => IsStatic; set => IsStatic = value; }
        public bool IsStatic { get => _isStatic; set => _isStatic = value; }
        public bool Active { get => _active; set => _active = value; }

        public void SetActiveTimer(double value)
        {
            if(value > 0)
            {
                Active = false;
                _activeTimer = value;
            }
        }

        public void RecalculatePoints()
        {
            Points[0] = new Vector2D(-((1.0 / 2) * Size.X), -((1.0 / 2) * Size.Y));
            Points[1] = new Vector2D(((1.0 / 2) * Size.X), -((1.0 / 2) * Size.Y));
            Points[2] = new Vector2D(((1.0 / 2) * Size.X), ((1.0 / 2) * Size.Y));
            Points[3] = new Vector2D(-((1.0 / 2) * Size.X), ((1.0 / 2) * Size.Y));
            if(Parent != null)
                for (int i = 0; i < Points.Length;i++)
                {
                    Points[i] = Points[i].Rotate(Math.PI * Parent.transform.Rotation / 180);
                }
        }

        private Vector2D RotatePoint(Vector2D point)
        {
            double alpha = (Math.PI / 180) * _degree;
            return new Vector2D(point.X * Math.Cos(alpha) - point.Y * Math.Sin(alpha), point.X * Math.Sin(alpha) + point.Y * Math.Cos(alpha));
        }

        private Vector2D[] GetAxis(RectangleCollider collider)
        {
            Vector2D[] axis = new Vector2D[4];
            axis[0] = Points[1] - Points[0];
            axis[1] = Points[3] - Points[0];
            axis[2] = collider.Points[1] - collider.Points[0];
            axis[3] = collider.Points[3] - collider.Points[0];

            for (int i = 0; i < 3; i++)
            {
                axis[i] = axis[i].Normalize();
            }

            return axis;
        }

        public void GetMinMax(Vector2D[] axis)
        {
            throw new NotImplementedException();
        }

        public bool IsCollided(RectangleCollider collider)
        {
            if (IsStatic && collider.IsStatic || !Active)
                return false;

            Vector2D mtd = Vector2D.Zero();
            List<Vector2D> axes = new List<Vector2D>();
            Position = this.Parent.transform.Position;
            collider.Position = collider.Parent.transform.Position;
            int i, j, l, axisZ;
            Vector2D tmp, proj, voffset;
            double dp, amin, amax, bmin, bmax, d1, d2, foffset, minD;

            axisZ = 0;
            minD = double.MinValue;

            // Offset berechnen
            voffset = this.Position - collider.Position;
            // A - alle Projektionsgeraden ermitteln und projezieren
            for(i = 0; i < this.Points.Length; i++)
            {
                l = i + 1;
                if (l > this.Points.Length - 1)
                {
                    l = 0;
                }
                // Berechnung der Seitenfläche
                tmp = Points[l] - Points[i];
                // Berechnet die Normale der Seitenfläche
                proj = tmp.Orthogonal().Normalize();
                axes.Add(proj);
                // Projeziert den ersten Wert
                amin = Points[0].Dot(proj);
                amax = amin;
                // Findet den kleinsten und größten projezierten Wert für die Gerade für A
                for (j = 1; j < Points.Length; j++)
                {
                    //projezieren
                    dp = Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }
                // s.o.
                bmin = collider.Points[0].Dot(proj);
                bmax = bmin;
                // B
                for (j = 1; j < collider.Points.Length; j++)
                {
                    dp = collider.Points[j].Dot(proj);
                    if (dp < bmin)
                        bmin = dp;
                    if (dp > bmax)
                        bmax = dp;
                }
                // 1D Kollision
                foffset = voffset.Dot(proj);
                amin = amin + foffset;
                amax = amax + foffset;
                d1 = amin - bmax;
                d2 = bmin - amax;
                // Wenn es keine Überschneidung gibt, abbrechen -> keine Kollision
                if(d1>0 || d2 > 0)
                {
                    return false;
                }else if(d1 > minD || d2 > minD)
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
            }
            // B - alle Projektionsgeraden ermitteln und projezieren (s.o.)
            for (i = 0; i < collider.Points.Length; i++)
            {
                l = i + 1;
                if (l > collider.Points.Length - 1)
                {
                    l = 0;
                }
                tmp = collider.Points[l] - collider.Points[i];
                proj = tmp.Orthogonal().Normalize();
                axes.Add(proj);
                amin = Points[0].Dot(proj);
                amax = amin;
                for (j = 1; j < Points.Length; j++)
                {
                    dp = Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }
                bmin = collider.Points[0].Dot(proj);
                bmax = bmin;
                for (j = 1; j < collider.Points.Length; j++)
                {
                    dp = collider.Points[j].Dot(proj);
                    if (dp < bmin)
                        bmin = dp;
                    if (dp > bmax)
                        bmax = dp;
                }
                foffset = voffset.Dot(proj);
                amin = amin + foffset;
                amax = amax + foffset;
                d1 = amin - bmax;
                d2 = bmin - amax;
                if (d1 > 0 || d2 > 0)
                {
                    return false;
                }
                else if (d1 > minD || d2 > minD)
                {
                    if (d1 > minD)
                    {
                        axisZ = i+4;
                        minD = d1;
                    }
                    if (d2 > minD)
                    {
                        axisZ = i+4;
                        minD = d2;
                    }
                }
            }
            if (!IsStatic)
            {
                /*mtd = axes[0];
                for (int k = 1; k < axes.Count; k++)
                {
                    if (axes[k].GetLength() < mtd.GetLength())
                    {
                        mtd = axes[i];
                    }
                }
                if (voffset.Dot(mtd) > 0)
                    mtd = mtd * -1;*/

                mtd = axes[axisZ];
                if(voffset.Dot(mtd) > 0)
                {
                    mtd = mtd * -1;
                }
                mtd = mtd * -minD;

                
                Parent.transform.Position = Parent.transform.Position - mtd;
            }
                

            return true;
        }

        public override void Update(double elapsedTime)
        {
            if (_activeTimer > 0)
            {
                _activeTimer -= elapsedTime;
                if (_activeTimer <= 0)
                    Active = true;
            }
            Position = Parent.transform.Position;
            RecalculatePoints();
        }

        public bool IsCollided(SphereCollider collider)
        {
            if (IsStatic && collider.IsStatic || !Active)
                return false;

            Vector2D mtd = Vector2D.Zero();
            List<Vector2D> axes = new List<Vector2D>();

            Position = this.Parent.transform.Position;
            int i, j, l;
            Vector2D tmp, proj, voffset;
            double dp, amin, amax, bmin, bmax, d1, d2, foffset;

            // Offset berechnen
            voffset = this.Position - collider.Parent.transform.Position;
            // A - alle Projektionsgeraden ermitteln und projezieren
            for (i = 0; i < this.Points.Length; i++)
            {
                l = i + 1;
                if (l > this.Points.Length - 1)
                {
                    l = 0;
                }
                // Berechnung der Seitenfläche
                tmp = Points[l] - Points[i];
                // Berechnet die Normale der Seitenfläche
                proj = tmp.Orthogonal().Normalize();
                axes.Add(proj);
                // Projeziert den ersten Wert
                amin = Points[0].Dot(proj);
                amax = amin;
                // Findet den kleinsten und größten projezierten Wert für die Gerade für A
                for (j = 1; j < Points.Length; j++)
                {
                    //projezieren
                    dp = Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }
                // s.o.
                bmax = (proj * collider.Radius).Dot(proj);
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
                // C - Alle Projektionsgeraden ermitteln und projezieren
                proj = (collider.Parent.transform.Position - (Points[i] + Parent.transform.Position)).Normalize();
                axes.Add(proj);
                //s.o.
                amin = Points[0].Dot(proj);
                amax = amin;
                for (j = 1; j < Points.Length; j++)
                {
                    //projezieren
                    dp = Points[j].Dot(proj);
                    if (dp < amin)
                        amin = dp;
                    if (dp > amax)
                        amax = dp;
                }

                bmax = (proj * collider.Radius).Dot(proj);
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
            }

            mtd = axes[0];
            for (int k = 1; k < axes.Count; k++)
            {
                if (axes[k].GetLength() < mtd.GetLength())
                {
                    mtd = axes[k];
                }
            }
            if (voffset.Dot(mtd) < 0)
                mtd = mtd * -1;

            if (!IsStatic)
                Parent.transform.Position = Parent.transform.Position - mtd;

            return true;
        }
    }
}