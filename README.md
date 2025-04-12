# Point of Sale (POS) System – Retail Management Software  
A secure and easy-to-use POS system developed in **C#** using **Windows Forms** and **Microsoft Access**. Designed for small retail businesses to manage stock, transactions, user roles, and legal compliance with a simple and intuitive interface.

## Features  
- Role-based login system (Owner, Manager, Employee)  
- Real-time transaction processing with receipt generation  
- Inventory and stock management with low-stock alerts  
- Age-restricted product checks  
- Persistent order history and sales tracking  
- Secure and reliable Microsoft Access database integration  

---

## 🖥️ Setting up Visual Studio  
1. Download the **latest version** of Visual Studio Community Edition:  
   👉 [https://visualstudio.microsoft.com/vs/community/](https://visualstudio.microsoft.com/vs/community/)  
2. Run the Visual Studio Installer and install it with the **recommended settings**.  
3. Ensure that the **.NET desktop development** workload is included.

---

## 📁 Setting up the Application  
1. Download the project as a **ZIP file** and extract it.  
2. Open the solution file: `Till System.sln` using Visual Studio.  
3. Make sure the `System Information.accdb` file (Microsoft Access database) is placed in the correct location as expected by the app.

> ❗ The Access file contains all stock, user, and transaction data required to run the system.

---

## 🚀 Running the Program  
1. Launch the program from Visual Studio (`Start` button or F5).  
2. Use the **Sign Up** screen to create a new account with the appropriate role.  
3. Log in with your credentials to begin using the system.

---

## 📄 System Output  
- After each successful transaction, a **text file receipt** is automatically generated and saved to the system directory.  
- This file includes a timestamp, items sold, and total payment received, enabling consistent record-keeping.
