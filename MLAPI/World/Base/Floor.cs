﻿using MagicalLifeAPI.Components.Generic.Renderable;
using MagicalLifeAPI.GUI;
using ProtoBuf;

namespace MagicalLifeAPI.World.Base
{
    /// <summary>
    /// Represents the floor of a tile.
    /// </summary>
    [ProtoContract]
    public class Floor : GameObject
    {
        [ProtoMember(1)]
        private bool Walkable { get; set; }

        /// <param name="visual">The visual representation of this floor to render.</param>
        /// <param name="walkable">If true it is possible to walk on this tile.</param>
        public Floor(AbstractVisual visual, bool walkable)
            : base(true)
        {
            ComponentHasTexture textureComponent = new ComponentHasTexture(false);

            textureComponent.Visuals.Add(visual);
            this.AddComponent(textureComponent);
            this.Walkable = walkable;
        }

        protected Floor()
        {
            //Protobuf-net constructor.
        }

        public override bool IsWalkable()
        {
            return this.Walkable;
        }
    }
}