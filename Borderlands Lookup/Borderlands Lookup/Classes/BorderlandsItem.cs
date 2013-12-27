using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows;

namespace Borderlands_Lookup.Classes
{
    class BorderlandsItem : Control
    {
        #region Properties
        public static readonly DependencyProperty ManufacturerProperty = DependencyProperty.Register("Manufacturer",
            typeof(string), typeof(ItemDataParser));
        public static readonly DependencyProperty WeaponTypeProperty = DependencyProperty.Register("WeaponType",
            typeof(string), typeof(ItemDataParser));
        public static readonly DependencyProperty WeaponModelProperty = DependencyProperty.Register("WeaponModel",
            typeof(string), typeof(ItemDataParser));
        public static readonly DependencyProperty ElementalTypeProperty = DependencyProperty.Register("ElementalType",
            typeof(string), typeof(ItemDataParser));
        public static readonly DependencyProperty BossProperty = DependencyProperty.Register("Boss", typeof(string), typeof(BorderlandsItem));
        public static readonly DependencyProperty LocationProperty = DependencyProperty.Register("Location",
            typeof(string), typeof(ItemDataParser));
        public static readonly DependencyProperty SpecialEffectsProperty = DependencyProperty.Register("SpecialEffects",
            typeof(string), typeof(ItemDataParser));
        public string Manufacturer
        {
            get { return (string)GetValue(ManufacturerProperty); }
            set { SetValue(ManufacturerProperty, value); }
        }
        public string WeaponType
        {
            get { return (string)GetValue(WeaponTypeProperty); }
            set { SetValue(WeaponTypeProperty, value); }
        }
        public string WeaponModel
        {
            get { return (string)GetValue(WeaponModelProperty); }
            set { SetValue(WeaponModelProperty, value); }
        }
        public string ElementalType
        {
            get { return (string)GetValue(ElementalTypeProperty); }
            set { SetValue(ElementalTypeProperty, value); }
        }
        public string Boss
        {
            get { return (string)GetValue(BossProperty); }
            set { SetValue(BossProperty, value); }
        }
        public string Location
        {
            get { return (string)GetValue(LocationProperty); }
            set { SetValue(LocationProperty, value); }
        }
        public string SpecialEffects
        {
            get { return (string)GetValue(SpecialEffectsProperty); }
            set { SetValue(SpecialEffectsProperty, value); }
        }
        #endregion
        #region Constructor
        public BorderlandsItem(string _manufacturer, string _type, string _model, string _elementalType, string _boss, string _location, 
            string _specialEffects)
        {
            this.Manufacturer = _manufacturer;
            this.WeaponType = _type;
            this.WeaponModel = _model;
            this.ElementalType = _elementalType;
            this.Location = _location;
            this.SpecialEffects = _specialEffects;
        }
        #endregion
    }
}
