namespace Customer.API.Controllers
{
    public static class HomeControler
    {
        public static void Index(this WebApplication app)
        {
            app.MapGet("/", () => "Wellcome to Customer API");
        }
    }
}