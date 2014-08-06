using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;

namespace Custom.Diagnostics
{
    using Custom.Filters;

    public class DiagnosticsManager
    {
        public const string Total = "Total";
        public const string PerformanceCounterManagerApplicationKey = "CustomaryPerformanceCounter";

        private Dictionary<string, PerformanceCounter> counterMap;
        
        public List<string> CounterTypeNames
        {
            get { return null; }
        }

        /// <summary>
        /// This method reflects over the given assembly(ies) in a given path 
        /// and creates the base operations required  perf counters
        /// </summary>
        /// <param name="assemblyPath"></param>
        /// <param name="assemblyFilter"></param>
        public void Create(string assemblyPath, string assemblyFilter)
        {
            counterMap = new Dictionary<string, PerformanceCounter>();

            foreach (string assemblyName in Directory.EnumerateFileSystemEntries(assemblyPath, assemblyFilter))
            {
                Type[] allTypes = Assembly.LoadFrom(assemblyName).GetTypes();

                foreach (Type t in allTypes)
                {
                    if (typeof(IController).IsAssignableFrom(t))
                    {
                        MemberInfo[] infos = Type.GetType(t.AssemblyQualifiedName).GetMembers();

                        foreach (MemberInfo memberInfo in infos)
                        {
                            foreach (object info in memberInfo.GetCustomAttributes(typeof(DiagnosticsAttribute), true))
                            {
                                DiagnosticsAttribute diagnostics = info as DiagnosticsAttribute;

                                string category = diagnostics.Category;
                                string instance = diagnostics.Instance;

                                // Create total rollup instances, if they don't exist
                                foreach (string type in CounterTypeNames)
                                {
                                    if (!counterMap.ContainsKey(KeyBuilder(Total, type)))
                                    {
                                        counterMap.Add(KeyBuilder(Total, type), CreateInstance(category, type, Total));
                                    }
                                }

                                // Create performance counters
                                foreach (string type in CounterTypeNames)
                                {
                                    counterMap.Add(KeyBuilder(instance, type), CreateInstance(category, type, instance));
                                }
                            }
                        }
                    }
                }
            }
        }

        /// <summary>
        /// Record the latency for a given instance name
        /// </summary>
        /// <param name="instance"></param>
        /// <param name="latency"></param>
        public void RecordLatency(string instance, long latency)
        {
            if (counterMap.ContainsKey(KeyBuilder(instance, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatency])) && counterMap.ContainsKey(KeyBuilder(instance, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatencyBase])))
            {
                counterMap[KeyBuilder(instance, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatency])].IncrementBy(latency);
                counterMap[KeyBuilder(Total, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatency])].IncrementBy(latency);
                counterMap[KeyBuilder(instance, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatencyBase])].Increment();
                counterMap[KeyBuilder(Total, CounterTypeNames[(int)PerformanceCounterTypes.AverageLatencyBase])].Increment();
            }
        }

        private PerformanceCounter CreateInstance(string category, string type, string instance)
        {
            return null;
        }

        private string KeyBuilder(string instance, string type)
        {
            return null;
        }
    }
}