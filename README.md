# FilmOS-microservices

This project is designed to acquire skills in developing microservices.

The project is incomplete and has bugs.

FilmOS-microservices is a microservices-based architecture project designed for managing various aspects of a movie platform, including favorite movies, ratings, shopping, and more. Each microservice is responsible for a specific functionality and communicates with others to provide a cohesive experience.

## Table of Contents

- [Architecture](#architecture)
- [Services](#services)
  - [Favorite Service](#favorite-service)
  - [Rating Service](#rating-service)
  - [Basket Service](#basket-service)
  - [Shopping Service](#shopping-service)
- [Technologies](#technologies)
- [Setup](#setup)
- [Usage](#usage)
- [License](#license)

## Architecture

FilmOS-microservices is designed using a microservices architecture, where each service is independently deployable and scalable. The services communicate with each other through asynchronous messaging using RabbitMQ. The architecture includes:

- **Identity Server**: Manages authentication and authorization.
- **Ocelot API Gateway**: Acts as a single entry point for all client requests, routing them to the appropriate microservice.
- **Elastic Stack**: Used for logging, monitoring, and analysis.

## Services

### Favorite Service

The Favorite Service allows users to manage their favorite movies.

#### Key Features:
- Add movies to favorites
- Remove movies from favorites
- List favorite movies

### Rating Service

The Rating Service allows users to rate movies.

#### Key Features:
- Add ratings to movies
- Update ratings
- Fetch movie ratings

### Basket Service

The Basket Service manages users' shopping baskets.

#### Key Features:
- Add items to basket
- Remove items from basket
- View basket contents

### Shopping Service

The Shopping Service handles the checkout process.

#### Key Features:
- Checkout items in the basket
- Process orders
- View order history

## Technologies

The project uses a variety of technologies:

- **ASP.NET Core**: Web framework used for building the services
- **MSSQL Server**: Database used for persistent storage
- **MongoDB**: NoSQL database used by the Favorite Service
- **Redis**: In-memory data structure store used for caching
- **RabbitMQ**: Messaging broker used for communication between services
- **Entity Framework Core**: ORM used for data access
- **Dapper**: Lightweight ORM used for data access in some services
- **Identity Server**: Provides authentication and authorization services
- **Ocelot API Gateway**: Manages routing, load balancing, and security for the microservices
- **Elastic Stack (Elasticsearch, Logstash, Kibana)**: Used for centralized logging, monitoring, and visualization

## Setup

### Prerequisites

- [.NET Core SDK](https://dotnet.microsoft.com/download)
- [Docker](https://www.docker.com/get-started) (optional, for containerization)
- [RabbitMQ](https://www.rabbitmq.com/download.html)
- [MSSQL Server](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
- [MongoDB](https://www.mongodb.com/try/download/community)
- [Redis](https://redis.io/download)
- [Elastic Stack](https://www.elastic.co/guide/en/elastic-stack-get-started/current/get-started-elastic-stack.html)

### Installation

1. Clone the repository:

    ```bash
    git clone https://github.com/Kolyanuss/FilmOS-microservices.git
    cd FilmOS-microservices
    ```

2. Set up the environment variables required by each service. Refer to the `.env.example` files in each service directory.

3. Build and run the services:

    ```bash
    dotnet build
    dotnet run --project [ServiceName]
    ```

4. (Optional) Run the services using Docker:

    ```bash
    docker-compose up --build
    ```

## Usage

Each service exposes its API endpoints. Refer to the individual service documentation for details on available endpoints and usage examples.

### Favorite Service

- Add a movie to favorites: `POST /api/favorites`
- Remove a movie from favorites: `DELETE /api/favorites/{movieId}`
- List favorite movies: `GET /api/favorites`

### Rating Service

- Add a rating: `POST /api/ratings`
- Update a rating: `PUT /api/ratings/{movieId}`
- Get movie ratings: `GET /api/ratings/{movieId}`

### Basket Service

- Add an item to the basket: `POST /api/basket`
- Remove an item from the basket: `DELETE /api/basket/{itemId}`
- View basket contents: `GET /api/basket`

### Shopping Service

- Checkout items: `POST /api/shopping/checkout`
- View order history: `GET /api/shopping/orders`

## License

This project is licensed under the MIT License. See the [LICENSE](LICENSE) file for details.
