﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Filing.Logging;
using MagicalLifeAPI.World;
using MagicalLifeGUIWindows.GUI.MainMenu;
using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.Input;
using MagicalLifeGUIWindows.Rendering.GUI;
using MagicalLifeGUIWindows.Rendering.Map;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using System;
using System.Linq;
using static MagicalLifeGUIWindows.Rendering.Text.SimpleTextRenderer;

namespace MagicalLifeGUIWindows.Rendering
{
    /// <summary>
    /// Handles drawing the entire screen.
    /// </summary>
    public static class RenderingPipe
    {
        /// <summary>
        /// The standard size of the tiles.
        /// </summary>
        public static readonly Microsoft.Xna.Framework.Point tileSize = Tile.GetTileSize();

        /// <summary>
        /// The standard color mask to apply to all tiles.
        /// </summary>
        public static readonly Microsoft.Xna.Framework.Color colorMask = Microsoft.Xna.Framework.Color.White;

        /// <summary>
        /// The x offset of the view due to the player moving the camera around the map.
        /// </summary>
        public static int XViewOffset = 0;

        /// <summary>
        /// The y offset of the view due to the player moving the camera around the map.
        /// </summary>
        public static int YViewOffset = 0;

        /// <summary>
        /// The z level that the player is currently viewing.
        /// </summary>
        public static int ZLevel = 0;

        /// <summary>
        /// Draws the screen.
        /// </summary>
        /// <param name="spBatch"></param>
        public static void DrawScreen(ref SpriteBatch spBatch)
        {
            //MasterLog.DebugWriteLine("Rendering frame");
            if (World.mainWorld != null)
            {
                MapRenderer.DrawMap(ref spBatch);
            }

            DrawGUI(ref spBatch);

            //DrawMouseLocation(ref spBatch);
        }

        public static void DrawMouseLocation(ref SpriteBatch spBatch)
        {
            int x = Mouse.GetState().X;
            int y = Mouse.GetState().Y;
            string mouseLocation = "{ " + x + ", " + y + " }";
            DrawString(MainMenuLayout.MainMenuFont, mouseLocation, new Rectangle(500, 500, 200, 50), Alignment.Center, Color.AliceBlue, ref spBatch);
        }

        /// <summary>
        /// Draws the GUI onto the screen.
        /// </summary>
        /// <param name="spBatch"></param>
        private static void DrawGUI(ref SpriteBatch spBatch)
        {
            DrawContainers(ref spBatch);
        }

        private static void DrawContainers(ref SpriteBatch spBatch)
        {
            foreach (GUIContainer item in Enumerable.Reverse(BoundHandler.GUIWindows))
            {
                if (item.Visible)
                {
                    spBatch.Draw(item.Image, item.DrawingBounds, colorMask);

                    foreach (GUIElement control in item.Controls)
                    {
                        if (control.Visible)
                        {
                            switch (control)
                            {
                                case MonoButton button:
                                    GUIRenderer.DrawButtonInContainer((MonoButton)control, ref spBatch, item);
                                    break;

                                case InputBox textBox:
                                    GUIRenderer.DrawInputBoxInContainer((InputBox)control, ref spBatch, item);
                                    break;
                                case Label label:
                                    GUIRenderer.DrawLabelInContainer((Label)control, ref spBatch, item);
                                    break;
                                default:
                                    //Should probably send out a event or something, to allow someone else to render it.
                                    //TODO:
                                    break;
                            }
                        }
                    }
                }
            }
        }
    }
}