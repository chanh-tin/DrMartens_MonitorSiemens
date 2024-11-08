using SourceBaseBE.Database.DBContexts;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Text;
using iSoft.Common.Utils;
using iSoft.Database.Extensions;
using iSoft.Common.Enums;
using Microsoft.Extensions.Logging;
using iSoft.Common.Utils;

namespace SourceBaseBE.MainService.Services.Generate
{
    public partial class GenerateSourceService
    {

        private string GetGenData(string entityName, List<CustomAttr> listAttr, string genKey, string inputString = "")
        {
            string strNew = "";
            StringBuilder sb = new StringBuilder();
            GetDicIdsIgnoreAttr(listAttr, EnumFileType.None);

            switch (genKey)
            {
                case "/*[GEN-1]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && (anno.Name == nameof(ListEntityAttribute) || anno.Name == "ForeignKey"))
                            {
                                if (newCRUD_base.name == anno.GetTableName())
                                {
                                    continue;
                                }
                                if ("User" == anno.GetTableName())
                                {
                                    continue;
                                }
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string newStr = $"        protected {tbName2}Repository _{lowTbName2}Repository;\r\n";
                                if (!IsExitsFileContent(sb.ToString(), newStr.Replace(genKey, "")))
                                {
                                    sb.Append(newStr);
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n        " + genKey;
                case "/*[GEN-2]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && (anno.Name == nameof(ListEntityAttribute) || anno.Name == "ForeignKey"))
                            {
                                if (newCRUD_base.name == anno.GetTableName())
                                {
                                    continue;
                                }
                                if ("User" == anno.GetTableName())
                                {
                                    continue;
                                }
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string newStr = $"            this._{lowTbName2}Repository = new {tbName2}Repository(this._dbContext);\r\n";
                                if (!IsExitsFileContent(sb.ToString(), newStr.Replace(genKey, "")))
                                {
                                    sb.Append(newStr);
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;
                case "/*[GEN-3]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                if (newCRUD_base.name == anno.GetTableName())
                                {
                                    sb.Append($@"
            List<{tbName2}Entity> {lowTbName2}Children = null;
            if (entity.{idsName2} != null && entity.{idsName2}.Count >= 1)
            {{
                {lowTbName2}Children = _repositoryCRUD.GetListByListIds(entity.{idsName2}, true, true);
                if ({lowTbName2}Children == null || {lowTbName2}Children.Count <= 0)
                {{
                    throw new DBException($""{lowTbName2}Children not found, ids: {{string.Join("","", entity.{idsName2})}}"");
                }}
            }}
");
                                }
                                else
                                {
                                    sb.Append($@"
            List<{tbName2}Entity> {lowTbName2}Children = null;
            if (entity.{idsName2} != null && entity.{idsName2}.Count >= 1)
            {{
                {lowTbName2}Children = _{lowTbName2}Repository.GetListByListIds(entity.{idsName2}, true, true);
                if ({lowTbName2}Children == null || {lowTbName2}Children.Count <= 0)
                {{
                    throw new DBException($""{lowTbName2}Children not found, ids: {{string.Join("","", entity.{idsName2})}}"");
                }}
            }}
");
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;
                case "/*[GEN-4]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($", {lowTbName2}Children");
                            }
                        }
                    }
                    return sb.ToString().Trim() + genKey;
                case "/*[GEN-5]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($@"
                case nameof({tbName2}Entity):
                    if (entity.{attr.Name} == null)
                    {{
                        return new List<long>();
                    }}
                    return entity.{attr.Name}.Select(x => x.Id).ToList();
");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                " + genKey;
                case "/*[GEN-6]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && (anno.Name == nameof(ListEntityAttribute) || anno.Name == "ForeignKey"))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                if (newCRUD_base.name == anno.GetTableName())
                                {
                                    string newStr = $@"
                    case nameof({tbName2}Entity):
                        listRS = this._repositoryCRUD.GetSelectData(entityName, category);
                        break;
";
                                    if (!IsExitsFileContent(sb.ToString(), newStr.Replace(genKey, "")))
                                    {
                                        sb.Append(newStr);
                                    }
                                }
                                else
                                {
                                    string newStr = $@"
                    case nameof({tbName2}Entity):
                        listRS = this._{lowTbName2}Repository.GetSelectData(entityName, category);
                        break;
";
                                    if (!IsExitsFileContent(sb.ToString(), newStr.Replace(genKey, "")))
                                    {
                                        sb.Append(newStr);
                                    }
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                " + genKey;
                case "/*[GEN-7]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($"                      .Include(entity => entity.{attr.Name})\r\n");
                            }
                            else if (anno != null && anno.Name == "ForeignKey")
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($"                      .Include(entity => entity.Item{tbName2})\r\n");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                      " + genKey;
                case "/*[GEN-11]*/":
                case "/*[GEN-13]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($"                                .Include(entity => entity.{attr.Name})\r\n");
                            }
                            else if (anno != null && anno.Name == "ForeignKey")
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($"                                .Include(entity => entity.Item{tbName2})\r\n");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                                " + genKey;
                case "/*[GEN-8]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($", List<{tbName2}Entity> {lowTbName2}Children");
                            }
                        }
                    }
                    return sb.ToString().Trim() + genKey;
                case "/*[GEN-9]*/":
                case "/*[GEN-10]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($@"
                    entity.{attr.Name} = MergeChildrenEntity(entity.{attr.Name}, {lowTbName2}Children);
");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                    " + genKey;
                case "/*[GEN-12]*/":
                case "/*[GEN-14]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                string idsName2 = anno.Param2;
                                sb.Append($"                        result[i].{attr.Name} = result[i].{attr.Name}?.Select(x => new {tbName2}Entity() {{Id = x.Id}}).ToList();\r\n");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                        " + genKey;
                case "/*[GEN-15]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                if (anno.Param3 == "EnumAttributeRelationshipType.One2Many")
                                {

                                }
                                else
                                {
                                    string tbName2 = anno.GetTableName();
                                    string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                    string idsName2 = anno.Param2;
                                    if (!IsExitsFileContent(inputString, GetJoinEntityST(newCRUD_base.name, tbName2, $"List{tbName2}")))
                                    {
                                        if (!IsExitsFileContent(inputString, GetJoinEntityST(tbName2, newCRUD_base.name, $"List{newCRUD_base.name}")))
                                        {
                                            sb.Append(GetJoinEntityST(newCRUD_base.name, tbName2, attr.Name));
                                        }
                                    }
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;
                case "/*[GEN-16]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == "ForeignKey")
                            {
                                string tbName2 = anno.GetTableName();
                                if (!IsExitsFileContent(inputString, GetJoinEntityForeignKey(newCRUD_base.name, tbName2, attr.Name)))
                                {
                                    if (!IsExitsFileContent(inputString, GetJoinEntityForeignKey(tbName2, newCRUD_base.name, attr.Name)))
                                    {
                                        sb.Append(GetJoinEntityForeignKey(newCRUD_base.name, tbName2, attr.Name));
                                    }
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;
                case "/*[GEN-17]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Image)
                                ))
                            {
                                sb.Append($@"
            if (this.{attr.Name} != null)
            {{
                dicRS.Add(nameof({attr.Name}), (Path.Combine(ConstFolderPath.Images, ConstFolderPath.Upload), this.{attr.Name}));
            }}");
                            }
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListImage)
                                ))
                            {
                                sb.Append($@"
            if (this.{attr.Name} != null)
            {{
                for(int i = 0; i < this.{attr.Name}.Length; i++)
                {{
                    var formFile = this.{attr.Name}[i];
                    dicRS.Add($""{{nameof({attr.Name})}}{{ConstCommon.ConstSeperatorFileKey}}{{i}}"", (Path.Combine(ConstFolderPath.Images, ConstFolderPath.Upload), formFile));
                }}
            }}");
                            }
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.File)
                                ))
                            {
                                sb.Append($@"
            if (this.{attr.Name} != null)
            {{
                dicRS.Add(nameof({attr.Name}), (Path.Combine(ConstFolderPath.Files, ConstFolderPath.Upload), this.{attr.Name}));
            }}");
                            }
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListFile)
                                ))
                            {
                                sb.Append($@"
            if (this.{attr.Name} != null)
            {{
                for(int i = 0; i < this.{attr.Name}.Length; i++)
                {{
                    var formFile = this.{attr.Name}[i];
                    dicRS.Add($""{{nameof({attr.Name})}}{{ConstCommon.ConstSeperatorFileKey}}{{i}}"", (Path.Combine(ConstFolderPath.Files, ConstFolderPath.Upload), formFile));
                }}
            }}");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;

                case "/*[GEN-18]*/":
                    strNew = GetAttrData(listAttr, EnumFileType.RequestFile);
                    return strNew.Trim() + "\r\n        " + genKey;

                case "/*[GEN-19]*/":
                    strNew = GetAttrSetData(listAttr, EnumFileType.RequestFile);
                    return strNew.Trim() + "\r\n        " + genKey;

                case "/*[GEN-20]*/":
                    strNew = GetAttrData(listAttr, EnumFileType.ResponseFile);
                    return strNew.Trim() + "\r\n        " + genKey;

                case "/*[GEN-21]*/":
                    strNew = GetAttrSetData(listAttr, EnumFileType.ResponseFile);
                    return strNew.Trim() + "\r\n        " + genKey;

                case "/*[GEN-22]*/":
                    strNew = $@"            modelBuilder.Entity<{entityName}Entity>().HasIndex(x => x.CreatedAt);";
                    return strNew.Trim() + "\r\n            " + genKey;

                case "/*[GEN-23]*/":
                    strNew = $@"            modelBuilder.Entity<{entityName}Entity>().HasIndex(x => x.UpdatedAt);";
                    return strNew.Trim() + "\r\n            " + genKey;

                case "/*[GEN-24]*/":
                    strNew = $@"            modelBuilder.Entity<{entityName}Entity>().HasIndex(x => x.Order);";
                    return strNew.Trim() + "\r\n            " + genKey;

                case "/*[GEN-25]*/":
                    strNew = $@"            modelBuilder.Entity<{entityName}Entity>().HasIndex(x => x.DeletedFlag);";
                    return strNew.Trim() + "\r\n            " + genKey;

                case "/*[GEN-26]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Image)
                                    || anno.Param1 == nameof(EnumFormDataType.File)
                                ))
                            {
                                sb.Append($@"
                if (dicImagePath.ContainsKey(nameof({entityName}Entity.{attr.Name})))
                {{
                    entity.{attr.Name} = dicImagePath[nameof({entityName}Entity.{attr.Name})];
                }}");
                            }
                            if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListImage)
                                    || anno.Param1 == nameof(EnumFormDataType.ListFile)
                                ))
                            {
                                sb.Append($@"
                List<string> list{attr.Name} = new List<string>();
                for(int i = 0; i < 1000; i++)
                {{
                    string key = $""{{nameof({entityName}Entity.{attr.Name})}}{{ConstCommon.ConstSeperatorFileKey}}{{i}}"";
                    if (dicImagePath.ContainsKey(key))
                    {{
                        list{attr.Name}.Add(dicImagePath[key]);
                    }}
                    else
                    {{
                        break;
                    }}
                }}
                entity.{attr.Name} = string.Join("","", list{attr.Name});");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;

                case "/*[GEN-27]*/":
                case "/*[GEN-29]*/":
                    string spaceStr = "";
                    switch (genKey)
                    {
                        case "/*[GEN-27]*/":
                            spaceStr = "                        ";
                            break;
                        case "/*[GEN-29]*/":
                            spaceStr = "                            ";
                            break;
                    }
                    bool setFlag = false;
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }

                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                sb.Append($"{spaceStr}{attr.Name} = entity.{attr.Name} == null ? null : entity.{attr.Name}.Where(x => x.DeletedFlag != true).ToList(),\r\n");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            sb.Append($"{spaceStr}{attr.Name} = entity.{attr.Name},\r\n");
                        }
                    }
                    return sb.ToString().Trim() + "\r\n" + spaceStr + genKey;

                case "/*[GEN-28]*/":
                case "/*[GEN-30]*/":
                    spaceStr = "";
                    switch (genKey)
                    {
                        case "/*[GEN-28]*/":
                            spaceStr = "                        ";
                            break;
                        case "/*[GEN-30]*/":
                            spaceStr = "                            ";
                            break;
                    }
                    setFlag = false;
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }

                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                sb.Append($"{spaceStr}entity.{attr.Name} = entity.{attr.Name} == null ? null : entity.{attr.Name}.Where(x => x.DeletedFlag != true).ToList();\r\n");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            //sb.Append($"{spaceStr}{attr.Name} = entity.{attr.Name},\r\n");
                        }
                    }
                    return sb.ToString().Trim() + "\r\n" + spaceStr + genKey;
                case "/*[GEN-31]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == "DisplayField")
                            {
                                sb.Append($"            return entity.{attr.Name};\r\n");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n            " + genKey;

                case "/*[GEN-32]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == "NotFormData")
                            {
                                sb.Append($"                    || property.Name == nameof({entityName}Entity.{attr.Name})\r\n");
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                    " + genKey;

                case "/*[GEN-33]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (!(anno != null && (anno.Name == nameof(ListEntityAttribute) || anno.Name == "ForeignKey")))
                            {
                                if (!attr.Type.Contains("List<"))
                                {
                                    sb.Append($"            dic.Add(nameof(Base{entityName}ResponseModel.{attr.Name}), typeof({attr.Type}).ToString());\r\n");
                                    break;
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                    " + genKey;

                case "/*[GEN-34]*/":
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (!(anno != null && (anno.Name == nameof(ListEntityAttribute) || anno.Name == "ForeignKey")))
                            {
                                if (!attr.Type.Contains("List<"))
                                {
                                    sb.Append($"            dic.Add(nameof(Base{entityName}ResponseModel.{attr.Name}), typeof({attr.Type}).ToString());\r\n");
                                    break;
                                }
                            }
                        }
                    }
                    return sb.ToString().Trim() + "\r\n                    " + genKey;

                default:
                    return "";
            }
        }

        string GetJoinEntityForeignKey(string table1, string table2, string fieldName)
        {
            return $@"
            modelBuilder.Entity<{table1}Entity>()
                        .HasOne(e => e.Item{table2})
                        .WithMany(e => e.List{table1})
                        .HasForeignKey(e => e.{fieldName})
                        .OnDelete(DeleteBehavior.ClientSetNull);
";
        }
        string GetJoinEntityST(string table1, string table2, string fieldName)
        {
            return $@"
            modelBuilder.Entity<{table1}Entity>()
                        .HasMany(e => e.{fieldName})
                        .WithMany(e => e.List{table1})
                        .UsingEntity<Dictionary<string, object>>(
                            ""ref{table1}{table2}"",
                            j => j
                                .HasOne<{table2}Entity>()
                                .WithMany()
                                .HasForeignKey(""{table2}Id"")
                                .OnDelete(DeleteBehavior.ClientSetNull),
                            j => j
                                .HasOne<{table1}Entity>()
                                .WithMany()
                                .HasForeignKey(""{table1}Id"")
                                .OnDelete(DeleteBehavior.ClientSetNull)
                        );
";
        }

        Dictionary<string, bool> dicIdsAttr = null;
        private void GetDicIdsIgnoreAttr(List<CustomAttr> listAttr, EnumFileType fileType)
        {
            //if (dicIdsAttr == null)
            {
                dicIdsAttr = new Dictionary<string, bool>();

                if (fileType == EnumFileType.RequestFile)
                {
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                if (!dicIdsAttr.ContainsKey(anno.Name))
                                {
                                    dicIdsAttr.Add(anno.Param2, true);
                                }
                            }
                            if (anno != null && anno.Name == "ForeignKey")
                            {
                                string tbName2 = anno.GetTableName();
                                string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                                if (!dicIdsAttr.ContainsKey($"Item{tbName2}"))
                                {
                                    dicIdsAttr.Add($"Item{tbName2}", true);
                                }
                            }
                        }
                    }
                }
                else
                {
                    foreach (CustomAttr attr in listAttr)
                    {
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                if (!dicIdsAttr.ContainsKey(anno.Name))
                                {
                                    dicIdsAttr.Add(anno.Param2, true);
                                }
                            }
                        }
                    }
                }
            }
            return;
        }
        private string GetAttrData(List<CustomAttr> listAttr, EnumFileType fileType)
        {
            GetDicIdsIgnoreAttr(listAttr, fileType);

            StringBuilder sb = new StringBuilder();
            bool setFlag = false;
            switch (fileType)
            {
                case EnumFileType.RequestFile:
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }

                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            //if (anno != null && anno.Name == "ForeignKey")
                            //{
                            //    string tbName2 = anno.GetTableName();
                            //    string lowTbName2 = StringUtil.LowerFirstLetter(anno.GetTableName());
                            //    string isNullStr = attr.IsNullable ? "?" : "";
                            //    sb.Append($"        public {attr.Type}{isNullStr} {attr.Name} {{ get; set; }}\r\n");
                            //    setFlag = true;
                            //}
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                sb.Append($"        public virtual List<long>? {attr.Name} {{ get; set; }}\r\n");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && anno.Param1 == nameof(EnumFormDataType.DateOnly))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        DateTime? _{lowAttrName} {{ get; set; }}
        public virtual string? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName}.HasValue)
                {{
                    return _{lowAttrName}.Value.ToString(ConstDateTimeFormat.YYYYMMDD);
                }}
                return null;
            }}
            set
            {{
                if (!string.IsNullOrEmpty(value))
                {{
                    try
                    {{
                        _{lowAttrName} = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.YYYYMMDD);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && anno.Param1 == nameof(EnumFormDataType.TimeOnly))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        DateTime? _{lowAttrName} {{ get; set; }}
        public virtual string? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName}.HasValue)
                {{
                    return _{lowAttrName}.Value.ToString(ConstDateTimeFormat.HHMMSS);
                }}
                return null;
            }}
            set
            {{
                if (!string.IsNullOrEmpty(value))
                {{
                    try
                    {{
                        _{lowAttrName} = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.HHMMSS);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                    && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                    && (anno.Param1 == nameof(EnumFormDataType.CheckboxMulti) || anno.Param1 == nameof(EnumFormDataType.SelectMulti)))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        string? _{lowAttrName} {{ get; set; }}
        public virtual string[]? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName} != null)
                {{
                    return _{lowAttrName}.Split("","", StringSplitOptions.RemoveEmptyEntries);
                }}
                return null;
            }}
            set
            {{
                if (value != null && value.Length >= 1)
                {{
                    try
                    {{
                        _{lowAttrName} = string.Join("","", value);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
                else
                {{
                    _{lowAttrName} = null;
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Image)
                                    || anno.Param1 == nameof(EnumFormDataType.File)
                                ))
                            {
                                sb.Append($"        public virtual IFormFile? {attr.Name} {{ get; set; }}\r\n");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListImage)
                                    || anno.Param1 == nameof(EnumFormDataType.ListFile)
                                ))
                            {
                                sb.Append($"        public virtual IFormFile[]? {attr.Name} {{ get; set; }}\r\n");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            string isNullStr = attr.IsNullable ? "?" : "";
                            sb.Append($"        public virtual {attr.Type}{isNullStr} {attr.Name} {{ get; set; }}\r\n");
                        }
                    }
                    return sb.ToString();
                case EnumFileType.ResponseFile:
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }
                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            if (anno != null
                                    && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                    && anno.Param1 == nameof(EnumFormDataType.DateOnly))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        DateTime? _{lowAttrName} {{ get; set; }}
        public virtual string? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName}.HasValue)
                {{
                    return _{lowAttrName}.Value.ToString(ConstDateTimeFormat.YYYYMMDD);
                }}
                return null;
            }}
            set
            {{
                if (!string.IsNullOrEmpty(value))
                {{
                    try
                    {{
                        _{lowAttrName} = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.YYYYMMDD);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                    && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                    && anno.Param1 == nameof(EnumFormDataType.TimeOnly))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        DateTime? _{lowAttrName} {{ get; set; }}
        public virtual string? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName}.HasValue)
                {{
                    return _{lowAttrName}.Value.ToString(ConstDateTimeFormat.HHMMSS);
                }}
                return null;
            }}
            set
            {{
                if (!string.IsNullOrEmpty(value))
                {{
                    try
                    {{
                        _{lowAttrName} = DateTimeUtil.GetDateTimeFromString(value, ConstDateTimeFormat.HHMMSS);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                    && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                    && (anno.Param1 == nameof(EnumFormDataType.CheckboxMulti) || anno.Param1 == nameof(EnumFormDataType.SelectMulti)))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        string? _{lowAttrName} {{ get; set; }}
        public virtual string[]? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName} != null)
                {{
                    return _{lowAttrName}.Split("","", StringSplitOptions.RemoveEmptyEntries);
                }}
                return null;
            }}
            set
            {{
                if (value != null && value.Length >= 1)
                {{
                    try
                    {{
                        _{lowAttrName} = string.Join("","", value);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
                else
                {{
                    _{lowAttrName} = null;
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListImage)
                                    || anno.Param1 == nameof(EnumFormDataType.ListFile)
                                ))
                            {
                                string lowAttrName = StringUtil.LowerFirstLetter(attr.Name);
                                sb.Append($@"
        string? _{lowAttrName} {{ get; set; }}
        public virtual string[]? {attr.Name}
        {{
            get
            {{
                if (_{lowAttrName} != null)
                {{
                    return _{lowAttrName}.Split("","", StringSplitOptions.RemoveEmptyEntries);
                }}
                return null;
            }}
            set
            {{
                if (value != null && value.Length >= 1)
                {{
                    try
                    {{
                        _{lowAttrName} = string.Join("","", value);
                    }}
                    catch
                    {{
                        _{lowAttrName} = null;
                    }}
                }}
                else
                {{
                    _{lowAttrName} = null;
                }}
            }}
        }}
");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            string isNullStr = attr.IsNullable ? "?" : "";
                            sb.Append($"        public virtual {attr.Type}{isNullStr} {attr.Name} {{ get; set; }}\r\n");
                        }
                    }
                    return sb.ToString();
            }
            return "";
        }

        private string GetAttrSetData(List<CustomAttr> listAttr, EnumFileType fileType)
        {
            GetDicIdsIgnoreAttr(listAttr, fileType);

            StringBuilder sb = new StringBuilder();
            bool setFlag = false;
            switch (fileType)
            {
                case EnumFileType.RequestFile:
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }

                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                sb.Append($@"            if (this.{attr.Name} != null)
            {{
                entity.{anno.Param2} = this.{attr.Name}.Select(x => x).ToList();
            }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.DateOnly)
                                    || anno.Param1 == nameof(EnumFormDataType.TimeOnly)
                                    || anno.Param1 == nameof(EnumFormDataType.CheckboxMulti)
                                    || anno.Param1 == nameof(EnumFormDataType.SelectMulti)
                                    ))
                            {
                                sb.Append($"            if (this._{attr.Name.LowerFirstLetter()} != null) entity.{attr.Name} = this._{attr.Name.LowerFirstLetter()};\r\n");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Image)
                                    || anno.Param1 == nameof(EnumFormDataType.File)
                                ))
                            {
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.ListImage)
                                    || anno.Param1 == nameof(EnumFormDataType.ListFile)
                                ))
                            {
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Password)
                                ))
                            {
                                sb.Append($@"            if (!string.IsNullOrEmpty(this.{attr.Name}))
            {{
                entity.{attr.Name} = EncodeUtil.EncodePassword(this.{attr.Name}, this.Username);
            }}
");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            sb.Append($"            if (this.{attr.Name} != null) entity.{attr.Name} = this.{attr.Name};\r\n");
                        }
                    }
                    return sb.ToString();
                case EnumFileType.ResponseFile:
                    foreach (CustomAttr attr in listAttr)
                    {
                        setFlag = false;
                        if (dicIdsAttr.ContainsKey(attr.Name))
                        {
                            continue;
                        }
                        if (attr.Name == "void")
                        {
                            continue;
                        }

                        foreach (var anno in attr.ListAnnotation)
                        {
                            if (setFlag)
                            {
                                break;
                            }
                            if (anno != null && anno.Name == nameof(ListEntityAttribute))
                            {
                                sb.Append($@"            if (entity.{attr.Name} != null)
            {{
                this.{attr.Name} = entity.{attr.Name}.Select(x => x).ToList();
            }}
");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.DateOnly)
                                    || anno.Param1 == nameof(EnumFormDataType.TimeOnly)
                                    || anno.Param1 == nameof(EnumFormDataType.CheckboxMulti)
                                    || anno.Param1 == nameof(EnumFormDataType.SelectMulti)
                                    || anno.Param1 == nameof(EnumFormDataType.ListImage)
                                    || anno.Param1 == nameof(EnumFormDataType.ListFile)
                                    ))
                            {
                                sb.Append($"            this._{attr.Name.LowerFirstLetter()} = entity.{attr.Name};\r\n");
                                setFlag = true;
                            }
                            else if (anno != null
                                && anno.Name.Contains(nameof(FormDataTypeAttribute))
                                && (anno.Param1 == nameof(EnumFormDataType.Password)
                                ))
                            {
                                sb.Append($@"            if (!string.IsNullOrEmpty(entity.{attr.Name}))
            {{
                this.{attr.Name} = new string('*', entity.{attr.Name}.Length);
            }}
");
                                setFlag = true;
                            }
                        }

                        if (!setFlag)
                        {
                            sb.Append($"            this.{attr.Name} = entity.{attr.Name};\r\n");
                        }
                    }
                    return sb.ToString();
                default:
                    return "";
            }
            return "";
        }
    }
}