using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Inject;
using Mono.Cecil.Cil;
using System.IO;
using System.Reflection;

namespace COM3D2.CategoryCreator.Patcher
{
    public static class Patcher
    {
        public static readonly string[] TargetAssemblyNames = { "Assembly-CSharp.dll" };


        private const string HOOK_NAME = "COM3D2.CategoryCreator.Hook";

        // array of filleds that are to be appended to enum MPN
        public static readonly string[] MPNarry = new string[]
            {
           "folder_eye2",
            "eye2",
            "acctoenail",
            "skintoon",
            "acchandl",
            "acchandr",
            "ears",
            "horns"

            };
        // array of filleds that are to be appended to enum SlotID 
        public static readonly string[] SlotID = new string[]
            {
            "accnail",
            "acctoenail",
            "acchandl",
            "acchandr",
            "ears",
            "horns"

            };




        //method that generates fileds(public, static, literal, valutetype) from string []
        // with "name" starting at "start" in specified typedefinition
        private static void fieldgen(string[] fieldnames, int start, TypeDefinition typedef)
        {
            for (int i = 0; i < fieldnames.Length; i++)
            {

                FieldDefinition newfield = new FieldDefinition(fieldnames[i],
                            Mono.Cecil.FieldAttributes.Public | Mono.Cecil.FieldAttributes.Static |
                            Mono.Cecil.FieldAttributes.Literal | Mono.Cecil.FieldAttributes.HasDefault,
                            typedef)
                {
                    Constant = start + i
                };
                typedef.Fields.Add(newfield);



            }
        }
        //method that generates filed(public, static, literal, valutetype) with "name" and "integer" value in specified typedefinition
        private static void fieldgen2(string name, int integer, TypeDefinition typedef)
        {
            FieldDefinition newfield = new FieldDefinition(name,
                            Mono.Cecil.FieldAttributes.Public | Mono.Cecil.FieldAttributes.Static |
                            Mono.Cecil.FieldAttributes.Literal | Mono.Cecil.FieldAttributes.HasDefault,
                            typedef)
            {
                Constant = integer
            };
            typedef.Fields.Add(newfield);
        }

