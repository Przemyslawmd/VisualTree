
using System;
using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Tests")]

namespace VisualTree
{
    class Message
    {
        public enum Code
        {
            OK,
            IMPROPER_DATA,
            DUPLICATED_SYMBOL,
            NO_TREE,
            NO_DATA_TO_CREATE_TREE,
            NO_NODE_SELECTED,
            ROTATION_MULTIPLE,
            ROTATION_ROOT,
            SYMBOL_LIMIT
        };            
        
        public Message()
        {
            messages = new Dictionary< Code, String >()
            {
                [Code.IMPROPER_DATA]          = "Improper symbol found to build a tree.",
                [Code.DUPLICATED_SYMBOL]      = "Duplicated symbols are not allowed.",
                [Code.NO_TREE]                = "There is no tree.",
                [Code.NO_DATA_TO_CREATE_TREE] = "There are no values to build a tree.",
                [Code.NO_NODE_SELECTED]       = "None node is selected.",
                [Code.ROTATION_MULTIPLE]      = "Select only one node to make rotation.",
                [Code.ROTATION_ROOT]          = "Root can not be rotated.",
                [Code.SYMBOL_LIMIT]           = "To many symbols typed."
            };
        }              
        
        public String GetMessageText( Code code )
        {
            return messages[code];
        }

        private Dictionary< Code, String > messages;
    }
}
