﻿// -----------------------------------------------------------------------------
// This file was automatically generated.
// Please do not edit this file manually.
//
// Generated Date: 
//
// -----------------------------------------------------------------------------

using iSoft.Common.Utils;
using SourceBaseBE.Database.Entities;
using Microsoft.EntityFrameworkCore;
using iSoft.Common.Models.ResponseModels;
using LinqKit;
using iSoft.Database.Models.ResponseModels.Base.BaseCRUD;
using static iSoft.Common.ConstCommon;
using iSoft.Database.ExtensionMethods;
using SourceBaseBE.Database.Enums;
using iSoft.Common.Enums;

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class BaseGenTemplateResponseModel : BaseCRUDResponseModel<GenTemplateEntity>
    {
        public virtual string SerialCode { get; set; }

        /*[GEN-20]*/

        /*[GEN-37]*/
        public override object SetData(GenTemplateEntity entity)
        {
            base.SetData(entity);
            this.SerialCode = entity.SerialCode;
            /*[GEN-21]*/
            return this;
        }
        public override List<object> SetData(List<GenTemplateEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (GenTemplateEntity entity in listEntity)
            {
                listRS.Add(new GenTemplateResponseModel().SetData(entity));
            }
            return listRS;
        }

        /// <summary>
        /// Build query for filter
        /// # FILTER LOGIC
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dicInputFieldKey2Value"></param>
        /// <param name="getFieldAttributes"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        public virtual IQueryable<GenTemplateEntity> PrepareWhereQueryFilter(
            IQueryable<GenTemplateEntity> query,
            Dictionary<string, object> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            foreach (var keyVal in dicInputFieldKey2Value)
            {
                bool renameInputFieldKeyFlag = false;
                string inputFieldKey = keyVal.Key;

                if (!dicFieldAttr.ContainsKey(inputFieldKey))
                {
                    continue;
                }

                var fieldType = dicFieldAttr[inputFieldKey];
                object? searchValue = ConvertUtil.ConvertData(keyVal.Value, fieldType);
                if (searchValue == null)
                {
                    continue;
                }

                if (false)
                {
                }
                /*[GEN-41]*/

                if (renameInputFieldKeyFlag)
                {
                    if (!dicFieldAttr.ContainsKey(inputFieldKey))
                    {
                        continue;
                    }

                    fieldType = dicFieldAttr[inputFieldKey];
                    searchValue = ConvertUtil.ConvertData(keyVal.Value, fieldType);
                    if (searchValue == null)
                    {
                        continue;
                    }
                }

                var propertyInfo = responseType.GetProperty(inputFieldKey);
                if (propertyInfo == null)
                {
                    continue;
                }

                query = query.WherePropertyEquals(inputFieldKey, searchValue);
            }
            return query;
        }

        /// <summary>
        /// Build query for sort
        /// # SORT LOGIC
        /// </summary>
        /// <param name="query"></param>
        /// <param name="dicInputFieldKey2SortOrder"></param>
        /// <param name="getFieldAttributes"></param>
        /// <param name="responseType"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public virtual IQueryable<GenTemplateEntity> PrepareQuerySort(
            IQueryable<GenTemplateEntity> query,
            Dictionary<string, long> dicInputFieldKey2SortOrder,
            Func<Dictionary<string, string>> getFieldAttributes,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            foreach (var keyVal in dicInputFieldKey2SortOrder)
            {
                string inputFieldKey = keyVal.Key;

                var propertyInfo = responseType.GetProperty(inputFieldKey);
                if (propertyInfo == null)
                {
                    continue;
                }

                if (false)
                {
                }
                /*[GEN-40]*/
                else
                {
                    if (keyVal.Value == 1)
                    {
                        query = query.OrderByPropertyAscending(inputFieldKey);
                    }
                    else
                    {
                        query = query.OrderByPropertyDescending(inputFieldKey);
                    }
                }
            }
            return query;
        }

        /// <summary>
        /// Build query for search data
        /// # SEARCH LOGIC
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="inputFieldKey"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public virtual IQueryable<GenTemplateEntity> PrepareWhereQuerySearch(
            IQueryable<GenTemplateEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<GenTemplateEntity>(true);
            bool searchAllFlag = false;
            foreach (var keyVal in dicInputFieldKey2Value)
            {
                string inputFieldKeyAll = keyVal.Key;
                if (inputFieldKeyAll.Trim().ToUpper() == "ALL")
                {
                    searchAllFlag = true;
                    string searchValue = ConvertUtil.GetString(keyVal.Value);
                    if (searchValue == null)
                    {
                        break;
                    }

                    foreach (var keyVal2 in dicFieldAttrSearchAll)
                    {
                        string inputFieldKey = keyVal2.Key;
                        predicate = this.GetPredicate(predicate, inputFieldKey, searchValue);
                    }
                    break;
                }
            }

            if (!searchAllFlag)
            {
                foreach (var keyVal in dicInputFieldKey2Value)
                {
                    string inputFieldKey = keyVal.Key;
                    if (!dicFieldAttr.ContainsKey(inputFieldKey))
                    {
                        continue;
                    }

                    var fieldType = dicFieldAttr[inputFieldKey];
                    string searchValue = ConvertUtil.GetString(keyVal.Value);
                    if (searchValue == null)
                    {
                        continue;
                    }

                    var propertyInfo = responseType.GetProperty(inputFieldKey);
                    if (propertyInfo == null)
                    {
                        continue;
                    }

                    predicate = this.GetPredicate(predicate, inputFieldKey, searchValue);
                }
            }
            return query.Where(predicate).AsQueryable();
        }

        /// <summary>
        /// Build predicate for search data
        /// # SEARCH LOGIC SUB
        /// </summary>
        /// <param name="predicate"></param>
        /// <param name="inputFieldKey"></param>
        /// <param name="searchValue"></param>
        /// <returns></returns>
        public virtual ExpressionStarter<GenTemplateEntity> GetPredicate(ExpressionStarter<GenTemplateEntity> predicate, string inputFieldKey, string searchValue)
        {    
            searchValue = searchValue.Trim().ToLower();
            if (inputFieldKey == nameof(BaseGenTemplateResponseModel.Id))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            else if (inputFieldKey == nameof(BaseGenTemplateResponseModel.Order))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Order.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            /*[GEN-39]*/
            return predicate;
        }

        /// <summary>
        /// All attributes for search and filter and sort
        /// # SEARCH FIELDS
        /// # FILTER FIELDS
        /// # SORT FIELDS
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetFieldAttributes()
        {
            // key: field name, value: field type (string, long, int, float, double, byte, short, DateTime, bool)
            Dictionary<string, string> dic = new Dictionary<string, string>();
            /*[GEN-33]*/

            return dic;
        }

        /// <summary>
        /// Atribute for search all (search default)
        /// # SEARCH ALL FIELDS
        /// </summary>
        /// <returns></returns>
        public virtual Dictionary<string, string> GetFieldAttributesSearchAll()
        {
            Dictionary<string, string> dic = new Dictionary<string, string>();
            /*[GEN-33]*/

            return dic;
        }

        /// <summary>
        /// Get column for FE
        /// # SEARCHABLE
        /// # FILTERABLE
        /// # SORTABLE
        /// # DISPLAY ON LIST
        /// # ORDER BY ON LIST
        /// # ORDER BY ON FORM DATA
        /// </summary>
        /// <returns></returns>
        public virtual List<ColumnResponseModel> GetColumnAttribute()
        {
            List<ColumnResponseModel> columns = new List<ColumnResponseModel>();

            columns.Add(new ColumnResponseModel(
                key: nameof(BaseGenTemplateResponseModel.Id),
                displayName: nameof(BaseGenTemplateResponseModel.Id),
                filterable: false,
                searchable: false,
                sortable: false,
                displayable: false
                ));

            /*[GEN-38]*/

            return columns;
        }
    }
}
