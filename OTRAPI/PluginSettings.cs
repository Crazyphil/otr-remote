using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

[assembly: CLSCompliant(true)]
namespace Crazysoft.OTRRemote
{
    [Serializable]
    /// <summary>
    /// A collection of PluginSettings.
    /// </summary>
    public class PluginSettingsCollection
    {
        private List<PluginSetting> settings = new List<PluginSetting>();

        /// <summary>
        /// Initializes a new PluginSettingsCollection.
        /// </summary>
        public PluginSettingsCollection() { }

        /// <summary>
        /// Initializes a new PluginSettingsCollection and adds settings.
        /// </summary>
        /// <param name="settings">One or more PluginSettings to add to the PluginSettingsCollection.</param>
        public PluginSettingsCollection(params PluginSetting[] settings)
        {
            foreach (PluginSetting setting in settings)
            {
                this.settings.Add(setting);
            }
        }

        /// <summary>
        /// Returns a PluginSetting, according to its index in the PluginSettingsCollection.
        /// </summary>
        /// <param name="index">The index of the PluginSetting to return.</param>
        /// <returns>PluginSetting containing the requested setting.</returns>
        public PluginSetting this[int index]
        {
            get
            {
                return (PluginSetting)settings[index];
            }
        }

        /// <summary>
        /// Returns a PluginSetting, according to its name.
        /// </summary>
        /// <param name="name">The case-insensitive name of the PluginSetting to return.</param>
        /// <returns>PluginSetting containing the requested setting, or null if it is not found.</returns>
        public PluginSetting this[string name]
        {
            get
            {
                return GetSetting(name);
            }
        }

        /// <summary>
        /// Adds a new PluginSetting to the PluginSettingsCollection.
        /// </summary>
        /// <param name="name">Name of the new setting.</param>
        /// <param name="value">Value of the setting.</param>
        /// <param name="valueType">Type of the setting's value.</param>
        public void Add(string name, string value, ValueType valueType)
        {
            this.Add(new PluginSetting(name, value, valueType));
        }

        /// <summary>
        /// Adds an existing PluginSetting to the PluginSettingsCollection.
        /// </summary>
        /// <param name="setting">The PluginSetting to add.</param>
        public void Add(PluginSetting setting)
        {
            if (!this.Contains(setting))
            {
                settings.Add(setting);
            }
            else
            {
                settings[settings.IndexOf(GetSetting(setting.Name))] = setting;
            }
        }

        /// <summary>
        /// Deletes all settings from the PluginSettingsCollection.
        /// </summary>
        public void Clear()
        {
            settings.Clear();
        }

        /// <summary>
        /// Returns true if a PluginSetting exists in the PluginSettingsCollection.
        /// </summary>
        /// <param name="name">The name of the PluginSetting to find.</param>
        /// <returns>True, if the PluginSetting is contained, else false.</returns>
        public bool Contains(string name)
        {
            if (GetSetting(name) != null)
            {
                return true;
            }
            else
            {
                return false;
            }
        }


        /// <summary>
        /// Returns true if a PluginSetting exists in the PluginSettingsCollection.
        /// </summary>
        /// <param name="setting">The PluginSetting to find.</param>
        /// <returns>True, if the PluginSetting is contained, else false.</returns>
        public bool Contains(PluginSetting setting)
        {
            return this.Contains(setting.Name);
        }

        /// <summary>
        /// Returns the count of PluginSettings in the PluginSettingsCollection.
        /// </summary>
        public int Count
        {
            get { return settings.Count; }
        }
	
        /// <summary>
        /// Removes a PluginSetting from the PluginSettingsCollection.
        /// </summary>
        /// <param name="setting">PluginSetting to remove.</param>
        public void Remove(PluginSetting setting)
        {
            settings.Remove(setting);
        }

        private PluginSetting GetSetting(string name)
        {
            foreach (PluginSetting setting in settings)
            {
                if (setting.Name.ToUpperInvariant() == name.ToUpperInvariant())
                {
                    return setting;
                }
            }

            return null;
        }
    }

    [Serializable]
    /// <summary>
    /// A single PluginSetting
    /// </summary>
    public class PluginSetting
    {
        /// <summary>
        /// Initializes a new PluginSetting.
        /// </summary>
        /// <param name="name">Name of the new PluginSetting.</param>
        /// <param name="value">Value of the new PluginSetting.</param>
        /// <param name="valueType">The PluginSetting value's type.</param>
        public PluginSetting(string name, string value, ValueType valueType)
        {
            _Name = name;
            _Value = value;
            _ValueType = valueType;
        }

        /// <summary>
        /// Initializes a new PluginSetting.
        /// </summary>
        /// <param name="name">Name of the new PluginSetting.</param>
        /// <param name="value">Value of the new PluginSetting.</param>
        public PluginSetting(string name, string value)
        {
            _Name = name;
            _Value = value;
            _ValueType = ValueType.String;
        }

        /// <summary>
        /// Initializes a new PluginSetting.
        /// </summary>
        /// <param name="name">Name of the new PluginSetting.</param>
        public PluginSetting(string name)
        {
            _Name = name;
            _Value = "";
            _ValueType = ValueType.String;
        }

        /// <summary>
        /// Initializes a new empty PluginSetting.
        /// </summary>
        public PluginSetting()
        {
            _Name = "";
            _Value = "";
            _ValueType = ValueType.String;
        }

        private string _Name;

        /// <summary>
        /// The name of the PluginSetting.
        /// </summary>
        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private string _Value;

        /// <summary>
        /// The value of the PluginSetting,
        /// </summary>
        public string Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        private ValueType _ValueType;

        /// <summary>
        /// The PluginSetting value's type.
        /// </summary>
        public ValueType ValueType
        {
            get { return _ValueType; }
            set { _ValueType = value; }
        }
    }

    /// <summary>
    /// The type of value the user can enter for a setting.
    /// </summary>
    public enum ValueType { String, Boolean, Integer }
}
