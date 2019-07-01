
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using VisualTree;

namespace Tests
{
    class Test
    {
        public void CreateTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CreateTreeInStepMode( List< int > keysToBuld, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            StepMode stepMode = StepMode.GetInstance();
            stepMode.PrepareStepsForAddNodes( tree, keysToBuld );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + 10 );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CreateTreeInStepModeBackAndPartial( List< int > keysToBuld, List< int > keysToCheck, int finalStep, 
                                                        TreeType treeType, int firstIter = 10, int secondIter = 20 )
        {
            Tree tree = GetTree( treeType );
            StepMode stepMode = StepMode.GetInstance();
            stepMode.PrepareStepsForAddNodes( tree, keysToBuld );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + firstIter );
            TriggerStepModeActions( stepMode.StepBackward, tree, stepMode.Steps.Count + secondIter );
            TriggerStepModeActions( stepMode.StepForward, tree, finalStep );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void AddNodes( List< int > keysToBuild, List< int > keysToAdd, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            tree.CreateNodes( keysToAdd );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AddNodesInStepModeAndBack( List< int > keysToBuld, List< int > keysToAdd, List< int > keysToCheck, 
                                            int finalStep, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuld );
            StepMode stepMode = StepMode.GetInstance();
            stepMode.PrepareStepsForAddNodes( tree, keysToAdd );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + 100 );
            TriggerStepModeActions( stepMode.StepBackward, tree, stepMode.Steps.Count + 200 );
            TriggerStepModeActions( stepMode.StepForward, tree, finalStep );
            CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodes( List< int > keysToBuild, List< int > keysToCheck, List< int > keysToDelete, 
                                 TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = Selection.Get();
            AddNodesToSelection( tree.Root, selection.nodes, keysToDelete );

            tree.DelSelectedNodes( selection.nodes );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodesInStepMode( List< int > keysToBuild, List< int > keysToDelete, 
                                           List< int > keysToCheck_1, List< int > keysToCheck_2, TreeType treeType,
                                           int firstIter = 3, int secondIter = 3 )
        { 
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = Selection.Get();
            AddNodesToSelection( tree.Root, selection.nodes, keysToDelete );

            StepMode stepMode = StepMode.GetInstance();
            stepMode.PrepareStepsForDeleteNodes( tree, selection.nodes );

            TriggerStepModeActions( stepMode.StepForward, tree, firstIter );
            CheckNode( tree.Root, keysToCheck_1);

            TriggerStepModeActions( stepMode.StepBackward, tree, secondIter );
            CheckNode( tree.Root, keysToCheck_2 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void BalanceTree( List< int > keysToBuild, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            new DSW().BalanceTree( tree );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CheckNode( Node node, List< int > expectedKeys )
        {
            if ( node.IsLeft() )
            {
                CheckNode( node.Left, expectedKeys );
            }
            if ( node.IsRight() )
            {
                CheckNode( node.Right, expectedKeys );
            }

            Assert.AreEqual( node.Key, expectedKeys[0] );
            expectedKeys.RemoveAt( 0 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddNodesToSelection( Node node, List< Node > selectedNodes, List< int > selectedKeys )
        {
            if ( node.IsLeft() )
            {
                AddNodesToSelection( node.Left, selectedNodes, selectedKeys );
            }

            if ( selectedKeys.Contains( node.Key ))
            {
                selectedNodes.Add( node );
            }

            if ( node.IsRight() )
            {
                AddNodesToSelection( node.Right, selectedNodes, selectedKeys );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private Tree GetTree( TreeType treeType )
        {
            if ( treeType == TreeType.CommonBST )
            {
                return new TreeBST();
            }
            return new TreeAVL();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void TriggerStepModeActions( DelegateStepModeAction action, Tree tree, int count )
        {
            for ( int i = 0; i < count; i++ )
            {
                action( tree );
            }
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        private delegate void DelegateStepModeAction( Tree tree );
    }
}

