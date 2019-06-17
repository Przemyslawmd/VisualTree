
namespace VisualTree
{
    class NodeAVL : Node
    {
        NodeAVL( int key ) : base( key ) { }

        public int Level {  get; set; } = 1;
    }
}

