
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

            this.tree = tree;
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

        public void StepForward()
        {	
            if ( stepNumber == Steps.Count )
            {
                return;
            }

            switch ( Steps[stepNumber].ActionType )
            {
                case ActionType.ADD:
                    return;
                case ActionType.REMOVE:
                    return;
                case ActionType.ROTATION:
                    StepRotation();
                    stepNumber++;
                    return;
            }	
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void StepBackward()
        { 
            if ( stepNumber == 0 )
            {
                return;
            }
    
            stepNumber--;
            switch ( Steps[stepNumber].ActionType )
            {
                case ActionType.ADD:
                    return;
                case ActionType.REMOVE:
                    return;
                case ActionType.ROTATION:
                    StepRotation();
                    return;
            }	
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private StepMode()
        {
            Steps = new List< Action >();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void StepRotation()
        {
            Node parent = Steps[stepNumber].Node.Parent;
            tree.RotateNode( Steps[stepNumber].Node );
            tree.RestoreRoot();
            Steps[stepNumber].Node = parent;
        }
            
        /*******************************************************************************************/
        /*******************************************************************************************/

        private static StepMode stepMode; 
        
        public List< Action > Steps {  get; }
        private int stepNumber;
        private Tree tree;
    }
}

