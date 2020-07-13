﻿using MLAPI.DataTypes;
using MLAPI.Visual.Rendering.Map;
using ProtoBuf;

namespace MLAPI.Visual.Rendering
{
    /// <summary>
    /// Used to offset a texture by a certain amount.
    /// </summary>
    [ProtoContract]
    public class OffsetTexture : AbstractVisual
    {
        /// <summary>
        /// The texture that is being offset.
        /// </summary>
        [ProtoMember(1)]
        private AbstractVisual Texture;

        [ProtoMember(2)]
        private int XOffset;

        [ProtoMember(3)]
        private int YOffset;

        /// <param name="priority"></param>
        /// <param name="offsetTexture">The texture that will be offset.</param>
        /// <param name="xOffset">The amount to offset the texture by.</param>
        /// <param name="yOffset">The amount to offset the texture by.</param>
        public OffsetTexture(int priority, AbstractVisual offsetTexture, int xOffset, int yOffset) : base(priority)
        {
            this.Texture = offsetTexture;
            this.XOffset = xOffset;
            this.YOffset = yOffset;
        }

        protected OffsetTexture()
        {
        }

        public override void Render(MapBatch batch, Point2D screenTopLeft)
        {
            this.Texture.Render(batch, new Point2D(screenTopLeft.X + this.XOffset, screenTopLeft.Y + this.YOffset));
        }
    }
}