using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using OTPSimulation.Common;
using OTPSimulation.Constants;
using OTPSimulation.DataModels;
using OTPSimulation.Interfaces;
using OTPSimulation.Mapper;
using OTPSimulation.Services;
using OTPSimulation.ViewModels;

ServiceCollection serviceCollection = new ServiceCollection();
ConfigureServices(serviceCollection);
IServiceProvider serviceProvider = serviceCollection.BuildServiceProvider();

IConsoleUserInteraction? consoleUserInteraction = serviceProvider.GetService<IConsoleUserInteraction>();
IEmailOTPModule? emailOtpModule = serviceProvider.GetService<IEmailOTPModule>();
await GenerateOtpApi();

async Task GenerateOtpApi()
{

    GenerateOtpViewModel generateOtpViewModel = new GenerateOtpViewModel();
    GenerateOtpDataModel generateOtpDataModel = new GenerateOtpDataModel();
    consoleUserInteraction.WriteLine(Messages.MOCK_EMAIL_INPUT);
    generateOtpDataModel.UserEmail = consoleUserInteraction.ReadUserInput();

    // isValidEmailDomain is set to true in order to allow usage of personal emails for testing.

    //bool isValidEmailDomain = EmailChecker.IsValidEmailDomain(generateOtpDataModel.UserEmail);
    bool isValidEmailDomain = true;

    if (!isValidEmailDomain)
    {
        generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.BadRequest, Messages.EMAIL_INVALID);
        ViewModelAndDataModelMapping.MapDataModelStatusCodeAndMessageToViewModel(generateOtpViewModel, generateOtpDataModel);
        PrintResponseStatusCodeAndMessage(generateOtpViewModel);
        return;
    }

    bool emailExists = EmailChecker.EmailExistInDatabase(generateOtpDataModel.UserEmail);

    if (!emailExists)
    {
        generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.BadRequest, Messages.EMAIL_FAIL);
        ViewModelAndDataModelMapping.MapDataModelStatusCodeAndMessageToViewModel(generateOtpViewModel, generateOtpDataModel);
        PrintResponseStatusCodeAndMessage(generateOtpViewModel);
        return;
    }

    

    try
    {
        await emailOtpModule.GenerateOtpEmailAsync(generateOtpDataModel);
    }
    catch
    {
        generateOtpDataModel.AssignStatusCodeAndMessage((int)StatusCodes.BadRequest, Messages.EMAIL_FAIL);
        ViewModelAndDataModelMapping.MapDataModelStatusCodeAndMessageToViewModel(generateOtpViewModel, generateOtpDataModel);
        PrintResponseStatusCodeAndMessage(generateOtpViewModel);
        return;
    }
    

    await emailOtpModule.SubmitOtpAsync(generateOtpDataModel);

    ViewModelAndDataModelMapping.MapDataModelStatusCodeAndMessageToViewModel(generateOtpViewModel, generateOtpDataModel);
    PrintResponseStatusCodeAndMessage(generateOtpViewModel);
}

static void PrintResponseStatusCodeAndMessage(GenerateOtpViewModel generateOtpViewModel)
{
    // common function to print return message for this mock api.
    Console.WriteLine(generateOtpViewModel.StatusCode);
    Console.WriteLine(generateOtpViewModel.Message);

}

static void ConfigureServices(IServiceCollection services)
{
    services.AddTransient<IConsoleUserInteraction, ConsoleUserInteraction>();
    services.AddTransient<IEmailOTPModule, EmailOTPModule>();
    services.AddTransient<IMailService, MailService>();
    services.AddTransient<IEmailSender, EmailSender>();

    // Configure appsettings.json loading
    var configuration = new ConfigurationBuilder()
        .SetBasePath(Directory.GetCurrentDirectory())
        .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
        .Build();

    services.AddSingleton<IConfiguration>(configuration);
}