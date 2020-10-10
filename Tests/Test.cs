
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

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

        public void CreateTreeRB( List< int > keysToBuild, Dictionary< int, NodeColor > nodesToCheck )
        {
            Tree tree = GetTree( TreeType.RB );
            tree.CreateNodes( keysToBuild );
            CheckNode( tree.Root, new List< int >( nodesToCheck.Keys ));
            CheckNode( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
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
            AddNodesToSelection( tree, selection.Nodes, keysToDelete );
            tree.DelSelectedNodes( selection.Nodes );
            
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        public void DeleteNodesTreeRB( List< int > keysToBuild, Dictionary< int, NodeColor > nodesToCheck, 
                                       List< int > keysToDelete )
        {
            Tree tree = GetTree( TreeType.RB );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = Selection.Get();
            AddNodesToSelection( tree, selection.Nodes, keysToDelete );
            tree.DelSelectedNodes( selection.Nodes );
            
            CheckNode( tree.Root, new List< int >( nodesToCheck.Keys ));
            CheckNode( tree.Root, new List< NodeColor >( nodesToCheck.Values ));
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
            AddNodesToSelection( tree, selection.Nodes, keysToDelete );

            StepMode stepMode = StepMode.GetInstance();
            stepMode.PrepareStepsForDeleteNodes( tree, selection.Nodes );

            TriggerStepModeActions( stepMode.StepForward, tree, firstIter );
            CheckNode( tree.Root, keysToCheck_1 );

            TriggerStepModeActions( stepMode.StepBackward, tree, secondIter );
            CheckNode( tree.Root, keysToCheck_2 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void BalanceTree( List< int > keysToBuild, List< int > keysToCheck )
        {
            Tree tree = GetTree( TreeType.CommonBST );
            tree.CreateNodes( keysToBuild );
            new DSW().BalanceTree( tree );
            CheckNode( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        private void CheckNode< T >( Node node, List< T > valuesToCheck )
        {
            if ( valuesToCheck is null )
            {
                Assert.IsNull( node );
                return;
            }

            if ( node.IsLeft() )
            {
                CheckNode( node.Left, valuesToCheck );
            }
            if ( node.IsRight() )
            {
                CheckNode( node.Right, valuesToCheck );
            }

            if ( typeof( T ) == typeof( int ))
            {
                Assert.AreEqual( node.Key, valuesToCheck[0] );
            }
            else
            {
                Assert.AreEqual( node.Color, valuesToCheck[0] );
            }
 
            valuesToCheck.RemoveAt( 0 );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void AddNodesToSelection( Tree tree, List< Node > selectedNodes, List< int > selectedKeys )
        {
            foreach ( int key in selectedKeys)
            {
                Node node = tree.FindNodeByKey( key, tree.Root );
                if ( node != null )
                {
                    selectedNodes.Add( node );
                }
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
            else if ( treeType == TreeType.AVL )
            {
                return new TreeAVL();
            }
            return new TreeRB();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        private void TriggerStepModeActions( Action< Tree > action, Tree tree, int count )
        {
            for ( int i = 0; i < count; i++ )
            {
                action( tree );
            }
        }
    }
}

