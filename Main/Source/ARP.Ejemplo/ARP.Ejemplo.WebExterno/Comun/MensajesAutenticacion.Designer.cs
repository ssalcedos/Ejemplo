﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.235
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace ARP.Ejemplo.WebExterno.Comun {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "4.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class MensajesAutenticacion {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal MensajesAutenticacion() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("ARP.Ejemplo.WebExterno.Comun.MensajesAutenticacion", typeof(MensajesAutenticacion).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Su usuario está bloqueado, por favor contacte al administrador..
        /// </summary>
        internal static string UsuarioBloqueado {
            get {
                return ResourceManager.GetString("UsuarioBloqueado", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Su usuario contraseña ha caducado. Debe cambiarla..
        /// </summary>
        internal static string UsuarioContrasenaCaduca {
            get {
                return ResourceManager.GetString("UsuarioContrasenaCaduca", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Su usuario está inactivo, por favor contacte al administrador..
        /// </summary>
        internal static string UsuarioInactivo {
            get {
                return ResourceManager.GetString("UsuarioInactivo", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Usuario ó contraseña no validos..
        /// </summary>
        internal static string UsuarioNoExiste {
            get {
                return ResourceManager.GetString("UsuarioNoExiste", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to Debe cambiar su contraseña..
        /// </summary>
        internal static string UsuarioObligarCambioContraseña {
            get {
                return ResourceManager.GetString("UsuarioObligarCambioContraseña", resourceCulture);
            }
        }
    }
}
