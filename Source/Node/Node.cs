
namespace VisualTree
{
    class Node
    {
        public Node( int key )
        {
            Key = key;
            left = null;
            right = null;
            parent = null;
        }
        
        public int Key { get; }
        
        private Node parent;
        public Node Parent 
        { 
            get { return parent; }
            set { parent = value; }
        }
        
        private Node left;
        public Node Left 
        {  
            get { return left; }
            set { left = value; }
        }
        
        private Node right;
        public Node Right 
        { 
            get { return right; } 
            set { right = value; } 
        }

        public bool IsParent()
        {
            return Parent != null;
        }

        public bool IsLeft()
        {
            return left is null ? false : true;
        }

        public bool IsRight()
        {
            return right is null ? false : true;
        }

        public int PosHor{ get; set; }
        public int PosVer{ get; set; }

        public int MatrixRow{ get; set; }
        public int MatrixCol{ get; set; }

        bool isSelected;
    }    
}

