
//        public NoteRepository<BaseCRUDEntity> GetList2(long id)
//        {
//            try
//            {
//                ISQLBuilder query = BaseSQLBuilder.GetSQLBuilderInstance(this._context.dbConnectionCustom.GetDBConfig().DatabaseType)
//                  .New()
//                  .Selects("\"Example001s\".*")
//                  .From("Example001s")
//                  .LeftJoin("Example001s", "refExample001GenTemplate", "Id", "Example001Id")
//                  .InnerJoin("refExample001GenTemplate", "GenTemplates", "GenTemplateId", "Id")
//                  .Where(new FieldName("Example001s", "Id"), id)
//                  ;

//                object[] parameters = null;
//                var sqlRaw = query.GetSQLRaw(ref parameters);
//                List<Example001Entity> result = this._context.Set<Example001Entity>().FromSqlRaw(sqlRaw, parameters).ToList();

//                return result;
//            }
//            catch (Exception ex)
//            {
//                throw new DBException(ex);
//            }
//        }


//public List<Example001Entity> GetList2(long id)
//{
//    var list = _repositoryImp.GetList2(id);
//    var entityRS = _userRepository.FillTrackingUser(list.Cast<BaseCRUDEntity>().ToList()).Cast<Example001Entity>().ToList();
//    return entityRS;
//}