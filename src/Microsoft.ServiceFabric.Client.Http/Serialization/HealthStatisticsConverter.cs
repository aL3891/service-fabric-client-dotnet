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
    /// Converter for <see cref="HealthStatistics" />.
    /// </summary>
    internal class HealthStatisticsConverter
    {
        /// <summary>
        /// Deserializes the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <returns>The object Value.</returns>
        internal static HealthStatistics Deserialize(JsonReader reader)
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
        internal static HealthStatistics GetFromJsonProperties(JsonReader reader)
        {
            var healthStateCountList = default(IEnumerable<EntityKindHealthStateCount>);

            do
            {
                var propName = reader.ReadPropertyName();
                if (string.Compare("HealthStateCountList", propName, StringComparison.Ordinal) == 0)
                {
                    healthStateCountList = reader.ReadList(EntityKindHealthStateCountConverter.Deserialize);
                }
                else
                {
                    reader.SkipPropertyValue();
                }
            }
            while (reader.TokenType != JsonToken.EndObject);

            return new HealthStatistics(
                healthStateCountList: healthStateCountList);
        }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="T: Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="obj">The object to serialize to JSON.</param>
        internal static void Serialize(JsonWriter writer, HealthStatistics obj)
        {
            // Required properties are always serialized, optional properties are serialized when not null.
            writer.WriteStartObject();
            if (obj.HealthStateCountList != null)
            {
                writer.WriteEnumerableProperty(obj.HealthStateCountList, "HealthStateCountList", EntityKindHealthStateCountConverter.Serialize);
            }

            writer.WriteEndObject();
        }
    }
}