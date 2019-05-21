using MonkeyCache.SQLite;
using System;
using System.Collections.Generic;

namespace WeatherMap.Helpers
{
    public class Cache
    {

        private static Cache _instance;

        private Cache()
        {

        }

        public static Cache Instance
        {
            get
            {
                if (_instance == null)
                {
                    _instance = new Cache();
                    Barrel.ApplicationId = "WeatherMap2";
                }

                return _instance;
            }
            set { }
        }

        public void Save<T>(List<T> dataObject, string key )
        {
            Barrel.Current.Add(key, dataObject, new TimeSpan(0));
        }

        public List<T> Get<T>(string key)
        {
            return Barrel.Current.Get<List<T>>(key);
        }

        public void Clear()
        {
            Barrel.Current.EmptyAll();
        }
    }
}
