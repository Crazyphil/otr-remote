﻿//------------------------------------------------------------------------------
// <auto-generated>
//     Dieser Code wurde von einem Tool generiert.
//     Laufzeitversion:2.0.50727.312
//
//     Änderungen an dieser Datei können falsches Verhalten verursachen und gehen verloren, wenn
//     der Code erneut generiert wird.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Crazysoft.OTR_Remote.Lang {
    using System;
    
    
    /// <summary>
    ///   Eine stark typisierte Ressourcenklasse zum Suchen von lokalisierten Zeichenfolgen usw.
    /// </summary>
    // Diese Klasse wurde von der StronglyTypedResourceBuilder automatisch generiert
    // -Klasse über ein Tool wie ResGen oder Visual Studio automatisch generiert.
    // Um einen Member hinzuzufügen oder zu entfernen, bearbeiten Sie die .ResX-Datei und führen dann ResGen
    // mit der /str-Option erneut aus, oder Sie erstellen Ihr VS-Projekt neu.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "2.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class TVgenial {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal TVgenial() {
        }
        
        /// <summary>
        ///   Gibt die zwischengespeicherte ResourceManager-Instanz zurück, die von dieser Klasse verwendet wird.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Crazysoft.OTR_Remote.Lang.TVgenial", typeof(TVgenial).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Überschreibt die CurrentUICulture-Eigenschaft des aktuellen Threads für alle
        ///   Ressourcenzuordnungen, die diese stark typisierte Ressourcenklasse verwenden.
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
        
        internal static System.Drawing.Bitmap Logo {
            get {
                object obj = ResourceManager.GetObject("Logo", resourceCulture);
                return ((System.Drawing.Bitmap)(obj));
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die TVgenial Version 4 saves its plugins in the application directory. Unfortunately, you don&apos;t have write access to it. Please run OTR Remote again as an Administrator. ähnelt.
        /// </summary>
        internal static string Plugin_Error_AccessDenied_Interface {
            get {
                return ResourceManager.GetString("Plugin_Error_AccessDenied_Interface", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die You don&apos;t have write access to the recording script path. Please enter  another path. ähnelt.
        /// </summary>
        internal static string Plugin_Error_AccessDenied_Script {
            get {
                return ResourceManager.GetString("Plugin_Error_AccessDenied_Script", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Please enter the name of the Recording Script. ähnelt.
        /// </summary>
        internal static string Plugin_Error_Name {
            get {
                return ResourceManager.GetString("Plugin_Error_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die TVgenial was found on your computer, but it seems, that the installation is corrupt. Please try reinstalling TVgenial. ähnelt.
        /// </summary>
        internal static string Plugin_Error_NoInstallDir {
            get {
                return ResourceManager.GetString("Plugin_Error_NoInstallDir", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Please enter the path to TVgenial. ähnelt.
        /// </summary>
        internal static string Plugin_Error_Path {
            get {
                return ResourceManager.GetString("Plugin_Error_Path", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Set as default Recording Interface ähnelt.
        /// </summary>
        internal static string Plugin_Settings_Default {
            get {
                return ResourceManager.GetString("Plugin_Settings_Default", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Enable Deletion of Recordings ähnelt.
        /// </summary>
        internal static string Plugin_Settings_Delete {
            get {
                return ResourceManager.GetString("Plugin_Settings_Delete", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Recording Script Name ähnelt.
        /// </summary>
        internal static string Plugin_Settings_Name {
            get {
                return ResourceManager.GetString("Plugin_Settings_Name", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Sucht eine lokalisierte Zeichenfolge, die Recording Script Path ähnelt.
        /// </summary>
        internal static string Plugin_Settings_Path {
            get {
                return ResourceManager.GetString("Plugin_Settings_Path", resourceCulture);
            }
        }
    }
}
