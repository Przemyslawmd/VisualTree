
namespace VisualTree
{
    class NodeAVL : Node
    {
        public NodeAVL( int key ) : base( key ) { }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public bool VerifyAVL()
        {
               
            int leftLevel = Left is null ? 0 : (( NodeAVL ) Left ).Level;
            int rightLevel = Right is null ? 0 : (( NodeAVL ) Right ).Level;


            if ( Left is null )
            {
                leftLevel = 0;
            }
            else
            {
                leftLevel = ((NodeAVL)Left).Level;
            }

            return System.Math.Abs( leftLevel - rightLevel ) <= 1;
        }

        public void SetLevelAVL()
        {
            if ( Right is null && Left is null )
            {
                Level = 1;
                return;
            }

             int leftLevel = Left is null ? 0 : (( NodeAVL ) Left ).Level;
            int rightLevel = Right is null ? 0 : (( NodeAVL ) Right ).Level;

            Level = ( leftLevel > rightLevel ) ? leftLevel + 1 : rightLevel + 1;
        }
        

        public void IncLevelAVL( NodeAVL child )
        {
            if ( child is null)
            {
                Level = 1;
                return;
            }

            if ( (child.Level + 1) > Level )
            {
                Level = child.Level + 1;
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void UpdateLevel()
        {
            int leftLevel = LeftAVL ?. Level ?? 0;
            
            int rightLevel = Right is null ? 0 : RightAVL.Level;

            if ( leftLevel is 0 && rightLevel is 0 )
            {
                Level = 1;
            }
            else
            {
                Level = ( leftLevel > rightLevel ) ? leftLevel + 1 : rightLevel + 1;
            }
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public NodeAVL LeftAVL {  get {  return ( NodeAVL ) Left; } set { Left = value; } }
        public NodeAVL RightAVL {  get {  return ( NodeAVL ) Right; } }
    }
}

