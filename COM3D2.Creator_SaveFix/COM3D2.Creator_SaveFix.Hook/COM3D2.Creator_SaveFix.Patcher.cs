using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Mono.Cecil;
using Mono.Cecil.Inject;
using Mono.Cecil.Cil;
using System.Reflection;
using System.IO;
namespace COM3D2.Creator_SaveFix.Patcher
{
    public class Class1
    {
        public static readonly string[] TargetAssemblyNames = { "Assembly-CSharp.dll" };
        private const string HOOK_NAME = "COM3D2.Creator_SaveFix.Hook";



        public static void Patch(AssemblyDefinition assembly)
        {

            string assemblyDir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string hookDir = $"{HOOK_NAME}.dll";
            AssemblyDefinition hookAssembly = AssemblyLoader.LoadAssembly(Path.Combine(assemblyDir, hookDir));
            TypeDefinition savefix = hookAssembly.MainModule.GetType($"{HOOK_NAME}.savefix");


            TypeDefinition Maid = assembly.MainModule.GetType("Maid");
            MethodDefinition GetPropstring = Maid.GetMethod("GetProp", typeof (string));
            Maid.ChangeAccess("m_dicMaidProp", true);//make static to allow hook access
            FieldDefinition m_dicMaidProp = Maid.GetField("m_dicMaidProp");
          


            MethodDefinition GetPropStringFix = savefix.GetMethod("GetPropStringFix");
            GetPropstring.InjectWith(GetPropStringFix, flags: InjectFlags.PassFields | InjectFlags.PassParametersRef, typeFields: new[] { m_dicMaidProp });

            // add entry for null_MPN so they game would shut it -_-
            TypeDefinition CM3 = assembly.MainModule.GetType("CM3");
            MethodDefinition CM3_cctor = CM3.GetMethod(".cctor");

            MethodDefinition delmenuadder = savefix.GetMethod("CM_dic_fix");
            CM3_cctor.InjectWith(delmenuadder, -1);


            // handle exception in MaidProp.Deserialize()
            TypeDefinition MaidProp = assembly.MainModule.GetType("MaidProp");
            // get field that need to overwritten
            FieldDefinition name = MaidProp.GetField("name");
            FieldDefinition idx = MaidProp.GetField("idx");

            MethodDefinition MaidPropDes = MaidProp.GetMethod("Deserialize");
            MethodDefinition MaidPropDesFix = savefix.GetMethod("MaidPropDesFix");

            for (int i = 0; i < MaidPropDes.Body.Instructions.Count; i++)
            {
                if (MaidPropDes.Body.Instructions[i].OpCode == OpCodes.Stfld) {
                    FieldReference target = MaidPropDes.Body.Instructions[i].Operand as FieldReference;
                    if (target.Name == name.Name )
                    {
                        MaidPropDes.InjectWith(MaidPropDesFix, i +2, flags: InjectFlags.PassFields, typeFields: new[] { name, idx });
                        break; 
                    }
                }

            }


        }
    }
}