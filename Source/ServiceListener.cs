
using System.Collections.Generic;

namespace VisualTree
{
    static class ServiceListener
    {
        static ServiceListener()
        {
            listeners = new List< IListener >();
        }
        
        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void AddListener( IListener listener )
        {
            listeners.Add( listener );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static bool RemoveListener( IListener listener )
        {
            return listeners.Remove( listener );
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static void Notify( ActionTreeType actionType, Node node )
        {
            foreach( var listener in listeners )
            {
                listener.AddAction( actionType, node );
            }
        }

        /*******************************************************************************************/
        /*******************************************************************************************/

        public static List< IListener > listeners;
    }
}

