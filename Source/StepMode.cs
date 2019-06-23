
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

