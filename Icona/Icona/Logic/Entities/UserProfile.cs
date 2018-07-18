using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web.Security;
using Elar.Framework.Core.Extensions;
using Icona.Logic.DAL;
using Icona.Logic.Entities.Security;
using Icona.Logic.Filters;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Анкета пользователя 
    /// </summary>
    public class UserProfile : BaseEntity
    {
        #region Переменные и константы

        /// <summary>
        /// Пользователь ASP.NET
        /// </summary>
        private MembershipUser _membershipUser;

        /// <summary>
        /// Собственные роли пользователя
        /// </summary>
        private List<ExRole> _ownRoles;

        #endregion

        #region Свойства

        /// <summary>
        /// Идентификатор пользователя
        /// </summary>
        public Guid UserId { get; set; }

        /// <summary>
        /// Имя пользователя
        /// </summary>
        public string FirstName { get; set; }

        /// <summary>
        /// Отчество пользователя
        /// </summary>
        public string MiddleName { get; set; }

        /// <summary>
        /// Фамилия пользователя
        /// </summary>
        public string LastName { get; set; }

        /// <summary>
        /// ФИО
        /// </summary>
        public string FIO => $"{LastName} {FirstName} {MiddleName}";
        
        /// <summary>
        /// Пользователь ASP.NET
        /// </summary>
        public MembershipUser MembershipUser => _membershipUser ?? (_membershipUser = Membership.GetUser(UserId));

        /// <summary>
        /// Логин
        /// </summary>
        public string Login => MembershipUser == null ? string.Empty : MembershipUser.UserName;

        /// <summary>
        /// Идентификатор пола
        /// </summary>
        public int? GenderId { get; set; }

        /// <summary>
        /// Ссылка для написания почты
        /// </summary>
        public string MailTo => string.Format("mailto:{0}", Email);

        /// <summary>
        /// Адрес электронной почты
        /// </summary>
        public string Email => MembershipUser == null ? string.Empty : MembershipUser.Email;

        /// <summary>
        /// Признак того, что пользователь находится в онлайне
        /// </summary>
        public bool IsOnline => MembershipUser == null ? false : MembershipUser.IsOnline;

        /// <summary>
        /// Признак того, что заблокирован
        /// </summary>
        public bool Blocked { get; set; }

        /// <summary>
        /// Признак того, что удален
        /// </summary>
        public new bool Deleted { get; set; }

        /// <summary>
        /// Перичисление человекопонятных названий ролей через ";"
        /// </summary>
        public string Roles
        {
            get { return string.Join("; ", OwnRoles.Select(x => x.Description)); }
        }

        /// <summary>
        /// Собственные роли
        /// </summary>
        public List<ExRole> OwnRoles
        {
            get { return _ownRoles = _ownRoles ?? DAL.Security.GetOwnRolesForUser(Login); }
        }

        #endregion

        #region Конструктор

        /// <summary>
        /// Конструктор
        /// </summary>
        public UserProfile()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="dr">Данные</param>
        internal UserProfile(IDataReader dr) : base(dr)
        {
            UserId = dr["UserId"].ToGuid();
            FirstName = dr["FirstName"].ToString();
            LastName = dr["LastName"].ToString();
            MiddleName = dr["MiddleName"].ToString();
            GenderId = dr["GenderId"].ToInt32(null);
            Blocked = dr["Blocked"].ToBoolean();
            Deleted = dr["Deleted"].ToBoolean();
        }

        #endregion

        #region Публичные методы

        /// <summary>
        /// Добавление пользователя
        /// </summary>
        /// <param name="profile">Профиль пользователя</param>
        /// <returns></returns>
        public static int Add(UserProfile profile)
        {
            return UserProfiles.Add(profile);
        }

        /// <summary>
        /// Обновление профиля
        /// </summary>
        public void Update()
        {
            UserProfiles.Update(this);
        }

        /// <summary>
        /// Получение профиля
        /// </summary>
        /// <param name="id">Идентификатор профиля</param>
        /// <returns></returns>
        public static UserProfile Get(int id)
        {
            return UserProfiles.Get(id);
        }

        /// <summary>
        /// Блокировка профиля
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, выполнившего действие</param>
        public void Block(int? userId)
        {
            Blocked = true;
            Update();
        }

        /// <summary>
        /// Разблокировка профиля
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, выполнившего действие</param>
        public void Unblock(int? userId)
        {
            Blocked = false;
            Update();
        }

        /// <summary>
        /// Удаление профиля
        /// </summary>
        /// <param name="userId">Идентификатор пользователя, выполнившего действие</param>
        public void Delete(int? userId)
        {
            UserProfiles.Delete(Id);
        }
        
        /// <summary>
        /// Получить профиль по уникальному идентификатору пользователя
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        public static UserProfile GetByUserId(Guid userId)
        {
            return UserProfiles.GetByUserId(userId);
        }

        /// <summary>
        /// Получение списка анкет пользователей
        /// </summary>
        /// <param name="f">Фильтр</param>
        /// <returns>Список анкет пользователей</returns>
        public static ListEx<UserProfile> GetList(UsersRegistryFilter f)
        {
            return UserProfiles.GetList(f);
        }

        /// <summary>
        /// Проверка существования пользователя по идентификатору
        /// </summary>
        /// <param name="userId">Идентификатор пользователя</param>
        /// <returns>true - существует, false - не существует</returns>
        public static bool Exists(Guid userId)
        {
            return UserProfiles.Exists(userId);
        }

        #endregion
    }
}
