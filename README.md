# 🚀 PulseRegistrationSystem

## 📌 Checkpoint 4 - 2TDS - CLEAN CODE, DDD e CLEAN ARCH COM .NET 8 (2025)

O **PulseRegistrationSystem** é uma Web API desenvolvida em .NET 8, aplicando os princípios de **Domain-Driven Design (DDD)**, **Clean Architecture** e **Clean Code**. O objetivo do projeto é fornecer uma solução escalável, legível e bem estruturada para o gerenciamento de **usuários, logins e endereços**, garantindo boas práticas de mercado.

---

## 👥 Integrantes do Grupo

- Gabriela de Sousa Reis - RM558830  
- Laura Amadeu Soares - RM556690  
- Raphael Lamaison Kim - RM557914  

---

## 📐 Arquitetura do Projeto

O projeto segue a estrutura de **Clean Architecture**:

src
┣ 📂 PulseRegistrationSystem.API -> Controllers, configurações e validações
┣ 📂 PulseRegistrationSystem.Application -> Casos de uso, DTOs, serviços
┣ 📂 PulseRegistrationSystem.Domain -> Entidades, Value Objects, interfaces
┗ 📂 PulseRegistrationSystem.Infrastructure -> Acesso a dados, migrations, repositórios


## 🔑 Principais conceitos aplicados

### DDD (Domain-Driven Design)
- **Entidades:** Usuario, Login, Endereco  
- **Agregado raiz:** Usuario (relaciona Login e Endereco)  
- **Value Objects** utilizados para estruturar dados  
- **Repositórios** definidos no domínio  

### Clean Code
- Código coeso, métodos pequenos e claros  
- Nomeação semântica de classes e métodos  
- Princípios aplicados: SRP, DRY, KISS, YAGNI  

### Clean Architecture
- Separação clara de responsabilidades  
- Inversão de dependência aplicada  
- Lógica de negócio concentrada no domínio  

---

## 🗄️ Persistência
- **Entity Framework Core** para persistência  
- **Migrations** configuradas na camada Infrastructure  
- Conexão ao banco definida no `appsettings.json` (ou variáveis de ambiente)  

---

## 📚 Swagger
O **Swagger** está configurado e disponível para documentação e testes dos endpoints.  

**Acessar:**  
https://localhost:5001/swagger

yaml
Copiar código

---

## ⚙️ Como Rodar o Projeto

### 🔧 Pré-requisitos
Antes de executar o projeto, certifique-se de ter instalado:
- [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)  
- Banco de dados SQL Server ou compatível  
- Visual Studio 2022 ou [VS Code](https://code.visualstudio.com/)  

---

### ▶️ Passos para execução

1. **Clonar o repositório**
```bash
git clone https://github.com/seu-usuario/PulseRegistrationSystem.git
cd PulseRegistrationSystem
```

2. Restaurar as dependências
```bash
dotnet restore
```

3. Executar as migrations
```bash
dotnet ef database update --project PulseRegistrationSystem.Infrastructure --startup-project PulseRegistrationSystem.API
```

4. Executar a aplicação
```bash
dotnet run --project PulseRegistrationSystem.API
```

5. Acessar a documentação Swagger
https://localhost:5001/swagger
Aqui você poderá testar todos os endpoints da API.
