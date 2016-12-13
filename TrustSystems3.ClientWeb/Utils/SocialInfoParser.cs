using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using Newtonsoft.Json.Linq;

namespace TrustSystems3.ClientWeb.Utils
{
    public class SocialInfoParser: IDisposable
    {
        private IEnumerable<Claim> _claims;

        public SocialInfoParser(IEnumerable<Claim> claimList)
        {
            _claims = claimList;
        }

        public string GetString(string type)
        {
            var claim = _claims.FirstOrDefault(c => c.Type == type);
            if (claim != null)
            {
                return claim.Value;
            }

            return null;
        }


        public string GetJsonItem(string type, string key)
        {
            var claim = _claims.FirstOrDefault(c => c.Type == type);
            if (claim != null)
            {
                JObject obj = JObject.Parse(claim.Value);
                if (obj != null && obj.HasValues && obj[key] != null)
                {
                    return obj[key].ToString();
                }
            }

            return null;
        }

        public string GetJsonArrayItem(string type, string key)
        {
            var claim = _claims.FirstOrDefault(c => c.Type == type);
            if (claim != null)
            {
                JArray array = JArray.Parse(claim.Value);
                if (array != null && array.HasValues)
                {
                    JToken obj = array.First;
                    if (obj != null && obj.HasValues && obj[key] != null)
                    {
                        return obj[key].ToString();
                    }
                }
                
            }

            return null;
        }


        public void Dispose()
        {
        }
    }
}