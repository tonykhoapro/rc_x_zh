using dotenv.net;
using Newtonsoft.Json;
using RingCentral;
using System.Net.Http;
using System.Text;

namespace ConsoleApp2
{
    public class RingCentral
    {
        static RestClient restClient;
        static string DELIVERY_ADDRESS = "";
        public static async Task Init()
        {
            DotEnv.Load();

            DELIVERY_ADDRESS = Environment.GetEnvironmentVariable("WEBHOOK_DELIVERY_ADDRESS");
            var RC_CLIENT_ID = Environment.GetEnvironmentVariable("RC_CLIENT_ID");
            var RC_CLIENT_SECRET = Environment.GetEnvironmentVariable("RC_CLIENT_SECRET");
            var RC_SERVER_URL = Environment.GetEnvironmentVariable("RC_SERVER_URL");
            var RC_JWT = Environment.GetEnvironmentVariable("RC_JWT");
            try
            {
                restClient = new RestClient(RC_CLIENT_ID, RC_CLIENT_SECRET, RC_SERVER_URL);
                await restClient.Authorize(RC_JWT);
                await subscribe_for_notification();
                await FetchAndSendSMSLogs();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        static private async Task FetchAndSendSMSLogs()
        {
            try
            {
                // Define the endpoint URL for fetching SMS logs
                var endpoint = "/restapi/v1.0/account/~/extension/~/message-store";
                var queryParams = new { messageType = "SMS" };
                var response = await restClient.Get(endpoint, queryParams);

                var smsRecords = JsonConvert.DeserializeObject<dynamic>(response.Content.ReadAsStringAsync().Result).records;

                foreach (var record in smsRecords)
                {
                    string jsonContent = JsonConvert.SerializeObject(record);
                    await SendToWebhook(jsonContent);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error fetching or sending SMS logs: " + ex.Message);
            }
        }

        static private async Task SendToWebhook(string content)
        {
            using (var httpClient = new HttpClient())
            {
                var httpContent = new StringContent(content, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(DELIVERY_ADDRESS, httpContent);
                if (response.IsSuccessStatusCode)
                {
                    Console.WriteLine("SMS log successfully sent to webhook.");
                }
                else
                {
                    Console.WriteLine($"Failed to send SMS log to webhook. Status code: {response.StatusCode}");
                }
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
