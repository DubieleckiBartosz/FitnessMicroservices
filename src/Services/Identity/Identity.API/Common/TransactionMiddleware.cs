namespace Identity.API.Common
{
    public class TransactionMiddleware
    {
        private readonly RequestDelegate _nextDelegate;

        public TransactionMiddleware(RequestDelegate nextDelegate)
        {
            _nextDelegate = nextDelegate;
        }

        public async Task Invoke(HttpContext context)
        {
            if (context.Request.Method.Equals(HttpMethod.Get.Method, StringComparison.CurrentCultureIgnoreCase))
            {
                await _nextDelegate(context);
                return;
            }
        }
    }
}
