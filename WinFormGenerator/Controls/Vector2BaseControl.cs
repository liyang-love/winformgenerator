//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;
using System.Reflection;
using System.Windows.Forms;
using Microsoft.Xna.Framework;
using Point = System.Drawing.Point;

namespace WinFormGenerator.Controls
{
    class Vector2BaseControl : BaseControl
    {
        public Vector2BaseControl(object baseObject, Config config) : base(baseObject, config)
        {
        }

        public override Control GetControl(PropertyInfo propertyInfo)
        {
            var vector = propertyInfo.GetValue(BaseObject);
            return GetPanel(vector != null ? (Vector2)vector : new Vector2(0,0));
        }

        public override Control GetControl(Type type)
        {
            return GetPanel(BaseObject != null ? (Vector2)BaseObject : new Vector2()); 
        }

        private Panel GetPanel(Vector2 vector2)
        {
            var panel = new Panel { Width = Config.DefaultControlWidth, Height = 50, Name = "vector2" };
            panel.Controls.Add(new Label { Text = "X", Width = 20, Location = new Point(0, 0) });
            panel.Controls.Add(new Label { Text = "Y", Width = 20, Location = new Point(0, 30) });
            panel.Controls.Add(new NumericUpDown
            {
                Name = "nmrX",
                DecimalPlaces = 1,
                Value = (decimal)vector2.X,
                Location = new Point(25, 0),
                Width = 155          
            });
            panel.Controls.Add(new NumericUpDown
            {
                Name = "nmrY",
                DecimalPlaces = 1,
                Value = (decimal)vector2.Y,
                Location = new Point(25, 30),
                Width = 155
            });
            return panel;
        }
    }
}
