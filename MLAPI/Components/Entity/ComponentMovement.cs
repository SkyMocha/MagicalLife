﻿using MLAPI.DataTypes;
using MLAPI.DataTypes.Attribute;
using MLAPI.DataTypes.Collection;
using MLAPI.Pathfinding;
using MLAPI.Util.Reusable;
using ProtoBuf;

namespace MLAPI.Components.Entity
{
    [ProtoContract]
    public class ComponentMovement : Component
    {
        /// <summary>
        /// The ticks in between each footstep.
        /// </summary>
        private static readonly int FootStepCooldown = 5;

        /// <summary>
        /// A queue that holds the queued movement steps up for this living creature.
        /// </summary>
        [ProtoMember(1)]
        public ProtoQueue<PathLink> QueuedMovement { get; set; }

        /// <summary>
        /// How fast this creature can during a single tick.
        /// </summary>
        [ProtoMember(2)]
        public AttributeDouble Movement { get; set; }

        /// <summary>
        /// The location of the creature on the screen. This represents the progress through a tile for a moving creature.
        /// </summary>
        [ProtoMember(3)]
        public Point2DDouble TileLocation { get; set; }

        [ProtoMember(4)]
        public TickTimer FootStepTimer { get; set; }

        public ComponentMovement(double movementSpeed, Point2D location)
        {
            this.QueuedMovement = new ProtoQueue<PathLink>();
            this.Movement = new AttributeDouble(movementSpeed);
            this.TileLocation = new Point2DDouble(location.X, location.Y);
            this.FootStepTimer = new TickTimer(FootStepCooldown);
        }

        protected ComponentMovement()
        {
            //Protobuf-net constructor.
        }

        [ProtoAfterDeserialization]
#pragma warning disable IDE0051 // Remove unused private members
        private void PostDeserialization()
#pragma warning restore IDE0051 // Remove unused private members
        {
            if (this.QueuedMovement == null)
            {
                this.QueuedMovement = new ProtoQueue<PathLink>();
            }
        }
    }
}