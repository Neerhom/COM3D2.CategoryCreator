using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace COM3D2.CategoryCreator.Hook
{
    public class hooks
    {
        //extended array for m_strDefSlotName field
        public static string[] m_strDefSlotName = new string[]
  {
      "body",
            "_ROOT_",
            "IK",
            "head",
            "Bip01 Head",
            "Jyouhanshin",
            "eye",
            "Bip01 Head",
            "Jyouhanshin",
            "hairF",
            "Bip01 Head",
            "Jyouhanshin",
            "hairR",
            "Bip01 Head",
            "Jyouhanshin",
            "hairS",
            "Bip01 Head",
            "Jyouhanshin",
            "hairT",
            "Bip01 Head",
            "Jyouhanshin",
            "wear",
            "_ROOT_",
            "Uwagi",
            "skirt",
            "_ROOT_",
            "Kahanshin",
            "onepiece",
            "_ROOT_",
            "Kahanshin",
            "mizugi",
            "_ROOT_",
            "Kahanshin",
            "panz",
            "_ROOT_",
            "Kahanshin",
            "bra",
            "_ROOT_",
            "Jyouhanshin",
            "stkg",
            "_ROOT_",
            "Kahanshin",
            "shoes",
            "_ROOT_",
            "Kahanshin",
            "headset",
            "Bip01 Head",
            "Jyouhanshin",
            "glove",
            "_ROOT_",
            "Uwagi",
            "accHead",
            "Bip01 Head",
            "Jyouhanshin",
            "hairAho",
            "Bip01 Head",
            "Jyouhanshin",
            "accHana",
            "_ROOT_",
            "Jyouhanshin",
            "accHa",
            "Bip01 Head",
            "Jyouhanshin",
            "accKami_1_",
            "Bip01 Head",
            "Jyouhanshin",
            "accMiMiR",
            "Bip01 Head",
            "Jyouhanshin",
            "accKamiSubR",
            "Bip01 Head",
            "Jyouhanshin",
            "accNipR",
            "_ROOT_",
            "Uwagi",
            "HandItemR",
            "_IK_handR",
            "Uwagi",
            "accKubi",
            "Bip01 Spine1a",
            "Jyouhanshin",
            "accKubiwa",
            "Bip01 Neck",
            "Jyouhanshin",
            "accHeso",
            "Bip01 Head",
            "Jyouhanshin",
            "accUde",
            "_ROOT_",
            "Uwagi",
            "accAshi",
            "_ROOT_",
            "Uwagi",
            "accSenaka",
            "_ROOT_",
            "Uwagi",
            "accShippo",
            "Bip01 Spine",
            "Uwagi",
            "accAnl",
            "_ROOT_",
            "Uwagi",
            "accVag",
            "_ROOT_",
            "Uwagi",
            "kubiwa",
            "_ROOT_",
            "Uwagi",
            "megane",
            "Bip01 Head",
            "Jyouhanshin",
            "accXXX",
            "_ROOT_",
            "Uwagi",
            "chinko",
            "Bip01 Pelvis",
            "Uwagi",
            "chikubi",
            "_ROOT_",
            "Jyouhanshin",
            "accHat",
            "Bip01 Head",
            "Jyouhanshin",
            "kousoku_upper",
            "_ROOT_",
            "Uwagi",
            "kousoku_lower",
            "_ROOT_",
            "Kahanshin",
            "seieki_naka",
            "_ROOT_",
            "Uwagi",
            "seieki_hara",
            "_ROOT_",
            "Uwagi",
            "seieki_face",
            "_ROOT_",
            "Uwagi",
            "seieki_mune",
            "_ROOT_",
            "Uwagi",
            "seieki_hip",
            "_ROOT_",
            "Uwagi",
            "seieki_ude",
            "_ROOT_",
            "Uwagi",
            "seieki_ashi",
            "_ROOT_",
            "Uwagi",
            "accNipL",
            "_ROOT_",
            "Uwagi",
            "accMiMiL",
            "Bip01 Head",
            "Jyouhanshin",
            "accKamiSubL",
            "Bip01 Head",
            "Jyouhanshin",
            "accKami_2_",
            "Bip01 Head",
            "Jyouhanshin",
            "accKami_3_",
            "Bip01 Head",
            "Jyouhanshin",
            "HandItemL",
            "_IK_handL",
            "Uwagi",
            "underhair",
            "_ROOT_",
            "Kahanshin",
            "moza",
            "_ROOT_",
            "Kahanshin",
            
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

            CM3.dicDelItem[MPN.folder_eye2] = "_I_folder_eye2_del.menu";
            CM3.dicDelItem[MPN.body] = "body001_i_.menu";
            CM3.dicDelItem[MPN.body] = "_i_skintoon_del.menu";
            CM3.dicDelItem[MPN.acctoenail] = "_i_acctoenail_del.menu";
            CM3.dicDelItem[MPN.acchandl] = "_i_acchandl_del.menu";
            CM3.dicDelItem[MPN.acchandr] = "_i_acchandr_del.menu";
            CM3.dicDelItem[MPN.ears] = "_I_ears_del.menu";
            CM3.dicDelItem[MPN.horns] = "_I_horns_del.menu";

        }

        // add more MPN's to preset set method
        public static void ExtSet(CharacterMgr self, Maid f_maid, CharacterMgr.Preset f_prest)
        {
            global::MaidProp[] array;

            if (f_prest.ePreType == global::CharacterMgr.PresetType.Body)
            {
                array = (from mp in f_prest.listMprop
                         where 100 <= mp.idx && mp.idx <= 103
                         select mp).ToArray<global::MaidProp>();
            }
            else if (f_prest.ePreType == global::CharacterMgr.PresetType.Wear)
            {
                array = (from mp in f_prest.listMprop
                         where 104 <= mp.idx && mp.idx <= 107
                         select mp).ToArray<global::MaidProp>();
            }

            else if (f_prest.ePreType == global::CharacterMgr.PresetType.All)
            {
                array = (from mp in f_prest.listMprop
                         where 100 <= mp.idx && mp.idx <= 107
                         select mp).ToArray<global::MaidProp>();
            }

            else
            { array = null; }

            if (array != null)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    MaidProp maidProp = array[i];
                    if (maidProp.type != 3)
                    {
                        f_maid.SetProp((MPN)maidProp.idx, maidProp.value, false);
                    }
                    else
                    {
                        if (string.IsNullOrEmpty(maidProp.strFileName))
                        {
                            string strFileName = maidProp.strFileName;
                            if (CM3.dicDelItem.TryGetValue((MPN)maidProp.idx, out strFileName))
                            {
                                maidProp.strFileName = strFileName;
                            }
                        }
                        if (self.IsEnableMenu(maidProp.strFileName))
                        {
                            f_maid.SetProp(maidProp);
                        }
                        else
                        {
                            f_maid.DelProp((MPN)maidProp.idx, false);
                        }
                    }
                }

            }
        }

        // method for modifying m_strDefSlotName filed. this method requires pre-patched Assembly-Charp.dll
        // that has this field witout IsInitOnly attribute
        public static void slotext()
        {

            TBody.m_strDefSlotName = m_strDefSlotName;
        }


        // extend CreateInitMaidPropList  so game won't go derp when new MPN field is added.
        //this method requires pre-patched Assembly-Charp.dll to work
        public static bool maidpropext(out List<MaidProp> result)
        {
            result = new List<global::MaidProp>
        {
            Maid.CreateProp(0, 2147483647, 0, global::MPN.null_mpn, 0),
            Maid.CreateProp(0, 130, 10, global::MPN.MuneL, 1),
            Maid.CreateProp(0, 100, 0, global::MPN.MuneS, 1),
            Maid.CreateProp(0, 130, 10, global::MPN.MuneTare, 1),
            Maid.CreateProp(0, 100, 40, global::MPN.RegFat, 1),
            Maid.CreateProp(0, 100, 20, global::MPN.ArmL, 1),
            Maid.CreateProp(0, 100, 20, global::MPN.Hara, 1),
            Maid.CreateProp(0, 100, 40, global::MPN.RegMeet, 1),
            Maid.CreateProp(20, 80, 50, global::MPN.KubiScl, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.UdeScl, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeScl, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeSclX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeSclY, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyePosX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyePosY, 2),
            Maid.CreateProp(0, 100, 0, global::MPN.EyeClose, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeBallPosX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeBallPosY, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeBallSclX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.EyeBallSclY, 2),
            Maid.CreateProp(0, 100, 0, global::MPN.FaceShape, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.MayuX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.MayuY, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.HeadX, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.HeadY, 2),
            Maid.CreateProp(20, 80, 50, global::MPN.DouPer, 2),
            Maid.CreateProp(20, 80, 50, global::MPN.sintyou, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.koshi, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.kata, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.west, 2),
            Maid.CreateProp(0, 100, 10, global::MPN.MuneUpDown, 2),
            Maid.CreateProp(0, 100, 40, global::MPN.MuneYori, 2),
            Maid.CreateProp(0, 100, 50, global::MPN.MuneYawaraka, 2),
            Maid.CreateProp(string.Empty, global::MPN.body, 3),
            Maid.CreateProp(string.Empty, global::MPN.head, 3),
            Maid.CreateProp(string.Empty, global::MPN.hairf, 3),
            Maid.CreateProp(string.Empty, global::MPN.hairr, 3),
            Maid.CreateProp(string.Empty, global::MPN.hairt, 3),
            Maid.CreateProp(string.Empty, global::MPN.hairs, 3),
            Maid.CreateProp(string.Empty, global::MPN.wear, 3),
            Maid.CreateProp(string.Empty, global::MPN.skirt, 3),
            Maid.CreateProp(string.Empty, global::MPN.mizugi, 3),
            Maid.CreateProp(string.Empty, global::MPN.bra, 3),
            Maid.CreateProp(string.Empty, global::MPN.panz, 3),
            Maid.CreateProp(string.Empty, global::MPN.stkg, 3),
            Maid.CreateProp(string.Empty, global::MPN.shoes, 3),
            Maid.CreateProp(string.Empty, global::MPN.headset, 3),
            Maid.CreateProp(string.Empty, global::MPN.glove, 3),
            Maid.CreateProp(string.Empty, global::MPN.acchead, 3),
            Maid.CreateProp(string.Empty, global::MPN.hairaho, 3),
            Maid.CreateProp(string.Empty, global::MPN.accha, 3),
            Maid.CreateProp(string.Empty, global::MPN.acchana, 3),
            Maid.CreateProp(string.Empty, global::MPN.acckamisub, 3),
            Maid.CreateProp(string.Empty, global::MPN.acckami, 3),
            Maid.CreateProp(string.Empty, global::MPN.accmimi, 3),
            Maid.CreateProp(string.Empty, global::MPN.accnip, 3),
            Maid.CreateProp(string.Empty, global::MPN.acckubi, 3),
            Maid.CreateProp(string.Empty, global::MPN.acckubiwa, 3),
            Maid.CreateProp(string.Empty, global::MPN.accheso, 3),
            Maid.CreateProp(string.Empty, global::MPN.accude, 3),
            Maid.CreateProp(string.Empty, global::MPN.accashi, 3),
            Maid.CreateProp(string.Empty, global::MPN.accsenaka, 3),
            Maid.CreateProp(string.Empty, global::MPN.accshippo, 3),
            Maid.CreateProp(string.Empty, global::MPN.accanl, 3),
            Maid.CreateProp(string.Empty, global::MPN.accvag, 3),
            Maid.CreateProp(string.Empty, global::MPN.megane, 3),
            Maid.CreateProp(string.Empty, global::MPN.accxxx, 3),
            Maid.CreateProp(string.Empty, global::MPN.handitem, 3),
            Maid.CreateProp(string.Empty, global::MPN.acchat, 3),
            Maid.CreateProp(string.Empty, global::MPN.haircolor, 3),
            Maid.CreateProp(string.Empty, global::MPN.skin, 3),
            Maid.CreateProp(string.Empty, global::MPN.acctatoo, 3),
            Maid.CreateProp(string.Empty, global::MPN.accnail, 3),
            Maid.CreateProp(string.Empty, global::MPN.underhair, 3),
            Maid.CreateProp(string.Empty, global::MPN.hokuro, 3),
            Maid.CreateProp(string.Empty, global::MPN.mayu, 3),
            Maid.CreateProp(string.Empty, global::MPN.lip, 3),
            Maid.CreateProp(string.Empty, global::MPN.eye, 3),
            Maid.CreateProp(string.Empty, global::MPN.eye_hi, 3),
            Maid.CreateProp(string.Empty, global::MPN.chikubi, 3),
            Maid.CreateProp(string.Empty, global::MPN.chikubicolor, 3),
            Maid.CreateProp(string.Empty, global::MPN.moza, 3),
            Maid.CreateProp(string.Empty, global::MPN.onepiece, 3),
            Maid.CreateProp(string.Empty, global::MPN.set_maidwear, 3),
            Maid.CreateProp(string.Empty, global::MPN.set_mywear, 3),
            Maid.CreateProp(string.Empty, global::MPN.set_underwear, 3),
            Maid.CreateProp(string.Empty, global::MPN.set_body, 3),
            Maid.CreateProp(string.Empty, global::MPN.folder_eye, 3),
            Maid.CreateProp(string.Empty, global::MPN.folder_mayu, 3),
            Maid.CreateProp(string.Empty, global::MPN.folder_underhair, 3),
            Maid.CreateProp(string.Empty, global::MPN.folder_skin, 3),
            Maid.CreateProp(string.Empty, global::MPN.kousoku_upper, 3),
            Maid.CreateProp(string.Empty, global::MPN.kousoku_lower, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_naka, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_hara, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_face, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_mune, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_hip, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_ude, 3),
            Maid.CreateProp(string.Empty, global::MPN.seieki_ashi, 3),

            Maid.CreateProp(string.Empty, MPN.folder_eye2, 3),
            Maid.CreateProp(string.Empty, MPN.eye2, 3),
            Maid.CreateProp(string.Empty, MPN.skintoon, 3),
          Maid.CreateProp(string.Empty, MPN.acctoenail, 3),
          Maid.CreateProp(string.Empty, MPN.acchandl, 3),
          Maid.CreateProp(string.Empty, MPN.acchandr, 3),
            Maid.CreateProp(string.Empty, MPN.ears, 3),
            Maid.CreateProp(string.Empty, MPN.horns, 3),

        };
            return result != null;
        }
        // method for injecting MPN's to Maid.AllProcProp 
        public static void AllProcExt(Maid maid, ref MaidProp maidProp)
        {
            if (maidProp.idx == 35)
            {
                maid.GetProp(MPN.eye2).boDut = true;
                maid.GetProp(MPN.skintoon).boDut = true;
            }
            else if (maidProp.idx == 42)
            {

                maid.GetProp(MPN.skintoon).boDut = true;

            }
            else if (maidProp.idx == 49 || maidProp.idx == 87)
            {
                maid.GetProp(MPN.folder_eye2).boDut = true;
                maid.GetProp(MPN.eye2).boDut = true;
            }
        }


        // method for injecting MPN's to Maid.AllProcProp and Maid.AllPropcPropSeq
        public static void AllProcSeqExt(Maid maid, ref MaidProp maidProp)
        {
            if (maidProp.type == 3)
            {
                if (maidProp.idx == 35)
                {
                    maid.GetProp(MPN.eye2).boDut = true;
                    maid.GetProp(MPN.skintoon).boDut = true;
                }
                else if (maidProp.idx == 42)
                {

                    maid.GetProp(MPN.skintoon).boDut = true;

                }

                else if (maidProp.idx == 49 || maidProp.idx == 87)
                {
                    maid.GetProp(MPN.folder_eye2).boDut = true;
                    maid.GetProp(MPN.eye2).boDut = true;
                }
                else if (maidProp.idx == 33)
                {
                    for (int i = 34; i <= 107; i++)
                    {
                        maid.GetProp((MPN)i).boDut = true;
                    }
                }

            }

        }
        public static bool GetParentMenuFileNamEext(out string result, SceneEdit.SMenuItem mi)
        {
            if (( mi.m_mpn == MPN.accnail ) || mi.m_mpn> MPN.skintoon || mi.m_mpn == MPN.acctoenail)
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

    }

    }



