Vet Life

Vet Life is a C# application designed to help veterinarians track pet vaccinations efficiently. The app ensures that all vaccination records are easily accessible and up to date.

Features

🐶 Manage pets and their medical history

💉 Track vaccinations with administered dates and next dose date

🏥 Record past medical operations

👤 Associate pets with their owners

Main Models

🦴 Pet: Stores pet details, medical history, past operations, and vaccine records.

👨‍👩‍👧 Owner: A pet owner who can have multiple pets.

💊 Vaccine: Contains vaccine details.

Database Relationships

Each Owner can have multiple Pets, but each Pet belongs to only one Owner.

Each Pet can have multiple Vaccines, ensuring comprehensive vaccine tracking.

Purpose

The primary goal of Vet Life is to simplify pet vaccination tracking, ensuring pets receive timely vaccinations and proper medical care.

Technologies Used

🖥 C#

🗄 Entity Framework

🌐 ASP.NET Core 

🛢 SQL Server 

Setup Instructions

Clone the repository

Configure the database connection

Run migrations if using Entity Framework

Start the application

Future Enhancements :

⏰ Implement reminders for upcoming vaccinations

🎨 Improve UI/UX for better user interaction

📜 Expand medical history tracking
