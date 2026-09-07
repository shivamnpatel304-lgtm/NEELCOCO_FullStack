# NEELCOCO - Full-Stack Dairy Brand Platform & AI Multi-Agent Operations

![NEELCOCO Logo](frontend/public/neelcoco-logo.png)

A modern, full-stack enterprise web application and autonomous business operations platform for **NEELCOCO** — a premium dairy, kulfi, ice pops, and frozen dessert brand.

---

## 🌟 Key Highlights & Features

### 🛍️ Customer Experience & E-Commerce
- **Dynamic Landing Page**: Premium branding, hero banners, category showcases, and customer testimonials.
- **Product Catalog & Filtering**: Real-time category filtering (Kulfi, Ice Pops, Dairy Desserts, Mukhwas) and live search.
- **Product Details & Cart System**: Interactive product previews, unit selection, and responsive sliding cart.
- **Checkout & Order Management**: Seamless customer checkout flow with order confirmation and tracking.
- **Inquiry & Contact Form**: Direct customer inquiry system saved to backend database.

### 🤖 8-Pillar Autonomous AI Agent Suite
Autonomous multi-agent orchestration for business operations:
1. **Core Business Agent**: Strategy, competitive intelligence, growth tracking.
2. **Sales & CRM Agent**: Lead qualification, customer retention, upsell alerts.
3. **Marketing Agent**: Campaign performance, viral trends, social engagement.
4. **Finance Agent**: Margin analysis, cash flow forecasting, expense alerts.
5. **Logistics & Cold-Chain Agent**: Route efficiency, cold storage telemetry, delivery SLAs.
6. **Manufacturing & QC Agent**: Batch consistency, spoilage prevention, dairy standards.
7. **Management Decision Agent**: Executive summaries, KPI alerts, escalation triggers.
8. **Communication Agent**: Automated customer updates, notifications, support responses.

### 🛡️ Administrative Portal
- Secure JWT authentication with role-based access control.
- Product CRUD management (add, edit, update stock & pricing).
- Live Multi-Agent Operations Command Center.

---

## 🛠️ Technology Stack

| Layer | Technologies |
|---|---|
| **Frontend** | React 18, Vite, Tailwind CSS, React Router v6, Axios, Lucide Icons |
| **Backend API** | ASP.NET Core (.NET 10) Web API, Entity Framework Core |
| **Database** | SQLite (with automatic EF Core migrations & seed data) |
| **Authentication** | JSON Web Tokens (JWT) with ASP.NET Core Identity Password Hasher |
| **Documentation** | Swagger / OpenAPI UI |

---

## 📁 Project Structure

```text
NEELCOCO_FullStack/
├── frontend/                       # React + Vite frontend application
│   ├── public/neelcoco-logo.png    # Brand logo & assets
│   ├── src/
│   │   ├── components/             # Reusable UI components (Navbar, ProductCard, etc.)
│   │   ├── pages/                  # Route views (Home, Products, Admin, Agents, Cart, etc.)
│   │   ├── services/api.js         # Centralized Axios API client
│   │   ├── context.jsx             # Cart & application state
│   │   ├── App.jsx                 # App routing & layout
│   │   └── main.jsx                # React DOM entry point
│   ├── .env.example                # Frontend environment template
│   ├── package.json
│   ├── tailwind.config.js
│   └── vite.config.js
│
├── backend/
│   └── Neelcoco.API/               # ASP.NET Core Web API project
│       ├── Agents/                 # 8 Pillars of Autonomous AI Agents & Orchestrator
│       │   ├── Pillars/            # Specialized pillar agent implementations
│       │   ├── AgentModels.cs
│       │   └── AgentOrchestrator.cs
│       ├── Controllers/            # API endpoints (Products, Orders, Auth, Agents, etc.)
│       ├── Data/                   # EF Core DbContext & initial data seeds
│       ├── DTOs/                   # Request & response data transfer objects
│       ├── Migrations/             # Database migration snapshots
│       ├── Models/                 # Domain entities (Product, Category, Order, etc.)
│       ├── Services/               # Core services (JWT service, etc.)
│       ├── Program.cs              # Startup configuration & middleware
│       └── appsettings.json        # Application configuration
│
└── database/
    └── README.md                   # Database schema documentation
```

---

## 🚀 Getting Started

### Prerequisites
- [.NET 10 SDK](https://dotnet.microsoft.com/download) (or .NET 8+)
- [Node.js](https://nodejs.org/) (v18 or higher) & `npm`

---

### 1. Backend Setup

1. Open terminal and navigate to the API directory:
   ```bash
   cd backend/Neelcoco.API
   ```

2. Restore dependencies:
   ```bash
   dotnet restore
   ```

3. Run the API:
   ```bash
   dotnet run
   ```
   > 💡 *Note: SQLite database migrations and initial seed data (categories, products, and admin account) are applied automatically on application startup.*

4. Access Swagger documentation:
   ```
   http://localhost:5000/swagger  (or https://localhost:7001/swagger)
   ```

---

### 2. Frontend Setup

1. Open a new terminal and navigate to the frontend directory:
   ```bash
   cd frontend
   ```

2. Install dependencies:
   ```bash
   npm install
   ```

3. Configure environment (optional, defaults to `http://localhost:5000/api`):
   ```bash
   cp .env.example .env
   ```

4. Start the development server:
   ```bash
   npm run dev
   ```

5. Open your browser and navigate to the Vite local URL (typically `http://localhost:5173`).

---

## 🔐 Default Admin Credentials

For local development and testing:
- **Email**: `admin@neelcoco.com`
- **Password**: `Admin@123`

Access the Admin dashboard at: `http://localhost:5173/admin`  
Access the AI Multi-Agent Command Center at: `http://localhost:5173/agents`

---

## 📜 License

This project is licensed under the MIT License.
