using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.CategoryCreator.Hook
{
    public class Catmanager
    {
        // since accessing MPN enum directly is not an option, it it's necessary to create a dictionary of enum string - int pairs
        // and because in some cases i need to check what string belongs to which integer there is also a need for inverted dictionary -_-
        public static Dictionary<string, int> MPN_string_int = new Dictionary<string, int>();
        public static Dictionary<int, string> MPN_int_string = new Dictionary<int, string>();
        public static void MPNgenerator()
        {
            string[] MPN_strings= Enum.GetNames(typeof(MPN));
            
            foreach (string str in MPN_strings)
            {
                int MPN = (int)Enum.Parse(typeof(MPN),str);
                MPN_int_string[MPN]= str;
                MPN_string_int[str] = MPN;
            }
            int[] MPN_int = (int[])Enum.GetValues(typeof(MPN));
          
        }

         

        //extended array for m_strDefSlotName field
        public static string[] m_strDefSlotName_Ext = new string[]
  {
                   
        /// start of new modelslots
        "nails",
        "_ROOT_",
        "Jyouhanshin",
        ///modelslot sample
       
         "accnail",
         "_ROOT_",
        "Jyouhanshin",

            "acctoenail",
            "_ROOT_",
        "Jyouhanshin",

            "acchandl",
            "_ROOT_",
        "Jyouhanshin",

            "acchandr",
            "_ROOT_",
        "Jyouhanshin",

     "ears",
     "Bip01 Head",
     "Jyouhanshin",
     "horns",
     "Bip01 Head",
     "Jyouhanshin",

        "end"
  };
        // method for adding new MPN's to scene editinfo. this method requires pre-patched Assembly-Charp.dll to work



        //register del menu in CM3 class
        public static void delmenuadder()
        {

            CM3.dicDelItem[(MPN)MPN_string_int["folder_eye2"]] = "_I_folder_eye2_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["body"]] = "body001_i_.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["body"]] = "_i_skintoon_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["acctoenail"]] = "_i_acctoenail_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["acchandl"]] = "_i_acchandl_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["acchandr"]] = "_i_acchandr_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["ears"]] = "_I_ears_del.menu";
            CM3.dicDelItem[(MPN)MPN_string_int["horns"]] = "_I_horns_del.menu";

            
        }

        // add more MPN's to preset set method
        public static void ExtSet(ref MaidProp[] array, Maid f_maid, CharacterMgr.Preset f_prest )
        {
            List<MaidProp> customlist;
            List<MaidProp> baselist= array.ToList();
            if (f_prest.ePreType == CharacterMgr.PresetType.Body)
            {
                customlist = (from mp in f_prest.listMprop
                              where MPN_string_int["folder_eye2"] <= mp.idx && mp.idx <= MPN_string_int["skintoon"]
                              select mp).ToList();
            }
            else if (f_prest.ePreType == CharacterMgr.PresetType.Wear)
            {
                customlist = (from mp in f_prest.listMprop
                              where MPN_string_int["acchandl"] <= mp.idx && mp.idx <= MPN_string_int["horns"]
                              select mp).ToList();
            }

            else if (f_prest.ePreType == CharacterMgr.PresetType.All)
            {
                customlist = (from mp in f_prest.listMprop
                              where MPN_string_int["folder_eye2"] <= mp.idx && mp.idx <= MPN_string_int["horns"]
                              select mp).ToList();
            }
            else customlist = null;
            if (customlist != null)
            {
                 baselist.AddRange(customlist);
                array = baselist.ToArray();
            }
        }

        // method for modifying m_strDefSlotName filed. this method requires pre-patched Assembly-Charp.dll
        // that has this field witout IsInitOnly attribute
        // because target method can be execute multiple times, i need a flag so this hook is only run once.

        static bool sloteext_flag = false;
        public static void slotext()
        {
            if (!sloteext_flag)
            {
                List<string> templist = TBody.m_strDefSlotName.ToList();
                templist.Remove(templist.Last());
                templist.AddRange(m_strDefSlotName_Ext);
                TBody.m_strDefSlotName = templist.ToArray();
                sloteext_flag = true;
            }


         }


        // extend CreateInitMaidPropList  so game won't go derp when new MPN field is added.
        //this method requires pre-patched Assembly-Charp.dll to work
        public static void maidpropext(ref List<MaidProp> baselist)
        {

            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["folder_eye2"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["eye2"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["skintoon"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["acctoenail"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["acchandl"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["acchandr"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["ears"], 3));
            baselist.Add(Maid.CreateProp(string.Empty, (MPN)MPN_string_int["horns"], 3));

       
           }        
        // method for injecting MPN's to Maid.AllProcProp 
        public static void AllProcExt(Maid maid, ref MaidProp maidProp)
        {
            if (MPN_int_string[maidProp.idx] == "head")
            {
                maid.GetProp((MPN)MPN_string_int["eye2"]).boDut = true;
                maid.GetProp((MPN)MPN_string_int["skintoon"]).boDut = true;
            }
            else if (MPN_int_string[maidProp.idx] == "skin")
            {

                maid.GetProp((MPN)MPN_string_int["skintoon"]).boDut = true;

            }
            else if (MPN_int_string[maidProp.idx] == "eye" || MPN_int_string[maidProp.idx] == "folder_eye")
            {
                maid.GetProp((MPN)MPN_string_int["folder_eye2"]).boDut = true;
                maid.GetProp((MPN)MPN_string_int["eye2"]).boDut = true;
                
            }
        }


        // method for injecting MPN's to Maid.AllProcProp and Maid.AllPropcPropSeq
        public static void AllProcSeqExt(Maid maid, ref MaidProp maidProp)
        {
            if (maidProp.type == 3)
            {
                if (MPN_int_string[maidProp.idx] == "head")
                {
                    maid.GetProp((MPN)MPN_string_int["eye2"]).boDut = true;
                    maid.GetProp((MPN)MPN_string_int["skintoon"]).boDut = true;
                }
                else if (MPN_int_string[maidProp.idx] == "skin")
                {

                    maid.GetProp((MPN)MPN_string_int["skintoon"]).boDut = true;

                }
                else if (MPN_int_string[maidProp.idx] == "eye" || MPN_int_string[maidProp.idx] == "folder_eye")
                {
                    maid.GetProp((MPN)MPN_string_int["folder_eye2"]).boDut = true;
                    maid.GetProp((MPN)MPN_string_int["eye2"]).boDut = true;

                }
                else if (MPN_int_string[maidProp.idx] =="body")
                {
                    for (int i = maidProp.idx+1; i <= MPN_string_int["horns"]; i++)
                    {
                        maid.GetProp((MPN)i).boDut = true;
                    }
                }

            }

        }
        public static bool GetParentMenuFileNamEext(out string result, SceneEdit.SMenuItem mi)
        {
            if (( mi.m_mpn == (MPN)MPN_string_int["accnail"] ) || mi.m_mpn> (MPN)MPN_string_int["skintoon"] || mi.m_mpn == (MPN)MPN_string_int["acctoenail"])
            {


                string text = mi.m_strMenuFileName;
                result = string.Empty;
                text = text.ToLower();
                int num = text.IndexOf("_z");
                if (0 < num)
                {
                    int num2 = text.IndexOf('_', num + 1);
                    if (num2 == -1)
                    {
                        num2 = text.IndexOf('.', num + 1);
                    }
                    if (0 < num2 - num && 0 < text.Length - num2)
                    {
                        result = text.Substring(0, num) + text.Substring(num2, text.Length - num2);
                    }
                }

            }
            else result = null;
            return result != null;



        }

        // ignore localization shenanigans of 1.17 and replace custom category name with one from edit_category_define.nei
        public static void locale_handler(ref SceneEdit.SPartsType sPartsType, ref UILabel label)
        {
            label.text = sPartsType.m_strPartsTypeName;

        }
    }

    }



