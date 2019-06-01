
namespace VisualTree
{
    class TreeBST : Tree
    {
        override public void AddNode( int key )
        {
            Node node = new Node( key );
            InsertNode( node );
        }        
    }
}

