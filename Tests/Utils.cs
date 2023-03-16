
using VisualTree;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System;

namespace Tests
{
    class Utils
    {
        public Tree CreateTree( List< int > keys, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keys );
            return tree;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CreateTreeInStepMode( List< int > keysToBuld, List< int > keysToCheck, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            StepMode stepMode = TreeServices.StepMode;
            stepMode.PrepareStepsForAddNodes( tree, keysToBuld );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + 10 );
            CheckNodes( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void CreateTreeInStepModeBackAndPartial( List< int > keysToBuld, List< int > keysToCheck, int finalStep, 
                                                        TreeType treeType, int firstIter = 10, int secondIter = 20 )
        {
            Tree tree = GetTree( treeType );
            StepMode stepMode = TreeServices.StepMode;
            stepMode.PrepareStepsForAddNodes( tree, keysToBuld );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + firstIter );
            TriggerStepModeActions( stepMode.StepBackward, tree, stepMode.Steps.Count + secondIter );
            TriggerStepModeActions( stepMode.StepForward, tree, finalStep );
            CheckNodes( tree.Root, keysToCheck );
        }
       
        /*******************************************************************************************/
        /*******************************************************************************************/

        public Tree BuildTreeAndAddNodes( List< int > keysToBuild, List< int > keysToAdd, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            tree.CreateNodes( keysToAdd );
            return tree;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AddNodesInStepModeAndBack( List< int > keysToBuld, List< int > keysToAdd, List< int > keysToCheck, 
                                            int finalStep, TreeType treeType )
        {
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuld );
            StepMode stepMode = TreeServices.StepMode;
            stepMode.PrepareStepsForAddNodes( tree, keysToAdd );

            TriggerStepModeActions( stepMode.StepForward, tree, stepMode.Steps.Count + 100 );
            TriggerStepModeActions( stepMode.StepBackward, tree, stepMode.Steps.Count + 200 );
            TriggerStepModeActions( stepMode.StepForward, tree, finalStep );
            CheckNodes( tree.Root, keysToCheck );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodes( Tree tree, List< int > keys )
        {
            Selection selection = TreeServices.Selection;
            AddNodesToSelection( tree, selection.Nodes, keys );
            tree.DelSelectedNodes( selection.Nodes );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void DeleteNodesInStepMode( List< int > keysToBuild, List< int > keysToDelete, 
                                           List< int > keysToCheck_1, List< int > keysToCheck_2, TreeType treeType,
                                           int firstIter = 3, int secondIter = 3 )
        { 
            Tree tree = GetTree( treeType );
            tree.CreateNodes( keysToBuild );
            
            Selection selection = TreeServices.Selection;
            AddNodesToSelection( tree, selection.Nodes, keysToDelete );

            StepMode stepMode = TreeServices.StepMode;
            stepMode.PrepareStepsForDeleteNodes( tree, selection.Nodes );

            TriggerStepModeActions( stepMode.StepForward, tree, firstIter );
            CheckNodes( tree.Root, keysToCheck_1 );

            TriggerStepModeActions( stepMode.StepBackward, tree, secondIter );
            CheckNodes( tree.Root, keysToCheck_2 );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void BalanceTree( List< int > keysToBuild, List< int > keysToCheck )
        {
            Tree tree = GetTree( TreeType.CommonBST );
            tree.CreateNodes( keysToBuild );
            new DSW().BalanceTree( tree );
            CheckNodes( tree.Root, keysToCheck );
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/
        
        public void CheckNodes< T >( Node node, List< T > valuesToCheck )
        {
            if ( valuesToCheck is null )
            {
                Assert.IsNull( node );
                return;
            }

            if ( node.IsLeft() )
            {
                CheckNodes( node.Left, valuesToCheck );
            }
            if ( node.IsRight() )
            {
                CheckNodes( node.Right, valuesToCheck );
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

        public Tree GetTree( TreeType treeType )
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

