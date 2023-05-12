using TestFramework.Driver;

namespace TestFramework.Config
{
    public class TestSettings
    {
        public DriverType DriverType { get; set; }

        public string ApplicationUrl { get; set; }

        public string[]? Args { get; set; }

        public float? Timeout = PlaywrightDriverInitializer.DEFAULT_TIMEOUT;
        
        public bool? Headless { get; set; }
        
        public float? SlowMo {  get; set; }
        
        public bool? DevTools { get; internal set; }
    }

    public enum DriverType
    {
        Chrome,
        Firefox,
        Edge,
        Chromium,
        Opera,
    }
}
