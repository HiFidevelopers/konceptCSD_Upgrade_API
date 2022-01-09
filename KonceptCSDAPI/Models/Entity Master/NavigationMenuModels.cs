namespace KonceptCSDAPI.Models.NavigationMenuModel
{
    public class NavigationMenuParameterModel
    {
        public string roleid { get; set; }
    }

    public class NavigationMenuModel
    {
        public string id { get; set; }
        public string link { get; set; }
        public string title { get; set; }
        public string icon { get; set; }
        public string parent { get; set; }
    }
}