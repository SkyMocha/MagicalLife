﻿using System;
using MLAPI.DataTypes.Collection;
using MLAPI.World.Data;

namespace MLAPI.World.Generation.Dungeon
{
    /// <summary>
    /// Implementers of this generate blank/empty rooms to be populated by other generators.
    /// This step happens after hallways are generated. 
    /// </summary>
    public abstract class RoomGenerator
    {
        public abstract ProtoArray<Chunk> GenerateEmptyRoom(ProtoArray<Chunk> blankChunks, string dimensionName, Random random);
    }
}