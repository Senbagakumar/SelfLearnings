using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Reflection.Emit;
using System.Security.Permissions;
using System.Text;
using System.Threading;

namespace Hyper.ComponentModel
{
    internal sealed class HyperTypeDescriptor : CustomTypeDescriptor
    {
        private static readonly Dictionary<PropertyInfo, PropertyDescriptor> properties = new Dictionary<PropertyInfo, PropertyDescriptor>();
        private static readonly ModuleBuilder moduleBuilder = AppDomain.CurrentDomain.DefineDynamicAssembly(new AssemblyName("Hyper.ComponentModel.dynamic"), AssemblyBuilderAccess.Run).DefineDynamicModule("Hyper.ComponentModel.dynamic.dll");
        private readonly PropertyDescriptorCollection propertyCollections;
        private static int counter;

        internal HyperTypeDescriptor(ICustomTypeDescriptor parent)
          : base(parent)
        {
            this.propertyCollections = HyperTypeDescriptor.WrapProperties(parent.GetProperties());
        }

        public override sealed PropertyDescriptorCollection GetProperties(Attribute[] attributes)
        {
            return this.propertyCollections;
        }

        public override sealed PropertyDescriptorCollection GetProperties()
        {
            return this.propertyCollections;
        }

        private static PropertyDescriptorCollection WrapProperties(PropertyDescriptorCollection oldProps)
        {
            PropertyDescriptor[] properties = new PropertyDescriptor[oldProps.Count];
            int num = 0;
            bool flag = false;
            Type type = Assembly.GetAssembly(typeof(PropertyDescriptor)).GetType("System.ComponentModel.ReflectPropertyDescriptor");
            foreach (PropertyDescriptor oldProp in oldProps)
            {
                PropertyDescriptor oldP = oldProp;
                if (object.ReferenceEquals((object)type, (object)oldProp.GetType()) && TryCreatePropertyDescriptor(ref oldP))
                    flag = true;
                properties[num++] = oldP;
            }
            if (!flag)
                return oldProps;
            return new PropertyDescriptorCollection(properties, true);
        }

