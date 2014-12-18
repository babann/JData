using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JData
{
    internal sealed class JDataColumnPropertyDescriptor : PropertyDescriptor
    {
        IJDataCell column;
 
        internal JDataColumnPropertyDescriptor(IJDataCell dataColumn) : base(dataColumn.Value, null) {
            this.column = dataColumn;    
        }
 
        public override AttributeCollection Attributes {
            get {
                if (typeof(System.Collections.IList).IsAssignableFrom(this.PropertyType)) {
                    Attribute[] attrs = new Attribute[base.Attributes.Count + 1];
                    base.Attributes.CopyTo(attrs, 0);
                    // we don't want to show the columns which are of type IList in the designer
                    attrs[attrs.Length - 1] = new ListBindableAttribute(false);
                    return new AttributeCollection(attrs);
                } else {
                    return base.Attributes;
                }
            }
        }
 
        public override Type ComponentType {
            get {
                return typeof(IJDataRow);
            }
        }
 
        public override bool IsReadOnly {
            get {
                return false;
            }
        }
 
        public override Type PropertyType {
            get {
                return typeof(string);
            }
        }
 
        public override bool Equals(object other) {
            if (other is JDataColumnPropertyDescriptor) {
                JDataColumnPropertyDescriptor descriptor = (JDataColumnPropertyDescriptor) other;
                return(descriptor.column == column);
            }
            return false;
        }
 
        public override Int32 GetHashCode() {
            return column.GetHashCode();
        }
 
        public override bool CanResetValue(object component) {
            //DataRowView dataRowView = (DataRowView) component;
            //if (!column.IsSqlType)
            //	return (dataRowView.GetColumnValue(column) != DBNull.Value);
            //return (!DataStorage.IsObjectNull(dataRowView.GetColumnValue(column)));

            return true;
        }
 
        public override object GetValue(object component) {
            IJDataRow row = (IJDataRow) component;
            return row[column.Value].Value;
        }
 
        public override void ResetValue(object component) {
            IJDataRow row = (IJDataRow)component;
            row[column.Value].Value = null;
            OnValueChanged(component, EventArgs.Empty);
        }
 
        public override void SetValue(object component, object value) {
            IJDataRow row = (IJDataRow)component;
            row[column.Value].Value = value as string;
            OnValueChanged(component, EventArgs.Empty);
        }
 
        public override bool ShouldSerializeValue(object component) {
            return false;
        }
 
		public override bool IsBrowsable {
			get {
				return true;// (column.ColumnMapping == System.Data.MappingType.Hidden ? false : base.IsBrowsable);
			}
		}
    }
}
