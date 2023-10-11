using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Terminal
{
    public class Filters
    {
        public int FindApplicableFilter(Profile _activeProfile, string str, int offset)
        {
            if (offset >= str.Length)
                return -1;

            if (_activeProfile.displayOptions.IgnoreCase)
            {
                for (int i = 0; i < _activeProfile.displayOptions.filter.Length; i++)
                {
                    if (_activeProfile.displayOptions.filter[i].text.Length > 0)
                    {
                        // starts with
                        if (_activeProfile.displayOptions.filter[i].mode == 0)
                        {
                            if (str.ToLower().Substring(offset).StartsWith(_activeProfile.displayOptions.filter[i].text.ToLower()))
                                return i;
                        }

                        // contains
                        else if (_activeProfile.displayOptions.filter[i].mode == 1)
                        {
                            if (str.ToLower().Substring(offset).Contains(_activeProfile.displayOptions.filter[i].text.ToLower()))
                                return i;
                        }
                    }
                }
            }
            else
            {
                for (int i = 0; i < _activeProfile.displayOptions.filter.Length; i++)
                {
                    if (_activeProfile.displayOptions.filter[i].text.Length > 0)
                    {
                        // starts with
                        if (_activeProfile.displayOptions.filter[i].mode == 0)
                        {
                            if (str.Substring(offset).StartsWith(_activeProfile.displayOptions.filter[i].text))
                                return i;
                        }

                        // contains
                        else if (_activeProfile.displayOptions.filter[i].mode == 1)
                        {
                            if (str.Substring(offset).Contains(_activeProfile.displayOptions.filter[i].text))
                                return i;
                        }
                    }
                }
            }
            return -1;
        }
    }
}
