// ----------------------------------------------------------------------------------
// Copyright (c) The Standard Organization: A coalition of the Good-Hearted Engineers
// ----------------------------------------------------------------------------------

using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace Standard.AI.PeerLLM.Models.Foundations.Chats
{
    /// <summary>
    /// Represents the request payload for starting a chat session with PeerLLM.
    /// This defines which model to use, the machines to target, the system role,
    /// and other initialization options such as anti-prompts and fallback behavior.
    /// </summary>
    public class ChatSessionConfigV2
    {
        /// <summary>
        /// Gets or sets the name of the model to use for this chat session.
        /// Example: "mistral-7b-instruct-v0.1.Q8_0".
        /// </summary>
        [JsonPropertyName("modelName")]
        public string ModelName { get; set; } = "mistral-7b-instruct-v0.1.Q8_0";

        /// <summary>
        /// Gets or sets the role under which the model should operate.
        /// Common values are "System", "User", or "Assistant".
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; } = "System";

        /// <summary>
        /// Gets or sets the initial content for the specified role.
        /// This can be used to prime the model with context such as:
        /// "You are a lawyer".
        /// </summary>
        [JsonPropertyName("roleContent")]
        public string RoleContent { get; set; }

        /// <summary>
        /// Optionally gets or sets the list of machine IDs that should be targeted
        /// when establishing this chat session.
        /// </summary>
        [JsonPropertyName("targetMachines")]
        public List<string>? TargetMachines { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the system should automatically
        /// fall back to the general pool of machines if the target machines are unavailable.
        /// </summary>
        [JsonPropertyName("fallBack")]
        public bool FallBack { get; set; } = true;

        /// <summary>
        /// Gets or sets a list of strings that the model should avoid producing
        /// in its output (anti-prompts).
        /// Example values: "User:", "System:", "Assistant:", "Note:".
        /// </summary>
        [JsonPropertyName("antiPrompts")]
        public List<string>? AntiPrompts { get; set; }
    }
}
