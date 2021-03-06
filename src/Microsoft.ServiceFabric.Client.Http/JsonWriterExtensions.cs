﻿// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Client.Http
{
    using System;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Xml;
    using Newtonsoft.Json;

    /// <summary>
    /// Defines extension methods for JsonWriter.
    /// </summary>
    internal static class JsonWriterExtensions
    {
        /// <summary>
        /// Writes a DateTime value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteDateTimeValue(this JsonWriter writer, DateTime? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                // write in ISO8601 foramt.
                writer.WriteValue(XmlConvert.ToString(value.Value, XmlDateTimeSerializationMode.Utc));
            }
        }

        /// <summary>
        /// Writes a TimeSpan value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteTimeSpanValue(this JsonWriter writer, TimeSpan? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                // write in ISO8601 foramt.
                writer.WriteValue(XmlConvert.ToString(value.Value));
            }
        }

        /// <summary>
        /// Writes a string value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteStringValue(this JsonWriter writer, string value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes integer value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteIntValue(this JsonWriter writer, int? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes long value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteLongValue(this JsonWriter writer, long? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes double value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteDoubleValue(this JsonWriter writer, double? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes boolean value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteBoolValue(this JsonWriter writer, bool? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value);
            }
        }

        /// <summary>
        /// Writes Guid value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteGuidValue(this JsonWriter writer, Guid? value)
        {
            if (value == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteValue(value.ToString());
            }
        }

        /// <summary>
        /// Writes byte value.
        /// </summary>
        /// <param name="writer">Json writer.</param>
        /// <param name="value">Value to write.</param>
        public static void WriteByteValue(this JsonWriter writer, byte value)
        {
            // byte is int in json.
            writer.WriteValue(value);
        }

        /// <summary>
        /// Writes property.
        /// </summary>
        /// <typeparam name="T">Type of property to write.</typeparam>
        /// <param name="writer">JsonWriter instance.</param>
        /// <param name="obj">object of type T to write.</param>
        /// <param name="propertyName">Property NameDescription to write as.</param>
        /// <param name="serializeFunc">Func to serialize obj of type T.</param>
        public static void WriteProperty<T>(this JsonWriter writer, T obj, string propertyName, Action<JsonWriter, T> serializeFunc)
        {
            writer.WritePropertyName(propertyName);
            if (obj == null)
            {
                writer.WriteNull();
            }
            else
            {
                serializeFunc.Invoke(writer, obj);
            }
        }

        /// <summary>
        /// Writes IEnumerable property as json array
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable elements.</typeparam>
        /// <param name="writer">JsonWriter instance.</param>
        /// <param name="sequence">IEnumerable to write.</param>
        /// <param name="propertyName">Property NameDescription to write as.</param>
        /// <param name="serializeFunc">Func to serialize obj of type T.</param>
        public static void WriteEnumerableProperty<T>(this JsonWriter writer, IEnumerable<T> sequence, string propertyName, Action<JsonWriter, T> serializeFunc)
        {
            writer.WritePropertyName(propertyName);
            if (sequence == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartArray();

                foreach (var item in sequence)
                {
                    if (item == null)
                    {
                        writer.WriteNull();
                    }
                    else
                    {
                        serializeFunc.Invoke(writer, item);
                    }
                }

                writer.WriteEndArray();
            }
        }

        /// <summary>
        /// Writes properties of IDictionary.
        /// </summary>
        /// <typeparam name="T">Type of dictionary values.</typeparam>
        /// <param name="writer">JsonWriter instance.</param>
        /// <param name="collection">Dictionary object to write.</param>
        /// <param name="propertyName">Property NameDescription to write as.</param>
        /// <param name="serializeFunc">Func to serialize obj of type T.</param>
        public static void WriteDictionaryProperty<T>(this JsonWriter writer, IReadOnlyDictionary<string, T> collection, string propertyName, Action<JsonWriter, T> serializeFunc)
        {
            writer.WritePropertyName(propertyName);
            if (collection == null)
            {
                writer.WriteNull();
            }
            else
            {
                writer.WriteStartObject();

                foreach (var item in collection)
                {
                    writer.WritePropertyName(item.Key);
                    if (item.Value == null)
                    {
                        writer.WriteNull();
                    }
                    else
                    {
                        serializeFunc.Invoke(writer, item.Value);
                    }
                }

                writer.WriteEndObject();
            }
        }
    }
}
