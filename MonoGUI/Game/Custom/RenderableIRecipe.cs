﻿using MagicalLifeAPI.Asset;
using MagicalLifeAPI.Crafting;
using MagicalLifeGUIWindows.GUI.Reusable;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Text;

namespace MonoGUI.Game.Custom
{
    public class RenderableIRecipe : RenderableImage
    {
        private readonly IActionBarItem BarItem;

        public RenderableIRecipe(Rectangle bounds, int textureID, bool isContained, IActionBarItem actionBarItem) : base(bounds, textureID, isContained)
        {
            this.BarItem = actionBarItem;
            this.ClickEvent += this.RenderableIRecipe_ClickEvent;
            this.DoubleClickEvent += this.RenderableIRecipe_ClickEvent;
        }

        private void RenderableIRecipe_ClickEvent(object sender, MagicalLifeGUIWindows.GUI.Reusable.Event.ClickEventArgs e)
        {
            this.BarItem.Clicked();
        }

        public RenderableIRecipe(Rectangle bounds, string image, bool isContained, IActionBarItem actionBarItem) : base(bounds, image, isContained)
        {
            this.BarItem = actionBarItem;
            this.ClickEvent += this.RenderableIRecipe_ClickEvent;
            this.DoubleClickEvent += this.RenderableIRecipe_ClickEvent;
        }

        public override void Render(SpriteBatch spBatch, Rectangle containerBounds)
        {
            base.Render(spBatch, containerBounds);
        }
    }
}
