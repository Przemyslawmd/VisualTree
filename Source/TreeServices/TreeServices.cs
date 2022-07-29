
namespace VisualTree
{
    public static class TreeServices
    {
        public static void Start()
        {
            Model = new Model();
            Selection = new Selection();
            StepMode = new StepMode();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void Stop()
        {
            Model = null;
            Selection = null;
            StepMode = null;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static Model Model { get; private set; }
        public static Selection Selection {  get; private set; }
        public static StepMode StepMode {  get; private set;}
    }
}

