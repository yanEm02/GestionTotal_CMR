using System;
using System.Globalization;
using System.Windows.Forms;

namespace CapaPresentacion
{
    internal static class Program
    {
        /// <summary>
        /// Punto de entrada principal para la aplicación.
        /// </summary>
        [STAThread]
        static void Main()
        {
            //configuramos que todo el programa use comas en vez de puntos
            CultureInfo culture = new CultureInfo("es-DO");
            CultureInfo.DefaultThreadCurrentCulture = culture;
            CultureInfo.DefaultThreadCurrentUICulture = culture;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Inicio()); //para que inicie con el formulario de inicio
        } 
    }
}
