﻿using ProtoBuf;
using RBush;

namespace MagicalLifeAPI.DataTypes
{
    [ProtoContract]
    public class Point2D : ISpatialData
    {
        [ProtoMember(1)]
        public int X { get; set; }

        [ProtoMember(2)]
        public int Y { get; set; }

        protected Envelope _Envelope;

        public Point2D(int x, int y)
        {
            this.X = x;
            this.Y = y;
            this._Envelope = new Envelope(x, y, x, y);
        }

        public Point2D()
        {
            this._Envelope = new Envelope(this.X, this.Y, this.X, this.Y);
        }

        public ref readonly Envelope Envelope
        {
            get
            {
                return ref this._Envelope;
            }
        }
    }
}