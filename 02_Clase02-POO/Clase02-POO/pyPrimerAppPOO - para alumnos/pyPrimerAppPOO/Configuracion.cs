using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace pyPrimerAppPOO
{
    class Configuracion
    {
       
        public String NombreDB { get; set; }
        public String Usuario { get; set; }
        public String Clave { get; set; }


        private Configuracion() {
            //TODO: leer archivo plano y asignar las líneas como propiedades
        }

        public void Guardar() {
            //TODO: tomar los valores a actuales del objeto y grabar un arreglo de cadenas
            //en un archivo plano.
        }

    }
}
