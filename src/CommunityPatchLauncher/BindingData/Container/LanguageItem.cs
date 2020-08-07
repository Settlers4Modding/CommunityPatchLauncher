using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CommunityPatchLauncher.BindingData.Container
{
    public class LanguageItem
    {
        public string Name { get; }
        public string IsoCode { get; }

        public LanguageItem(string name, string isoCode)
        {
            Name = name;
            IsoCode = isoCode;
        }


    }
}
