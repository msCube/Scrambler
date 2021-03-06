﻿using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Shapes;

namespace Scrambler.View
{
    //internal static class LocalExtensions
    //{
    //    public static void ForWindowFromTemplate(this object templateFrameworkElement, Action<Window> action)
    //    {
    //        Window window = ((FrameworkElement)templateFrameworkElement).TemplatedParent as Window;
    //        if (window != null) action(window); 
    //    }

    //    public static void ForTextBoxFromTemplate(this object templateFrameworkElement, Action<TextBox> action)
    //    {
    //        TextBox textBox = ((FrameworkElement)templateFrameworkElement).TemplatedParent as TextBox;
    //        action(textBox);
    //    }
    //}

    public partial class FlatWindow
    {
        void TopBar_MouseDown(object sender, MouseButtonEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.DragMove());
        }

        void btnClose_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.Close());
        }

        void btnMinimize_Click(object sender, RoutedEventArgs e)
        {
            sender.ForWindowFromTemplate(w => w.WindowState = System.Windows.WindowState.Minimized);
        }
        
        private void ComboGalleryWindow_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            //sender.ForWindowFromTemplate(w => w.Closing -= ComboGalleryWindow_Closing);
            //sender.ForWindowFromTemplate(w => w.OnClosing());
            //e.Cancel = true;
            //var anim = new DoubleAnimation(0, (Duration)TimeSpan.FromMilliseconds(250));
            //anim.Completed += (s, _) => this.Close();
            //this.BeginAnimation(UIElement.OpacityProperty, anim);
        }

        #region WinResizeble
        bool ResizeInProcess = false;
        private void Resize_Init(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = true;
                senderRect.CaptureMouse();
            }
        }

        private void Resize_End(object sender, MouseButtonEventArgs e)
        {
            Rectangle senderRect = sender as Rectangle;
            if (senderRect != null)
            {
                ResizeInProcess = false; ;
                senderRect.ReleaseMouseCapture();
            }
        }

        private void Resizeing_Form(object sender, MouseEventArgs e)
        {
            if (ResizeInProcess)
            {
                Rectangle senderRect = sender as Rectangle;
                Window mainWindow = senderRect.Tag as Window;
                if (senderRect != null)
                {
                    double width = e.GetPosition(mainWindow).X;
                    double height = e.GetPosition(mainWindow).Y;
                    senderRect.CaptureMouse();
                    if (senderRect.Name.ToLower().Contains("right"))
                    {
                        width += 5;
                        if (width > 0)
                            mainWindow.Width = width;
                    }
                    if (senderRect.Name.ToLower().Contains("left"))
                    {
                        width -= 5;
                        mainWindow.Left += width;
                        width = mainWindow.Width - width;
                        if (width > 0)
                        {
                            mainWindow.Width = width;
                        }
                    }
                    if (senderRect.Name.ToLower().Contains("bottom"))
                    {
                        height += 5;
                        if (height > 0)
                            mainWindow.Height = height;
                    }
                    if (senderRect.Name.ToLower().Contains("top"))
                    {
                        height -= 5;
                        mainWindow.Top += height;
                        height = mainWindow.Height - height;
                        if (height > 0)
                        {
                            mainWindow.Height = height;
                        }
                    }
                }
            }
        }
        #endregion
    }
}
