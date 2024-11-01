# Email OTP Module

This repository provides a simple module for generating and sending One-Time Passwords (OTP) via email. It includes the ability to configure email settings, send OTPs to users, and validate user-provided OTPs with timeout handling.

## Features

- Generate OTP and send via email.
- Configurable SMTP settings for email sending.
- Support for OTP validation with multiple attempts and timeout.
- Modular design for easy integration into existing projects.
- Unit tests using XUnit and FakeItEasy.

## Prerequisites

- .NET 6.0 or later
- SMTP server credentials for sending emails

## Step-by-Step Instructions

1. **Clone the Repository**
    ```bash
    git clone https://github.com/calvinsim2/email-otp-module.git
    cd email-otp-module

2. **Configure Email Settings (appsettings.json)**
    ```bash
    {
      "smtp": {
        "host": "smtp.your-email-provider.com",
        "port": 587,
        "username": "your-email@example.com",
        "password": "your-email-password"
      }
    }


3. **Build the Project**
    ```bash
    dotnet build

5. **Run the Application**
    ```bash
    dotnet run


## Helpful Links
- [How to Send Emails Using .NET Core, MailKit, and Google’s SMTP Server](https://medium.com/@abhinandkr56/how-to-send-emails-using-net-core-mailkit-and-googles-smtp-server-6521827c4198)

## Contributors

Calvin Sim
