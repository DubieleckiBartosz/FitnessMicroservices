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

        }
    }
}
