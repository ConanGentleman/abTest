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
    unsafe class StringField_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::StringField);

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
            return ((global::StringField)o).fieldName;
        }

        static StackObject* CopyToStack_fieldName_0(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::StringField)o).fieldName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_fieldName_0(ref object o, object v)
        {
            ((global::StringField)o).fieldName = (System.String)v;
        }

        static StackObject* AssignFromStack_fieldName_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @fieldName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::StringField)o).fieldName = @fieldName;
            return ptr_of_this_method;
        }

        static object get_filedValue_1(ref object o)
        {
            return ((global::StringField)o).filedValue;
        }

        static StackObject* CopyToStack_filedValue_1(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::StringField)o).filedValue;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_filedValue_1(ref object o, object v)
        {
            ((global::StringField)o).filedValue = (System.String)v;
        }

        static StackObject* AssignFromStack_filedValue_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @filedValue = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::StringField)o).filedValue = @filedValue;
            return ptr_of_this_method;
        }



    }
}
