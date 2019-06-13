
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
            NONE_NODE_SELECTED,
            ROTATION_MULTIPLE,
            ROTATION_ROOT,
            SYMBOL_LIMIT
        };            
        
        public Message()
        {
            messages = new Dictionary< Code, String >();

            messages.Add( Code.IMPROPER_DATA,                   "Improper symbol found to build a tree." );
            messages.Add( Code.DUPLICATED_SYMBOL,               "Duplicated symbols are not allowed." );  
            messages.Add( Code.NO_TREE,                         "There is no tree." );
            messages.Add( Code.NO_DATA_TO_CREATE_TREE,          "There are no values to build a tree." );
            messages.Add( Code.NONE_NODE_SELECTED,              "None node is selected." );
            messages.Add( Code.ROTATION_MULTIPLE,               "Select only one node to rotate.");
            messages.Add( Code.ROTATION_ROOT,                   "Root can not be rotated.");
            messages.Add( Code.SYMBOL_LIMIT,                    "To many symbols typed.");
        }              
        
        public String GetMessageText( Code code )
        {
            return messages[code];
        }

        private Dictionary< Code, String > messages;
    }
}
