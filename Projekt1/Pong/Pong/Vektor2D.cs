using SFML.System;
using System;

namespace ConsoleApp2
{
    public class Vektor2D
    {
        /*
         * Vektor2D is a class representing a vector in a 2D room.
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

        public Vektor2D(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        /*
         * Static function to get an instance of Vektor2D which Coordinates are both Zero.
         */
        public static Vektor2D Zero()
        {
            return new Vektor2D(0.0, 0.0);
        }

        /*
         * Static function to get an instance of Vektor2D which points upward.
         */
        public static Vektor2D Up()
        {
            return new Vektor2D(0.0, -1.0);
        }

        /*
         * Static function to get an instance of Vektor2D which points down.
         */
        public static Vektor2D Down()
        {
            return new Vektor2D(0.0, 1.0);
        }

        /*
         * Static function to get an instance of Vektor2D which points left.
         */
        public static Vektor2D Left()
        {
            return new Vektor2D(-1.0, 0.0);
        }

        /*
         * Static function to get an instance of Vektor2D which points right.
         */
        public static Vektor2D Right()
        {
            return new Vektor2D(1.0, 0.0);
        }

        /*
         * Function which returns a Vektor2D of this instance which length is one.
         */
        public Vektor2D Normalize()
        {
            double scalar = Math.Sqrt((this.x * this.x) + (this.y * this.y));
            return this / scalar;
        }

        /*
         * Function to get an instance of Vektor2D which is orthogonal to the instance on which it is called.
         */
        public Vektor2D Orthogonal()
        {
            return new Vektor2D(this.y, -this.x);
        }

        /*
         * Function which returns the length of this instance as a Double.
         */
        public double GetLength()
        {
            return Math.Sqrt((this.x * this.x) + (this.y * this.y));
        }

        /*
         * Function to which calculates the Distance, between this instance and another Vektor2D, as a double.
         */
        public double GetDistance(Vektor2D point)
        {
            Vektor2D vector = new Vektor2D(this.x - point.X, this.y - point.Y);
            return vector.GetLength();
        }

        /*
         * Returns true if the Vector2D is parallel to this instance.
         */
        public bool IsParallel(Vektor2D vector)
        {
            if(this.Normalize() == vector.Normalize())
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
            double ank = this.x;

            return Math.Acos(ank / hyp);
        }

        /*
         * Function which returns the angle between to instances of Vector2D.
         */
        public double GetAngleBetween(Vektor2D b)
        {
            double hyp1 = this.GetLength();
            double ank1 = this.x;
            
            double hyp2 = b.GetLength();
            double ank2 = b.X;

            return Math.Acos(ank1 / hyp1) - Math.Acos(ank2 / hyp2);

        }

        public static Vektor2D operator + (Vektor2D a, Vektor2D b) =>
            new Vektor2D(a.X + b.X, a.Y + b.Y);

        public static Vektor2D operator - (Vektor2D a, Vektor2D b) =>
            new Vektor2D(a.X - b.X, a.Y - b.Y);

        public static Vektor2D operator * (double a, Vektor2D b) =>
            new Vektor2D(a * b.X, a * b.Y);

        public static Vektor2D operator / (Vektor2D a, double b) =>
            new Vektor2D(a.X / b, a.Y / b);

        public static bool operator == (Vektor2D a, Vektor2D b) =>
            a.X == b.X && a.Y == b.Y;

        public static bool operator !=(Vektor2D a, Vektor2D b) =>
            a.X != b.X || a.Y != b.Y;

        public static implicit operator Vektor2D (Vector2f vector)
        {
            return new Vektor2D(vector.X, vector.Y);
        }

        public static explicit operator Vector2f (Vektor2D vector)
        {
            return new Vector2f((float)vector.X, (float)vector.Y);
        }
    }
}