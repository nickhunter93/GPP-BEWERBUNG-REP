using SFML.System;
using System;

namespace ConsoleApp2
{
    public struct Vector2D
    {
        /*
         * Vector2D is a class representing a vector in a 2D room.
         * The X and Y coordinates a representated as doubles.
        */

        private double x;
        private double y;

        public double X
        {
            get { return x; }
            set { this.x = value; }
        }

        public double Y
        {
            get { return y; }
            set { this.y = value; }
        }

        public Vector2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public Vector2D(Vector2D a)
        {
            this.x = a.x;
            this.y = a.y;
        }

        public override String ToString()
        {
            return this.x + " " + this.y;
        }

        /*
         * Static function to get an instance of Vector2D which Coordinates are both Zero.
         */
        public static Vector2D Zero()
        {
            return new Vector2D(0.0, 0.0);
        }

        /*
         * Static function to get an instance of Vector2D which Coordinates are both One.
         */
        public static Vector2D One()
        {
            return new Vector2D(1.0, 1.0);
        }

        /*
         * Static function to get an instance of Vector2D which points upward.
         */
        public static Vector2D Up()
        {
            return new Vector2D(0.0, -1.0);
        }

        /*
         * Static function to get an instance of Vector2D which points down.
         */
        public static Vector2D Down()
        {
            return new Vector2D(0.0, 1.0);
        }

        /*
         * Static function to get an instance of Vector2D which points left.
         */
        public static Vector2D Left()
        {
            return new Vector2D(-1.0, 0.0);
        }

        /*
         * Static function to get an instance of Vector2D which points right.
         */
        public static Vector2D Right()
        {
            return new Vector2D(1.0, 0.0);
        }

        /*
         * Function which returns a Vector2D of this instance which length is one.
         */
        public Vector2D Normalize()
        {
            return this * (1 / GetLength());
        }

        /*
         * Function to get an instance of Vector2D which is orthogonal to the instance on which it is called.
         */
        public Vector2D Orthogonal()
        {
            return new Vector2D(-this.y, this.x);
        }

        /*
         * Function which returns the length of this instance as a Double.
         */
        public double GetLength()
        {
            return Math.Sqrt((this.x * this.x) + (this.y * this.y));
        }

        /*
         * Function to which calculates the Distance, between this instance and another Vector2D, as a double.
         */
        public double GetDistance(Vector2D point)
        {
            Vector2D vector = new Vector2D(this.x - point.X, this.y - point.Y);
            return vector.GetLength();
        }

        /*
         * Returns true if the Vector2D is parallel to this instance.
         */
        public bool IsParallel(Vector2D vector)
        {
            if (this.Normalize() == vector.Normalize())
            {
                return true;
            }
            return false;
        }
        /*
         * Function which returns the angle from the origin.
         */
        public double GetAngle()
        {
            double hyp = this.GetLength();
            double ank = this.X;

            if (ank == 0)
            {
                double gek = this.Y;
                return Math.Asin(gek/hyp);
            }

            return Math.Acos(ank / hyp);
        }

        /*
         * Function which returns the angle between to instances of Vector2D.
         */
        public double GetAngleBetween(Vector2D b)
        {
            double angle = Dot(b) / (this.GetLength() * b.GetLength());
            angle = (180 / Math.PI) * Math.Acos(angle);
            if (Cross(b) >= 0)
                angle = -angle;
            return angle;
        }
        /*
         * Funtions to Rotate a specific Vektor.
         */
        public Vector2D Rotate(double angle)
        {
            double a = X * Math.Cos(angle) - Y  * Math.Sin(angle);
            double b = X * Math.Sin(angle) + Y  * Math.Cos(angle);
            return new Vector2D(a,b);
        }

        public Vector2D RotateAround(double angle,Vector2D z)
        {
            double a = z.X + (X - z.X) * Math.Cos(angle) - (Y - z.Y) * Math.Sin(angle);
            double b = z.Y + (X - z.X) * Math.Sin(angle) + (Y - z.Y) * Math.Cos(angle);
            return new Vector2D(a, b);
        }

        public double Dot(Vector2D p)
        {
            return this.X * p.X + this.Y * p.Y;
        }

        public double Cross(Vector2D p)
        {
            return this.x * p.Y + this.Y * p.X;
        }

        public static double LengthSQ(Vector2D p)
        {
            return p.GetLength() * p.GetLength();
        }

        public new bool Equals(object o)
        {
            if(o is Vector2D v)
            {
                return this == v;
            }
            return false;
        }

        public static Vector2D operator +(Vector2D a) =>
            new Vector2D(a.X,a.Y);

        public static Vector2D operator +(Vector2D a, Vector2D b) =>
            new Vector2D(a.X + b.X, a.Y + b.Y);

        public static Vector2f operator +(Vector2f a, Vector2D b) =>
            new Vector2f((float)(a.X + b.X), (float)(a.Y + b.Y));

        public static Vector2D operator +(Vector2D a, Vector2f b) =>
            new Vector2D(a.X + b.X, a.Y + b.Y);

        public static Vector2D operator -(Vector2D a) =>
            new Vector2D(-a.X,-a.Y);

        public static Vector2D operator -(Vector2D a, Vector2D b) =>
            new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2f operator -(Vector2f a, Vector2D b) =>
            new Vector2f((float)(a.X - b.X), (float)(a.Y - b.Y));

        public static Vector2D operator -(Vector2D a, Vector2f b) =>
            new Vector2D(a.X - b.X, a.Y - b.Y);

        public static Vector2D operator *(double a, Vector2D b) =>
            new Vector2D(a * b.X, a * b.Y);

        public static Vector2D operator *(Vector2D b, double a) =>
            new Vector2D(a * b.X, a * b.Y);

        public static Vector2D operator *(float a, Vector2D b) =>
            new Vector2D(a * b.X, a * b.Y);

        public static Vector2D operator *(Vector2D b, float a) =>
            new Vector2D(a * b.X, a * b.Y);

        public static Vector2D operator *(Vector2D a, Vector2D b) =>
             new Vector2D(a.X * b.X, a.Y * b.Y);

        public static Vector2D operator /(Vector2D a, double b) =>
            new Vector2D(a.X / b, a.Y / b);

        public static Vector2D operator /(Vector2D a, float b) =>
            new Vector2D(a.X / b, a.Y / b);

        public static bool operator ==(Vector2D a, Vector2D b) =>
            a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Vector2D a, Vector2D b) =>
            a.X != b.X || a.Y != b.Y;

        public static implicit operator Vector2D(Vector2f vector)
        {
            return new Vector2D(vector.X, vector.Y);
        }

        public static implicit operator Vector2D(Vector2i vector)
        {
            return new Vector2D(vector.X, vector.Y);
        }

        public static implicit operator Vector2i(Vector2D vector)
        {
            return new Vector2i((int)vector.X, (int)vector.Y);
        }

        public static implicit operator Vector2f(Vector2D vector)
        {
            return new Vector2f((float)vector.X, (float)vector.Y);
        }
    }
}