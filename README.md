# 🚀 Automação de Login com Playwright, C# .NET e Extent Reports

![Automação Playwright](https://media.giphy.com/media/QHE5gWI0QjqF2/giphy.gif)

Este projeto implementa testes automatizados para um fluxo de **Login** utilizando **Microsoft Playwright**, **Reqnroll (SpecFlow)** e **Extent Reports** para geração de relatórios.

---

## 📌 Funcionalidades

✅ Teste de login com credenciais válidas  
✅ Validação de mensagem de erro para login inválido  
✅ Configuração de instâncias com **IObjectContainer**  
✅ Utilização de **Injeção de Dependência**  
✅ **Relatórios Extent Reports** com evidências e logs  
✅ Geração de **screenshots, vídeos e trace**  

---

## 🛠️ Tecnologias Utilizadas

- 💻 **C# .NET 8**
- 🌍 **Microsoft Playwright**
- 🧪 **Reqnroll (ex-SpecFlow)**
- 📊 **Extent Reports**
- 🏗️ **Injeção de Dependência (IObjectContainer)**
- 📄 **NUnit**

---

## 🎬 Demonstração da Automação

### 🔹 **Fluxo de Login com sucesso**
![Login Sucesso](https://media.giphy.com/media/3o7abldj0b3rxrZUxW/giphy.gif)

### 🔹 **Fluxo de Login com erro**
![Login Falha](https://media.giphy.com/media/3orieUe6ejxSFxYCXe/giphy.gif)

---

## 📂 Estrutura do Projeto

```
📦 LoginTests
 ┣ 📂 Features
 ┃ ┣ 📜 LoginApp.feature
 ┣ 📂 Pages
 ┃ ┣ 📜 ILoginAppPage.cs
 ┃ ┣ 📜 LoginAppPage.cs
 ┣ 📂 Reports
 ┃ ┣ 📜 ExtentReport.html
 ┣ 📂 Support
 ┃ ┣ 📂 Drivers
 ┃ ┃ ┣ 📜 IDriver.cs
 ┃ ┃ ┣ 📜 Driver.cs
 ┃ ┣ 📜 StartUp.cs
 ┃ ┣ 📜 ExtentReportManager.cs
 ┣ 📂 Tests
 ┃ ┣ 📜 LoginTests.cs
 ┣ 📜 .gitignore
 ┣ 📜 README.md
```

---

## 🚀 Como Executar a Automação?

### 🔹 **1️⃣ Instalar as dependências**
```sh
dotnet restore
```

### 🔹 **2️⃣ Executar os testes**
```sh
dotnet test
```

### 🔹 **3️⃣ Gerar Relatório Extent Reports**
Após rodar os testes, o relatório será salvo na pasta `/Reports`.  

Para visualizar, abra o arquivo:
```
Reports/ExtentReport.html
```
Ou execute:
```sh
start Reports/ExtentReport.html
```

---

## 📄 Configuração do Extent Reports

No **ExtentReportManager.cs**, inicializamos o relatório:
```csharp
var htmlReporter = new ExtentHtmlReporter("Reports/ExtentReport.html");
var extent = new ExtentReports();
extent.AttachReporter(htmlReporter);
```
E no teardown dos testes, finalizamos:
```csharp
extent.Flush();
```

---

## 📷 Captura de Evidências nos Relatórios

### **Adicionar Screenshots ao Relatório**
```csharp
var screenshotPath = "Reports/screenshot.png";
await page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
test.Log(Status.Pass, "Teste passou com sucesso!", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
```

---

## 🔗 Integração com Playwright Trace Viewer

Para visualizar um **trace completo** da execução:
```sh
npx playwright show-trace trace.zip
```

---

## 📂 Exemplo de Código

### **Início da Automação com Playwright e Extent Reports**
```csharp
using Microsoft.Playwright;
using ExtentReports;
using System.Threading.Tasks;

public class LoginTests
{
    private IPage Page;
    private ExtentReports extent;
    private ExtentTest test;

    [SetUp]
    public async Task SetUp()
    {
        var playwright = await Playwright.CreateAsync();
        var browser = await playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions { Headless = false });
        var context = await browser.NewContextAsync();
        Page = await context.NewPageAsync();

        // Inicializar o Extent Reports
        extent = new ExtentReports();
        var htmlReporter = new ExtentHtmlReporter("Reports/ExtentReport.html");
        extent.AttachReporter(htmlReporter);
    }

    [Test]
    public async Task LoginTest_ValidCredentials()
    {
        test = extent.CreateTest("Login com credenciais válidas");

        // Realizando o login
        await Page.FillAsync("input[name='username']", "validUser");
        await Page.FillAsync("input[name='password']", "validPassword");
        await Page.ClickAsync("button[type='submit']");

        // Validando login
        var successMessage = await Page.InnerTextAsync(".success-message");
        Assert.AreEqual("Login bem-sucedido!", successMessage);

        // Adicionando evidência no Extent Reports
        test.Log(Status.Pass, "Login realizado com sucesso!");
        var screenshotPath = "Reports/screenshot.png";
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
        test.Log(Status.Pass, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
    }

    [Test]
    public async Task LoginTest_InvalidCredentials()
    {
        test = extent.CreateTest("Login com credenciais inválidas");

        // Realizando o login com credenciais inválidas
        await Page.FillAsync("input[name='username']", "invalidUser");
        await Page.FillAsync("input[name='password']", "invalidPassword");
        await Page.ClickAsync("button[type='submit']");

        // Validando mensagem de erro
        var errorMessage = await Page.InnerTextAsync(".error-message");
        Assert.AreEqual("Credenciais inválidas!", errorMessage);

        // Adicionando evidência no Extent Reports
        test.Log(Status.Fail, "Login falhou com credenciais inválidas");
        var screenshotPath = "Reports/screenshot-error.png";
        await Page.ScreenshotAsync(new PageScreenshotOptions { Path = screenshotPath });
        test.Log(Status.Fail, "Screenshot", MediaEntityBuilder.CreateScreenCaptureFromPath(screenshotPath).Build());
    }

    [TearDown]
    public void TearDown()
    {
        extent.Flush();
    }
}
```

---

## 🤝 Contribuindo

Contribuições são bem-vindas! Siga os passos abaixo:

1. **Fork** este repositório 🍴  
2. **Crie** uma nova branch (`git checkout -b minha-feature`) 🌱  
3. **Commit** suas alterações (`git commit -m 'Minha nova feature'`) 📝  
4. **Envie** seu código (`git push origin minha-feature`) 🚀  
5. **Abra um Pull Request** ✅  

---

## 📄 Licença

Este projeto está licenciado sob a **MIT License**.

---

### 🚀 Feito com ❤️, **Playwright**, **C#**, **Reqnroll** e **Extent Reports** para automação eficiente!
```
