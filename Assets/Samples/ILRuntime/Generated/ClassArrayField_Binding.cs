using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;

using ILRuntime.CLR.TypeSystem;
using ILRuntime.CLR.Method;
using ILRuntime.Runtime.Enviorment;
using ILRuntime.Runtime.Intepreter;
using ILRuntime.Runtime.Stack;
using ILRuntime.Reflection;
using ILRuntime.CLR.Utils;
#if DEBUG && !DISABLE_ILRUNTIME_DEBUG
using AutoList = System.Collections.Generic.List<object>;
#else
using AutoList = ILRuntime.Other.UncheckedList<object>;
#endif
namespace ILRuntime.Runtime.Generated
{
    unsafe class ClassArrayField_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::ClassArrayField);

            field = type.GetField("classType", flag);
            app.RegisterCLRFieldGetter(field, get_classType_0);
            app.RegisterCLRFieldSetter(field, set_classType_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_classType_0, AssignFromStack_classType_0);
            field = type.GetField("classDatas", flag);
            app.RegisterCLRFieldGetter(field, get_classDatas_1);
            app.RegisterCLRFieldSetter(field, set_classDatas_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_classDatas_1, AssignFromStack_classDatas_1);
            field = type.GetField("fieldName", flag);
            app.RegisterCLRFieldGetter(field, get_fieldName_2);
            app.RegisterCLRFieldSetter(field, set_fieldName_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_fieldName_2, AssignFromStack_fieldName_2);


        }



        static object get_classType_0(ref object o)
        {
            return ((global::ClassArrayField)o).classType;
        }

        static StackObject* CopyToStack_classType_0(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassArrayField)o).classType;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_classType_0(ref object o, object v)
        {
            ((global::ClassArrayField)o).classType = (System.String)v;
        }

        static StackObject* AssignFromStack_classType_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @classType = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassArrayField)o).classType = @classType;
            return ptr_of_this_method;
        }

        static object get_classDatas_1(ref object o)
        {
            return ((global::ClassArrayField)o).classDatas;
        }

        static StackObject* CopyToStack_classDatas_1(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassArrayField)o).classDatas;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_classDatas_1(ref object o, object v)
        {
            ((global::ClassArrayField)o).classDatas = (System.Collections.Generic.List<global::ClassData>)v;
        }

        static StackObject* AssignFromStack_classDatas_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::ClassData> @classDatas = (System.Collections.Generic.List<global::ClassData>)typeof(System.Collections.Generic.List<global::ClassData>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassArrayField)o).classDatas = @classDatas;
            return ptr_of_this_method;
        }

        static object get_fieldName_2(ref object o)
        {
            return ((global::ClassArrayField)o).fieldName;
        }

        static StackObject* CopyToStack_fieldName_2(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassArrayField)o).fieldName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_fieldName_2(ref object o, object v)
        {
            ((global::ClassArrayField)o).fieldName = (System.String)v;
        }

        static StackObject* AssignFromStack_fieldName_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @fieldName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassArrayField)o).fieldName = @fieldName;
            return ptr_of_this_method;
        }



    }
}
