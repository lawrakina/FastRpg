namespace Controller
{
    internal class TimeRemainingInitializator
    {
        public TimeRemainingInitializator(Services services)
        {
            services.MainController.AddUpdated(new TimeRemainingController());
        }
    }
}