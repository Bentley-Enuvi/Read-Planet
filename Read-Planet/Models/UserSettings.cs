namespace Read_Planet.Models
{
    public class UserSettings
    {
        public List<SettingsObj> GeneralSettings { get; set; } = new()
    {
        new()
        {
            Category = "Communication Settings",
            SubCategory = new Dictionary<string, bool>
            {
                { "Email Notification", true }
            }
        },

        new()
        {
            Category = "Theme and Appearance",
            SubCategory = new Dictionary<string, bool>
            {
                { "Theme Selection", true },
                { "Customization", true }
            }
        },

        new()
        {
            Category = "Security Settings",
            SubCategory = new Dictionary<string, bool>
            {
                { "Two-Factor Authentication", false },
                { "Password Update", true }
            }
        },

        new()
        {
            Category = "Privacy Settings",
            SubCategory = new Dictionary<string, bool>
            {
                { "Profile Visibility", true },
                { "Email Privacy", false }
            }
        },

        new()
        {
            Category = "Content Management",
            SubCategory = new Dictionary<string, bool>
            {
                { "Categories and Tags", false },
                { "Featured Posts", true }
            }
        },

        new()
        {
            Category = "Account Deactivation",
            SubCategory = new Dictionary<string, bool>
            {
                { "Deactivate Account", true }
            }
        }
    };
    }

    public class SettingsObj
    {
        public string Category { get; set; } = "";
        public Dictionary<string, bool> SubCategory { get; set; } = new();
    }
}
