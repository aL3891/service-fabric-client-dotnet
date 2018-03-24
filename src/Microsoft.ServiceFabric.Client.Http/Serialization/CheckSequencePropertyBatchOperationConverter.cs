// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Client.Http.Serialization
{
    using System;
    using System.Collections.Generic;
    using Microsoft.ServiceFabric.Common;
    using Newtonsoft.Json;
    using Newtonsoft.Json.Linq;

    /// <summary>
    /// Converter for <see cref="CheckSequencePropertyBatchOperation" />.
    /// </summary>
    internal class CheckSequencePropertyBatchOperationConverter
    {
        /// <summary>
        /// Deserializes the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <returns>The object Value.</returns>
        internal static CheckSequencePropertyBatchOperation Deserialize(JsonReader reader)
        {
            reader.ReadStartObject();
            var obj = GetFromJsonProperties(reader);
            reader.ReadEndObject();
            return obj;
        }

        /// <summary>
        /// Gets the object from Json properties.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from, reader must be placed at first property.</param>
        /// <returns>The object Value.</returns>
        internal static CheckSequencePropertyBatchOperation GetFromJsonProperties(JsonReader reader)
        {
            var propertyName = default(string);
            var sequenceNumber = default(string);

            do
            {
                var propName = reader.ReadPropertyName();
                if (string.Compare("PropertyName", propName, StringComparison.Ordinal) == 0)
                {
                    propertyName = reader.ReadValueAsString();
                }
                else if (string.Compare("SequenceNumber", propName, StringComparison.Ordinal) == 0)
                {
                    sequenceNumber = reader.ReadValueAsString();
                }
                else
                {
                    reader.SkipPropertyValue();
                }
            }
            while (reader.TokenType != JsonToken.EndObject);

            return new CheckSequencePropertyBatchOperation(
                propertyName: propertyName,
                sequenceNumber: sequenceNumber);
        }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="T: Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="obj">The object to serialize to JSON.</param>
        internal static void Serialize(JsonWriter writer, CheckSequencePropertyBatchOperation obj)
        {
            // Required properties are always serialized, optional properties are serialized when not null.
            writer.WriteStartObject();
            writer.WriteProperty(obj.Kind.ToString(), "Kind", JsonWriterExtensions.WriteStringValue);
            writer.WriteProperty(obj.PropertyName, "PropertyName", JsonWriterExtensions.WriteStringValue);
            writer.WriteProperty(obj.SequenceNumber, "SequenceNumber", JsonWriterExtensions.WriteStringValue);
            writer.WriteEndObject();
        }
    }
}