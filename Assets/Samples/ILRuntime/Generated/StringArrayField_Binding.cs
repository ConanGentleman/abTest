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
    unsafe class StringArrayField_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::StringArrayField);

            field = type.GetField("values", flag);
            app.RegisterCLRFieldGetter(field, get_values_0);
            app.RegisterCLRFieldSetter(field, set_values_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_values_0, AssignFromStack_values_0);
            field = type.GetField("fieldName", flag);
            app.RegisterCLRFieldGetter(field, get_fieldName_1);
            app.RegisterCLRFieldSetter(field, set_fieldName_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_fieldName_1, AssignFromStack_fieldName_1);


        }



        static object get_values_0(ref object o)
        {
            return ((global::StringArrayField)o).values;
        }

        static StackObject* CopyToStack_values_0(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::StringArrayField)o).values;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_values_0(ref object o, object v)
        {
            ((global::StringArrayField)o).values = (System.Collections.Generic.List<System.String>)v;
        }

        static StackObject* AssignFromStack_values_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<System.String> @values = (System.Collections.Generic.List<System.String>)typeof(System.Collections.Generic.List<System.String>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::StringArrayField)o).values = @values;
            return ptr_of_this_method;
        }

        static object get_fieldName_1(ref object o)
        {
            return ((global::StringArrayField)o).fieldName;
        }

        static StackObject* CopyToStack_fieldName_1(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::StringArrayField)o).fieldName;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_fieldName_1(ref object o, object v)
        {
            ((global::StringArrayField)o).fieldName = (System.String)v;
        }

        static StackObject* AssignFromStack_fieldName_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.String @fieldName = (System.String)typeof(System.String).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::StringArrayField)o).fieldName = @fieldName;
            return ptr_of_this_method;
        }



    }
}
