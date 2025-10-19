# [Standard.AI.PeerLLM]

[![Build](https://github.com/The-Standard-Organization/Standard.AI.PeerLLM/actions/workflows/build.yml/badge.svg)](https://github.com/The-Standard-Organization/Standard.AI.PeerLLM/actions/workflows/build.yml)
[![The Standard](https://img.shields.io/github/v/release/hassanhabib/The-Standard?filter=v2.10.2&style=default&label=Standard%20Version&color=2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard Community](https://img.shields.io/discord/934130100008538142?color=%237289da&label=The%20Standard%20Community&logo=Discord)](https://discord.gg/vdPZ7hS52X)

## Introduction

Standard.AI.PeerLLM — A Standardized .NET library for PeerLLM, enabling developers to integrate AI-powered solutions into their .NET applications.


### Chats
The following example demonstrate how you can write your first Completions program.

#### Program.cs
```csharp
using System;
using System.Threading.Tasks;
using Standard.AI.PeerLLM.Models.Foundations.Chats;

namespace ExamplePeerLLMDotNet
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            var peerLLMConfiguration = new PeerLLMConfiguration();

            PeerLLMClient peerLLMClient =
                new PeerLLMClient(peerLLMConfiguration);

            ChatSessionConfig chatSessionConfig = new ChatSessionConfig
            {
                ModelName = "mistral-7b-instruct-v0.1.Q8_0",
            };

            Guid conversationId = await peerLLMClient.V1.Chats.StartChatAsync(chatSessionConfig);
            List<string> tokens = new List<string>();

            IAsyncEnumerable<string> responseStream = peerLLMClient.V1.Chats.StreamChatAsync(
                conversationId,
                text: "Hello, how are you?");

            await foreach (string token in responseStream)
            {
                tokens.Add(token);
            }

            Console.WriteLine(string.Concat(tokens));
        }
    }
}
```

## Standard-Compliance
This library was built according to The Standard. The library follows engineering principles, patterns and tooling as recommended by [The Standard](https://github.com/hassanhabib/The-Standard).

This library is also a community effort which involves many hours of pair-programming, test-driven development and in-depth exploration, research, and design discussions.

## Standard-Promise
The most important fulfillment aspect in a Standard complaint system is aimed towards contributing to people, its evolution, and principles.
An organization that systematically honors an environment of learning, training, and sharing of knowledge is an organization that learns from the past, makes calculated risks for the future, 
and brings everyone within it up to speed on the current state of things as honestly, rapidly, and efficiently as possible. 
 
We believe that everyone has the right to privacy, and will never do anything that could violate that right.
We are committed to writing ethical and responsible software, and will always strive to use our skills, coding, and systems for the good.
We believe that these beliefs will help to ensure that our software(s) are safe and secure and that it will never be used to harm or collect personal data for malicious purposes.
 
The Standard Community as a promise to you is in upholding these values.

## Contact Information

If you have any suggestions, comments or questions, please feel free to contact me on:

>[Twitter](https://twitter.com/hassanrezkhabib)
>
>[LinkedIn](https://www.linkedin.com/in/hassanrezkhabib/)
>
>[E-Mail](mailto:hassanhabib@live.com)

### Important Notice and Acknowledgements
A special thanks to all the community members, and the following dedicated engineers for their hard work and dedication to this project.
>Mr. Hassan Habib
>
>Mr. Christo du Toit
