namespace Controller
{
    public sealed class ZoneInitializator
    {
        public ZoneInitializator(Services services, GameContext context)
        {
            services.ZoneController = new ZoneController(services, context);
            services.MainController.AddEnabledAndDisabled(services.ZoneController);
        }
    }
}