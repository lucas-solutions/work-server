using System.Configuration;

namespace Lucas.Solutions.Configuration
{
    public abstract class TaskElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsRequired = true)]
        public string Name
        {
            get { return (string)base["name"]; }
            set { base["name"] = value; }
        }

        [ConfigurationProperty("start", IsRequired = true)]
        public string Start
        {
            get { return (string)base["start"]; }
            set { base["start"] = value; }
        }

        [ConfigurationProperty("summary", IsRequired = false)]
        public virtual string Summary
        {
            get { return (string)base["summary"]; }
            set { base["summary"] = value; }
        }
    }
}
