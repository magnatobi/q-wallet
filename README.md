
```markdown
# Q Wallet

A simple wallet application implementing CQRS pattern and Event Sourcing.

## Description

Q Wallet is designed to demonstrate the use of CQRS (Command Query Responsibility Segregation) and Event Sourcing in a straightforward wallet application. The project leverages modern development practices to ensure a clean and maintainable codebase.

## Getting Started

Follow these instructions to get a copy of the project up and running on your local machine.

### Prerequisites

- **Docker Desktop:** Ensure you have Docker Desktop installed on your machine. You can download it from [here](https://www.docker.com/products/docker-desktop).

### Installation

1. **Clone the repository:**
   ```bash
   git clone https://github.com/magnatobi/q-wallet.git
   cd q-wallet
   ```

2. **Run Docker:**
   Make sure Docker Desktop is running on your machine. You can check the Docker status by running:
   ```bash
   docker --version
   ```

3. **Build and start the Docker containers:**
   In the root directory of the project, execute the following command to build and start the containers:
   ```bash
   docker-compose up --build
   ```

### Usage

Once the Docker containers are up and running, you can navigate to the Swagger endpoint to explore the API.

1. **Navigate to Swagger:**
   Open your web browser and go to:
   ```
   http://localhost:8080/swagger/index.html
   ```

This will bring up the Swagger UI where you can interact with the Q Wallet API endpoints.

## Built With

- **Docker:** For containerization
- **CQRS:** Command Query Responsibility Segregation
- **Event Sourcing:** Event-driven architecture

## Contributing

If you would like to contribute to this project, please fork the repository and use a feature branch. Pull requests are warmly welcome.

## License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.

## Acknowledgments

- Inspired by modern software architecture principles.
```

