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
    public class Patcher
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
            FieldDefinition newfield = new FieldDefinition($"{name}",
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
            TypeDefinition catmanager = hookDefinition.MainModule.GetType($"{HOOK_NAME}.hooks");
            // add model slots         

            TypeDefinition tbody = assembly.MainModule.GetType("TBody");
            TypeDefinition tslotid = tbody.NestedTypes.First(t => t.Name == "SlotID");

            //remove IsInitOnly attribute from m_strDefSlotName
            FieldDefinition defslot = tbody.GetField("m_strDefSlotName");
            defslot.IsInitOnly = false;


            // extend m_strDefSlotName

            MethodDefinition slotext = catmanager.GetMethod("slotext");
            MethodDefinition TbodyInit = tbody.GetMethod("Init");
            TbodyInit.InjectWith(slotext);


            //remove "end" field
            tslotid.Fields.RemoveAt(59);

            fieldgen(SlotID, 58, tslotid);
            //add "end" field back. not sure if there is a point in messing with end field, but i can't be bothered checking
            fieldgen2("end", 64, tslotid);


            // add MPN
          TypeDefinition maid = assembly.MainModule.GetType("Maid");
           TypeDefinition mpn = assembly.MainModule.GetType("MPN");
           MethodDefinition CreateInitMaidPropList = maid.GetMethod("CreateInitMaidPropList");
           MethodDefinition maidpropext = catmanager.GetMethod("maidpropext");
           CreateInitMaidPropList.InjectWith(maidpropext, flags: InjectFlags.ModifyReturn);
   
           fieldgen(MPNarry, 100, mpn);


            //fix mpn update
            MethodDefinition AllProcProp = maid.GetMethod("AllProcProp");
            MethodDefinition AllProcExt = catmanager.GetMethod("AllProcExt");
            MethodDefinition AllProcSeqExt = catmanager.GetMethod("AllProcSeqExt");
            MethodDefinition AllProcPropSeq = maid.GetMethod("AllProcPropSeq");

            //extend loop within Maid.AllProcProp()
            int counter = 0;
            for (int instn = 0; instn < AllProcProp.Body.Instructions.Count; instn++)
            {


                if (AllProcProp.Body.Instructions[instn].OpCode == OpCodes.Add)
                {
                    counter += 1;
                    if (counter == 4)
                    {

                        AllProcProp.InjectWith(AllProcExt, codeOffset: instn - 2, flags: InjectFlags.PassInvokingInstance | InjectFlags.PassLocals, localsID: new[] { 2 });
                        break;
                    }
                }


            }
            //extend loop within Maid.AllProcPropSeq()

            counter = 0;
            for (int instn = 0; instn < AllProcPropSeq.Body.Instructions.Count; instn++)
         {
     
             if (AllProcPropSeq.Body.Instructions[instn].OpCode == OpCodes.Br)
             {
                 counter += 1;
     
                 if (counter == 9)
                 {
     
                     AllProcPropSeq.InjectWith(AllProcSeqExt, codeOffset: instn, flags: InjectFlags.PassInvokingInstance | InjectFlags.PassLocals,
                         localsID: new[] { 0 });
                        counter = 0;
                     break;
     
                 }
     
             }
     
     
         }

            //add categories to the list
            TypeDefinition sceneEditInfo = assembly.MainModule.GetType("SceneEditInfo");

            MethodDefinition loadCustom = catmanager.GetMethod("loadcustomcats");

          

            //target definition
            TypeDefinition charactermgr = assembly.MainModule.GetType("CharacterMgr");
            MethodDefinition setpreset = charactermgr.GetMethod("PresetSet");
                   
            // inject method definition
            MethodDefinition ExtSet = catmanager.GetMethod("ExtSet");
            MethodDefinition IsEnableMenu = charactermgr.GetMethod("IsEnableMenu");
            IsEnableMenu.IsPrivate = false;
            IsEnableMenu.IsPublic = true;

            // expand preset reads

            for (int inst = 0; inst < setpreset.Body.Instructions.Count; inst++)
            {
                if (setpreset.Body.Instructions[inst].OpCode == OpCodes.Ceq)
                {
                    setpreset.InjectWith(ExtSet,codeOffset: inst-2 , flags: InjectFlags.PassParametersVal | InjectFlags.PassLocals, localsID: new[] { 0 });
                    break;
                }
            }



            // add del menus to dictionaryy. mostly OCD thing.
            // this kinda redundant as same can be doen with addition comands in del menus themselves, but whatever
            TypeDefinition CM3 = assembly.MainModule.GetType("CM3");
            MethodDefinition CM3_cctor = CM3.GetMethod(".cctor");

            MethodDefinition delmenuadder = catmanager.GetMethod("delmenuadder");
            CM3_cctor.InjectWith(delmenuadder, -1);

            // enable menu grouping for added categories
            MethodDefinition GetParentMenuFileName = assembly.MainModule.GetType("SceneEdit").GetMethod("GetParentMenuFileName");

            MethodDefinition GetParentMenuFileNameExt = catmanager.GetMethod("GetParentMenuFileNamEext");

            GetParentMenuFileName.InjectWith(GetParentMenuFileNameExt, flags: InjectFlags.ModifyReturn | InjectFlags.PassParametersVal);

        }
    }






}
