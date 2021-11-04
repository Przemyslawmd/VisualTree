
using System;
using System.Collections.Generic;

[assembly: System.Runtime.CompilerServices.InternalsVisibleTo("Tests")]

namespace VisualTree
{
    class Message
    {
        public Message()
        {
            messages = new Dictionary< Result, String >()
            {
                [Result.IMPROPER_DATA]          = "Improper symbol found to build a tree.",
                [Result.DUPLICATED_SYMBOL]      = "Duplicated symbols are not allowed.",
                [Result.NO_TREE]                = "There is no tree.",
                [Result.NO_DATA_TO_CREATE_TREE] = "There are no values to build a tree.",
                [Result.NO_NODE_SELECTED]       = "None node is selected.",
                [Result.ROTATION_MULTIPLE]      = "Select only one node to make rotation.",
                [Result.ROTATION_ROOT]          = "Root can not be rotated.",
                [Result.SYMBOL_LIMIT]           = "Too many symbols typed."
            };
        }              
        
        public String GetMessageText( Result result )
        {
            return messages[result];
        }

        private readonly Dictionary< Result, String > messages;
    }
}

