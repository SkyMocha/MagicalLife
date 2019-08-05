﻿using System;
using System.Collections.Generic;
using MLAPI.DataTypes;
using MLAPI.World.Base;
using ProtoBuf;

namespace MLAPI.Components.Resource
{
    /// <summary>
    /// Anything that implements this must describe its behavior in terms of mining.
    /// </summary>
    [ProtoContract]
    public abstract class ComponentHarvestable : Component
    {
        /// <summary>
        /// The total percentage of the IMinable that has been mined so far.
        /// </summary>
        [ProtoMember(1)]
        public double PercentHarvested { get; private set; }

        protected ComponentHarvestable()
        {
            //Protobuf-net constructor
        }

        public Type GetBaseType()
        {
            return typeof(ComponentHarvestable);
        }

        /// <summary>
        /// Called when the object is harvested completely.
        /// </summary>
        /// <returns>Any items that should be dropped when completely harvested. Return null to drop nothing.</returns>
        public abstract List<Item> Harvested(Point2D position);

        /// <summary>
        /// Called anytime the object is in the process of being harvested, but not totally harvested.
        /// Should the object be completely harvested in one step, this is called with 100%, then <see cref="Harvested"/> is called.
        /// </summary>
        /// <param name="percentHarvested"></param>
        /// <returns>Any items that should be dropped due to the progress in harvesting the object. Return null to drop nothing.</returns>
        public List<Item> HarvestSomePercent(double percentHarvested, Point2D position)
        {
            this.PercentHarvested += percentHarvested;

            if (this.PercentHarvested < 1)
            {
                return this.HarvestPercent(this.PercentHarvested, position);
            }
            else
            {
                return this.Harvested(position);
            }
        }

        /// <summary>
        /// Returns the amount harvested so far.
        /// </summary>
        /// <param name="percent">The total percent harvested so far.</param>
        /// <returns></returns>
        protected abstract List<Item> HarvestPercent(double percent, Point2D position);
    }
}