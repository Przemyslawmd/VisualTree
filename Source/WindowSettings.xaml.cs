
using System;
using System.Windows;

namespace VisualTree
{
    public partial class WindowSettings : Window
    {
        public WindowSettings()
        {
            InitializeComponent();
            TextDiameter.Text = Settings.Diameter.ToString();
            CheckToolTips.IsChecked = Settings.MenuPanelToolTips;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void ActionAccept( object sender, RoutedEventArgs e )
        {
            int diameter;
            
            try 
            { 
                diameter = Int32.Parse( TextDiameter.Text );
            }
            catch( FormatException )
            {
                MessageBox.Show( "Improper data for a diameter" );
                return;
            }
            
            if ( Settings.SetDiameter( diameter ) is false )
            {
                MessageBox.Show( "Diameter value must be between 5 and 100" );
                return;
            }

            Settings.MenuPanelToolTips = CheckToolTips.IsChecked == true;
            Close();
        }
    }
}

