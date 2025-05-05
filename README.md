# 📝 Habit Tracker Console App

Developed to practice core programming concepts, ADO.NET, and basic SQL with SQLite.

This is a console-based CRUD application to track habit progress over time. It was developed using C# and SQLite, without any ORMs, to reinforce understanding of raw SQL interaction.

---

## 📋 Given Requirements

- On startup, the app should create a SQLite database if it doesn't already exist.
- It must also create the necessary table(s) for storing habit data.
- Users should be able to:
  - Insert new habits
  - View all records
  - Update existing entries
  - Delete specific records
- All errors must be handled gracefully to avoid crashing the app.
- The app runs continuously until the user opts to exit.
- All interactions with the database must be done using **raw SQL only**, no Entity Framework or other mappers.

---

## 🔧 Features

### 📂 SQLite Database Connection

- A local `.sqlite` database is created automatically on first run.
- Required tables are created if not already present.

### 🧭 Console-Based UI

- Menu-driven navigation using number inputs.
- Simple text prompts guide the user through each operation.

### ✏️ CRUD Operations

- Create, read, update, and delete records for habits.
- Dates are inputted in `yyyy-MM-dd` format and validated before processing.

### 📊 Habit Logging

- Users can track progress by logging a quantity value for a habit on a specific date.

---

## 📚 Lessons Learned

- Structuring console apps in a clean way with a proper separation of concerns.
- Always sanitize and validate input before passing it to your queries.
- Understanding the trade-offs between building features fast vs clean and maintainable.
- Basic use of ADO.NET helped build a foundation before moving to more abstracted tools like EF Core.

---

## 🚀 Future Improvements

- Add data filtering (e.g., show logs for a given week or habit only).
- Improve reporting (e.g., most consistent habits, streak tracking).
- Introduce basic unit testing for core methods.
- Potential migration to Entity Framework for easier scaling (after learning raw SQL properly).

---

## 🛠 Tech Stack

- **Language:** C# (.NET Console)
- **Database:** SQLite
- **Data Access:** ADO.NET (raw SQL)
- **UI:** Console Interface
- **Serilog** For application logging

---



