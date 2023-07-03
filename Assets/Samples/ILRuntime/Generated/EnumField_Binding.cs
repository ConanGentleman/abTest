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
    unsafe class EnumField_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::EnumField);

            field = type.GetField("fieldName", flag);
            app.RegisterCLRFieldGetter(field, get_fieldName_0);
            app.RegisterCLRFieldSetter(field, set_fieldName_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_fieldName_0, AssignFromStack_fieldName_0);
            field = type.GetField("filedValue", flag);
            app.RegisterCLRFieldGetter(field, get_filedValue_1);
            app.RegisterCLRFieldSetter(field, set_filedValue_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_filedValue_1, AssignFromStack_filedValue_1);


        }



        static object get_fieldName_0(ref object o)
        {
            return ((global::EnumField)o).fieldName;
        }

        static StackObject* CopyToStack_fieldName_0(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::EnumField)o).fieldName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_fieldName_0(ref object o, object v)
        {
            ((global::EnumField)o).fieldName = (System.String)v;
        }

        static StackObject* AssignFromStack_fieldName_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @fieldName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::EnumField)o).fieldName = @fieldName;
            return ptr_of_this_method;
        }

        static object get_filedValue_1(ref object o)
        {
            return ((global::EnumField)o).filedValue;
        }

        static StackObject* CopyToStack_filedValue_1(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::EnumField)o).filedValue;
            __ret->ObjectType = ObjectTypes.Integer;
            __ret->Value = result_of_this_method;
            return __ret + 1;
        }

        static void set_filedValue_1(ref object o, object v)
        {
            ((global::EnumField)o).filedValue = (System.Int32)v;
        }

        static StackObject* AssignFromStack_filedValue_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Int32 @filedValue = ptr_of_this_method->Value;
            ((global::EnumField)o).filedValue = @filedValue;
            return ptr_of_this_method;
        }



    }
}
