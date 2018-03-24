// ------------------------------------------------------------
// Copyright (c) Microsoft Corporation.  All rights reserved.
// Licensed under the MIT License (MIT). See License.txt in the repo root for license information.
// ------------------------------------------------------------

namespace Microsoft.ServiceFabric.Client.Http
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Net.Http;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using Microsoft.ServiceFabric.Client;
    using Microsoft.ServiceFabric.Client.Http.Serialization;
    using Microsoft.ServiceFabric.Common;
    using Newtonsoft.Json;

    /// <summary>
    /// Class containing methods for performing CodePackageClient operataions.
    /// </summary>
    internal partial class CodePackageClient : ICodePackageClient
    {
        private readonly ServiceFabricHttpClient httpClient;

        /// <summary>
        /// Initializes a new instance of the CodePackageClient class.
        /// </summary>
        /// <param name="httpClient">ServiceFabricHttpClient instance.</param>
        public CodePackageClient(ServiceFabricHttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        /// <inheritdoc />
        public Task<IEnumerable<DeployedCodePackageInfo>> GetDeployedCodePackageInfoListAsync(
            NodeName nodeName,
            ApplicationName applicationName,
            string serviceManifestName = default(string),
            string codePackageName = default(string),
            long? serverTimeout = 60,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            nodeName.ThrowIfNull(nameof(nodeName));
            applicationName.ThrowIfNull(nameof(applicationName));
            serverTimeout?.ThrowIfOutOfInclusiveRange("serverTimeout", 1, 4294967295);
            var requestId = Guid.NewGuid().ToString();
            var url = "Nodes/{nodeName}/$/GetApplications/{applicationId}/$/GetCodePackages";
            url = url.Replace("{nodeName}", Uri.EscapeDataString(nodeName.ToString().ToString()));
            url = url.Replace("{applicationId}", applicationName.GetId().ToString());
            var queryParams = new List<string>();
            
            // Append to queryParams if not null.
            serviceManifestName?.AddToQueryParameters(queryParams, $"ServiceManifestName={serviceManifestName}");
            codePackageName?.AddToQueryParameters(queryParams, $"CodePackageName={codePackageName}");
            serverTimeout?.AddToQueryParameters(queryParams, $"timeout={serverTimeout}");
            queryParams.Add("api-version=6.0");
            url += "?" + string.Join("&", queryParams);
            
            HttpRequestMessage RequestFunc()
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                };
                return request;
            }

            return this.httpClient.SendAsyncGetResponseAsList(RequestFunc, url, DeployedCodePackageInfoConverter.Deserialize, requestId, cancellationToken);
        }

        /// <inheritdoc />
        public Task RestartDeployedCodePackageAsync(
            NodeName nodeName,
            ApplicationName applicationName,
            RestartDeployedCodePackageDescription restartDeployedCodePackageDescription,
            long? serverTimeout = 60,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            nodeName.ThrowIfNull(nameof(nodeName));
            applicationName.ThrowIfNull(nameof(applicationName));
            restartDeployedCodePackageDescription.ThrowIfNull(nameof(restartDeployedCodePackageDescription));
            serverTimeout?.ThrowIfOutOfInclusiveRange("serverTimeout", 1, 4294967295);
            var requestId = Guid.NewGuid().ToString();
            var url = "Nodes/{nodeName}/$/GetApplications/{applicationId}/$/GetCodePackages/$/Restart";
            url = url.Replace("{nodeName}", Uri.EscapeDataString(nodeName.ToString().ToString()));
            url = url.Replace("{applicationId}", applicationName.GetId().ToString());
            var queryParams = new List<string>();
            
            // Append to queryParams if not null.
            serverTimeout?.AddToQueryParameters(queryParams, $"timeout={serverTimeout}");
            queryParams.Add("api-version=6.0");
            url += "?" + string.Join("&", queryParams);
            
            string content;
            using (var sw = new StringWriter())
            {
                RestartDeployedCodePackageDescriptionConverter.Serialize(new JsonTextWriter(sw), restartDeployedCodePackageDescription);
                content = sw.ToString();
            }

            HttpRequestMessage RequestFunc()
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Post,
                    Content = new StringContent(content, Encoding.UTF8)
                };
                request.Content.Headers.ContentType = System.Net.Http.Headers.MediaTypeHeaderValue.Parse("application/json; charset=utf-8");
                return request;
            }

            return this.httpClient.SendAsync(RequestFunc, url, requestId, cancellationToken);
        }

        /// <inheritdoc />
        public Task<ContainerLogs> GetContainerLogsDeployedOnNodeAsync(
            NodeName nodeName,
            ApplicationName applicationName,
            string serviceManifestName,
            string codePackageName,
            string tail = default(string),
            long? serverTimeout = 60,
            CancellationToken cancellationToken = default(CancellationToken))
        {
            nodeName.ThrowIfNull(nameof(nodeName));
            applicationName.ThrowIfNull(nameof(applicationName));
            serviceManifestName.ThrowIfNull(nameof(serviceManifestName));
            codePackageName.ThrowIfNull(nameof(codePackageName));
            serverTimeout?.ThrowIfOutOfInclusiveRange("serverTimeout", 1, 4294967295);
            var requestId = Guid.NewGuid().ToString();
            var url = "Nodes/{nodeName}/$/GetApplications/{applicationId}/$/GetCodePackages/$/ContainerLogs";
            url = url.Replace("{nodeName}", Uri.EscapeDataString(nodeName.ToString().ToString()));
            url = url.Replace("{applicationId}", applicationName.GetId().ToString());
            var queryParams = new List<string>();
            
            // Append to queryParams if not null.
            serviceManifestName?.AddToQueryParameters(queryParams, $"ServiceManifestName={serviceManifestName}");
            codePackageName?.AddToQueryParameters(queryParams, $"CodePackageName={codePackageName}");
            tail?.AddToQueryParameters(queryParams, $"Tail={tail}");
            serverTimeout?.AddToQueryParameters(queryParams, $"timeout={serverTimeout}");
            queryParams.Add("api-version=6.1");
            url += "?" + string.Join("&", queryParams);
            
            HttpRequestMessage RequestFunc()
            {
                var request = new HttpRequestMessage()
                {
                    Method = HttpMethod.Get,
                };
                return request;
            }

            return this.httpClient.SendAsyncGetResponse(RequestFunc, url, ContainerLogsConverter.Deserialize, requestId, cancellationToken);
        }
    }
}