using System.IO;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using MinecraftNotifier.Lib.Exceptions;
using MinecraftNotifier.Lib.Models;
using Newtonsoft.Json;

namespace MinecraftNotifier.Lib.Core
{
    /// <summary>
    /// Class representing WebRequest
    /// </summary>
    public class WebRequest
    {
        /// <summary>
        /// Deserialize Json From Stream
        /// </summary>
        /// <typeparam name="T">the type</typeparam>
        /// <param name="stream">the stream</param>
        /// <returns>the type</returns>
        private T DeserializeJsonFromStream<T>(Stream stream)
        {
            if (stream == null || stream.CanRead == false)
                return default(T);

            using (var sr = new StreamReader(stream))
            using (var jtr = new JsonTextReader(sr))
            {
                var js = new JsonSerializer();
                var searchResult = js.Deserialize<T>(jtr);
                return searchResult;
            }
        }

        /// <summary>
        /// Stream to string
        /// </summary>
        /// <param name="stream">the Stream</param>
        /// <returns>String</returns>
        private async Task<string> StreamToStringAsync(Stream stream)
        {
            string content = null;

            if (stream != null)
                using (var sr = new StreamReader(stream))
                    content = await sr.ReadToEndAsync();

            return content;
        }

        /// <summary>
        /// Deserialize Stream
        /// </summary>
        /// <param name="cancellationToken">the cancellation token</param>
        /// <returns>Taskresult for MinecraftJson</returns>
        public async Task<MinecraftJson> DeserializeStreamAsync(CancellationToken cancellationToken)
        {
            using (var client = new HttpClient())
            using (var request = new HttpRequestMessage(HttpMethod.Get, "https://launchermeta.mojang.com/mc/game/version_manifest.json"))
            using (var response = await client.SendAsync(request, HttpCompletionOption.ResponseHeadersRead, cancellationToken))
            {
                var stream = await response.Content.ReadAsStreamAsync();

                if (response.IsSuccessStatusCode)
                    return DeserializeJsonFromStream<MinecraftJson>(stream);

                var content = await StreamToStringAsync(stream);
                throw new ApiException
                {
                    StatusCode = (int)response.StatusCode,
                    Content = content
                };
            }
        }
    }
}
