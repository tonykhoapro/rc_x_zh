using dotenv.net;
using Newtonsoft.Json;
using RingCentral;

namespace ConsoleApp2
{
    public class RingCentral
    {
        static RestClient restClient;
        static string DELIVERY_ADDRESS = "";
        public static async Task Init()
        {
            DotEnv.Load();

            // For the purpose of testing the code, we put the deliver address in the environment variable.
            // Feel free to set the delivery address directly.
            DELIVERY_ADDRESS = Environment.GetEnvironmentVariable("WEBHOOK_DELIVERY_ADDRESS");
            var RC_CLIENT_ID = Environment.GetEnvironmentVariable("RC_CLIENT_ID");
            var RC_CLIENT_SECRET = Environment.GetEnvironmentVariable("RC_CLIENT_SECRET");
            var RC_SERVER_URL = Environment.GetEnvironmentVariable("RC_SERVER_URL");
            var RC_JWT = Environment.GetEnvironmentVariable("RC_JWT");
            try
            {
                // Instantiate the SDK
                restClient = new RestClient(
                    RC_CLIENT_ID,
                    RC_CLIENT_SECRET,
                    RC_SERVER_URL);

                // Authenticate a user using a personal JWT token
                await restClient.Authorize(RC_JWT);
                await subscribe_for_notification();
                //await read_subscriptions();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        static private async Task subscribe_for_notification()
        {
            try
            {
                var bodyParams = new CreateSubscriptionRequest();
                bodyParams.eventFilters = new[] { "/restapi/v1.0/account/~/extension/~/message-store/instant?type=SMS" };
                bodyParams.deliveryMode = new NotificationDeliveryModeRequest()
                {
                    transportType = "WebHook",
                    address = DELIVERY_ADDRESS
                };
                bodyParams.expiresIn = 3600;

                var resp = await restClient.Restapi().Subscription().Post(bodyParams);
                Console.WriteLine("Subscription Id: " + resp.id);
                Console.WriteLine("Ready to receive incoming SMS via WebHook.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*
        * Read all created subscriptions
        */
        static private async Task read_subscriptions()
        {
            try
            {
                var resp = await restClient.Restapi().Subscription().List();
                if (resp.records.Length == 0)
                {
                    Console.WriteLine("No subscription.");
                }
                else
                {
                    foreach (var record in resp.records)
                    {
                        Console.WriteLine(JsonConvert.SerializeObject(record));
                        await delete_subscription(record.id);
                    }
                }

            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        /*
        * Delete a subscription identified by the subscription id
        */
        static private async Task delete_subscription(String subscriptionId)
        {
            try
            {
                var resp = await restClient.Restapi().Subscription(subscriptionId).Delete();
                Console.WriteLine("Subscription " + subscriptionId + " deleted.");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

    }
}
