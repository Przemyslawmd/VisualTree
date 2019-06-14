﻿
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
        

        public static void SetRemoveDuplicatedNodes( bool remove )
        {
            RemoveDuplicatedNodes = remove;
        }
        

        public static int Diameter {  get; private set; } = 30;

        public static bool RemoveDuplicatedNodes {  get; private set; } = true;

        private static readonly int MaxDiameter = 100;
        private static readonly int MinDiameter = 5;
    }
}
