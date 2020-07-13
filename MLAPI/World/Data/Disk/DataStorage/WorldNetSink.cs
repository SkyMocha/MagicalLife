﻿using System;
using System.Collections.Generic;
using System.Net.Sockets;
using MLAPI.GameRegistry.Items;
using MLAPI.Networking.Messages;
using MLAPI.Networking.Serialization;

namespace MLAPI.World.Data.Disk.DataStorage
{
    /// <summary>
    /// Used to send the parts of the world to the client piece by piece.
    /// This sink was meant to be used by the server only.
    /// </summary>
    public class WorldNetSink : AbstractWorldSink
    {
        private readonly Socket Client;

        public WorldNetSink(Socket client)
        {
            this.Client = client;
        }

        private void Send(BaseMessage msg)
        {
            byte[] data = ProtoUtil.Serialize(msg);
            this.Client.Send(data);
        }

        public override void Receive<T>(T data, string filePath, Guid dimensionId)
        {
            switch (data)
            {
                case Chunk chunk:
                    this.Send(new WorldTransferBodyMessage(chunk, dimensionId));
                    break;

                case List<DimensionHeader> headers:
                    this.Send(new WorldTransferHeaderMessage(headers));
                    break;

                case ItemRegistry registry:
                    this.Send(new WorldTransferRegistryMessage(registry, dimensionId));
                    break;

                case DimensionHeader header:
                    WorldDiskSink sink = new WorldDiskSink();
                    sink.Receive(header, filePath, dimensionId);
                    break;

                default:
                    throw new InvalidOperationException("Unexpected type for data: " + data.GetType().ToString());
            }
        }
    }
}