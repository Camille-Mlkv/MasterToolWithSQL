namespace MasterTool_WebApp.LocalData
{
    public static class CurrentUser
    {
        public static string UserName { get; set; }
        public static bool IsClient { get; set; }
        public static bool IsMaster { get; set; }
        public static bool IsAdmin { get; set; }
    }
}
