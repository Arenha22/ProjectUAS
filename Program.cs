using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenTK.Windowing.Desktop;

namespace projek
{
    class program
    {
        static void Main(string[] args)
        {
            var nativeWindowSettings = new NativeWindowSettings()
            {
                Size = new OpenTK.Mathematics.Vector2i(800, 800),
                Title = "Pertemuan 2"
            };


            using (var window = new Window(GameWindowSettings.Default,
                nativeWindowSettings))
            {
                window.Run();

            }

        }
    }
}
