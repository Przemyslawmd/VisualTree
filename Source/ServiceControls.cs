
using System.Windows.Controls;

namespace VisualTree
{
    class ServiceControls
    {
        public static ServiceControls GetInstance()
        {
            return serviceControls;
        }
        
        public static void CreateServiceControls( Canvas canvas )
        {
            serviceControls = new ServiceControls( canvas );
        }

        public ServiceControls( Canvas canvas )
        { 
            Canvas = canvas;    
        }
        
        public static ServiceControls serviceControls;
        
        public Canvas Canvas { get; }
    }
}

