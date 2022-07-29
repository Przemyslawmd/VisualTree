
namespace VisualTree
{
    public static class TreeServices
    {
        public static void Start()
        {
            Model = new Model();
            Selection = new Selection();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void Stop()
        {
            Model = null;
            Selection = null;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static Model Model { get; private set; }
        public static Selection Selection {  get; private set; }
    }
}

