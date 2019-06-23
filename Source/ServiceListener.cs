
using System.Collections.Generic;

namespace VisualTree
{
    class ServiceListener
    {
        public static void AddListener( IListener listener )
        {
        }


        public bool RemoveListener( IListener listener )
        {
            return true;
        }

        public static List< IListener > listeners;
    }
}

