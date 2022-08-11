//using Domain.Core.Bus;
using Autofac;
//using MicroRabbit.Infra.Bus;
using Microsoft.Extensions.DependencyInjection;

namespace PartnerManagement
{
    public class DependencyContainer
    {
        public static void RegisterServices(IServiceCollection services)
        {
            //Domain Bus
            //services.AddSingleton<IEventBus, RabbitMQBus>(sp =>
            //{
            //    var scopeFactory = sp.GetRequiredService<IServiceScopeFactory>();
            //    return new RabbitMQBus(sp.GetService<IMediator>(), scopeFactory);
            //});

            //Subscriptions
            //services.AddTransient<EditUserInformationEventHandler>();
            // services.AddTransient<IPOS,POS>();

            //Domain Events
            //services.AddTransient<IEventHandler<EditPasswordEvent>, EditPasswordEventHandler>();

            //Domain Banking Commands
            //services.AddTransient<IRequestHandler<EditRoleManagerCommand, bool>, EditRoleManagerCommandHandler>();

            //data          
            //services.AddTransient<IBusinessPartnerBasicInfo, BusinessPartnerBasicInfo>();
            //services.AddTransient<IBusinessPartnerBankInfo , BusinessPartnerBankInfo>();
            //services.AddTransient<IBusinessPartnerPurchase , BusinessPartnerPurchase>();
            //services.AddTransient<IBusinessPartnerSales, BusinessPartnerSales>();
            //services.AddTransient<IPartnerManagementCommonDDL, PartnerManagementCommonDDL>();
            //services.AddTransient<IPartnerThanaRate, PartnerThanaRate>();
            //services.AddTransient<IPartnerLocationRegister, PartnerLocationRegister>();
            //services.AddTransient<IBusinessPartnerShippingAddress, BusinessPartnerShippingAddress>();
            //services.AddTransient<IGeneralLedgerExtend, GeneralLedgerExtend>();
            //services.AddTransient<IPartnerInformation, PartnerInformation>();
            //services.AddTransient<IPartnerAllotment, PartnerAllotment>();
            //services.AddTransient<IPartnerAllotmentHeader, PartnerAllotmentHeader>();
        }

        public static void ConfigureContainer(ContainerBuilder builder)
        {
            #region === Services ===
            //builder.RegisterType<Weather>().As<IBusinessPartnerBasicInfo>();

            #endregion
        }
    }
}
