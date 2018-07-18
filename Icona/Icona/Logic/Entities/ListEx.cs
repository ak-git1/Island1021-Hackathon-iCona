using System;
using System.Collections.Generic;
using System.Data;
using System.Xml;
using System.Xml.Schema;
using System.Xml.Serialization;
using Elar.Framework.Core.Extensions;
using Elar.Framework.Core.Helpers;

namespace Icona.Logic.Entities
{
    /// <summary>
    /// Список предоставляющий общее количество записей
    /// </summary>
    /// <typeparam name="T">Тип сущности</typeparam>
    [Serializable]
    public class ListEx<T> : List<T>, IXmlSerializable
    {
        #region Переменные и константы

        /// <summary>
        /// Нужно ли использовать свой алгоритм сериализации
        /// </summary>
        private static readonly bool UseCustomSerialization = typeof(T).IsInterface;

        #endregion

        #region Методы

        #region Основной функционал

        /// <summary>
        /// Конструктор
        /// </summary>
        public ListEx()
        {
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        public ListEx(int totalRecords)
        {
            TotalRecords = totalRecords;
        }

        /// <summary>
        /// Конструктор
        /// </summary>
        /// <param name="reader">Данные</param>
        /// <param name="constructor">Конструктор для сущности</param>
        public ListEx(IDataReader reader, Func<IDataReader, T> constructor)
        {
            while (reader.Read())
            {
                TotalRecords = reader["TotalRecords"].ToInt32();
                Add(constructor(reader));
            }
        }

        /// <summary>
        /// Общее количество записей
        /// </summary>
        public int TotalRecords { get; private set; }
        
        #endregion

        #region Сериализация

        /// <summary>
        /// Сериализовать в xml
        /// </summary>
        /// <param name="writer">Писалка xml</param>
        public void WriteXml(XmlWriter writer)
        {
            if (UseCustomSerialization) 
                WriteXmlCustomImplementation(writer);
            else
                WriteXmlNativeImplementation(writer);
        }

        /// <summary>
        /// Десериализовать из xml
        /// </summary>
        /// <param name="reader">Читалка xml</param>
        public void ReadXml(XmlReader reader)
        {
            if (UseCustomSerialization) 
                ReadXmlCustomImplementation(reader);
            else 
                ReadXmlNativeImplementation(reader);
        }

        /// <summary>
        /// Получить схему xml
        /// </summary>
        public XmlSchema GetSchema() { return (null); }
        
        /// <summary>
        /// Сериализовать объект с использованием нативного алгоритма
        /// </summary>
        /// <param name="writer">Писалка xml</param>
        private void WriteXmlNativeImplementation(XmlWriter writer)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            SerializeFields(writer);
            serializer.Serialize(writer, this);
        }

        /// <summary>
        /// Десериализовать объект с использованием нативного алгоритма
        /// </summary>
        /// <param name="reader">Читалка xml</param>
        private void ReadXmlNativeImplementation(XmlReader reader)
        {
            DeserializeFields(reader);
            reader.Read();
            XmlSerializer serializer = new XmlSerializer(typeof(List<T>));
            List<T> data = serializer.Deserialize(reader) as List<T>;
            if (data != null) 
                AddRange(data);
        }
        
        /// <summary>
        /// Сериализовать объект с использованием своего алгоритма сериализации
        /// </summary>
        /// <param name="writer">Писалка xml</param>
        private void WriteXmlCustomImplementation(XmlWriter writer)
        {
            SerializeFields(writer);
            ListSerializationHelper.WriteXml(this, writer);
        }

        /// <summary>
        /// Десериализовать объект с использованием своего алгоритма десериализации
        /// </summary>
        /// <param name="reader">Читалка xml</param>
        private void ReadXmlCustomImplementation(XmlReader reader)
        {
            DeserializeFields(reader);
            ListSerializationHelper.ReadXml(this, reader);
        }

        /// <summary>
        /// Десериализовать поля объекта
        /// </summary>
        /// <param name="reader">Читалка xml</param>
        private void DeserializeFields(XmlReader reader)
        {
            TotalRecords = reader.GetAttribute("TotalRecords").ToInt32();
        }

        /// <summary>
        /// Сериализовать поля объекта
        /// </summary>
        /// <param name="writer">Писалка xml</param>
        private void SerializeFields(XmlWriter writer)
        {
            writer.WriteAttributeString("TotalRecords", TotalRecords.ToStr());
        }

        #endregion

        #endregion
    }
}
