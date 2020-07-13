﻿using System;
using System.Collections.Generic;
using System.Globalization;
using Microsoft.Xna.Framework;
using ProtoBuf;

namespace MLAPI.DataTypes
{
    /// <summary>
    /// A point that includes and x, y, and dimension.
    /// </summary>
    [ProtoContract]
    public class Point3D : IComparable<Point3D>, IEquatable<Point3D>
    {
        public static readonly Point3D Zero = new Point3D(0, 0, Guid.Empty);

        [ProtoMember(1)]
        public int X { get; set; }

        [ProtoMember(2)]
        public int Y { get; set; }

        [ProtoMember(3)]
        public Guid DimensionId { get; set; }

        public Point3D(int x, int y, Guid dimensionId)
        {
            this.X = x;
            this.Y = y;
            this.DimensionId = dimensionId;
        }

        public Point3D()
        {
            //Protobuf-net constructor.
        }

        public override bool Equals(object obj)
        {
            if (obj is Point3D)
            {
                Point3D c = obj as Point3D;
                return this.X == c.X && this.Y == c.Y && this.DimensionId.Equals(c.DimensionId);
            }

            return false;
        }

        public override int GetHashCode()
        {
            unchecked
            {
                int hash = 17;
                hash = this.X ^ this.Y ^ this.DimensionId.GetHashCode();
                return hash;
            }
        }

        public static implicit operator Microsoft.Xna.Framework.Point(Point3D value)
        {
            return new Microsoft.Xna.Framework.Point(value.X, value.Y);
        }

        public static implicit operator Point2D(Point3D value)
        {
            return new Point2D(value.X, value.Y);
        }

        public static implicit operator Vector2(Point3D value)
        {
            return new Vector2(value.X, value.Y);
        }

        public static Point3D Parse(string str)
        {
            int x;
            int y;
            Guid dimensionId;

            const int xStart = 2;
            int xEnd = str.IndexOf(',');
            x = Convert.ToInt32(str.Substring(xStart, xEnd - xStart));

            int yStart = xEnd + 2;
            int yEnd = str.IndexOf(',', yStart) - 2;
            y = Convert.ToInt32(str.Substring(yStart, yEnd - yStart));

            int dimensionStart = yEnd + 2;
            int dimensionEnd = str.Length - 2;
            dimensionId = Guid.Parse(str.Substring(dimensionStart, dimensionEnd));

            return new Point3D(x, y, dimensionId);
        }

        public override string ToString()
        {
            return "{ " + this.X.ToString(CultureInfo.InvariantCulture) + ", " + this.Y.ToString(CultureInfo.InvariantCulture) + ", " + this.DimensionId.ToString() + " }";
        }

        public virtual bool Equals(Point2D other)
        {
            return other.X == this.X && other.Y == this.Y;
        }

        public static Point3D From2D(Point2D point, Guid dimensionId)
        {
            return new Point3D(point.X, point.Y, dimensionId);
        }

        public static List<Point3D> From2D(IEnumerable<Point2D> points, Guid dimensionId)
        {
            List<Point3D> points3D = new List<Point3D>();

            foreach (Point2D item in points)
            {
                points3D.Add(Point3D.From2D(item, dimensionId));
            }

            return points3D;
        }

        public static Point2D To2D(Point3D point)
        {
            return new Point2D(point.X, point.Y);
        }

        public static List<Point2D> To2D(IEnumerable<Point3D> points)
        {
            List<Point2D> points2D = new List<Point2D>();

            foreach (Point3D item in points)
            {
                points2D.Add(Point3D.To2D(item));
            }

            return points2D;
        }

        public int CompareTo(Point3D other)
        {
            int xComparison = other.X.CompareTo(this.X);

            if (xComparison == 0)
            {
                int yComparison = other.Y.CompareTo(this.Y);

                if (yComparison == 0)
                {
                    return other.DimensionId.CompareTo(this.DimensionId);
                }
                else
                {
                    return yComparison;
                }
            }
            else
            {
                return xComparison;
            }
        }

        public bool Equals(Point3D other)
        {
            return other.X == this.X && other.Y == this.Y && other.DimensionId.Equals(this.DimensionId);
        }
    }
}