/*
	This file was generated automatically by Garcia Framework. 
	Do not edit manually. 
	Add a new partial class with the same name if you want to add extra functionality.
*/
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using MannFramework;
using System.ComponentModel.DataAnnotations;

namespace MannFramework.Application
{
    public partial class ItemProperty : MannFramework.Application.ApplicationEntity
    {
        public string Name { get; set; }
        public bool IsNullable { get; set; }
        public ItemPropertyType Type { get; set; }
        public ItemPropertyMappingType MappingType { get; set; }
        public Item InnerType { get { return Get(_innerTypeId, ref _innerType); } set { Set(ref _innerType, ref _innerTypeId, value); } }
        [NotSelected]
        [NotSaved]
        public int? InnerTypeId { get { return _innerTypeId; } set { _innerTypeId = value; } }
        public int MinLength { get; set; }
        public int MaxLength { get; set; }
        public bool IsUnicode { get; set; }
        public Item Item { get { return Get(_itemId, ref _item); } set { Set(ref _item, ref _itemId, value); } }
        public AccessorType AccessorType { get; set; }
        public AssociationType AssociationType { get; set; }
        public bool MvcIgnore { get; set; }
        public bool MvcListIgnore { get; set; }
        public bool NotSelected { get; set; }
        public bool NotSaved { get; set; }
        public bool AppendToToString { get; set; }
        public string FieldName { get; set; }
        [NotSelected]
        [NotSaved]
        public int? ItemId { get { return _itemId; } set { _itemId = value; } }
        public string RegularExpressionValidation { get; set; }
        public bool AddOnChange { get; set; }
        [StringLength(200, MinimumLength = 0)]
        public string EditGroupName { get; set; }

        #region Lazy load
        private Item _innerType;
        private int? _innerTypeId;
        private Item _item;
        private int? _itemId;
        #endregion

        public ItemProperty()
        {
        }
    }
}

