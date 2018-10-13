using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.CategoryCreator.Hook
{
    public static class EditHooks
    {
        // all methods require pre-patched assembly to compile
        // camera stuff
        public static bool m_dicPartsTypeCamera_ext_flag = false;
        public static void m_dicPartsTypeCamera_ext ()
        {
            if (m_dicPartsTypeCamera_ext_flag)
            {return; }
            m_dicPartsTypeCamera_ext_flag = true;
            string text = "edit_attention_point_define_creator.nei";

            using (AFileBase afileBase = GameUty.FileSystemMod.FileOpen(text))
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    NDebug.Assert(condition, text + "\nopen failed.");
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            SceneEditInfo.CamToBone value = default(SceneEditInfo.CamToBone);
                            int num = 0;
                            MPN key = MPN.null_mpn;
                            try
                            {
                                key = (MPN)Enum.Parse(typeof(MPN), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {}
                            value.bone = csvParser.GetCellAsString(num++, i);
                            value.angle = wf.Parse.Vector2(csvParser.GetCellAsString(num++, i));
                            value.distance = csvParser.GetCellAsReal(num++, i);
                            SceneEditInfo.m_dicPartsTypeCamera_[key] = value;
                        }
                    }
                }

            }

        }

        // actual edit categories
        public static bool m_dicSliderPartsTypeBtnName_ext_flag = false;
        public static void m_dicSliderPartsTypeBtnName_ext()
        {
            if (m_dicSliderPartsTypeBtnName_ext_flag)
            {
                return;
            }
            m_dicSliderPartsTypeBtnName_ext_flag = true;

            string text = "edit_category_define_creator.nei";
           
            Dictionary<SceneEditInfo.EMenuCategory, int> dictionary = new Dictionary<SceneEditInfo.EMenuCategory, int>();
            using (AFileBase afileBase = GameUty.FileSystemMod.FileOpen(text))
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    NDebug.Assert(condition, text + "\nopen failed.");
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            int num = 0;
                            MPN key = MPN.null_mpn;
                            try
                            {
                                key = (MPN)Enum.Parse(typeof(MPN), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {                                
                            }
                            SceneEditInfo.CCateNameType ccateNameType = new SceneEditInfo.CCateNameType();
                            ccateNameType.m_eMenuCate = SceneEditInfo.EMenuCategory.頭;
                            try
                            {
                                ccateNameType.m_eMenuCate = (SceneEditInfo.EMenuCategory)Enum.Parse(typeof(SceneEditInfo.EMenuCategory), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {                               
                            }
                            ccateNameType.m_eType = SceneEditInfo.CCateNameType.EType.Item;
                            try
                            {
                                ccateNameType.m_eType = (SceneEditInfo.CCateNameType.EType)Enum.Parse(typeof(SceneEditInfo.CCateNameType.EType), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {                                
                            }
                            ccateNameType.m_ePartsType = csvParser.GetCellAsString(num++, i);
                            ccateNameType.m_strBtnPartsTypeName = csvParser.GetCellAsString(num++, i);
                            ccateNameType.m_nIdx = 50 + i;
                            SceneEditInfo.dicPartsTypePair_[key] = ccateNameType;
                        }
                    }
                }
                
            }

        }
        // edit masking modes
        static bool m_dicPartsTypeWearMode_ext_flag = false;
        public static void m_dicPartsTypeWearMode_ext()
        {
            if (m_dicPartsTypeWearMode_ext_flag)
            {
                return;
            }
            m_dicPartsTypeWearMode_ext_flag = true;
            string text = "edit_mask_define_creator.nei";
            SceneEditInfo.dicPartsTypeWearMode_ = new Dictionary<MPN, TBody.MaskMode>();
            using (AFileBase afileBase = GameUty.FileSystem.FileOpen(text))
            {
                using (CsvParser csvParser = new CsvParser())
                {
                    bool condition = csvParser.Open(afileBase);
                    
                    for (int i = 1; i < csvParser.max_cell_y; i++)
                    {
                        if (csvParser.IsCellToExistData(0, i))
                        {
                            int num = 0;
                            MPN key = MPN.null_mpn;
                            try
                            {
                                key = (MPN)Enum.Parse(typeof(MPN), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {}
                            TBody.MaskMode value = TBody.MaskMode.None;
                            try
                            {
                                value = (TBody.MaskMode)Enum.Parse(typeof(TBody.MaskMode), csvParser.GetCellAsString(num++, i));
                            }
                            catch 
                            {                              
                            }
                           
                            SceneEditInfo.dicPartsTypeWearMode_[key]= value;
                        }
                    }
                }
            }
        }
      

    }
}
