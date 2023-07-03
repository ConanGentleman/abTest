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
    unsafe class ClassData_Binding
    {
        public static void Register(ILRuntime.Runtime.Enviorment.AppDomain app)
        {
            BindingFlags flag = BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly;
            FieldInfo field;
            Type[] args;
            Type type = typeof(global::ClassData);

            field = type.GetField("stringFields", flag);
            app.RegisterCLRFieldGetter(field, get_stringFields_0);
            app.RegisterCLRFieldSetter(field, set_stringFields_0);
            app.RegisterCLRFieldBinding(field, CopyToStack_stringFields_0, AssignFromStack_stringFields_0);
            field = type.GetField("stringArrayFields", flag);
            app.RegisterCLRFieldGetter(field, get_stringArrayFields_1);
            app.RegisterCLRFieldSetter(field, set_stringArrayFields_1);
            app.RegisterCLRFieldBinding(field, CopyToStack_stringArrayFields_1, AssignFromStack_stringArrayFields_1);
            field = type.GetField("int32Field", flag);
            app.RegisterCLRFieldGetter(field, get_int32Field_2);
            app.RegisterCLRFieldSetter(field, set_int32Field_2);
            app.RegisterCLRFieldBinding(field, CopyToStack_int32Field_2, AssignFromStack_int32Field_2);
            field = type.GetField("Int32ArrayField", flag);
            app.RegisterCLRFieldGetter(field, get_Int32ArrayField_3);
            app.RegisterCLRFieldSetter(field, set_Int32ArrayField_3);
            app.RegisterCLRFieldBinding(field, CopyToStack_Int32ArrayField_3, AssignFromStack_Int32ArrayField_3);
            field = type.GetField("floatFields", flag);
            app.RegisterCLRFieldGetter(field, get_floatFields_4);
            app.RegisterCLRFieldSetter(field, set_floatFields_4);
            app.RegisterCLRFieldBinding(field, CopyToStack_floatFields_4, AssignFromStack_floatFields_4);
            field = type.GetField("enumFields", flag);
            app.RegisterCLRFieldGetter(field, get_enumFields_5);
            app.RegisterCLRFieldSetter(field, set_enumFields_5);
            app.RegisterCLRFieldBinding(field, CopyToStack_enumFields_5, AssignFromStack_enumFields_5);
            field = type.GetField("gameObjectFields", flag);
            app.RegisterCLRFieldGetter(field, get_gameObjectFields_6);
            app.RegisterCLRFieldSetter(field, set_gameObjectFields_6);
            app.RegisterCLRFieldBinding(field, CopyToStack_gameObjectFields_6, AssignFromStack_gameObjectFields_6);
            field = type.GetField("vector3Fields", flag);
            app.RegisterCLRFieldGetter(field, get_vector3Fields_7);
            app.RegisterCLRFieldSetter(field, set_vector3Fields_7);
            app.RegisterCLRFieldBinding(field, CopyToStack_vector3Fields_7, AssignFromStack_vector3Fields_7);
            field = type.GetField("classFields", flag);
            app.RegisterCLRFieldGetter(field, get_classFields_8);
            app.RegisterCLRFieldSetter(field, set_classFields_8);
            app.RegisterCLRFieldBinding(field, CopyToStack_classFields_8, AssignFromStack_classFields_8);
            field = type.GetField("classArrayFields", flag);
            app.RegisterCLRFieldGetter(field, get_classArrayFields_9);
            app.RegisterCLRFieldSetter(field, set_classArrayFields_9);
            app.RegisterCLRFieldBinding(field, CopyToStack_classArrayFields_9, AssignFromStack_classArrayFields_9);


        }



        static object get_stringFields_0(ref object o)
        {
            return ((global::ClassData)o).stringFields;
        }

        static StackObject* CopyToStack_stringFields_0(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).stringFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_stringFields_0(ref object o, object v)
        {
            ((global::ClassData)o).stringFields = (System.Collections.Generic.List<global::StringField>)v;
        }

        static StackObject* AssignFromStack_stringFields_0(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::StringField> @stringFields = (System.Collections.Generic.List<global::StringField>)typeof(System.Collections.Generic.List<global::StringField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).stringFields = @stringFields;
            return ptr_of_this_method;
        }

        static object get_stringArrayFields_1(ref object o)
        {
            return ((global::ClassData)o).stringArrayFields;
        }

        static StackObject* CopyToStack_stringArrayFields_1(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).stringArrayFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_stringArrayFields_1(ref object o, object v)
        {
            ((global::ClassData)o).stringArrayFields = (System.Collections.Generic.List<global::StringArrayField>)v;
        }

        static StackObject* AssignFromStack_stringArrayFields_1(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::StringArrayField> @stringArrayFields = (System.Collections.Generic.List<global::StringArrayField>)typeof(System.Collections.Generic.List<global::StringArrayField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).stringArrayFields = @stringArrayFields;
            return ptr_of_this_method;
        }

        static object get_int32Field_2(ref object o)
        {
            return ((global::ClassData)o).int32Field;
        }

        static StackObject* CopyToStack_int32Field_2(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).int32Field;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_int32Field_2(ref object o, object v)
        {
            ((global::ClassData)o).int32Field = (System.Collections.Generic.List<global::Int32Field>)v;
        }

        static StackObject* AssignFromStack_int32Field_2(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::Int32Field> @int32Field = (System.Collections.Generic.List<global::Int32Field>)typeof(System.Collections.Generic.List<global::Int32Field>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).int32Field = @int32Field;
            return ptr_of_this_method;
        }

        static object get_Int32ArrayField_3(ref object o)
        {
            return ((global::ClassData)o).Int32ArrayField;
        }

        static StackObject* CopyToStack_Int32ArrayField_3(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).Int32ArrayField;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_Int32ArrayField_3(ref object o, object v)
        {
            ((global::ClassData)o).Int32ArrayField = (System.Collections.Generic.List<global::Int32ArrayField>)v;
        }

        static StackObject* AssignFromStack_Int32ArrayField_3(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::Int32ArrayField> @Int32ArrayField = (System.Collections.Generic.List<global::Int32ArrayField>)typeof(System.Collections.Generic.List<global::Int32ArrayField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).Int32ArrayField = @Int32ArrayField;
            return ptr_of_this_method;
        }

        static object get_floatFields_4(ref object o)
        {
            return ((global::ClassData)o).floatFields;
        }

        static StackObject* CopyToStack_floatFields_4(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).floatFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_floatFields_4(ref object o, object v)
        {
            ((global::ClassData)o).floatFields = (System.Collections.Generic.List<global::FloatField>)v;
        }

        static StackObject* AssignFromStack_floatFields_4(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::FloatField> @floatFields = (System.Collections.Generic.List<global::FloatField>)typeof(System.Collections.Generic.List<global::FloatField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).floatFields = @floatFields;
            return ptr_of_this_method;
        }

        static object get_enumFields_5(ref object o)
        {
            return ((global::ClassData)o).enumFields;
        }

        static StackObject* CopyToStack_enumFields_5(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).enumFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_enumFields_5(ref object o, object v)
        {
            ((global::ClassData)o).enumFields = (System.Collections.Generic.List<global::EnumField>)v;
        }

        static StackObject* AssignFromStack_enumFields_5(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::EnumField> @enumFields = (System.Collections.Generic.List<global::EnumField>)typeof(System.Collections.Generic.List<global::EnumField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).enumFields = @enumFields;
            return ptr_of_this_method;
        }

        static object get_gameObjectFields_6(ref object o)
        {
            return ((global::ClassData)o).gameObjectFields;
        }

        static StackObject* CopyToStack_gameObjectFields_6(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).gameObjectFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_gameObjectFields_6(ref object o, object v)
        {
            ((global::ClassData)o).gameObjectFields = (System.Collections.Generic.List<global::GameObjectField>)v;
        }

        static StackObject* AssignFromStack_gameObjectFields_6(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::GameObjectField> @gameObjectFields = (System.Collections.Generic.List<global::GameObjectField>)typeof(System.Collections.Generic.List<global::GameObjectField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).gameObjectFields = @gameObjectFields;
            return ptr_of_this_method;
        }

        static object get_vector3Fields_7(ref object o)
        {
            return ((global::ClassData)o).vector3Fields;
        }

        static StackObject* CopyToStack_vector3Fields_7(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).vector3Fields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_vector3Fields_7(ref object o, object v)
        {
            ((global::ClassData)o).vector3Fields = (System.Collections.Generic.List<global::Vector3Field>)v;
        }

        static StackObject* AssignFromStack_vector3Fields_7(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::Vector3Field> @vector3Fields = (System.Collections.Generic.List<global::Vector3Field>)typeof(System.Collections.Generic.List<global::Vector3Field>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).vector3Fields = @vector3Fields;
            return ptr_of_this_method;
        }

        static object get_classFields_8(ref object o)
        {
            return ((global::ClassData)o).classFields;
        }

        static StackObject* CopyToStack_classFields_8(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).classFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_classFields_8(ref object o, object v)
        {
            ((global::ClassData)o).classFields = (System.Collections.Generic.List<global::ClassField>)v;
        }

        static StackObject* AssignFromStack_classFields_8(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::ClassField> @classFields = (System.Collections.Generic.List<global::ClassField>)typeof(System.Collections.Generic.List<global::ClassField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).classFields = @classFields;
            return ptr_of_this_method;
        }

        static object get_classArrayFields_9(ref object o)
        {
            return ((global::ClassData)o).classArrayFields;
        }

        static StackObject* CopyToStack_classArrayFields_9(ref object o, ILIntepreter __intp, StackObject* __ret, AutoList __mStack)
        {
            var result_of_this_method = ((global::ClassData)o).classArrayFields;
            return ILIntepreter.PushObject(__ret, __mStack, result_of_this_method);
        }

        static void set_classArrayFields_9(ref object o, object v)
        {
            ((global::ClassData)o).classArrayFields = (System.Collections.Generic.List<global::ClassArrayField>)v;
        }

        static StackObject* AssignFromStack_classArrayFields_9(ref object o, ILIntepreter __intp, StackObject* ptr_of_this_method, AutoList __mStack)
        {
            ILRuntime.Runtime.Enviorment.AppDomain __domain = __intp.AppDomain;
            System.Collections.Generic.List<global::ClassArrayField> @classArrayFields = (System.Collections.Generic.List<global::ClassArrayField>)typeof(System.Collections.Generic.List<global::ClassArrayField>).CheckCLRTypes(StackObject.ToObject(ptr_of_this_method, __domain, __mStack), (CLR.Utils.Extensions.TypeFlags)0);
            ((global::ClassData)o).classArrayFields = @classArrayFields;
            return ptr_of_this_method;
        }



    }
}
