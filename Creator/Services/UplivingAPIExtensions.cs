// <auto-generated>
// Code generated by Microsoft (R) AutoRest Code Generator.
// Changes may cause incorrect behavior and will be lost if the code is
// regenerated.
// </auto-generated>

namespace Creator.Services
{
    using Models;
    using System.Collections;
    using System.Collections.Generic;
    using System.Threading;
    using System.Threading.Tasks;

    /// <summary>
    /// Extension methods for UplivingAPI.
    /// </summary>
    public static partial class UplivingAPIExtensions
    {
            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            public static object Hour1400Upload(this IUplivingAPI operations, Hour1400UploadRequest request = default(Hour1400UploadRequest))
            {
                return operations.Hour1400UploadAsync(request).GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='request'>
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<object> Hour1400UploadAsync(this IUplivingAPI operations, Hour1400UploadRequest request = default(Hour1400UploadRequest), CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.Hour1400UploadWithHttpMessagesAsync(request, null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            public static IList<WeatherForecast> WeatherForecasts(this IUplivingAPI operations)
            {
                return operations.WeatherForecastsAsync().GetAwaiter().GetResult();
            }

            /// <param name='operations'>
            /// The operations group for this extension method.
            /// </param>
            /// <param name='cancellationToken'>
            /// The cancellation token.
            /// </param>
            public static async Task<IList<WeatherForecast>> WeatherForecastsAsync(this IUplivingAPI operations, CancellationToken cancellationToken = default(CancellationToken))
            {
                using (var _result = await operations.WeatherForecastsWithHttpMessagesAsync(null, cancellationToken).ConfigureAwait(false))
                {
                    return _result.Body;
                }
            }

    }
}