using PayPal.Api;

namespace AYMDating.API.Helpers
{
    public class PayPalService
    {
        private readonly IConfiguration _config;

        public PayPalService(IConfiguration config)
        {
            _config = config;
        }

        private APIContext GetAPIContext()
        {
            var clientId = _config["PayPal:ClientId"];
            var clientSecret = _config["PayPal:ClientSecret"];
            var config = new Dictionary<string, string>
        {
            { "mode", _config["PayPal:Mode"] }
        };

            var accessToken = new OAuthTokenCredential(clientId, clientSecret, config).GetAccessToken();
            return new APIContext(accessToken) { Config = config };
        }

        public Payment CreatePayment(string redirectUrl, int amountInDollars)
        {
            var apiContext = GetAPIContext();

            var payer = new Payer { payment_method = "paypal" };

            var redirectUrls = new RedirectUrls
            {
                cancel_url = $"{redirectUrl}cancelPayment",
                return_url = $"{redirectUrl}successPayment"
            };

            var amountInDollarsFormatted = amountInDollars.ToString("F2");

            var details = new Details
            {
                subtotal = amountInDollarsFormatted
            };

            var amountObj = new Amount
            {
                currency = "USD",
                total = amountInDollarsFormatted,
                details = details
            };

            var transactionList = new List<Transaction>
    {
        new Transaction
        {
            description = "Payment transaction",
            invoice_number = Guid.NewGuid().ToString(),
            amount = amountObj
        }
    };

            var payment = new Payment
            {
                intent = "sale",
                payer = payer,
                transactions = transactionList,
                redirect_urls = redirectUrls
            };

            return payment.Create(apiContext);
        }

        public Payment ExecutePayment(string paymentId, string payerId)
        {
            try
            {
                var apiContext = GetAPIContext();
                var paymentExecution = new PaymentExecution { payer_id = payerId };
                var payment = new Payment { id = paymentId };
                return payment.Execute(apiContext, paymentExecution);
            }
            catch (Exception ex)
            {
                var x = ex;
                return new Payment();
            }
        }
    }
}
