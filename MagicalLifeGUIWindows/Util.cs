﻿using MagicalLifeAPI.DataTypes;
using MagicalLifeAPI.World;
using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MagicalLifeGUIWindows
{
    /// <summary>
    /// Various utilities.
    /// </summary>
    public static class Util
    {
        /// <summary>
        /// Returns the in game tile coordinates of the specified point on the screen.
        /// </summary>
        /// <param name="x"></param>
        /// <param name="y"></param>
        /// <returns></returns>
        public static Point GetMapLocation(int x, int y)
        {
            int x2 = x + Rendering.RenderingPipe.XViewOffset;
            int y2 = y + Rendering.RenderingPipe.YViewOffset;
            Point size = Tile.GetTileSize();

            x2 /= size.X;
            y2 /= size.Y;

            return new Point(x2, y2);
        }
    }
}