
namespace VisualTree
{
    class Node
    {
        public Node( int key )
        {
            Key = key;
            Left = null;
            Right = null;
            Parent = null;
        }
        
        public int Key { get; }
        
        public Node Parent {  get; set; } 
        public Node Left {  get; set; } 
        public Node Right {  get; set; } 

        public int PosHor{ get; set; }
        public int PosVer{ get; set; }

        public int MatrixRow{ get; set; }
        public int MatrixCol{ get; set; }

        public bool IsSelected { get; set; }

        public bool IsParent()
        {
            return Parent is null ? false : true;
        }

        public bool IsLeft()
        {
            return Left is null ? false : true;
        }

        public bool IsRight()
        {
            return Right is null ? false : true;
        }
    }    
}

