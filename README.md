# Splitify
Splitify is a link builder and redirection service for A/B testing. It's designed with a client-server architecture with micro services on the backend and SPA on the frontend. On the server side, it adopts the Domain-Driven Design (DDD) approach featuring rich domain models and a structured layered architecture. The client side is a single page application crafted using Angular 16.

#### Key Features:
* Generate a single URL for two distinct links.
* Equitably redirect traffic to one of the versions when the generated URL is accessed.

## Architecture & Technologies
![splitify](https://github.com/alex-selehenenko/splitify/blob/main/Assets/architecture.png)
### Backend
- .Net 6
- ASP .NET Web API
- EntityFramework
- PostgreSQL
- Docker
- Azure
- RabbitMQ
- MassTransit
- MediatR
- NUnit

### Frontend
- HTML & CSS
- Angular
- TypeScript
