// See https://aka.ms/new-console-template for more information
using Com.Zoho.API.Authenticator;
using Com.Zoho.API.Authenticator.Store;
using Com.Zoho.Crm.API;
using Com.Zoho.Crm.API.Dc;
using Com.Zoho.Crm.API.Logger;
using Environment = Com.Zoho.Crm.API.Dc.DataCenter.Environment;
using SDKInitializer = Com.Zoho.Crm.API.Initializer;

using static Com.Zoho.Crm.API.Record.RecordOperations;
using ActionHandler = Com.Zoho.Crm.API.Record.ActionHandler;
using ActionResponse = Com.Zoho.Crm.API.Record.ActionResponse;
using ActionWrapper = Com.Zoho.Crm.API.Record.ActionWrapper;
using APIException = Com.Zoho.Crm.API.Record.APIException;
using BodyWrapper = Com.Zoho.Crm.API.Record.BodyWrapper;
using FileBodyWrapper = Com.Zoho.Crm.API.Record.FileBodyWrapper;
using Info = Com.Zoho.Crm.API.Record.Info;
using ResponseHandler = Com.Zoho.Crm.API.Record.ResponseHandler;
using ResponseWrapper = Com.Zoho.Crm.API.Record.ResponseWrapper;
using SuccessResponse = Com.Zoho.Crm.API.Record.SuccessResponse;
using ConsoleApp2;


Console.WriteLine("Hello, World!");
var clientId = "1000.YRVS4UOZVZWOWADGSA2FXZ5HOB3ICY";
var clientSecret = "6776da6a02bac43bfc1d8fd4b7736512ea3d061c5b";

Logger logger = new Logger.Builder()
    .Level(Logger.Levels.ALL)
    .FilePath("D:/csharp_sdk_log.log")
    .Build();

UserSignature user = new UserSignature("mexicocosmeticcenter1@gmail.com");

//Environment environment = USDataCenter.DEVELOPER;

IToken token = new OAuthToken.Builder()
    .ClientId(clientId)
    .ClientSecret(clientSecret)
    .GrantToken("1000.a370e70bbeae23ae52df09deb4bdd4d9.03d8ba1e3106c85c2bef166d4e293ba9")
    .UserSignature(user)
    .Build();


var tokenPath = "/token";
if (!Directory.Exists(tokenPath))
    Directory.CreateDirectory(tokenPath);
ITokenStore tokenStore = new FileStore(tokenPath + "/csharp_sdk_token.txt");

SDKConfig sdkConfig = new SDKConfig.Builder()
    .AutoRefreshFields(false)
    .PickListValidation(true)
    .Build();

new SDKInitializer.Builder()
    //.Environment(environment)
    .Token(token)
    .Store(tokenStore)
    .SDKConfig(sdkConfig)
    .Logger(logger)
    .Initialize();

//Modules.GetModules();

//Record.GetRecords("SMS");

await ConsoleApp2.RingCentral.Init();