using iSoft.Common.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace iSoft.Database.Extensions
{
    [AttributeUsage(AttributeTargets.Property)]
    public class ListEntityAttribute : Attribute
    {
        public string EntityTargetName { get; }
        public string IdsAttrName { get; }
        public EnumAttributeRelationshipType Category { get; }
        public EnumFormDataType TypeName { get; set; }


        public ListEntityAttribute(string entityTargetName, string idsAttrName, EnumAttributeRelationshipType category, EnumFormDataType typeName = EnumFormDataType.SelectMulti)
        {
            EntityTargetName = entityTargetName;
            IdsAttrName = idsAttrName;
            Category = category;
            TypeName = typeName;
        }
    }
}
