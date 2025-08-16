using Microsoft.Extensions.DependencyInjection;
using TimeTwoFix.Application.AppointmentServices.Interfaces;
using TimeTwoFix.Application.AppointmentServices.Services;
using TimeTwoFix.Application.CategoryService.Interfaces;
using TimeTwoFix.Application.ClientServices.Interfaces;
using TimeTwoFix.Application.ClientServices.Services;
using TimeTwoFix.Application.LiftingBridgeServices.Interfaces;
using TimeTwoFix.Application.LiftingBridgeServices.Services;
using TimeTwoFix.Application.ProvidedServicesService.Interfaces;
using TimeTwoFix.Application.ProvidedServicesService.Services;
using TimeTwoFix.Application.UserServices.Interfaces;
using TimeTwoFix.Application.UserServices.Services;
using TimeTwoFix.Application.VehicleServices.Interfaces;
using TimeTwoFix.Application.VehicleServices.Services;
using TimeTwoFix.Application.WorkOrderService.Interfaces;

namespace TimeTwoFix.Application.Extension
{
    public static class ApplicationRegistration
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<IRoleService, RoleService>();
            services.AddScoped<IClientServices, ClientService>();
            services.AddScoped<IVehicleService, VehicleService>();
            services.AddScoped<IAppointmentService, AppointmentService>();
            services.AddScoped<ILiftingBridgeServices, LiftingBridgeServices>();
            services.AddScoped<ICategoryService, CategoryService.Services.CategoryService>();
            services.AddScoped<IProvidedServiceService, ProvidedServiceService>();
            services.AddScoped<IWorkOrderService, WorkOrderService.Services.WorkOrderService>();


            return services;
        }
    }
}