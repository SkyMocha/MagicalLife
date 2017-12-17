﻿namespace EarthMagicItems.Ammo.Arrows.Normal
{
    using EarthWithMagicAPI.API.Util;

    /// <summary>
    /// A +5 arrow item.
    /// </summary>
    public class _5Arrow : GenericAmmo
    {
        public _5Arrow()
            : base(new Die(6, 7, 0), "+5 Arrow", AmmoUtil.StandardArrow(new Die(1, 8, 5)), "EarthMagicDocumentation.ASCII_Art.Items.Ammo.Arrows.ProfessionalArrow.txt",
            "EarthMagicDocumentation.Items.Ammo.Arrows.+5Arrow.md", .2)
        {
        }
    }
}