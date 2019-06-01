
namespace VisualTree
{
    class Node
    {
        public Node( int key )
        {
            this.Key = key;
        }
        
        public int Key { get; }
        
        public Node Parent { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }

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

        public int PosHor{ get; set; }
        public int PosVer{ get; set; }

        public int MatrixRow{ get; set; }
        public int MatrixCol{ get; set; }

        bool isSelected;
    }    
}

