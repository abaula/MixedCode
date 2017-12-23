using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;

namespace NilloLib
{
    static class NilloBuilder
    {
        private static AssemblyBuilder _assemblyBuilder;
        private static ModuleBuilder _moduleBuilder;

        public static void BuildAndAddToStorage(Type type)
        {
            if (NilloStorage.Storage.ContainsKey(type))
                return;

            NilloStorage.Storage.TryAdd(type, CreateNullable(type));

            foreach (var outType in GetOutTypes(type))
                BuildAndAddToStorage(outType);
        }

        private static object CreateNullable(Type type)
        {
            var typeBuilder = type.GetTypeInfo().IsInterface
                ? DefineType(null, new[] { type })
                : DefineType(type, Array.Empty<Type>());

            ImplementMethods(typeBuilder, GetVirtualMethods(type));


            return Activator.CreateInstance(typeBuilder.CreateTypeInfo().AsType());
        }

        private static void ImplementMethods(TypeBuilder typeBuilder, IEnumerable<MethodInfo> methods)
        {
            foreach (var methodInfo in methods)
            {
                var method = typeBuilder.DefineMethod(methodInfo.Name,
                    MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.NewSlot | MethodAttributes.Virtual,
                    methodInfo.CallingConvention,
                    methodInfo.ReturnType,
                    methodInfo.GetParameters()
                        .Select(p => p.ParameterType)
                        .ToArray());

                var gen = method.GetILGenerator();

                if (methodInfo.ReturnType != null)
                {
                    var getTypeFromHandle = typeof(Type).GetMethod(nameof(Type.GetTypeFromHandle));
                    var storageMethod = typeof(NilloStorage).GetMethod(nameof(NilloStorage.GetObjectForType));
                    gen.Emit(OpCodes.Ldtoken, methodInfo.ReturnType);
                    gen.Emit(OpCodes.Call, getTypeFromHandle);
                    gen.EmitCall(OpCodes.Call, storageMethod, storageMethod.GetParameters()
                        .Select(p => p.ParameterType)
                        .ToArray());
                }

                gen.Emit(OpCodes.Ret);

                typeBuilder.DefineMethodOverride(method, methodInfo);
            }
        }

        private static TypeBuilder DefineType(Type baseClass, IEnumerable<Type> interfaces)
        {
            return GetModuleBuilder()
                .DefineType($"NilloDynamicType_{Guid.NewGuid()}",
                    TypeAttributes.Class | TypeAttributes.Public,
                    baseClass,
                    new[] { typeof(INillo) }.Union(interfaces).ToArray());
        }

        private static AssemblyBuilder GetAssemblyBuilder()
        {
            if (_assemblyBuilder == null)
            {
                _assemblyBuilder = AssemblyBuilder.DefineDynamicAssembly(
                    new AssemblyName($"NilloDynamicAssembly_{Guid.NewGuid()}"),
                    AssemblyBuilderAccess.Run);
            }

            return _assemblyBuilder;
        }

        private static ModuleBuilder GetModuleBuilder()
        {
            if (_moduleBuilder == null)
            {
                _moduleBuilder = GetAssemblyBuilder()
                    .DefineDynamicModule($"NilloDynamicModule_{Guid.NewGuid()}");
            }

            return _moduleBuilder;
        }

        private static IEnumerable<Type> GetOutTypes(Type type)
        {
            var methodsTypes = GetVirtualMethods(type)
                .Where(mi => mi.ReturnType != null)
                .Select(mi => mi.ReturnType);

            var propsTypes = GetProperties(type)
                .Where(pt => pt.CanRead)
                .Select(pi => pi.PropertyType);

            var fieldTypes = GetFields(type)
                .Select(pi => pi.FieldType);

            Func<Type, bool> NotPrimitive() => t =>
                    !t.GetTypeInfo().IsValueType
                    && !t.GetTypeInfo().IsPrimitive
                    && t != typeof(string);

            return methodsTypes
                .Union(propsTypes)
                .Union(fieldTypes)
                .Where(NotPrimitive())
                .Distinct();
        }

        private static IEnumerable<MethodInfo> GetVirtualMethods(Type type)
        {
            return type.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly)
                .Where(mi => mi.IsVirtual);
        }

        private static IEnumerable<PropertyInfo> GetProperties(Type type)
        {
            return type.GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
        }

        private static IEnumerable<FieldInfo> GetFields(Type type)
        {
            return type.GetFields(BindingFlags.Public | BindingFlags.Instance | BindingFlags.Static | BindingFlags.DeclaredOnly);
        }
    }
}
