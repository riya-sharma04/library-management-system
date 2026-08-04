# 📚 Library Management System

A web-based Library Management System developed using **ASP.NET, C#, HTML, CSS, JavaScript, and MySQL**.

This project is designed to manage library operations digitally, including books, students, teachers, book issue and return records, and fine collection.

---

## ✨ Features

* 🔐 Secure Login System
* 👤 Create New User
* 🔄 Switch User
* 📚 Book Management

  * Add books
  * Update book details
  * Delete books
  * Search books
  * Track book availability
* 🎓 Student Management

  * Add and manage student records
  * Search student records
* 👨‍🏫 Teacher Management

  * Add and manage teacher records
  * Search teacher records
* 📖 Book Issue Management

  * Issue books to students
  * Issue books to teachers
  * Track issued books
* 🔄 Book Return Management

  * Return issued books
  * Calculate applicable fines
* 💰 Fine Collection

  * Track collected fines
* 📊 Book Status Tracking

  * Issued books
  * Returned books
  * Available books
* 🔎 Search functionality across library records
* 🖥️ User-friendly web interface

---

## 🛠️ Technologies Used

| Technology            | Purpose                   |
| --------------------- | ------------------------- |
| **C#**                | Backend programming       |
| **ASP.NET Web Forms** | Web application framework |
| **HTML**              | Page structure            |
| **CSS**               | Styling and responsive UI |
| **JavaScript**        | Client-side functionality |
| **MySQL**             | Database management       |
| **Visual Studio**     | Development environment   |

---

## 📂 Main Modules

### 🔐 User Authentication

* Login
* Create New User
* Switch User

### 📚 Book Management

* Book Details
* Search Books
* Book Status

### 🎓 Student Management

* Student Details
* Search Students
* Student Book Issue

### 👨‍🏫 Teacher Management

* Teacher Details
* Search Teachers
* Teacher Book Issue

### 📖 Issue & Return

* Issued Book Details
* Return Book
* Fine Collection

---

## 🗄️ Database

The application uses **MySQL** for storing and managing library data.

The project uses a local database connection through `Web.config`.

For security, the actual `Web.config` file containing database credentials is **not included in this repository**.

A sample configuration is provided:

`Web.config.example`

Replace the placeholder values in `Web.config.example` with your own local MySQL credentials before running the application.

---

## ⚙️ Setup & Installation

### 1. Clone the repository

```bash
git clone https://github.com/riya-sharma04/library-management-system.git
```

### 2. Open the project

Open the project in **Visual Studio**.

### 3. Configure MySQL

Create the required MySQL database and tables.

Then create your local `Web.config` using:

`Web.config.example`

Replace the placeholder values with your own local MySQL username and password.

### 4. Run the application

Build and run the project through Visual Studio.

---

## 🔐 Security

Sensitive database credentials are intentionally excluded from this repository.

Do not upload `Web.config` containing real database credentials.

Use `Web.config.example` as the configuration template.

---

## 📁 Project Structure

```text
Library-Management-System/
│
├── *.aspx
├── *.aspx.cs
├── *.css
├── *.js
├── Bin/
├── images/
├── .gitignore
├── Web.Debug.config
└── Web.config.example
```

---

## 👩‍💻 Author

**Riya Sharma**

BCA Student & Web Developer
