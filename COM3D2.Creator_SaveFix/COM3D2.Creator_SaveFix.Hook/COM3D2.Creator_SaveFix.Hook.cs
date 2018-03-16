using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace COM3D2.Creator_SaveFix.Hook
{
    public static class savefix
    {
        public static void GetPropStringFix(ref Dictionary<string, MaidProp> m_dicMaidProp, ref string tag)
        {
            if (!m_dicMaidProp.ContainsKey(tag))
            {
                tag = "null_mpn";
            }
        }


        public static void CM_dic_fix()
        {
         CM3.dicDelItem[MPN.null_mpn] = string.Empty;
        
            }

        public static void MaidPropDesFix( ref string name)
        {
            int idx = 0;

            try
            {
                idx = (int)Enum.Parse(typeof(MPN), name, false);

            }
            catch (Exception e)
            {
                name = "null_mpn";

            }

        }
    }

   }
