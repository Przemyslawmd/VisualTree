
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

        public static void Destroy()
        {
            stepMode = null;
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
                BackRotationAfterPrepareSteps( tree, i );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void PrepareStepsForAddNodes( Tree tree, List< int > keys ) 
        {	
            ServiceListener.AddListener( this );
            tree.CreateNodes( keys );
            ServiceListener.RemoveListener( this );

            for ( int i = Steps.Count - 1; i >= 0; i-- )
            {
                if ( Steps[i].ActionTreeType == ActionTreeType.ADD )
                {
                    tree.DetachNode( Steps[i].Node );
                }
                else if ( Steps[i].ActionTreeType == ActionTreeType.ROTATION )
                {
                   BackRotationAfterPrepareSteps( tree, i );
                }
            }	
        }

        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void PrepareStepsForDeleteNodes( Tree tree, List< Node > nodes )
        {	
            ServiceListener.AddListener( this );
            tree.DelSelectedNodes( nodes );	
            ServiceListener.RemoveListener( this );

            for ( int i = Steps.Count - 1; i >= 0; i-- )
            {
                if ( Steps[i].ActionTreeType == ActionTreeType.REMOVE )
                {
                    tree.AttachNode( Steps[i].Node );
                }
                else if ( Steps[i].ActionTreeType == ActionTreeType.ROTATION )
                {			
                    BackRotationAfterPrepareSteps( tree, i );
                }
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AddAction( ActionTreeType actionType, Node node )
        {
            if ( actionType == ActionTreeType.ROTATION )
            {
                node = node.Parent;
            }
            Steps.Add( new ActionTree( actionType, node ));
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void StepForward( Tree tree )
        {	
            if ( stepNumber == Steps.Count )
            {
                return;
            }

            switch ( Steps[stepNumber].ActionTreeType )
            {
                case ActionTreeType.ADD:
                    tree.AttachNode( Steps[ stepNumber ].Node );
                    break;
                case ActionTreeType.REMOVE:
                    tree.DetachNode( Steps[ stepNumber].Node );
                    break;
                case ActionTreeType.ROTATION:
                    TriggerStepRotation( tree );
                    break;
            }
            stepNumber++;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void StepBackward( Tree tree )
        { 
            if ( stepNumber == 0 )
            {
                return;
            }
    
            stepNumber--;
            switch ( Steps[stepNumber].ActionTreeType )
            {
                case ActionTreeType.ADD:
                    tree.DetachNode( Steps[ stepNumber].Node );
                    return;
                case ActionTreeType.REMOVE:
                    tree.AttachNode( Steps[ stepNumber ].Node );
                    return;
                case ActionTreeType.ROTATION:
                    TriggerStepRotation( tree );
                    return;
            }	
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private StepMode() {}

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void TriggerStepRotation( Tree tree )
        {
            Node parent = Steps[stepNumber].Node.Parent;
            tree.RotateNode( Steps[stepNumber].Node );
            tree.RestoreRoot();
            Steps[stepNumber].Node = parent;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void BackRotationAfterPrepareSteps( Tree tree, int stepNumber )
        {
            Node node = Steps[stepNumber].Node.Parent;
            tree.RotateNode( Steps[stepNumber].Node );
            tree.RestoreRoot();
            Steps[stepNumber].Node = node;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private static StepMode stepMode; 
        
        public List< ActionTree > Steps { get; } = new List< ActionTree >();
        private int stepNumber;
    }
}

