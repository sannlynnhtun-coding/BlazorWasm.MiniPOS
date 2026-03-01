# Mini POS

A lightweight, client-side Point of Sale (POS) system built with **Blazor WebAssembly**. This project demonstrates a functional POS application that runs entirely in the browser, leveraging various client-side storage options and modern web technologies.

![POS Dashboard Mockup](https://raw.githubusercontent.com/sannlynnhtun-coding/BlazorWasm.MiniPOS/master/wwwroot/icon-512.png)

## 🚀 Features

- **📊 Dynamic Dashboard**: Real-time sales visualization using Highcharts.
- **📦 Product Management**: CRUD operations for products and categories.
- **🛒 Sales Processing**: Easy-to-use checkout interface with voucher generation.
- **📄 Reporting**: Detailed sales and inventory reports.
- **💾 Flexible Storage**: Supports multiple storage providers:
  - **LocalStorage**: Persistent browser storage.
  - **SessionStorage**: Temporary session-based storage.
  - **IndexedDB**: Robust client-side database for larger datasets.
- **📥 Data Export**: Export sales data to Excel format.
- **🌱 Data Seeding**: Built-in mock data generation for testing and demonstration.
- **🌓 Modern UI**: Responsive design built with Tailwind CSS.

## 🛠️ Technologies Used

- **Framework**: [Blazor WebAssembly](https://dotnet.microsoft.com/en-us/apps/aspnet/web-apps/blazor) (.NET 8/9)
- **Styling**: [Tailwind CSS](https://tailwindcss.com/)
- **Charts**: [Highcharts](https://www.highcharts.com/)
- **Storage**: [Blazored LocalStorage](https://github.com/Blazored/LocalStorage) & [SessionStorage](https://github.com/Blazored/SessionStorage)
- **Excel Export**: `@code-zhf/export-json-2-excel`
- **Deployment**: [GitHub Actions](https://github.com/features/actions) & [Vercel](https://vercel.com/)

---

## ⚙️ Technical Setup

### Webpack & JS Bundling

The project uses Webpack to bundle JavaScript dependencies (like the Excel export tool).

1. Navigate to the `NpmJS` directory:
   ```bash
   cd BlazorWasm.MiniPOS/NpmJS
   ```
2. Install dependencies:
   ```bash
   npm install
   ```
3. Build the JavaScript bundle:
   ```bash
   npm run build
   ```
   *Note: The project is configured to run this automatically during the .NET build process.*

### Build & Run

To run the project locally:

1. Clone the repository.
2. Open the solution in Visual Studio or use the CLI:
   ```bash
   dotnet watch run --project BlazorWasm.MiniPOS/BlazorWasm.MiniPOS.csproj
   ```

## 🚢 Deployment

This project is configured for automated deployment to **Vercel** via **GitHub Actions**.

- **Workflow**: `.github/workflows/deploy.yml`
- **Hosting Config**: `vercel.json`

Every push to the `master` branch triggers a build and deployment cycle.

---

## 📝 License

This project is open-source and available under the MIT License.
