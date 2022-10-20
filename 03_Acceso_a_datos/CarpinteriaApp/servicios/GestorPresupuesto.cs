using CarpinteriaApp.datos.Dao;
using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.servicios
{
    public class GestorPresupuesto
    {
        private IPresupuestoDao dao;

        public GestorPresupuesto()
        {
            dao = new PresupuestoDao();
        }

        public bool ConfirmarPresupuesto(Presupuesto presupuesto)
        {
            return dao.Crear(presupuesto);
        }

        public int ObtenerUltimoId()
        {
            return dao.ProximoId();
        }

    }
}
