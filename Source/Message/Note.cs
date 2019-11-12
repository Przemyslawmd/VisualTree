
using System.Collections.Generic;

namespace VisualTree
{
    class Note : IListener
    {
        public static Note Get()
        {
            if ( note is null )
            {
                note = new Note();
            }
            return note;
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void Destroy()
        {
            note = null;
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public void AddAction( ActionTreeType actionType, Node node )
        {
            Actions.Add( new ActionTree( actionType, node ));
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public void ClearActions()
        {
            Actions.Clear();
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public List< ActionTree > Actions { get; private set; } = new List< ActionTree >();

        private static Note note = null;    
    }
}

