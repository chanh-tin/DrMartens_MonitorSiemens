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

namespace SourceBaseBE.Database.Models.ResponseModels
{
    public class BaseExample003ResponseModel : BaseCRUDResponseModel<Example003Entity>
    {
        public virtual string Name { get; set; }
        public virtual List<Example001Entity>? ListExample001 { get; set; }
        

        
                                
        public override object SetData(Example003Entity entity)
        {
            base.SetData(entity);
            this.Name = entity.Name;
            if (entity.ListExample001 != null)
            {
                this.ListExample001 = entity.ListExample001;
            }
        
            return this;
        }
        public override List<object> SetData(List<Example003Entity> listEntity)
        {
            List<Object> listRS = new List<object>();
            foreach (Example003Entity entity in listEntity)
            {
                listRS.Add(new Example003ResponseModel().SetData(entity));
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
        public virtual IQueryable<Example003Entity> PrepareWhereQueryFilter(
            IQueryable<Example003Entity> query,
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
        public virtual IQueryable<Example003Entity> PrepareQuerySort(
            IQueryable<Example003Entity> query,
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
        public virtual IQueryable<Example003Entity> PrepareWhereQuerySearch(
            IQueryable<Example003Entity> query,
            Dictionary<string, string> dicInputFieldKey2Value,
            Func<Dictionary<string, string>> getFieldAttributes,
            Func<Dictionary<string, string>> getFieldAttributesSearchAll,
            Type responseType)
        {
            var dicFieldAttr = getFieldAttributes();
            var dicFieldAttrSearchAll = getFieldAttributesSearchAll();
            var predicate = LinqKit.PredicateBuilder.New<Example003Entity>(true);
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
        public virtual ExpressionStarter<Example003Entity> GetPredicate(ExpressionStarter<Example003Entity> predicate, string inputFieldKey, string searchValue)
        {    
            searchValue = searchValue.Trim().ToLower();
            if (inputFieldKey == nameof(BaseExample003ResponseModel.Id))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Id.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            else if (inputFieldKey == nameof(BaseExample003ResponseModel.Order))
            {
                predicate = predicate.Or(x => EF.Functions.Unaccent(x.Order.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            if (inputFieldKey == nameof(Example003ResponseModel.Name))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.Name.ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
            }
            if (inputFieldKey == nameof(Example003ResponseModel.ListExample001))
            {
                predicate = predicate.Or(x => Microsoft.EntityFrameworkCore.EF.Functions.Unaccent(x.ListExample001.ToString().ToLower()).Contains(EF.Functions.Unaccent($"{searchValue}")));
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
            dic.Add(nameof(Example003ResponseModel.Name), typeof(string).ToString());
            dic.Add(nameof(Example003ResponseModel.ListExample001), typeof(string).ToString());
                                

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
            dic.Add(nameof(Example003ResponseModel.Name), typeof(string).ToString());
            dic.Add(nameof(Example003ResponseModel.ListExample001), typeof(string).ToString());
                                

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
                key: nameof(BaseExample003ResponseModel.Id),
                displayName: nameof(BaseExample003ResponseModel.Id),
                filterable: false,
                searchable: false,
                sortable: false,
                displayable: false
                ));

            columns.Add(new ColumnResponseModel(
                key: nameof(Example003ResponseModel.Name),
                displayName: nameof(Example003ResponseModel.Name),
                searchable: true, 
                filterable: false, 
                sortable: true, 
                displayable: true
                ));

            columns.Add(new ColumnResponseModel(
                key: nameof(Example003ResponseModel.ListExample001),
                displayName: nameof(Example003ResponseModel.ListExample001),
                searchable: true, 
                filterable: true, 
                sortable: true, 
                displayable: true
                ));
                                

            return columns;
        }
    }
}
