namespace lib_dominio.Nucleo
{
    public class LogConversor
    {
        public static void Log(Exception exception,
            IDictionary<string, object> ViewData)
        {
            if (ViewData == null)
                return;
            var mensaje = exception.Message.ToString();
            if (exception.InnerException != null)
                mensaje = exception.InnerException!.Message.ToString();

            if (mensaje.Length >= 110)
                mensaje = mensaje.Substring(0, 110);
            ViewData!["Mensaje"] = mensaje;
        }

        public static void Log(Exception exception, string? message,
            IDictionary<string, object> ViewData)
        {
            if (ViewData == null)
                return;
            var mensaje = exception.Message.ToString();
            if (exception.InnerException != null)
                mensaje = exception.InnerException!.Message.ToString();

            if (mensaje.Length >= 110)
                mensaje = mensaje.Substring(0, 110);
            ViewData!["Mensaje"] = message!;
        }
    }
}