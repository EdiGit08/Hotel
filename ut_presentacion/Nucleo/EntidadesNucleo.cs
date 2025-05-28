using lib_dominio.Entidades;

namespace ut_presentacion.Nucleo
{
    public class EntidadesNucleo
    {
        public static Habitaciones? Habitaciones()
        {
            var entidad = new Habitaciones();
            entidad.Nombre = "L 804";
            entidad.Camas = 4;
            entidad.Estado = true;
            return entidad;
        }

        public static Recepcionistas? Recepcionistas()
        {
            var entidad = new Recepcionistas();
            entidad.Carnet = "ACR698";
            entidad.Nombre = "Carla Martinez";
            entidad.Salario = 1789000;
            return entidad;
        }

        public static Clientes? Clientes()
        {
            var entidad = new Clientes();
            entidad.Cedula = "899654123";
            entidad.Nombre = "Julian Cardenas";
            entidad.Opinion = 1;
            return entidad;
        }

        public static Reservas? Reservas()
        {
            var entidad = new Reservas();
            entidad.Codigo = "BH5666x989";
            entidad.Fecha_Entrada = DateTime.Now;
            entidad.Fecha_Salida = DateTime.Now;
            entidad.Recepcionista = 2;
            entidad.Cliente = 1;
            entidad.Habitacion = 3;
            return entidad;
        }

        public static Servicios? Servicios()
        {
            var entidad = new Servicios();
            entidad.Tipo = "Toboganes";
            entidad.Tarifa = 199000;
            entidad.Descripcion = "Acceso al parque de juegos"; 
            return entidad;
        }

        public static Servicios_Reservas? Servicios_Reservas()
        {
            var entidad = new Servicios_Reservas();
            entidad.Reserva = 2;
            entidad.Servicio = 3;
            return entidad;
        }

        public static Opiniones? Opiniones()
        {
            var entidad = new Opiniones();
            entidad.Opcion = "Normal";
            entidad.Cantidad = 101;
            return entidad;
        }

        public static Promociones? Promociones()
        {
            var entidad = new Promociones();
            entidad.Descuento = 5;
            entidad.Fecha_Inicio =DateTime.Now;
            entidad.Fecha_Fin = DateTime.Now;
            return entidad;
        }

        public static Pagos? Pagos()
        {
            var entidad = new Pagos();
            entidad.Total = 1569123;
            entidad.Medio = "Efectivo";
            entidad.Reserva = 2;
            entidad.Promocion = 2;
            return entidad;
        }

        public static Usuarios? Usuarios()
        {
            var entidad = new Usuarios();
            entidad.Nombre = "Octavio";
            entidad.Contrasena = "contrasena";
            entidad.Rol = 2;
            return entidad;
        }

        public static Roles? Roles()
        {
            var entidad = new Roles();
            entidad.Nombre = "Contador";
            return entidad;
        }
        public static Permisos? Permisos()
        {
            var entidad = new Permisos();
            entidad.Tipo = "Editar";

            return entidad;
        }
        public static Auditorias? Auditorias()
        {
            var entidad = new Auditorias();
            entidad.Codigo = "Prueba 234";
            entidad.Fecha = DateTime.Now;
            entidad.Accion = "Eliminar Reservas";
            return entidad;
        }
    }
}
