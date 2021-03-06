﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Scrambler.Strategies
{
    [Attributes.Strategy(Type=typeof(Cyphers.Blowfish))]
    public class BlowfishSt : Strategy
    {
        Grid grid;
        TextBox txtKey;
        Label label;
        Label labelKeySize;

        public BlowfishSt()
        {
            grid = new Grid();
            txtKey = new TextBox();
            txtKey.Name = "txtKey";
            txtKey.Width = 100;
            txtKey.Height = 23;
            txtKey.Text = "";
            txtKey.HorizontalAlignment = HorizontalAlignment.Right;
            txtKey.TextChanged += TxtKey_TextChanged;
            txtKey.BorderThickness = new Thickness(2.0);

            label = new Label();
            label.Name = "label";
            label.Content = "Enter the key:";
            label.HorizontalAlignment = HorizontalAlignment.Left;
            label.VerticalAlignment = VerticalAlignment.Top;

            labelKeySize = new Label();
            labelKeySize.Name = "labelKeySize";
            labelKeySize.Content = "Key size: ";
            labelKeySize.HorizontalAlignment = HorizontalAlignment.Left;
            labelKeySize.VerticalAlignment = VerticalAlignment.Top;

            grid.Children.Add(txtKey);
            grid.Children.Add(label);
        }

        private void TxtKey_TextChanged(object sender, TextChangedEventArgs e)
        {
            labelKeySize.Content = "Key size: " + Mathematics.Converter.ToBits(txtKey.Text).Length;
            int txtSize = Mathematics.Converter.ToBits(txtKey.Text).Length;
            if (txtSize < 32 && txtSize > 448)
                txtKey.Foreground = Brushes.Red;
            else
                txtKey.Foreground = Brushes.Green;
        }

        public override void AddElements(StackPanel parent)
        {
            parent.Children.Add(grid);
            parent.Children.Add(labelKeySize);
        }

        public override string Decrypt(string text)
        {
            return Cypher.Decrypt(text);
        }

        public override void DeleteElements(StackPanel parent)
        {
            parent.Children.Remove(grid);
            parent.Children.Remove(labelKeySize);
        }

        public override string Encrypt(string text)
        {
            createNewCypher();
            return Cypher.Encrypt(text);
        }

        protected override void createNewCypher()
        {
            try
            {
                Cypher = new Cyphers.Blowfish(txtKey.Text);
            }
            catch (FormatException e)
            {
                System.Windows.Forms.MessageBox.Show(e.Message, "Key is invalid!");
            }
        }
    }
}
