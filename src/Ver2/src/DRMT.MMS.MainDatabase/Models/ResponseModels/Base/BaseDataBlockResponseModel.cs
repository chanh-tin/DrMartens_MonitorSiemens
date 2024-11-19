// -----------------------------------------------------------------------------
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
    public class BaseDataBlockResponseModel : BaseCRUDResponseModel<DataBlockEntity>
    {
        public virtual string SerialCode { get; set; }

        public virtual string Name { get; set; }
        public virtual List<TagEntity>? ListTag { get; set; }
        public virtual List<PlcEntity>? ListPlc { get; set; }
        

        
                                
        public override object SetData(DataBlockEntity entity)
        {
            base.SetData(entity);
            this.Name = entity.Name;
            if (entity.ListTag != null)
            {
                this.ListTag = entity.ListTag;
            }
            if (entity.ListPlc != null)
            {
                this.ListPlc = entity.ListPlc;
            }
        
            return this;
        }
        public override List<object> SetData(List<DataBlockEntity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (DataBlockEntity entity in listEntity)
            {
                listRS.Add(new DataBlockResponseModel().SetData(entity));
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
        public virtual IQueryable<DataBlockEntity> PrepareWhereQueryFilter(
            IQueryable<DataBlockEntity> query,
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
        public virtual IQueryable<DataBlockEntity> PrepareQuerySort(
            IQueryable<DataBlockEntity> query,
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
        public virtual IQueryable<DataBlockEntity> PrepareWhereQuerySearch(
            IQueryable<DataBlockEntity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<DataBlockEntity>(true);
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
        public virtual ExpressionStarter<DataBlockEntity> GetPredicate(ExpressionStarter<DataBlockEntity> predicate, string inputFieldKey, string searchValue)
        {    
            searchValue = searchValue.Trim().ToLower();
            if (inputFieldKey == nameof(BaseDataBlockResponseModel.Id))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            else if (inputFieldKey == nameof(BaseDataBlockResponseModel.Order))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Order.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            if (inputFieldKey == nameof(DataBlockResponseModel.Name))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            if (inputFieldKey == nameof(DataBlockResponseModel.ListTag))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ListTag.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            if (inputFieldKey == nameof(DataBlockResponseModel.ListPlc))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ListPlc.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
                                
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
            dic.Add(nameof(DataBlockResponseModel.Name), typeof(string).ToString());
            dic.Add(nameof(DataBlockResponseModel.ListTag), typeof(string).ToString());
            dic.Add(nameof(DataBlockResponseModel.ListPlc), typeof(string).ToString());
                                

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
            dic.Add(nameof(DataBlockResponseModel.Name), typeof(string).ToString());
            dic.Add(nameof(DataBlockResponseModel.ListTag), typeof(string).ToString());
            dic.Add(nameof(DataBlockResponseModel.ListPlc), typeof(string).ToString());
                                

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
                key: nameof(BaseDataBlockResponseModel.Id),
                displayName: nameof(BaseDataBlockResponseModel.Id),
                filterable: false,
                searchable: false,
                sortable: false,
                displayable: false
                ));

            columns.Add(new ColumnResponseModel(
                key: nameof(DataBlockResponseModel.Name),
                displayName: nameof(DataBlockResponseModel.Name),
                searchable: true, 
                filterable: false, 
                sortable: true, 
                displayable: true
                ));

            columns.Add(new ColumnResponseModel(
                key: nameof(DataBlockResponseModel.ListTag),
                displayName: nameof(DataBlockResponseModel.ListTag),
                searchable: true, 
                filterable: true, 
                sortable: true, 
                displayable: true
                ));

            columns.Add(new ColumnResponseModel(
                key: nameof(DataBlockResponseModel.ListPlc),
                displayName: nameof(DataBlockResponseModel.ListPlc),
                searchable: true, 
                filterable: true, 
                sortable: true, 
                displayable: true
                ));
                                

            return columns;
        }
    }
}
