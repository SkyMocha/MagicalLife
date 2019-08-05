﻿using System;
using System.Collections.Generic;
using MLAPI.DataTypes;
using MLAPI.Entity.AI.Task.Qualifications;
using MLAPI.Error.InternalExceptions;
using MLAPI.GameRegistry.Items;
using MLAPI.World.Base;
using ProtoBuf;

namespace MLAPI.Entity.AI.Task.Tasks
{
    /// <summary>
    /// Has the character pick up the item specified.
    /// Reserves the item that is to be picked up.
    /// </summary>
    [ProtoContract]
    public class GrabSpecificItemTask : MagicalTask
    {
        /// <summary>
        /// The task used to move to the nearest item.
        /// </summary>
        [ProtoMember(1)]
        private MoveTask Move;

        [ProtoMember(2)]
        private bool MoveTaskCompleted;

        [ProtoMember(3)]
        protected Point3D ReservedItemLocation;

        private object SyncObject = new object();

        public GrabSpecificItemTask(Guid boundID, Point3D itemLocation)
            : base(Dependencies.CreateEmpty(), boundID, GetQualifications(),
                  PriorityLayers.Default)
        {
            this.MoveTaskCompleted = false;
            this.ReserveItem(itemLocation);
            this.ReservedItemLocation = itemLocation;
        }

        protected GrabSpecificItemTask()
        {
            //Protobuf-net constructor
        }

        private void ReserveItem(Point3D itemLocation)
        {
            Tile containing = World.Data.World.GetTile(itemLocation.DimensionID, itemLocation.X, itemLocation.Y);

            Item item = containing.MainObject as Item;
            if (item != null && item.ReservedID == Guid.Empty)
            {
                item.ReservedID = this.ID;
            }
            else
            {
                throw new UnexpectedStateException("An item was unexpectedly reserved");
            }
        }

        private static List<Qualification> GetQualifications()
        {
            return new List<Qualification>
            {
                new CanMoveQualification(),
            };
        }

        public override void MakePreparations(Living l)
        {
            //Setup the task to move to the reserved item
            this.Move = new MoveTask(this.BoundID, this.ReservedItemLocation);
            this.Move.Completed += this.Move_Completed;
            this.Move.MakePreparations(l);
        }

        private void Move_Completed(MagicalTask task)
        {
            this.MoveTaskCompleted = true;
        }

        public override void Reset()
        {
            //This method is not in use yet
        }

        public override void Tick(Living l)
        {
            lock (this.SyncObject)
            {
                if (this.MoveTaskCompleted)
                {
                    //Pick it up
                    Item pickedUp = ItemRemover.RemoveAllItems(this.ReservedItemLocation);
                    pickedUp.ReservedID = Guid.Empty;
                    l.Inventory.AddItem(pickedUp);
                    this.CompleteTask();
                }
                else
                {
                    //Move closer to it
                    this.Move.Tick(l);
                }
            }
        }

        public override bool CreateDependencies(Living l)
        {
            return true;
        }
    }
}