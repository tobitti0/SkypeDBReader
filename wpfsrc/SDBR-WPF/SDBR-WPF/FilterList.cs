using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SkypeDBReader
{
    [Serializable]
    public class FilterList : IEquatable<FilterList>
    {
        public string Collection { get; set; }

        public override int GetHashCode()
        {
            return this.Collection.GetHashCode();
        }
        public bool Equals(FilterList other)
        {
            if (other == null)
                return false;
            return (this.Collection == other.Collection);
        }
    }
}
