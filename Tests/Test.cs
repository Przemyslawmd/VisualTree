
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

            for ( int i = 0; i < stepMode.Steps.Count + 10; i++ )
            {
                stepMode.StepForward( tree );
            }

	        CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CreateTreeInStepModeBackAndPartial( List< int > keysToBuld, List< int > keysToCheck, int finalStep, 
                                                        TreeType treeType )
        {
            Tree tree = GetTree( treeType );
	        StepMode stepMode = StepMode.GetInstance();
	        stepMode.PrepareStepsForAddNodes( tree, keysToBuld );

            for ( int i = 0; i < stepMode.Steps.Count + 10; i++ )
            {
                stepMode.StepForward( tree );
            }

            for ( int i = 0; i < stepMode.Steps.Count + 20; i++ )
            {
                stepMode.StepBackward( tree );
            }

            for ( int i = 0; i < finalStep; i++ )
            {
                stepMode.StepForward( tree );
            }

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

            for ( int i = 0; i < stepMode.Steps.Count + 100; i++ )
            {
                stepMode.StepForward( tree );
            }

            for ( int i = 0; i < stepMode.Steps.Count + 200; i++ )
            {
                stepMode.StepBackward( tree );
            }

            for ( int i = 0; i < finalStep; i++ )
            {
                stepMode.StepForward( tree );
            }

	        CheckNode( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodes( List< int > keysToBuild, List< int > keysToCheck, List< int > keysToDelete, 
                                 TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = Selection.GetInstance();
            AddNodesToSelection( tree.Root, selection.nodes, keysToDelete );

            tree.DelSelectedNodes( selection.nodes );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodesInStepMode( List< int > keysToBuild, List< int > keysToDelete, 
                                           List< int > keysToCheck_1, List< int > keysToCheck_2, TreeType treeType )
        { 
            Tree tree = GetTree( treeType );
	        tree.CreateNodes( keysToBuild );
	        
            Selection selection = Selection.GetInstance();
            AddNodesToSelection( tree.Root, selection.nodes, keysToDelete );

	        StepMode stepMode = StepMode.GetInstance();
	        stepMode.PrepareStepsForDeleteNodes( tree, selection.nodes );

            for ( int i = 0; i < 3; i++ )
            {
                stepMode.StepForward( tree );
            }

	        CheckNode( tree.Root, keysToCheck_1);

            for ( int i = 0; i < 3; i++ )
            {
                stepMode.StepBackward( tree );
            }

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
    }
}

