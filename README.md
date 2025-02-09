# 🌐 AES-CHAT: Secure Chat Application in C#

[![GitHub license](https://img.shields.io/badge/license-MIT-blue.svg)](https://github.com/yourusername/AES-CHAT/blob/main/LICENSE)
[![.NET](https://img.shields.io/badge/.NET-Core-brightgreen.svg)](https://dotnet.microsoft.com/)
[![Platform](https://img.shields.io/badge/platform-Windows%20%7C%20Linux%20%7C%20macOS-lightgrey.svg)](https://github.com/yourusername/AES-CHAT)

## About the Project

**AES-CHAT** is a simple client-server chat application written in C# that uses AES encryption to ensure secure message transmission. The application allows users to establish encrypted communication channels between clients and a server.

---

## Components

### Server

The server component listens for incoming connections from clients and broadcasts messages to all connected clients. It acts as the central hub for communication.

By default, the server listens on port `5000`. You can change this in the source code if needed.

---

### Client

The client component connects to the server, sends encrypted messages, and receives messages from other clients via the server.

#### Running the Client

You can run the client in two ways:

1. **Interactive Mode (Manual Input)**:
   If no command-line arguments are provided, the client will prompt you to enter the server address, username, and password manually.

2. **Command-Line Arguments**:
   You can provide the server address, username, and password as command-line arguments to avoid manual input.

   Usage:
   ```bash
   AES-CHAT.exe [server_address] [username] [password]
   ```

   Example:
   ```bash
   AES-CHAT.exe 127.0.0.1:5000 JohnDoe secret_password
   ```

   - `[server_address]`: The IP and port of the server (format: `IP:PORT`).
   - `[username]`: Your desired username.
   - `[password]`: The shared password for server communication.

---

## Features

- **End-to-End Encryption**: All messages are encrypted using AES, ensuring privacy and security.
- **Cross-Platform**: Works on Windows, Linux, and macOS with .NET Core or .NET 6+.
- **Easy Setup**: Simple configuration for both server and client.
- **Real-Time Communication**: Instant messaging between connected clients.

---

## Getting Started

### Prerequisites

- [.NET 6 SDK](https://dotnet.microsoft.com/download) or higher.

---

## Contributing

Contributions are welcome! If you find any issues or have suggestions for improvements, please open an issue or submit a pull request.


---

> May the force be with You!