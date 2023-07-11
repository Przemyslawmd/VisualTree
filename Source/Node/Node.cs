
namespace VisualTree
{
    public class Node
    {
        public Node( int key )
        {
            Key = key;
        }
        
        public int Key { get; }
        
        public int Level {  get; set; } = 1;

        public Node Parent { get; set; } 
        public Node Left { get; set; } 
        public Node Right  { get; set; } 

        public int PosHor { get; set; }
        public int PosVer { get; set; }

        public int MatrixRow { get; set; }
        public int MatrixCol { get; set; }

        public bool IsSelected { get; set; }

        public bool IsParent()
        {
            return Parent != null;
        }

        public bool IsLeft()
        {
            return Left != null;
        }

        public bool IsRight()
        {
            return Right != null;
        }

        public bool IsLeaf()
        {
            return Right is null && Left is null;
        }

        public bool AreBothChildren()
        {
            return Left != null && Right != null;
        }

        public NodeColor Color { get; set; } = NodeColor.NONE;
        
        public static bool operator > ( Node nodeA, Node nodeB )
        {
            return nodeA.Key > nodeB.Key;
        }

        public static bool operator < ( Node nodeA, Node nodeB )
        {
            return nodeA.Key < nodeB.Key;
        }
    }    
}

