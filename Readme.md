## Project Overview

ClassApiMCPServer is a Model Context Protocol (MCP) server that exposes the Coso ClassCom API as MCP tools and prompts. It runs as an ASP.NET Core web application serving HTTP-based MCP endpoints (Streamable HTTP transport).

**Key Technologies:**
- .NET 10.0
- ModelContextProtocol packages (0.4.0-preview.1)
- Coso.ClassComNet.Sdk (2.0.9)
- ASP.NET Core with MCP integration

## Architecture

### MCP Server Structure

The server uses attribute-based registration to expose tools and prompts:

1. **Tools** (`McpTools.cs`): Static methods decorated with `[McpServerTool]` that directly invoke ClassApiClient methods
2. **Prompts** (`McpPrompts.cs`): Static methods decorated with `[McpServerPrompt]` that return ChatMessage objects with instructions
3. **Services** (`Services/`): Wrapper services around the SDK
    - `ClassApiClient`: Wraps IClassComClient SDK and applies default settings (e.g., JoinBeforeHost=true)
    - `TimeService`: Utility for Unix timestamp conversions

### Tool Categories

Tools are organized into functional groups:
- **Classes**: Create, get, remove classes
- **Enrollments**: Manage user enrollments in classes
- **Schedules**: Add/remove class dates
- **Launch**: Generate launch URLs for class sessions
- **Reporting**: Get attendance data
- **Users**: Full CRUD operations on users

### Configuration

Environment variables are loaded from `.env` file (not in repo):
- `ClassApiClientSettings:BaseAddress` - API base URL
- `ClassApiClientSettings:SecretKey` - Bearer token

Server runs on `http://localhost:5050` with endpoints:
- `/sse` - Server-sent events endpoint
- `/messages` - Message handling endpoint

## Development Commands

### Build and Run
```bash
dotnet build
dotnet run --project ClassApiMCPServer
```

### Build Only
```bash
dotnet build ClassApiMCPServer.sln
```

### Clean
```bash
dotnet clean
```

### Expose Server via ngrok

To make the local MCP server accessible from external clients (e.g., Claude Desktop, web clients):

1. **Install ngrok**: Download from https://ngrok.com/download or use package manager
2. **Run the server**: Start the server locally on port 5050
   ```bash
   dotnet run --project ClassApiMCPServer
   ```
3. **Start ngrok** (in a separate terminal):
   ```bash
   ngrok http 5050
   ```
4. **Use the ngrok URL**: Copy the HTTPS forwarding URL from ngrok output (e.g., `https://abc123.ngrok.io`)

MCP clients should connect to:
- SSE endpoint: `https://<ngrok-url>/sse`
- Messages endpoint: `https://<ngrok-url>/messages`

## Important Implementation Details

### Default Class Settings
When creating classes via `ClassApiClient.CreateClass()`, default settings are automatically applied:
- `JbhTime = 0`
- `WaitingRoom = false`
- `JoinBeforeHost = true`

### ID Types
Be careful with ID types across the API:
- **ClassId**: GUID for some operations, string for others (attendance)
- **ScheduleId**: string
- **UserId**: int for internal ID, string for external user ID
- **ExtClassId**: string for external class references

### Tool Discovery
Tools and prompts are auto-discovered via assembly scanning in `Program.cs`:
```csharp
.WithPromptsFromAssembly()
.WithResourcesFromAssembly()
.WithToolsFromAssembly()
```

Adding new tools requires only:
1. Add static method to `McpTools` class with `[McpServerTool]` attribute
2. Add Description attribute for tool documentation
3. Accept injected services as parameters (ClassApiClient, TimeService, etc.)
