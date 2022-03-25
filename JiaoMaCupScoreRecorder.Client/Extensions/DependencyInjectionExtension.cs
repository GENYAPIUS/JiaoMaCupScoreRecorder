using JiaoMaCupScoreRecorder.Client.Models;
using JiaoMaCupScoreRecorder.Client.Services;
using JiaoMaCupScoreRecorder.Client.Services.Interfaces;
using JiaoMaCupScoreRecorder.Client.Utils;

namespace JiaoMaCupScoreRecorder.Client.Extensions;

public static class DependencyInjectionExtension
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddTransient<IGoogleSheetService, GoogleSheetService>();
        services.AddSingleton<StateContainer.StateContainer>();

        return services;
    }

    public static IServiceCollection AddGoogleSheetsApiAuth(this IServiceCollection services,
                                                            IConfiguration configuration)
    {
        StaticServiceCollectUtils.StaticService.AddSingleton(serviceProvider =>
        {
            var result = new GoogleSheetsAuthModel();
            configuration.GetSection("GoogleSheetsApiAuthorization").Bind(result);

            return result;
        });

        return services;
    }

    public static IServiceCollection AddSpreadSheetData(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddSingleton(serviceProvider =>
        {
            var result = new Dictionary<string, SheetInfoModel>();
            configuration.GetSection("SpreadSheet").Bind(result);

            return result;
        });

        return services;
    }
}