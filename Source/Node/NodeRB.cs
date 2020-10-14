
namespace VisualTree
{
    class NodeRB : Node
    {
        public NodeRB( int key ) : base( key ) 
        {
            Color = NodeColor.RED;
        }

        public void ChangeColor()
        {
            Color = Color == NodeColor.BLACK ? NodeColor.RED : NodeColor.BLACK;
        }
    }
}

