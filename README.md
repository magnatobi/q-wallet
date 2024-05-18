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

2. **Create User Account Endpoint:**
   To create a new user account, use the following endpoint and payload:

   - **Endpoint:**
     ```
     POST /q-wallet/api/v1/user-accounts/create
     ```
   - **Payload:**
     ```json
     {
       "firstName": "string",
       "lastName": "string",
       "placeOfBirth": "string",
       "dateOfBirth": "2024-05-18T11:24:25.933Z",
       "nationality": "string",
       "sex": "string"
     }
     ```
   - **Example Payload:**
     ```json
     {
       "firstName": "Mark",
       "lastName": "Twain",
       "placeOfBirth": "Lagos",
       "dateOfBirth": "2024-05-18T11:31:52.276Z",
       "nationality": "Nigerian",
       "sex": "Male"
     }
     ```

3. **Create Bank Account Endpoint:**
   To create a new bank account, use the following endpoint and payload:

   - **Endpoint:**
     ```
     POST /q-wallet/api/v1/bank-accounts/create
     ```
   - **Payload:**
     ```json
     {
       "accountTypeId": 0,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```
     - `accountTypeId` can be either `1` (Savings) or `2` (Current)
   - **Example Payload:**
     ```json
     {
       "accountTypeId": 1,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```

4. **Deposit / Credit Endpoint:**
   To deposit money into a bank account, use the following endpoint and payload:

   - **Endpoint:**
     ```
     POST /q-wallet/api/v1/bank-accounts/deposit
     ```
   - **Payload:**
     ```json
     {
       "accountNumber": 0,
       "depositAmount": 0,
       "accountTypeId": 0,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```
   - **Example Payload:**
     ```json
     {
       "accountNumber": 12345678,
       "depositAmount": 1000,
       "accountTypeId": 1,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```

5. **Withdrawal / Debit Endpoint:**
   To withdraw money from a bank account, use the following endpoint and payload:

   - **Endpoint:**
     ```
     POST /q-wallet/api/v1/bank-accounts/withdraw
     ```
   - **Payload:**
     ```json
     {
       "accountNumber": 0,
       "withdrawalAmount": 0,
       "accountTypeId": 0,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```
   - **Example Payload:**
     ```json
     {
       "accountNumber": 12345678,
       "withdrawalAmount": 500,
       "accountTypeId": 1,
       "userId": "3fa85f64-5717-4562-b3fc-2c963f66afa6"
     }
     ```

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

This README now includes detailed explanations for creating user accounts, bank accounts, depositing, and withdrawing money, along with example payloads for each action.