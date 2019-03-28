﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.Sound;
using MagicalLifeAPI.World.Base;
using ProtoBuf;
using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;

namespace MagicalLifeAPI.Components.Resource
{
    [ProtoContract]
    public class TillablePercentDone : ComponentTillable
    {
        public TillablePercentDone()
        {
        }

        protected override List<Item> TillPercent(float percentMined, Point2D position)
        {
            FMODUtil.RaiseEvent(SoundsTable.PickaxeHit, "", 0, position);
            return null;
        }
    }
}