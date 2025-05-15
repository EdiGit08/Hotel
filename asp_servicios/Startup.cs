using asp_servicios.Controllers;
using lib_aplicaciones.Implementaciones;
using lib_aplicaciones.Interfaces;
using lib_repositorios.Implementaciones;
using lib_repositorios.Interfaces;
using Microsoft.AspNetCore.Server.Kestrel.Core;

namespace asp_servicios
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public static IConfiguration? Configuration { set; get; }

        public void ConfigureServices(WebApplicationBuilder builder, IServiceCollection services)
        {
            services.Configure<KestrelServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.Configure<IISServerOptions>(x => { x.AllowSynchronousIO = true; });
            services.AddControllers();
            services.AddEndpointsApiExplorer();
            //services.AddSwaggerGen();
            // Repositorios
            services.AddScoped<IConexion, Conexion>();
            // Aplicaciones
            services.AddScoped<IAuditoriasAplicacion, AuditoriasAplicacion>();
            services.AddScoped<IClientesAplicacion, ClientesAplicacion>();
            services.AddScoped<IHabitacionesAplicacion, HabitacionesAplicacion>();
            services.AddScoped<IOpinionesAplicacion, OpinionesAplicacion>();
            services.AddScoped<IPagosAplicacion, PagosAplicacion>();
            services.AddScoped<IPermisosAplicacion, PermisosAplicacion>();
            services.AddScoped<IPromocionesAplicacion, PromocionesAplicacion>();
            services.AddScoped<IRecepcionistasAplicacion, RecepcionistasAplicacion>();
            services.AddScoped<IReservasAplicacion, ReservasAplicacion>();
            services.AddScoped<IRolesAplicacion, RolesAplicacion>();
            services.AddScoped<IRoles_PermisosAplicacion, Roles_PermisosAplicacion>();
            services.AddScoped<IServiciosAplicacion, ServiciosAplicacion>();
            services.AddScoped<IServicios_ReservasAplicacion, Servicios_ReservasAplicacion>();
            services.AddScoped<IUsuariosAplicacion, UsuariosAplicacion>();
            // Controladores
            services.AddScoped<TokenController, TokenController>();

            services.AddCors(o => o.AddDefaultPolicy(b => b.AllowAnyOrigin()));
        }

        public void Configure(WebApplication app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseSwagger();
                //app.UseSwaggerUI();
            }
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Error");
            }
            app.UseHttpsRedirection();
            app.UseAuthorization();
            app.MapControllers();
            app.UseCors();
        }
    }
}