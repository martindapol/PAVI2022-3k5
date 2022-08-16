using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyPrimerAppPOO
{
    class Configuracion
    {
        private static readonly Configuracion instance = new Configuracion();

        public String NombreDB { get; set; }
        public String Usuario { get; set; }
        public String Clave { get; set; }


        private Configuracion() {
            String[] parametros = GestorArchivoTexto.Leer(Properties.Resources.nombreArchivo);
            
            if(parametros != null && parametros.Length == 3)
            {
                NombreDB = parametros[0];
                Usuario = parametros[1];
                Clave = parametros[2];
            }
          
        }

        public void Guardar() {
            String[] lineas = { NombreDB, Usuario, Clave };
            GestorArchivoTexto.Guardar(lineas, Properties.Resources.nombreArchivo);
        }



        public static Configuracion Instance {
            get {
                return instance;
            }
        }




    }
}
