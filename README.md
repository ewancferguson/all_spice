# 🍽️ All Spice

All Spice is a fullstack web application designed for users to create, share, and save their favorite recipes. Built with a **.NET backend** and **MySQL database**, this app provides a seamless experience for managing and discovering delicious recipes.

## 🚀 Features
- 📝 **Create Recipes** – Users can add and customize their own recipes.
- 🔍 **Explore Recipes** – Browse through a collection of user-submitted recipes.
- 📌 **Save Favorites** – Bookmark favorite recipes for easy access later.
- 🔒 **User Authentication** – Secure login and account management.
- 🛠 **CRUD Functionality** – Full Create, Read, Update, and Delete operations.

## 🏗️ Tech Stack
- **Backend:** .NET Core, MySQL
- **Frontend:** Vue.js (Planned Future Enhancement)
- **Authentication:** JWT-based authentication
- **ORM:** Entity Framework Core
- **Database:** MySQL

## 🔧 Setup & Installation
1. **Clone the repository**
   ```sh
   git clone https://github.com/ewancferguson/all_spice.git
   cd all_spice
   ```
2. **Set up the database**
   - Ensure MySQL is installed and running.
   - Create a new database named `allspice_db`.
   - Run migrations using Entity Framework:
     ```sh
     dotnet ef database update
     ```
3. **Configure environment variables**
   - Create an `appsettings.json` file and add your database connection string.

4. **Run the application**
   ```sh
   dotnet run
   ```

## 📜 API Endpoints
| Method | Endpoint            | Description                |
|--------|--------------------|----------------------------|
| GET    | `/api/recipes`     | Fetch all recipes         |
| POST   | `/api/recipes`     | Create a new recipe       |
| GET    | `/api/recipes/{id}` | Get a specific recipe    |
| PUT    | `/api/recipes/{id}` | Update a recipe          |
| DELETE | `/api/recipes/{id}` | Delete a recipe          |

## 📫 Contact
- **GitHub:** [Ewan Ferguson](https://github.com/ewancferguson)
- **Portfolio:** [My Work](https://ewanferguson.org/)
- **Email:** ewanferg01@gmail.com

---
🔹 *Contributions and feedback are welcome! Feel free to fork and improve the project.*
