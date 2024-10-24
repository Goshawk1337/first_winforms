using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;

//Be able to create cars with parameters like this:
/* 
 Brand, model, year, max speed, color

save to a text file(.txt) with a unique id example: 1,2,3
*/

//Read cars when loading the program. 
/*
 Read and list cars when the program is loading, if there are no available cars then show: no car is available
*/

//Be able to delete cars.


namespace add_and_read_cars
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }
}
