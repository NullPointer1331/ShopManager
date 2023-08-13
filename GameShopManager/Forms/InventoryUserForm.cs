﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GameShopManager.Forms
{
    public partial class InventoryUserForm : Form
    {
        NavigationForm navigationForm;
        public InventoryUserForm()
        {
            InitializeComponent();
        }
        public InventoryUserForm(NavigationForm navigationForm)
        {
            this.navigationForm = navigationForm;
            InitializeComponent();
        }
        private void InventoryUserForm_Load(object sender, EventArgs e)
        {
            listBox1.Items.Clear();
            foreach (var combinedObject in navigationForm.Inventory)
            {
                //Add each new item
                if (!listBox1.Items.Contains(combinedObject.Item.ItemName))
                {
                    listBox1.Items.Add(combinedObject.Item.ItemName + " Quantity: " + combinedObject.Quantity);
                }         
            }
            
        }
    }
}
