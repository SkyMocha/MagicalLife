﻿using System.Collections;
using System.Collections.Generic;

namespace MLAPI.DataTypes.Collection
{
    /// <summary>
    /// A 2D array wrapper that is compatible with Protobuf-net.
    /// </summary>
    [ProtoBuf.ProtoContract(IgnoreListHandling = true)]
    public class ProtoArray<T> : IEnumerable<T>
    {
        /// <summary>
        /// The width of this array.
        /// </summary>
        [ProtoBuf.ProtoMember(1)]
        public int Width { get; private set; }

        /// <summary>
        /// The height of this array.
        /// </summary>
        [ProtoBuf.ProtoMember(2)]
        public int Height { get; private set; }

        /// <summary>
        /// The actual data this array holds.
        /// </summary>
        [ProtoBuf.ProtoMember(3)]
        public T[] Data { get; set; }

        /*
         *     [0, 1, 2, 3]
         *     [4, 5, 6, 7]
         *     8
         */

        public ProtoArray(int width, int height)
        {
            this.Width = width;
            this.Height = height;
            this.Data = new T[width * height];
        }

        public ProtoArray(int width, int height, T[] data) : this(width, height)
        {
            this.Data = data;
        }

        public ProtoArray()
        {
        }

        public T this[int x, int y]
        {
            get
            {
                int index = (x * this.Height) + y;
                return this.Data[index];
            }

            set
            {
                int index = (x * this.Height) + y;
                this.Data[index] = value;
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator()
        {
            foreach (T item in this.Data)
            {
                yield return item;
            }
        }

        public IEnumerator GetEnumerator()
        {
            return this.Data.GetEnumerator();
        }
    }
}