        public static void Patch(AssemblyDefinition assembly)
        {
            string hookloc = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            //load hook

            AssemblyDefinition hookDefinition = AssemblyLoader.LoadAssembly(Path.Combine(hookloc, $"{HOOK_NAME}.dll"));
            TypeDefinition Catmanager = hookDefinition.MainModule.GetType($"{HOOK_NAME}.Catmanager");
            // add model slots         

            TypeDefinition tbody = assembly.MainModule.GetType("TBody");
            TypeDefinition tslotid = tbody.NestedTypes.First(t => t.Name == "SlotID");

            //remove IsInitOnly attribute from m_strDefSlotName
            FieldDefinition defslot = tbody.GetField("m_strDefSlotName");
            defslot.IsInitOnly = false;


            // extend m_strDefSlotName

            MethodDefinition slotext = Catmanager.GetMethod("slotext");
            MethodDefinition TbodyInit = tbody.GetMethod("Init");
            TbodyInit.InjectWith(slotext);


            //remove "end" field
            int tslotid_count = tslotid.Fields.Count(); // get inital field count


            tslotid.Fields.RemoveAt(tslotid_count - 1);


            fieldgen(SlotID, tslotid_count - 2, tslotid);
            //add "end" field back. not sure if there is a point in messing with end field, but i can't be bothered checking
            fieldgen2("end", tslotid_count + 4, tslotid);


            // add MPN
            TypeDefinition maid = assembly.MainModule.GetType("Maid");
            TypeDefinition mpn = assembly.MainModule.GetType("MPN");
            MethodDefinition CreateInitMaidPropList = maid.GetMethod("CreateInitMaidPropList");
            MethodDefinition maidpropext = Catmanager.GetMethod("maidpropext");
            CreateInitMaidPropList.InjectWith(maidpropext, CreateInitMaidPropList.Body.Instructions.Count - 1, flags: InjectFlags.PassLocals, localsID: new int[] { 0 });

            int MPN_fields = mpn.Fields.Count();

            fieldgen(MPNarry, MPN_fields - 1, mpn);

            


            // get MPN enum 
            TypeDefinition charactermgr = assembly.MainModule.GetType("CharacterMgr");
            MethodDefinition charactermgr_init = charactermgr.GetMethod("Init");
            charactermgr_init.InjectWith(Catmanager.GetMethod("MPNgenerator"));

            //fix mpn update
            MethodDefinition AllProcProp = maid.GetMethod("AllProcProp");
            MethodDefinition AllProcExt = Catmanager.GetMethod("AllProcExt");
            MethodDefinition AllProcSeqExt = Catmanager.GetMethod("AllProcSeqExt");
            MethodDefinition AllProcPropSeq = maid.GetMethod("AllProcPropSeq");

            //extend loop within Maid.AllProcProp()
            // this injection is the most likely to cockup

            for (int instn = 0; instn < AllProcProp.Body.Instructions.Count; instn++)
            {


                if (AllProcProp.Body.Instructions[instn].OpCode == OpCodes.Ldftn)
                {
                    
                        AllProcProp.InjectWith(AllProcExt, codeOffset: instn - 15, flags: InjectFlags.PassInvokingInstance | InjectFlags.PassLocals, localsID: new[] { 2 });
                        break;
                    
                }


            }
            //extend loop within Maid.AllProcPropSeq()
            // this injection is the most likely to cockup

            for (int instn = 0; instn < AllProcPropSeq.Body.Instructions.Count; instn++)
            {

                if (AllProcPropSeq.Body.Instructions[instn].OpCode == OpCodes.Callvirt)
                {
                    MethodReference target = AllProcPropSeq.Body.Instructions[instn].Operand as MethodReference;


                    if (target.Name == "get_Values")
                    {

                        AllProcPropSeq.InjectWith(AllProcSeqExt, codeOffset: instn - 24, flags: InjectFlags.PassInvokingInstance | InjectFlags.PassLocals,
                            localsID: new[] { 0 });

                        break;

                    }

                }


            }



            MethodDefinition loadCustom = Catmanager.GetMethod("loadcustomcats");

            MethodDefinition setpreset = charactermgr.GetMethod("PresetSet");

            MethodDefinition ExtSet = Catmanager.GetMethod("ExtSet");
           


            // expand preset reads

            for (int inst = 0; inst < setpreset.Body.Instructions.Count; inst++)
            {
                if (setpreset.Body.Instructions[inst].OpCode == OpCodes.Call)
                {
                    MethodReference target = setpreset.Body.Instructions[inst].Operand as MethodReference;
                    if (target.Name == "Assert")
                    {
                        setpreset.InjectWith(ExtSet, codeOffset: inst + 1, flags: InjectFlags.PassParametersVal | InjectFlags.PassLocals, localsID: new[] { 0 });
                        break;
                    }
                }
            }



            // add del menus to dictionaryy. mostly OCD thing.
            // this kinda redundant as same can be done with addition comands in del menus themselves, but whatever
            TypeDefinition CM3 = assembly.MainModule.GetType("CM3");
            MethodDefinition CM3_cctor = CM3.GetMethod(".cctor");

            MethodDefinition delmenuadder = Catmanager.GetMethod("delmenuadder");
            CM3_cctor.InjectWith(delmenuadder, -1);

            // enable menu grouping for added categories
            TypeDefinition SceneEdit = assembly.MainModule.GetType("SceneEdit");
            MethodDefinition GetParentMenuFileName = SceneEdit.GetMethod("GetParentMenuFileName");

            MethodDefinition GetParentMenuFileNameExt = Catmanager.GetMethod("GetParentMenuFileNamEext");

            GetParentMenuFileName.InjectWith(GetParentMenuFileNameExt, flags: InjectFlags.ModifyReturn | InjectFlags.PassParametersVal);


            //  ignore localization shenanigans of v 1.17 and replace custom category name with one from edit_category_define.nei
            MethodDefinition UpdatePanel_PartsType = SceneEdit.GetMethod("UpdatePanel_PartsType");
            MethodDefinition locale_handler = Catmanager.GetMethod("locale_handler");

            for (int i = 0; i < UpdatePanel_PartsType.Body.Instructions.Count; i++)
            {
                if (UpdatePanel_PartsType.Body.Instructions[i].OpCode == OpCodes.Callvirt)
                {
                    MethodReference target = UpdatePanel_PartsType.Body.Instructions[i].Operand as MethodReference;
                    if (target.Name == "SetTerm")
                    {
                        int SpartsType = 0;
                        int UIlabel = 0;

                        for (int loc = 0; loc < UpdatePanel_PartsType.Body.Variables.Count; loc++)
                        {
                            if (UpdatePanel_PartsType.Body.Variables[loc].VariableType.FullName == "UnityEngine.Object")
                            {
                                SpartsType = loc - 1;

                            }
                            if (UpdatePanel_PartsType.Body.Variables[loc].VariableType.Name == "UILabel")
                            {
                                UIlabel = loc;
                                break;
                            }


                        }

                        UpdatePanel_PartsType.InjectWith(locale_handler, codeOffset: i + 1, flags: InjectFlags.PassLocals, localsID: new[] { SpartsType, UIlabel });

                        break;
                    }
                }

            }

            // add categories to edit mode from custom NEI file
            TypeDefinition SceneEditInfo = assembly.MainModule.GetType("SceneEditInfo");
            TypeDefinition EditHooks = hookDefinition.MainModule.GetType("COM3D2.CategoryCreator.Hook.EditHooks");
            SceneEditInfo.ChangeAccess("dicPartsTypePair_", true);
            SceneEditInfo.ChangeAccess("dicPartsTypeWearMode_", true);
            SceneEditInfo.ChangeAccess("m_dicPartsTypeCamera_", true);

            MethodDefinition get_m_dicPartsTypeCamera = SceneEditInfo.GetMethod("get_m_dicPartsTypeCamera");
            get_m_dicPartsTypeCamera.InjectWith(EditHooks.GetMethod("m_dicPartsTypeCamera_ext"), get_m_dicPartsTypeCamera.Body.Instructions.Count - 2);
            MethodDefinition get_m_dicPartsTypePair = SceneEditInfo.GetMethod("get_m_dicPartsTypePair");
            get_m_dicPartsTypePair.InjectWith(EditHooks.GetMethod("m_dicSliderPartsTypeBtnName_ext"), get_m_dicPartsTypePair.Body.Instructions.Count - 2);
            MethodDefinition get_m_dicPartsTypeWearMode = SceneEditInfo.GetMethod("get_m_dicPartsTypeWearMode");
            get_m_dicPartsTypeWearMode.InjectWith(EditHooks.GetMethod("m_dicPartsTypeWearMode_ext"),get_m_dicPartsTypeWearMode.Body.Instructions.Count-2);

           


        }
    }

}
