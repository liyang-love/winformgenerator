//------------------------------------------------------
// 
// Copyright - (c) - 2014 - Mille Boström 
//
//------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Microsoft.Xna.Framework;

namespace WinFormGenerator
{
    public partial class ClassForm : Form
    {
        public object Object { get; set; }

        public Type ObjectType { get; set; }
        public List<int> ListOfIndex { get; set; }

        public ClassForm(Type objectType, object @object = null)
        {
            InitializeComponent();
            Object = @object;
            ObjectType = objectType;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            if (Object == null && !ObjectType.IsValueType && ObjectType != typeof(string))
            {
                Object = Activator.CreateInstance(ObjectType);
            }

            if (ObjectType == typeof (Vector2))
            {
                var panel = Controls[2]; 
                Object = new Vector2((float)((NumericUpDown)panel.Controls[2]).Value,
                            (float)((NumericUpDown)panel.Controls[3]).Value);
                return; 
            }

            if (ObjectType.IsValueType || ObjectType == typeof(string))
            {
                Object = Controls[2].Text;
                return;
            }
           
            var properties = ObjectType.GetProperties();
            int index = 0;
            for (int n = 0; n + 3 < Controls.Count; n += 2)
            {
                if (Controls[n + 3] is ComboBox)
                {
                    var combobox = Controls[n + 3] as ComboBox;
                    properties[ListOfIndex[index]].SetValue(Object,
                        Enum.Parse(properties[ListOfIndex[index]].PropertyType, combobox.SelectedItem.ToString()));
                }
                else if (Controls[n + 3] is CheckBox)
                {
                    var checkbox = Controls[n + 3] as CheckBox;
                    properties[ListOfIndex[index]].SetValue(Object, checkbox.Checked);
                }
                else if (Controls[n + 3] is Button)
                {
                    //Do nothing
                }
                else if (Controls[n + 3] is Panel)
                {
                    var panel = (Panel) Controls[n + 3];
                    if (panel.Name == "vector2")
                    {
                        var vector = new Vector2((float) ((NumericUpDown) panel.Controls[2]).Value,
                            (float) ((NumericUpDown) panel.Controls[3]).Value);
                        properties[ListOfIndex[index]].SetValue(Object, vector);
                    }
                }
                else
                {
                    if (!string.IsNullOrEmpty(Controls[n + 3].Text))
                        properties[ListOfIndex[index]].SetValue(Object, Convert.ChangeType(Controls[n + 3].Text, properties[ListOfIndex[index]].PropertyType));
                }
                index++;
            }
        }
    }
}
