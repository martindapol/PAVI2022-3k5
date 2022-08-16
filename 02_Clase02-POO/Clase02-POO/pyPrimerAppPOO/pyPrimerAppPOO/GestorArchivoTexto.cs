using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyPrimerAppPOO
{
    class GestorArchivoTexto
    {
        public static String[] Leer(String nombreArchivo) {
            String fullPath = ObtenerFullPath(nombreArchivo);
            if (File.Exists(fullPath))
                return File.ReadAllLines(fullPath);

            return null;
        }

        public static void Guardar(String [] lineas, String nombreArchivo) {
            String fullPath = ObtenerFullPath(nombreArchivo);
            if (File.Exists(fullPath))
                File.Delete(fullPath);

            File.AppendAllLines(fullPath, lineas);
        }

        private static String ObtenerFullPath(String nombreArchivo) {
            String path = Directory.GetParent(Environment.CurrentDirectory).Parent.FullName;
            String fullPath = path + "\\" + nombreArchivo;
            return fullPath;
        }

    }
}
