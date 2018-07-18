using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using Elar.Framework.Core.Extensions;
using Icona.Logic.Helpers;
using Newtonsoft.Json;

namespace Icona.Logic.UI
{
    /// <summary>
    /// Класс (Singleton) для обработки и форматированного хранения значений cookied контролов
    /// </summary>
    public sealed class CookiedControlsValueManager
    {
        #region Приватные поля

        /// <summary>
        /// Приватная копия экземпляра
        /// </summary>
        private static CookiedControlsValueManager instance = null;

        /// <summary>
        /// объект для блокировки доступа
        /// </summary>
        private static readonly object locker = new object();

        #endregion

        #region Конструктор

        /// <summary>
        /// Приватный конструктор
        /// </summary>
        CookiedControlsValueManager()
        {
        }

        #endregion

        #region Singltone

        /// <summary>
        /// Singltone - свойство для получения потоко-безопастного экземпляра объекта
        /// </summary>
        public static CookiedControlsValueManager Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (locker)
                    {
                        if (instance == null)
                        {
                            instance = new CookiedControlsValueManager();
                        }
                    }
                }

                return instance;
            }
        }

        #endregion

        #region Методы

        /// <summary>
        /// Получение из кэша контент кука
        /// </summary>
        /// <param name="userControl">Контрол</param>
        private string GetCachedItem(UserControl userControl)
        {
            string pageName = userControl.Request.Url.AbsolutePath.Replace("/", "_");
          
            if (HttpContext.Current.Items[pageName] == null)
                HttpContext.Current.Items[pageName] =
                    !string.IsNullOrEmpty(userControl.Request.Cookies[pageName]?.Value)
                        ? CompressionHelper.Unzip(
                            System.Convert.FromBase64String(userControl.Request.Cookies[pageName]?.Value))
                        : string.Empty;

            return HttpContext.Current.Items[pageName]?.ToString();
        }

        /// <summary>
        /// Сохранение значения контрола в cookies
        /// </summary>
        /// <param name="userControl">Контрол</param>
        /// <param name="key">Ключ (если контролу необходимо хранить несколько значений)</param>
        /// <param name="value">Значение, которое необходимо сохранить</param>
        public void SaveValue(UserControl userControl, string key, string value)
        {
            if (userControl == null) return;

            lock (locker)
            {
                string pageName = userControl.Request.Url.AbsolutePath.Replace("/", "_");
                string keyName = $"{userControl.ClientID.ToMD5()}_{key}";
                string cookieContent = GetCachedItem(userControl);

                CookiedControlsGroup groupValue = (string.IsNullOrEmpty(cookieContent))
                    ? new CookiedControlsGroup()
                    : JsonConvert.DeserializeObject<CookiedControlsGroup>(cookieContent);

                if (!string.IsNullOrEmpty(value))
                {
                    if (!groupValue.KeyValues.ContainsKey(keyName))
                        groupValue.KeyValues.Add(keyName, value);
                    else
                        groupValue.KeyValues[keyName] = value;
                }
                else
                {
                    if (groupValue.KeyValues.ContainsKey(keyName))
                        groupValue.KeyValues.Remove(keyName);
                }
             
                HttpCookie cookie = new HttpCookie(pageName,
                    System.Convert.ToBase64String(CompressionHelper.Zip(JsonConvert.SerializeObject(groupValue))))
                {
                    Expires = DateTime.Now.AddDays(30 * (groupValue.HasData ? 1 : -1))
                };
           
                HttpContext.Current.Items[pageName] = JsonConvert.SerializeObject(groupValue);

                if (userControl.Response.Cookies[pageName] == null)
                    userControl.Response.Cookies.Add(cookie);
                else
                    userControl.Response.Cookies.Set(cookie);


            }
        }

        /// <summary>
        /// Сохранение значения контрола в cookies
        /// </summary>
        /// <param name="userControl">Контрол</param>
        /// <param name="value">Значение, которое необходимо сохранить</param>
        public void SaveValue(UserControl userControl, string value)
        {
            SaveValue(userControl, string.Empty, value);
        }


        /// <summary>
        /// Чтение значения контрола из cookies
        /// </summary>
        /// <param name="userControl">Контрол</param>
        /// <param name="key">Ключ (если контролу необходимо хранить несколько значений)</param>
        public string GetValue(UserControl userControl, string key)
        {
            if(userControl == null) return string.Empty;

            lock (locker)
            {
                string pageName = userControl.Request.Url.AbsolutePath.Replace("/", "_");
                string keyName = $"{userControl.ClientID.ToMD5()}_{key}";
                HttpCookie cookie = userControl.Request.Cookies[pageName];
             
                CookiedControlsGroup groupValue = (string.IsNullOrEmpty(cookie?.Value))
                    ? new CookiedControlsGroup()
                    : JsonConvert.DeserializeObject<CookiedControlsGroup>(
                        CompressionHelper.Unzip(System.Convert.FromBase64String(cookie?.Value)));
             
                if (groupValue.KeyValues.ContainsKey(keyName))
                    return groupValue.KeyValues.FirstOrDefault(i => i.Key == keyName).Value;
            }

            return string.Empty;
        }

        /// <summary>
        /// Чтение значения контрола из cookies
        /// </summary>
        /// <param name="userControl">Контрол</param>
        public string GetValue(UserControl userControl)
        {
            return GetValue(userControl, string.Empty);
        }

        #endregion
    }

    /// <summary>
    /// Класс для храния сериализованных значений в куках клиента
    /// </summary>
    public sealed class CookiedControlsGroup
    {
        /// <summary>
        /// Есть ли данные в группе куков
        /// </summary>
        public bool HasData => KeyValues.Count > 0;

        /// <summary>
        /// Коллекция Ключ-Значение для хранения значений cookied control
        /// </summary>
        public Dictionary<string, string> KeyValues { get; set; }

        /// <summary>
        /// Публичный конструктор, необходим для сериализвации объекта
        /// </summary>
        public CookiedControlsGroup()
        {
            KeyValues = new Dictionary<string, string>() { };
        }
    }
}