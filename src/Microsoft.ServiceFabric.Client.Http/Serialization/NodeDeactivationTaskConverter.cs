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
    /// Converter for <see cref="NodeDeactivationTask" />.
    /// </summary>
    internal class NodeDeactivationTaskConverter
    {
        /// <summary>
        /// Deserializes the JSON representation of the object.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from.</param>
        /// <returns>The object Value.</returns>
        internal static NodeDeactivationTask Deserialize(JsonReader reader)
        {
            return reader.Deserialize(GetFromJsonProperties);
        }

        /// <summary>
        /// Gets the object from Json properties.
        /// </summary>
        /// <param name="reader">The <see cref="T: Newtonsoft.Json.JsonReader" /> to read from, reader must be placed at first property.</param>
        /// <returns>The object Value.</returns>
        internal static NodeDeactivationTask GetFromJsonProperties(JsonReader reader)
        {
            var nodeDeactivationTaskId = default(NodeDeactivationTaskId);
            var nodeDeactivationIntent = default(NodeDeactivationIntent?);

            do
            {
                var propName = reader.ReadPropertyName();
                if (string.Compare("NodeDeactivationTaskId", propName, StringComparison.Ordinal) == 0)
                {
                    nodeDeactivationTaskId = NodeDeactivationTaskIdConverter.Deserialize(reader);
                }
                else if (string.Compare("NodeDeactivationIntent", propName, StringComparison.Ordinal) == 0)
                {
                    nodeDeactivationIntent = NodeDeactivationIntentConverter.Deserialize(reader);
                }
                else
                {
                    reader.SkipPropertyValue();
                }
            }
            while (reader.TokenType != JsonToken.EndObject);

            return new NodeDeactivationTask(
                nodeDeactivationTaskId: nodeDeactivationTaskId,
                nodeDeactivationIntent: nodeDeactivationIntent);
        }

        /// <summary>
        /// Serializes the object to JSON.
        /// </summary>
        /// <param name="writer">The <see cref="T: Newtonsoft.Json.JsonWriter" /> to write to.</param>
        /// <param name="obj">The object to serialize to JSON.</param>
        internal static void Serialize(JsonWriter writer, NodeDeactivationTask obj)
        {
            // Required properties are always serialized, optional properties are serialized when not null.
            writer.WriteStartObject();
            writer.WriteProperty(obj.NodeDeactivationIntent, "NodeDeactivationIntent", NodeDeactivationIntentConverter.Serialize);
            if (obj.NodeDeactivationTaskId != null)
            {
                writer.WriteProperty(obj.NodeDeactivationTaskId, "NodeDeactivationTaskId", NodeDeactivationTaskIdConverter.Serialize);
            }

            writer.WriteEndObject();
        }
    }
}
