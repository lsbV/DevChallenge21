# DevChallenge21

## Solution Summary

### Project Overview

This project is designed to assist the Ministry of Foreign Affairs of Ukraine (MFA of Ukraine) in processing and analyzing a large volume of telephone conversations. The goal is to create a structured dataset from audio recordings of these conversations, extracting key information such as the caller's name, location, emotional tone, and relevant categories.

### Instructions for Running the Service

1. **Clone the Repository**: Download the project files.

2. **Build and Run with Docker in root directory**:
   ```bash
   docker-compose up --build
	```

### Access the API

The service will be available at `http://localhost:8080/api`.

**API Endpoints**:
- `POST http://localhost:8080/api/call`: Create a new call with an audio file URL.
- `POST http://localhost:8080/api/call/AudioInBody`: Create a new call with an audio file provided in the request body (as form-data).
- `GET http://localhost:8080/api/call/{id}`: Retrieve details of a call by its unique identifier.

You can use tools like Postman or cURL to interact with the API. There is AudioExample.m4a file in the root directory that can be used for testing.

### Key Features

1. **Category Management**:
   - CRUD Operations: Create, Read, Update, and Delete conversation topics.

2. **Call Processing**:
   - Audio File Handling: Supports audio file URLs in `.wav` and `.mp3` formats.
   - Transcription and Analysis: Downloads, transcribes, and analyzes the audio content to extract names, locations, emotional tone, and categorize the conversation.

3. **API Endpoints**:
   - `POST /api/call`: Creates a new call based on the provided audio file URL.
   - `POST /api/call/AudioInBody`: Creates a new call with an audio file provided in the request body (as form-data).
   - `GET /api/call/{id}`: Retrieves details of a call by its unique identifier.

### Technical Details

- **Technologies**: .NET 8, C# 12.0, Entity Framework Core, AutoMapper, ASP.NET Core, Docker, Python, Ollama.
- **Database**: In-memory database for development and testing; MS SQL Server for production.
- **Dependency Injection**: Utilizes .NET's built-in dependency injection for service management.
- **Logging**: Integrated logging for tracking errors and processing steps.

### Project Structure

- **AppBuilder**: Contains the service registration and configuration.
- **CallComponent**: Handles the core logic for processing and analyzing calls.
- **Database**: Manages the database context and entity configurations.
- **MainServer**: Hosts the API controllers and endpoints.
- **TranscriptionServer**: Hosts the Python server for audio transcription using the Whisper model.
- **AnalysisComponent**: Contains the logic for extracting names, locations, and emotional tone from the transcribed text using the Llama model.
- **CategoryComponent**: Manages the conversation categories and their CRUD operations.
- **Core**: Defines the core models and exceptions used across the project.

### Key Files

- `AppDbContext.cs`: Configures the database context and entity relationships.
- `CallController.cs`: Defines the API endpoints for call processing.
- `Builder.cs`: Registers services and configures dependencies.
- `docker-compose.yml`: Defines the Docker setup for running the application.



### Next Steps for Improvement

- **Enhance Test Coverage**: Increase unit and integration tests to cover more edge cases.
- **Optimize Performance**: Fine-tune the transcription and analysis processes for better performance.
- **Expand Functionality**: Add more detailed analysis and categorization features.

[REQUIREMENTS](https://github.com/lsbV/DevChallenge21/Requirements.txt)