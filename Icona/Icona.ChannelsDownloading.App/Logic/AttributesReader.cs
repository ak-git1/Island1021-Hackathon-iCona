using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ak.Framework.Core.Extensions;

namespace Icona.ChannelsDownloading.App.Logic
{
    class AttributesReader
    {
        private readonly string _attributesString;

        public AttributesReader(string attributesString)
        {
            _attributesString = attributesString;
        }

        public Dictionary<string, string> Read()
        {
            Dictionary<string, string> result = new Dictionary<string, string>();

            if (!_attributesString.IsNullOrEmpty())
                using (StringReader r = new StringReader(_attributesString))
                    while (true)
                    {
                        string line = r.ReadLine();
                        if (line == null)
                            break;
                        else
                        {
                            if (line.Trim().Length > 0)
                            {
                                string[] vals = line.Split('=');
                                if (vals.Length == 2)
                                    result.Add(vals[0], vals[1]);
                            }
                        }
                    }

            return result;
        }
    }
}
