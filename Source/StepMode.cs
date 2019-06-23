
using System.Collections.Generic;

namespace VisualTree
{
    class StepMode : IListener
    {
        public static StepMode GetInstance()
        {
            if ( stepMode is null )
            {
                stepMode = new StepMode();
            }
            return stepMode;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void PrepareStepsForTreeBalancing( Tree tree )
        {
            ServiceListener.AddListener( this );
            new DSW().BalanceTree( tree );
            ServiceListener.RemoveListener( this );

            for ( int i = Steps.Count - 1; i >= 0; i-- )
            {
                Node node = Steps[i].Node.Parent;
                tree.RotateNode( Steps[i].Node );
                Steps[i].Node = node;
                tree.RestoreRoot();
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AddAction( ActionType actionType, Node node )
        {
            if ( actionType == ActionType.ROTATION )
            {
                node = node.Parent;
            }
            Steps.Add( new Action( actionType, node ));
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private StepMode()
        {
            Steps = new List< Action >();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private static StepMode stepMode; 
        
        public List< Action > Steps {  get; }
    }
}

