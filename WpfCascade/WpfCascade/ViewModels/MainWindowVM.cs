using System.Collections.Generic;
using System.Collections.ObjectModel;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WpfCascade.Models;
using WPFDevelopers.Controls;

namespace WpfCascade.ViewModels
{
    public partial class MainWindowVM : ObservableObject
    {
        private string _city;

        private ObservableCollection<CityInfo> _cityInfos;
        public MainWindowVM()
        {
            _cityInfos = new ObservableCollection<CityInfo>();
            _cityInfos.Add(new CityInfo
            {
                Name = "北京",
                ItemsSource = new List<CityInfo>
                {
                    new CityInfo {Name = "西城区"}, new CityInfo {Name = "东城区"}, new CityInfo {Name = "海淀区"},
                    new CityInfo {Name = "朝阳区"}, new CityInfo {Name = "丰台区"}, new CityInfo {Name = "石景山区"}
                }
            });
            _cityInfos.Add(new CityInfo
            {
                Name = "四川", ItemsSource = new List<CityInfo>
                {
                    new CityInfo {Name = "成都市"},
                    new CityInfo
                    {
                        Name = "巴中市", ItemsSource = new List<CityInfo>
                        {
                            new CityInfo
                            {
                                Name = "恩阳区"
                            },
                            new CityInfo
                            {
                                Name = "南江县"
                            },
                            new CityInfo
                            {
                                Name = "通江县"
                            }
                        }
                    }
                }
            });
            _cityInfos.Add(new CityInfo
            {
                Name = "山东", ItemsSource = new List<CityInfo>
                {
                    new CityInfo {Name = "青岛市"},
                    new CityInfo {Name = "烟台市"},
                    new CityInfo {Name = "威海市"},
                    new CityInfo {Name = "枣庄市"},
                    new CityInfo
                    {
                        Name = "潍坊市",
                        ItemsSource = new List<CityInfo>
                        {
                            new CityInfo
                            {
                                Name = "青州市"
                            },
                            new CityInfo
                            {
                                Name = "诸城市"
                            },
                            new CityInfo
                            {
                                Name = "寿光市"
                            },
                            new CityInfo
                            {
                                Name = "安丘市"
                            },
                            new CityInfo
                            {
                                Name = "高密市"
                            },
                            new CityInfo
                            {
                                Name = "昌邑市"
                            }
                        }
                    }
                }
            });
        }

        public string City
        {
            get => _city;
            set => SetProperty(ref _city, value);
        }

        public ObservableCollection<CityInfo> CityInfos
        {
            get => _cityInfos;
            set => SetProperty(ref _cityInfos, value);
        }

        [RelayCommand]
        private void GetCity()
        {
            if (string.IsNullOrWhiteSpace(City)) return;
            Message.Push($"选择的城市为：{City}");
        }
    }
}