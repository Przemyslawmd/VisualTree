
namespace VisualTree
{
    class NodeAVL : Node
    {
        public NodeAVL( int key ) : base( key ) { }

        public new NodeAVL Parent { get; set; } 
        public new NodeAVL Left   { get; set; } 
        public new NodeAVL Right  { get; set; } 
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public bool VerifyAVL()
        {
            int leftLevel = IsLeft() ? Left.Level : 0;
            int rightLevel = IsRight() ? Right.Level : 0;

            return System.Math.Abs( leftLevel - rightLevel ) <= 1;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
         
        public int Level {  get; set; } = 1;
    }
}

