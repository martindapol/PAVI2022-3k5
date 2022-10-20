using CarpinteriaApp.dominio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CarpinteriaApp.datos.Dao
{
    public interface IPresupuestoDao
    {
        bool Crear(Presupuesto presupuesto);
        int ProximoId();
    }
}
