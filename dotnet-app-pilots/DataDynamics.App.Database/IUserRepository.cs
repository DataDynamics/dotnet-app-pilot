﻿using SmartSql.DyRepository;
using SmartSql.DyRepository.Annotations;

namespace DataDynamics.App.Database;

public interface IUserRepository : IRepository<User, long>
{
    new long Insert(User entity);

    IEnumerable<T> Query<T>(object reqParams);

    IEnumerable<T> QueryByPage<T>(object reqParams);

    [Statement(Sql = "Select Top 1 T.* From T_User T Where T.Id=@id")]
    User GetById_RealSql(long id);

    int InsertExtendData(UserExtendData extendData);

    int UpdateExtendData(UserExtendData extendData);

    UserExtendData GetExtendData([Param("UserId")] long userId);

    (TUser, UserExtendData) GetUserInfo<TUser>([Param("UserId")] long userId);

    T QueryByPage_M<T>(object reqParams);
}