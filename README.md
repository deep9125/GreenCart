# 🛒 GreenCart: A Homemade Products Marketplace

<p>GreenCart is a complete e-commerce web application that bridges the gap between local artisans and customers who value authentic, homemade goods. It automates the selling and purchasing workflow, enabling distinct user roles, a persistent database-driven shopping cart, and transparent order status tracking.</p>

## Project Overview
GreenCart streamlines the process of buying and selling homemade products through two primary user roles:
* **Seller** – Can list products, manage their inventory, and view and update the status of incoming orders for their items.
* **Buyer** – Can browse the marketplace, add items to a persistent cart, check out, and track their order history.

The system is built from the ground up using ASP.NET Core MVC and the Repository Pattern, ensuring a clean separation of concerns and a scalable architecture.

## ✨ Features Implemented
### 🔐 Authentication & Core System
* **Custom Authentication:** Secure, session-based registration and login for both Buyers and Sellers.
* **Role-Based UI:** The entire user interface, from navigation links to action buttons, dynamically changes based on the logged-in user's role.
* **Repository Pattern:** All data access is abstracted through repositories, keeping controllers clean and business logic centralized.
* **ViewModels & AutoMapper:** Secure and efficient data transfer between the backend and frontend views.

### 🧑‍🌾 Seller Module
* **Private Seller Dashboard:** A dedicated and secure area for sellers to manage their store.
* **Full Product Management (CRUD):** Sellers have complete Create, Read, Update, and Delete capabilities for their own product listings.
* **Inventory Management:** Sellers set and track stock quantity. The system prevents overselling at checkout and displays stock status to buyers.
* **Order Fulfillment:** Sellers view a list of "Incoming Orders" containing their products and can update the status of each individual item (e.g., from `Pending` to `Shipped`).

### 🛍️ Buyer Module
* **Role-Aware Marketplace:** A clean, shopping-focused gallery of all products from all sellers, with action buttons hidden from sellers.
* **Persistent Shopping Cart:** A database-driven cart that saves a user's items, allowing them to log out and return later with their cart intact.
* **Quantity Management:** Users can specify the quantity of an item they wish to add to their cart. The system validates this against available stock.
* **Complete Checkout Workflow:** A simple process to enter a shipping address and convert the cart into a permanent order.
* **Order History:** A private dashboard for buyers to view their past and current orders and track the status of each item.

<h2>🚀 Setup Instructions (.NET Core 3.1.1)</h2>
Ensure the following are installed:
<ul>
  <li>.NET Core SDK 3.1.1 or later</li>
  <li>Visual Studio</li>
  <li>Git</li>
</ul>


<h4>Steps to run the project:</h4>
<ol>
  <li><h4>Clone the repository:</h4>
    Run the following command in your terminal:
    <br>git clone https://github.com/deep9125/GreenCart.git</br>
    <br>cd GreenCart</br>
  </li>
<li><h4>Configure Database Connection</h4>
  <b><u>Open appsettings.json and modify your SQL Server connection string:</u></b><br>
    "ConnectionStrings": {<br>
  "MyDb": "Server=(localdb)\\mssqllocaldb;Database=GreenCartDB;Trusted_Connection=True;MultipleActiveResultSets=true;TrustServerCertificate=true"<br>
}
  </li>

  <li><h4>run the migrations:</h4>
    <b><u>Open the Package Manager Console in Visual Studio and run:</u></b><br>
      Update-Database
    </li>

  <li><h4>Run the project:</h4>
    Press <b>Ctrl + F5</b> in Visual Studio or click the green run button to start the application.
  </li>
</ol>

<br>

## 👥 Team Members & Contributions
<table>
  <tr>
    <th>Member</th>
    <th>Contribution</th>
  </tr>
  <tr>
    <td><b>Deep</b></td>
    <td>Implemented the core data models (`ApplicationUser`, `Product`, `Order`, `OrderItem`).<br/>Developed the `HomeController` and `AccountController` for authentication and homepage logic.<br/>Collaborated on the `ProductController` and the overall Repository Pattern structure.</td>
  </tr>
  <tr>
    <td><b>Krish</b></td>
    <td>Developed the `ProductsController` for the marketplace and product management, and the `SellerController` for the seller dashboard.<br/>Collaborated on the implementation of all repositories.<br/>Collaborated on the creation and mapping of all ViewModels.</td>
  </tr>
  <tr>
    <td><b>Ronak</b></td>
    <td>Implemented the cart-specific data models (`Cart`, `CartItem`).<br/>Developed the `OrdersController` for the checkout workflow and the `CartController` for the database-driven cart.<br/>Collaborated on the creation and mapping of all ViewModels.</td>
  </tr>
</table>
