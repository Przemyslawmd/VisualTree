
namespace VisualTree
{
    class Settings
    {
        public static bool SetDiameter( int diameter )
        {
            if ( diameter > MaxDiameter || diameter < MinDiameter )
            {
                return false;
            }
             
            Diameter = diameter;
            return true;
        }

        public static void SetTreeType( TreeType type )
        {
            TreeType = type;
        }
               
        public static int Diameter {  get; private set; } = 30;

        public static bool RemoveDuplicatedNodes {  get; set; } = true;

        public static TreeType TreeType {  get; private set; } = TreeType.CommonBST; 
        
        public static bool Notifications {  get; set; } = true;

        public static bool MenuPanelToolTips {  get; set; } = true;

        private static readonly int MaxDiameter = 100;
        private static readonly int MinDiameter = 5;
    }
}