        private static bool TryCreatePropertyDescriptor(ref PropertyDescriptor descriptor)
        {
            try
            {
                PropertyInfo property = descriptor.ComponentType.GetProperty(descriptor.Name);
                if (property == null)
                    return false;
                lock (HyperTypeDescriptor.properties)
                {
                    PropertyDescriptor propertyDescriptor1;
                    if (HyperTypeDescriptor.properties.TryGetValue(property, out propertyDescriptor1))
                    {
                        descriptor = propertyDescriptor1;
                        return true;
                    }
                    string name = "_c" + Interlocked.Increment(ref HyperTypeDescriptor.counter).ToString();
                    TypeBuilder typeBuilder = HyperTypeDescriptor.moduleBuilder.DefineType(name, TypeAttributes.Public | TypeAttributes.Sealed | TypeAttributes.AutoClass | TypeAttributes.BeforeFieldInit, typeof(ChainingPropertyDescriptor));
                    ILGenerator ilGenerator1 = typeBuilder.DefineConstructor(MethodAttributes.Public | MethodAttributes.HideBySig | MethodAttributes.SpecialName | MethodAttributes.RTSpecialName, CallingConventions.Standard, new Type[1]
                    {
            typeof (PropertyDescriptor)
                    }).GetILGenerator();
                    ilGenerator1.Emit(OpCodes.Ldarg_0);
                    ilGenerator1.Emit(OpCodes.Ldarg_1);
                    ilGenerator1.Emit(OpCodes.Call, typeof(ChainingPropertyDescriptor).GetConstructor(BindingFlags.Instance | BindingFlags.NonPublic, (Binder)null, new Type[1]
                    {
            typeof (PropertyDescriptor)
                    }, (ParameterModifier[])null));
                    ilGenerator1.Emit(OpCodes.Ret);
                    if (property.CanRead)
                    {
                        MethodInfo method = typeof(ChainingPropertyDescriptor).GetMethod("GetValue");
                        MethodBuilder methodBuilder = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig, method.CallingConvention, method.ReturnType, new Type[1]
                        {
              typeof (object)
                        });
                        ILGenerator ilGenerator2 = methodBuilder.GetILGenerator();
                        if (property.DeclaringType.IsValueType)
                        {
                            LocalBuilder local = ilGenerator2.DeclareLocal(property.DeclaringType);
                            ilGenerator2.Emit(OpCodes.Ldarg_1);
                            ilGenerator2.Emit(OpCodes.Unbox_Any, property.DeclaringType);
                            ilGenerator2.Emit(OpCodes.Stloc_0);
                            ilGenerator2.Emit(OpCodes.Ldloca_S, local);
                        }
                        else
                        {
                            ilGenerator2.Emit(OpCodes.Ldarg_1);
                            ilGenerator2.Emit(OpCodes.Castclass, property.DeclaringType);
                        }
                        ilGenerator2.Emit(OpCodes.Callvirt, property.GetGetMethod());
                        if (property.PropertyType.IsValueType)
                            ilGenerator2.Emit(OpCodes.Box, property.PropertyType);
                        ilGenerator2.Emit(OpCodes.Ret);
                        typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder, method);
                    }
                    bool supportsChangeEvents = descriptor.SupportsChangeEvents;
                    bool isReadOnly = descriptor.IsReadOnly;
                    MethodInfo getMethod1 = typeof(ChainingPropertyDescriptor).GetProperty("SupportsChangeEvents").GetGetMethod();
                    MethodBuilder methodBuilder1 = typeBuilder.DefineMethod(getMethod1.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, getMethod1.CallingConvention, getMethod1.ReturnType, Type.EmptyTypes);
                    ILGenerator ilGenerator3 = methodBuilder1.GetILGenerator();
                    if (supportsChangeEvents)
                        ilGenerator3.Emit(OpCodes.Ldc_I4_1);
                    else
                        ilGenerator3.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator3.Emit(OpCodes.Ret);
                    typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder1, getMethod1);
                    MethodInfo getMethod2 = typeof(ChainingPropertyDescriptor).GetProperty("IsReadOnly").GetGetMethod();
                    MethodBuilder methodBuilder2 = typeBuilder.DefineMethod(getMethod2.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, getMethod2.CallingConvention, getMethod2.ReturnType, Type.EmptyTypes);
                    ILGenerator ilGenerator4 = methodBuilder2.GetILGenerator();
                    if (isReadOnly)
                        ilGenerator4.Emit(OpCodes.Ldc_I4_1);
                    else
                        ilGenerator4.Emit(OpCodes.Ldc_I4_0);
                    ilGenerator4.Emit(OpCodes.Ret);
                    typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder2, getMethod2);
                    if (!property.DeclaringType.IsValueType)
                    {
                        if (!isReadOnly && property.CanWrite)
                        {
                            MethodInfo method = typeof(ChainingPropertyDescriptor).GetMethod("SetValue");
                            MethodBuilder methodBuilder3 = typeBuilder.DefineMethod(method.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig, method.CallingConvention, method.ReturnType, new Type[2]
                            {
                typeof (object),
                typeof (object)
                            });
                            ILGenerator ilGenerator2 = methodBuilder3.GetILGenerator();
                            ilGenerator2.Emit(OpCodes.Ldarg_1);
                            ilGenerator2.Emit(OpCodes.Castclass, property.DeclaringType);
                            ilGenerator2.Emit(OpCodes.Ldarg_2);
                            if (property.PropertyType.IsValueType)
                                ilGenerator2.Emit(OpCodes.Unbox_Any, property.PropertyType);
                            else
                                ilGenerator2.Emit(OpCodes.Castclass, property.PropertyType);
                            ilGenerator2.Emit(OpCodes.Callvirt, property.GetSetMethod());
                            ilGenerator2.Emit(OpCodes.Ret);
                            typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder3, method);
                        }
                        if (supportsChangeEvents)
                        {
                            EventInfo eventInfo = property.DeclaringType.GetEvent(property.Name + "Changed");
                            if (eventInfo != null)
                            {
                                MethodInfo method1 = typeof(ChainingPropertyDescriptor).GetMethod("AddValueChanged");
                                MethodBuilder methodBuilder3 = typeBuilder.DefineMethod(method1.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, method1.CallingConvention, method1.ReturnType, new Type[2]
                                {
                  typeof (object),
                  typeof (EventHandler)
                                });
                                ILGenerator ilGenerator2 = methodBuilder3.GetILGenerator();
                                ilGenerator2.Emit(OpCodes.Ldarg_1);
                                ilGenerator2.Emit(OpCodes.Castclass, property.DeclaringType);
                                ilGenerator2.Emit(OpCodes.Ldarg_2);
                                ilGenerator2.Emit(OpCodes.Callvirt, eventInfo.GetAddMethod());
                                ilGenerator2.Emit(OpCodes.Ret);
                                typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder3, method1);
                                MethodInfo method2 = typeof(ChainingPropertyDescriptor).GetMethod("RemoveValueChanged");
                                MethodBuilder methodBuilder4 = typeBuilder.DefineMethod(method2.Name, MethodAttributes.Public | MethodAttributes.Final | MethodAttributes.Virtual | MethodAttributes.HideBySig | MethodAttributes.SpecialName, method2.CallingConvention, method2.ReturnType, new Type[2]
                                {
                  typeof (object),
                  typeof (EventHandler)
                                });
                                ILGenerator ilGenerator5 = methodBuilder4.GetILGenerator();
                                ilGenerator5.Emit(OpCodes.Ldarg_1);
                                ilGenerator5.Emit(OpCodes.Castclass, property.DeclaringType);
                                ilGenerator5.Emit(OpCodes.Ldarg_2);
                                ilGenerator5.Emit(OpCodes.Callvirt, eventInfo.GetRemoveMethod());
                                ilGenerator5.Emit(OpCodes.Ret);
                                typeBuilder.DefineMethodOverride((MethodInfo)methodBuilder4, method2);
                            }
                        }
                    }
                    PropertyDescriptor propertyDescriptor2 = typeBuilder.CreateType().GetConstructor(new Type[1]
                    {
            typeof (PropertyDescriptor)
                    }).Invoke(new object[1] { (object)descriptor }) as PropertyDescriptor;
                    if (propertyDescriptor2 == null)
                        return false;
                    descriptor = propertyDescriptor2;
                    HyperTypeDescriptor.properties.Add(property, descriptor);
                    return true;
                }
            }
            catch
            {
                return false;
            }
        }
    }
    internal sealed class HyperTypeDescriptionProvider : TypeDescriptionProvider
    {
        private static readonly Dictionary<Type, ICustomTypeDescriptor> descriptors = new Dictionary<Type, ICustomTypeDescriptor>();

        public static void Add(Type type)
        {
            TypeDescriptor.AddProvider((TypeDescriptionProvider)new HyperTypeDescriptionProvider(TypeDescriptor.GetProvider(type)), type);
        }

        public HyperTypeDescriptionProvider()
          : this(typeof(object))
        {
        }

        public HyperTypeDescriptionProvider(Type type)
          : this(TypeDescriptor.GetProvider(type))
        {
        }

        public HyperTypeDescriptionProvider(TypeDescriptionProvider parent)
          : base(parent)
        {
        }

        public static void Clear(Type type)
        {
            lock (HyperTypeDescriptionProvider.descriptors)
                HyperTypeDescriptionProvider.descriptors.Remove(type);
        }

        public static void Clear()
        {
            lock (HyperTypeDescriptionProvider.descriptors)
                HyperTypeDescriptionProvider.descriptors.Clear();
        }

        public override sealed ICustomTypeDescriptor GetTypeDescriptor(Type objectType, object instance)
        {
            lock (HyperTypeDescriptionProvider.descriptors)
            {
                ICustomTypeDescriptor customTypeDescriptor;
                if (!HyperTypeDescriptionProvider.descriptors.TryGetValue(objectType, out customTypeDescriptor))
                {
                    try
                    {
                        customTypeDescriptor = this.BuildDescriptor(objectType);
                    }
                    catch
                    {
                        return base.GetTypeDescriptor(objectType, instance);
                    }
                }
                return customTypeDescriptor;
            }
        }

        [ReflectionPermission(SecurityAction.Assert, Flags = ReflectionPermissionFlag.AllFlags)]
        private ICustomTypeDescriptor BuildDescriptor(Type objectType)
        {
            ICustomTypeDescriptor typeDescriptor = base.GetTypeDescriptor(objectType, (object)null);
            HyperTypeDescriptionProvider.descriptors.Add(objectType, typeDescriptor);
            try
            {
                ICustomTypeDescriptor customTypeDescriptor = (ICustomTypeDescriptor)new HyperTypeDescriptor(typeDescriptor);
                HyperTypeDescriptionProvider.descriptors[objectType] = customTypeDescriptor;
                return customTypeDescriptor;
            }
            catch
            {
                HyperTypeDescriptionProvider.descriptors.Remove(objectType);
                throw;
            }
        }
    }
    internal abstract class ChainingPropertyDescriptor : PropertyDescriptor
    {
        private readonly PropertyDescriptor _root;

        protected PropertyDescriptor Root
        {
            get
            {
                return this._root;
            }
        }

        protected ChainingPropertyDescriptor(PropertyDescriptor root)
          : base((MemberDescriptor)root)
        {
            this._root = root;
        }

        public override void AddValueChanged(object component, EventHandler handler)
        {
            this.Root.AddValueChanged(component, handler);
        }

        public override AttributeCollection Attributes
        {
            get
            {
                return this.Root.Attributes;
            }
        }

        public override bool CanResetValue(object component)
        {
            return this.Root.CanResetValue(component);
        }

        public override string Category
        {
            get
            {
                return this.Root.Category;
            }
        }

        public override Type ComponentType
        {
            get
            {
                return this.Root.ComponentType;
            }
        }

        public override TypeConverter Converter
        {
            get
            {
                return this.Root.Converter;
            }
        }

        public override string Description
        {
            get
            {
                return this.Root.Description;
            }
        }

        public override bool DesignTimeOnly
        {
            get
            {
                return this.Root.DesignTimeOnly;
            }
        }

        public override string DisplayName
        {
            get
            {
                return this.Root.DisplayName;
            }
        }

        public override bool Equals(object obj)
        {
            return this.Root.Equals(obj);
        }

        public override PropertyDescriptorCollection GetChildProperties(object instance, Attribute[] filter)
        {
            return this.Root.GetChildProperties(instance, filter);
        }

        public override object GetEditor(Type editorBaseType)
        {
            return this.Root.GetEditor(editorBaseType);
        }

        public override int GetHashCode()
        {
            return this.Root.GetHashCode();
        }

        public override object GetValue(object component)
        {
            return this.Root.GetValue(component);
        }

        public override bool IsBrowsable
        {
            get
            {
                return this.Root.IsBrowsable;
            }
        }

        public override bool IsLocalizable
        {
            get
            {
                return this.Root.IsLocalizable;
            }
        }

        public override bool IsReadOnly
        {
            get
            {
                return this.Root.IsReadOnly;
            }
        }

        public override string Name
        {
            get
            {
                return this.Root.Name;
            }
        }

        public override Type PropertyType
        {
            get
            {
                return this.Root.PropertyType;
            }
        }

        public override void RemoveValueChanged(object component, EventHandler handler)
        {
            this.Root.RemoveValueChanged(component, handler);
        }

        public override void ResetValue(object component)
        {
            this.Root.ResetValue(component);
        }

        public override void SetValue(object component, object value)
        {
            this.Root.SetValue(component, value);
        }

        public override bool ShouldSerializeValue(object component)
        {
            return this.Root.ShouldSerializeValue(component);
        }

        public override bool SupportsChangeEvents
        {
            get
            {
                return this.Root.SupportsChangeEvents;
            }
        }

        public override string ToString()
        {
            return this.Root.ToString();
        }
    }
}

