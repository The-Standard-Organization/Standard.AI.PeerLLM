# Standard.AI.PeerLLM

![Standard.AI.OpenAI](https://raw.githubusercontent.com/the-standard-organization/Standard.AI.PeerLLM/main/Standard.AI.PeerLLM/standard-ai-peerllm-cover-big.png)

[![Nuget](https://img.shields.io/nuget/v/Xeption)](https://www.nuget.org/packages/Standard.AI.PeerLLM)
[![Build](https://github.com/The-Standard-Organization/Standard.AI.PeerLLM/actions/workflows/build.yml/badge.svg)](https://github.com/The-Standard-Organization/Standard.AI.PeerLLM/actions/workflows/build.yml)
[![Nuget](https://img.shields.io/nuget/v/Xeption)](https://www.nuget.org/packages/Xeption/)
[![The Standard](https://img.shields.io/github/v/release/hassanhabib/The-Standard?filter=v2.10.2&style=default&label=Standard%20Version&color=2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard - COMPLIANT](https://img.shields.io/badge/The_Standard-COMPLIANT-2ea44f)](https://github.com/hassanhabib/The-Standard)
[![The Standard Community](https://img.shields.io/discord/934130100008538142?color=%237289da&label=The%20Standard%20Community&logo=Discord)](https://discord.gg/vdPZ7hS52X)

---

## 🧠 Introduction

**Standard.AI.PeerLLM** — A standardized .NET library for [PeerLLM](https://www.peerllm.com), enabling developers to integrate AI-powered and decentralized intelligence into their .NET applications.

This library follows **The Standard** design and architectural principles — ensuring ethical, maintainable, and testable AI-driven software systems that can be trusted, evolved, and reused across teams and organizations.

---

## 🌐 What is PeerLLM?

**PeerLLM** is a decentralized, community-powered AI network where compute, data, and models are contributed by individuals (“hosts”).  
Consumers of AI services — such as inference or fine-tuning — can access that distributed capacity securely and transparently.

Unlike traditional centralized AI providers, PeerLLM distributes ownership, computation, and benefit — allowing everyone to participate in the AI economy.

**Key Pillars:**
- ⚖️ **Fairness & Transparency** — Governance and accounting built into the network.  
- 🧩 **Open Collaboration** — Compute, data, and models shared through standardized APIs.  
- 💡 **Democratized Access** — AI for everyone, not just a few centralized providers.  

---

## 🚀 Why .NET Developers Should Build on PeerLLM

PeerLLM gives .NET developers the power to build AI-driven applications that are **private**, **distributed**, and **community-powered** — all without relying on a single centralized provider.

By integrating your .NET app with the PeerLLM network, you can:

- **🖥️ Run Anywhere** — Deploy on Windows, Linux, or macOS using familiar .NET tooling while connecting seamlessly to a global AI host network.  
- **🔒 Stay Private** — Keep user data and chat history client-side or within your own infrastructure, with no centralized data collection.  
- **⚡ Scale Efficiently** — Tap into distributed compute provided by community hosts instead of managing expensive cloud clusters.  
- **🤖 Extend with AI** — Call into local or remote models (like Mistral or LLaMA) through PeerLLM’s simple APIs for inference, fine-tuning, or orchestration.  
- **💰 Contribute and Earn** — Turn your .NET service or desktop app into a PeerLLM Host and earn tokens by serving AI workloads.

> 🧩 **Build smarter, faster, and more independently — with .NET on PeerLLM, your code becomes part of a decentralized AI ecosystem designed for fairness, transparency, and innovation.**

---

## 🧪 Quick Start — Your First PeerLLM Chat

Here’s how you can start your first chat completion program in .NET:

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
            var peerLLMConfiguration =
                new PeerLLMConfiguration();
            
            var peerLLMClient = 
                new PeerLLMClient(peerLLMConfiguration);
            
            var chatSessionConfig = new ChatSessionConfig
            {
                ModelName = "mistral-7b-instruct-v0.1.Q8_0",
            };
            
            Guid conversationId = 
                await peerLLMClient.V1.Chats
                    .StartChatAsync(chatSessionConfig);
            
            IAsyncEnumerable<string> responseStream =
                peerLLMClient.V1.Chats.StreamChatAsync(
                    conversationId,
                    text: "Hello, how are you?");
            
            await foreach (string token in responseStream)
            {
                Console.Write(token);
            }
            
            Console.WriteLine("Chat session ended.");
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
