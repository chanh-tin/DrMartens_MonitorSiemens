using iSoft.Database.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UserEntity = SourceBaseBE.Database.Entities.UserEntity;

namespace SourceBaseBE.Database.Repository
{
    public static class UserRepositoryExtension
    {
        public static BaseCRUDEntity FillTrackingUser(this UserRepository userRepository, BaseCRUDEntity entity)
        {
            if (entity == null)
            {
                return entity;
            }
            string createdUserName = "";
            string updatedUserName = "";
            List<UserEntity> listUser = userRepository.GetListWithNoInclude();
            Dictionary<long, UserEntity> dicEntity = listUser.ToDictionary(x => x.Id);
            if (entity.CreatedBy != null && dicEntity.ContainsKey(entity.CreatedBy.Value))
            {
                createdUserName = dicEntity[entity.CreatedBy.Value].Username;
            }
            if (entity.UpdatedBy != null && dicEntity.ContainsKey(entity.UpdatedBy.Value))
            {
                updatedUserName = dicEntity[entity.UpdatedBy.Value].Username;
            }
            entity.CreatedUsername = createdUserName;
            entity.UpdatedUsername = updatedUserName;
            return entity;
        }
        public static List<BaseCRUDEntity> FillTrackingUser(this UserRepository userRepository, List<BaseCRUDEntity> listEntity)
        {
            if (listEntity == null || listEntity.Count <= 0)
            {
                return listEntity;
            }
            string createdUserName = "";
            string updatedUserName = "";
            List<UserEntity> listUser = userRepository.GetListWithNoInclude();
            Dictionary<long, UserEntity> dicEntity = listUser.ToDictionary(x => x.Id);
            for (int i = 0; i < listEntity.Count; i++)
            {
                var entity = listEntity[i];
                createdUserName = "";
                updatedUserName = "";
                if (entity.CreatedBy != null && dicEntity.ContainsKey(entity.CreatedBy.Value))
                {
                    createdUserName = dicEntity[entity.CreatedBy.Value].Username;
                }
                if (entity.UpdatedBy != null && dicEntity.ContainsKey(entity.UpdatedBy.Value))
                {
                    updatedUserName = dicEntity[entity.UpdatedBy.Value].Username;
                }
                listEntity[i].CreatedUsername = createdUserName;
                listEntity[i].UpdatedUsername = updatedUserName;
            }
            return listEntity;
        }
    }
}
