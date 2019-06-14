
namespace VisualTree
{
    class Settings
    {
        public void SetDiameter( int diameter )
        {
            Diameter = diameter;
        }
        

        public void SetRemoveDuplicatedNodes( bool remove )
        {
            RemoveDuplicatedNodes = remove;
        }
        

        public static int Diameter {  get; private set; } = 30;

        public static bool RemoveDuplicatedNodes {  get; private set; } = true;
    }
}

