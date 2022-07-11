namespace IIS_503_Error
{
    public class CurrentRoutes
    {
        public string GroupName { get; set; }
        public string RouteName { get; set; }
    }

    public class Groups
    {
        public int GroupID { get; set; }
        public string GroupName { get; set; }

    }

    public class Routes
    {
        public int RouteID { get; set; }
        public string Name { get; set; }

    }
}
