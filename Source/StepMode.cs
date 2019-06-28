
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
                Node node = Steps[i].Node.Parent;
                tree.RotateNode( Steps[i].Node );
                Steps[i].Node = node;
                tree.RestoreRoot();
            }

            this.tree = tree;
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
                if ( Steps[i].ActionType == ActionType.ADD )
                {
                    tree.DetachNode( Steps[i].Node );
                    tree.RestoreRoot();
                }
                else if ( Steps[i].ActionType == ActionType.ROTATION )
                {
                    Node node = Steps[i].Node.Parent;
                    tree.RotateNode( Steps[i].Node );
                    Steps[i].Node = node;
                    tree.RestoreRoot();
                }
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
                    tree.AttachNode( Steps[ stepNumber ].Node );
                    stepNumber++;
                    return;
                case ActionType.REMOVE:
                    return;
                case ActionType.ROTATION:
                    TriggerStepRotation();
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
                    tree.DetachNode( Steps[ stepNumber].Node );
                    return;
                case ActionType.REMOVE:
                    return;
                case ActionType.ROTATION:
                    TriggerStepRotation();
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

        private void TriggerStepRotation()
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

