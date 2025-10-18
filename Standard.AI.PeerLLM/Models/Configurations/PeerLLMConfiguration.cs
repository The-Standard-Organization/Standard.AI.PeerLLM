// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

namespace Standard.AI.PeerLLM.Models.Configurations
{
    /// <summary>
    /// Represents the configuration settings required to connect and authenticate
    /// with the PeerLLM API, including the API endpoint, authentication key,
    /// and organization identifier.
    /// </summary>
    public class PeerLLMConfiguration
    {
        /// <summary>
        /// Gets or sets the base URL of the PeerLLM API.
        /// Defaults to "https://api.peerllm.com".
        /// </summary>
        public string ApiUrl { get; set; } = "https://api.peerllm.com";

        /// <summary>
        /// Gets or sets the API key used for authenticating requests to the PeerLLM API.
        /// </summary>
        public string ApiKey { get; set; }
    }
}