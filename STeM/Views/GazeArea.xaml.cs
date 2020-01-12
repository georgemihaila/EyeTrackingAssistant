using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace STeM.Views
{
    /// <summary>
    /// Interaction logic for GazeArea.xaml
    /// </summary>
    public partial class GazeArea : UserControl
    {
        private readonly bool _hiddenUntilLocked;
        private bool _positionLocked = false;

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="GazeArea"/> should be locked in place.
        /// </summary>
        public bool PositionLocked
        {
            get
            {
                return _positionLocked;
            }
            set
            {
                _positionLocked = value;
                if (value)
                {
                    Locked?.Invoke(this, new EventArgs());
                }
                else
                {
                    Unlocked?.Invoke(this, new EventArgs());
                }
            }
        }

        private event EventHandler Locked;
        private event EventHandler Unlocked;

        public GazeArea(bool hiddenUntilLocked = true)
        {
            _hiddenUntilLocked = hiddenUntilLocked;

            InitializeComponent();

            Locked += GazeArea_Locked;
            Unlocked += GazeArea_Unlocked;

            if (_hiddenUntilLocked)
            {
                Border.BorderBrush = Brushes.Transparent;
            }
        }

        private void GazeArea_Unlocked(object sender, EventArgs e)
        {
            if (_hiddenUntilLocked)
            {
                Border.BorderBrush = Brushes.Transparent;
            }
        }

        private void GazeArea_Locked(object sender, EventArgs e)
        {
            if (_hiddenUntilLocked)
            {
                Border.BorderBrush = Brushes.Red;
            }
        }
    }
}
