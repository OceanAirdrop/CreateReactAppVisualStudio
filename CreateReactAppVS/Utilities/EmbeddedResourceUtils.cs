using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// Alias Dictionary Items
using ResourceName = System.String;
using ResourceContents = System.String;
using System.Reflection;
using System.IO;

namespace CreateReactAppVS.Utilities
{
    public enum ResourceAccessType { CallingAssembly, EntryAssembly, ExecutingAssembly }
    public static class EmbeddedResourceUtils
    {
        private static Dictionary<ResourceName, ResourceContents> ResourceDictionary = new Dictionary<ResourceName, ResourceContents>();

        public static string GetResource(string resourceName, ResourceAccessType type = ResourceAccessType.EntryAssembly)
        {
            string resourceContents = "";

            try
            {
                if (ResourceDictionary.ContainsKey(resourceName))
                {
                    resourceContents = ResourceDictionary[resourceName];
                    return resourceContents;
                }

                // This version uses GetEntryAssembly!!! 
                string[] names = null;

                switch (type)
                {
                    case ResourceAccessType.CallingAssembly:
                        names = Assembly.GetCallingAssembly().GetManifestResourceNames();
                        break;
                    case ResourceAccessType.EntryAssembly:
                        names = Assembly.GetEntryAssembly().GetManifestResourceNames();
                        break;
                    case ResourceAccessType.ExecutingAssembly:
                        names = Assembly.GetExecutingAssembly().GetManifestResourceNames();
                        break;
                }


                string resource = "";
                foreach (string str in names)
                {
                    if (str.ToLower().Contains(resourceName.ToLower()) == true)
                    {
                        resource = str;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(resource) == false)
                {
                    switch (type)
                    {
                        case ResourceAccessType.CallingAssembly:
                            using (StreamReader sreader = new StreamReader(Assembly.GetCallingAssembly().GetManifestResourceStream(resource), Encoding.Default))
                            {
                                resourceContents = sreader.ReadToEnd();
                            }
                            break;
                        case ResourceAccessType.EntryAssembly:
                            using (StreamReader sreader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream(resource), Encoding.Default))
                            {
                                resourceContents = sreader.ReadToEnd();
                            }
                            break;
                        case ResourceAccessType.ExecutingAssembly:
                            using (StreamReader sreader = new StreamReader(Assembly.GetExecutingAssembly().GetManifestResourceStream(resource), Encoding.Default))
                            {
                                resourceContents = sreader.ReadToEnd();
                            }
                            break;
                    }

                    ResourceDictionary.Add(resourceName, resourceContents);
                }
            }
            catch (Exception ex)
            {
                // log exception here
                throw;
            }

            return resourceContents;
        }

        public static string GetAppResource(string resourceName)
        {
            string resourceContents = "";

            try
            {
                if (ResourceDictionary.ContainsKey(resourceName))
                {
                    resourceContents = ResourceDictionary[resourceName];
                    return resourceContents;
                }

                // This version uses GetEntryAssembly!!! 
                string[] names = Assembly.GetEntryAssembly().GetManifestResourceNames();

                string resource = "";
                foreach (string str in names)
                {
                    if (str.ToLower().Contains(resourceName.ToLower()) == true)
                    {
                        resource = str;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(resource) == false)
                {
                    using (StreamReader sreader = new StreamReader(Assembly.GetEntryAssembly().GetManifestResourceStream(resource), Encoding.Default))
                    {
                        resourceContents = sreader.ReadToEnd();
                    }

                    ResourceDictionary.Add(resourceName, resourceContents);
                }
            }
            catch (Exception ex)
            {
                // log exception here
                throw;
            }

            return resourceContents;
        }

        public static string GetDLLResource(string dllName, string resourceName)
        {
            string resourceContents = "";

            try
            {
                if (ResourceDictionary.ContainsKey(resourceName))
                {
                    resourceContents = ResourceDictionary[resourceName];
                    return resourceContents;
                }

                if (dllName.ToLower().EndsWith(".dll"))
                {
                    dllName = dllName.Replace(".dll", "");
                }

                var ass = Assembly.Load(dllName);

                string[] names = ass.GetManifestResourceNames();

                string resource = "";
                foreach (string str in names)
                {
                    if (str.ToLower().Contains(resourceName.ToLower()) == true)
                    {
                        resource = str;
                        break;
                    }
                }

                if (string.IsNullOrEmpty(resource) == false)
                {
                    using (StreamReader sreader = new StreamReader(ass.GetManifestResourceStream(resource), Encoding.Default))
                    {
                        resourceContents = sreader.ReadToEnd();
                    }

                    ResourceDictionary.Add(resourceName, resourceContents);
                }
            }
            catch (Exception ex)
            {
                // log exception here
                throw;
            }

            return resourceContents;
        }
    }
}
