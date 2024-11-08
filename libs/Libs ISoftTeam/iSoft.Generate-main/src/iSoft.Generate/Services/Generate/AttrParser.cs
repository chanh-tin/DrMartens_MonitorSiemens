using iSoft.DBLibrary.DBConnections.Factory;
using iSoft.Common.Enums.DBProvider;
using Serilog;
using iSoft.Common.ConfigsNS;
using SourceBaseBE.Database.Repository;
using SourceBaseBE.Database.DBContexts;
using SourceBaseBE.Database.Entities;
using iSoft.Common.Models.RequestModels;
using System.Collections.Generic;
using System;
using System.IO;
using System.Text.RegularExpressions;
using ConnectionCommon.Connection;
using Nest;
using System.Text;
using iSoft.Common.Utils;

namespace SourceBaseBE.MainService.Services.Generate
{
    public class AttrParser
    {
        public List<CustomAttr> ParseAttributes(string input, ref string tableNameOut)
        {

            List<CustomAttr> attributes = new List<CustomAttr>();

            string patternTableName = @"\[Table\(""(?<tablename>.+)""\)\]"; // "[Table("OeePoints")]"
            string patternAttr = @"^\s*public( virtual)? (?<type>[\<\>\w]+\??) (?<name>\w+) "; // "    public class OeePointEntity"
            string patternAnno2 = @"^\s*\[(?<name>\w+)\(nameof\((?<param1>[^\(\)\,\[\]]+)\)\, nameof\((?<param2>[^\(\)\,\[\]]+)\)\,[^\[\]]+\)\]";
            string patternAnno3 = @"^\s*\[(?<name>\w+)\(nameof\((?<param1>[^\(\)\,\[\]]+)\)\, nameof\((?<param2>[^\(\)\,\[\]]+)\)\, (?<param3>[^\(\)\,\[\]]+)\)\]"; // "        [ListEntityAttribute(nameof(MachineBlockDataEntity), nameof(MachineBlockDataIds), EnumAttributeRelationshipType.One2Many)]"
            string patternAnnoForeignkey = @"^\s*\[(?<name>\w+)\(nameof\((?<param1>[^\(\)\,\[\]]+)\)\)\]"; // "        [ForeignKey(nameof(OeePointConfigEntity))]"
            //string patternAnnoFormData = @"^\s*\[(?<name>\w+)\(EnumFormDataType\.(?<param1>[\w\d_]+)[\,\s]+[^\[\]\(\)]*\)\]";
            string patternAnnoFormData = @"^\s*\[(?<name>\w+)\(EnumFormDataType\.(?<param1>[\w\d_]+)[\,\s]*[^\(\)]*\)\]"; // "        [FormDataTypeAttributeText(EnumFormDataType.Textbox, isRequired: true, maxLen: 255)]"
            string patternAnnoOther = @"^\s*\[(?<name>\w+)[^\[\]]*\]"; // "        [NotMapped]"

            string[] lines = input.Split("\n", StringSplitOptions.RemoveEmptyEntries);

            List<CustomAnno> listAnnotation = new List<CustomAnno>();

            bool matchedFlag = false;
            foreach (string line in lines)
            {
                matchedFlag = false;

                Match matchTableName = Regex.Match(line, patternTableName);
                if (!matchedFlag && matchTableName.Success)
                {
                    string tableName = matchTableName.Groups["tablename"].Value;

                    tableNameOut = tableName;
                    matchedFlag = true;
                }

                Match matchAttribute = Regex.Match(line, patternAttr);
                if (!matchedFlag && matchAttribute.Success)
                {
                    string name = matchAttribute.Groups["name"].Value;
                    string type = matchAttribute.Groups["type"].Value;
                    bool isNullable = type.EndsWith("?");
                    type = type.TrimEnd('?');

                    if (type != "class" && type != "override" && type != "virtual")
                    {
                        attributes.Add(new CustomAttr
                        {
                            Name = name,
                            Type = type,
                            IsNullable = isNullable,
                            ListAnnotation = listAnnotation
                        });
                        listAnnotation = new List<CustomAnno>();
                        matchedFlag = true;
                    }
                }

                Match matchAnnotation3 = Regex.Match(line, patternAnno3);
                if (!matchedFlag && matchAnnotation3.Success)
                {
                    string name = matchAnnotation3.Groups["name"].Value;
                    string param1 = matchAnnotation3.Groups["param1"].Value;
                    string param2 = matchAnnotation3.Groups["param2"].Value;
                    string param3 = matchAnnotation3.Groups["param3"].Value;

                    listAnnotation.Add(new CustomAnno
                    {
                        Name = name,
                        Param1 = param1,
                        Param2 = param2,
                        Param3 = param3,

                    });
                    matchedFlag = true;
                }
                else
                {
                    Match matchAnnotation2 = Regex.Match(line, patternAnno2);
                    if (!matchedFlag && matchAnnotation2.Success)
                    {
                        string name = matchAnnotation2.Groups["name"].Value;
                        string param1 = matchAnnotation2.Groups["param1"].Value;
                        string param2 = matchAnnotation2.Groups["param2"].Value;

                        listAnnotation.Add(new CustomAnno
                        {
                            Name = name,
                            Param1 = param1,
                            Param2 = param2,
                        });
                        matchedFlag = true;
                    }
                }

                Match matchAnnoForeignkey = Regex.Match(line, patternAnnoForeignkey);
                if (!matchedFlag && matchAnnoForeignkey.Success)
                {
                    string name = matchAnnoForeignkey.Groups["name"].Value;
                    string param1 = matchAnnoForeignkey.Groups["param1"].Value;

                    listAnnotation.Add(new CustomAnno
                    {
                        Name = name,
                        Param1 = param1,
                    });
                    matchedFlag = true;
                }

                Match matchAnnoFormData = Regex.Match(line, patternAnnoFormData);
                if (!matchedFlag && matchAnnoFormData.Success)
                {
                    string name = matchAnnoFormData.Groups["name"].Value;
                    string param1 = matchAnnoFormData.Groups["param1"].Value;

                    listAnnotation.Add(new CustomAnno
                    {
                        Name = name,
                        Param1 = param1,
                    });
                    matchedFlag = true;
                }

                Match matchAnnoOther = Regex.Match(line, patternAnnoOther);
                if (!matchedFlag && matchAnnoOther.Success)
                {
                    string name = matchAnnoOther.Groups["name"].Value;

                    listAnnotation.Add(new CustomAnno
                    {
                        Name = name,
                    });
                    matchedFlag = true;
                }
            }

            return attributes;
        }
    }
}