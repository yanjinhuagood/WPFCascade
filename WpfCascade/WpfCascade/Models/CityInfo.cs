using System.Collections.Generic;

namespace WpfCascade.Models
{
    public struct CityInfo
    {
        public string Name { get; set; }
        public List<CityInfo> ItemsSource { get; set; }
    }
}
