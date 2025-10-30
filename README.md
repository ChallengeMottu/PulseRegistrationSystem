# 🚀 PulseRegistrationSystem

O PulseRegistrationSystem é uma Web API desenvolvida em .NET 8, aplicando os princípios de Domain-Driven Design (DDD), Clean Architecture e Clean Code.

O objetivo do projeto é fornecer uma solução escalável, legível e bem estruturada para o gerenciamento de usuários e logins, garantindo boas práticas de desenvolvimento e manutenção.

---

## 👥 Integrantes do Grupo

- Gabriela de Sousa Reis - RM558830  
- Laura Amadeu Soares - RM556690  
- Raphael Lamaison Kim - RM557914  

---

## 🛠️ Tecnologias e Ferramentas Utilizadas
- .NET 8 - Plataforma principal da API
- Entity Framework Core - Persistência de dados
- MongoDB - Banco de dados principal
- ASP.NET Core JWT - Autenticação e segurança
- Swagger / OpenAPI - Documentação e teste de endpoints
- Health Checks - Monitoramento da saúde da aplicação
- Versionamento de API - Controle de versões via Asp.Versioning

---

## 📐 Arquitetura do Projeto

O projeto segue a estrutura de **Clean Architecture**:

```bash
src
┣ 📂 PulseRegistrationSystem.API           -> Controllers, configurações, validações e versionamento
┣ 📂 PulseRegistrationSystem.Application   -> Casos de uso, DTOs, serviços
┣ 📂 PulseRegistrationSystem.Domain        -> Entidades, Value Objects, interfaces
┗ 📂 PulseRegistrationSystem.Infrastructure -> Acesso a dados, migrations, repositórios
```

---

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

## 🔒 Segurança

A autenticação na versão 2 da aplicação é feita com JWT (JSON Web Token).
Cada login gera um token que deve ser enviado no cabeçalho Authorization para acessar endpoints protegidos.

```bash
Authorization: Bearer {seu_token_jwt}
```

Configuração necessária no appsettings.json:
```bash
"JwtSettings": {
  "SecretKey": "{SECRET_KEY}",
  "Issuer": "PulseRegistrationSystem",
  "Audience": "PulseRegistrationSystemUsers",
  "ExpirationMinutes": 60
}
```

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
- .NET 8 SDK
- Visual Studio ou Rider
- MongoDB (local ou via container Docker)

### 💻 Sugestão de MongoDB

1. Rodando localmente:
Baixe e instale o MongoDB Community Server.

2. Rodando via Docker:
```bash
docker run -d --name pulse-mongo -p 27017:27017 mongo:7
```
Isso cria um MongoDB rodando na porta padrão 27017.
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

3. Configurar conexão com banco (appsettings.json):
```bash
"Settings": {
    "MongoDb": {
      "ConnectionString": "{CONNECTION_MONGODB}",
      "DatabaseName": "UserDb"
    }
```

4. Executar as migrations
```bash
dotnet ef database update --project PulseRegistrationSystem.Infrastructure --startup-project PulseRegistrationSystem.API
```

5. Executar a aplicação
```bash
dotnet run --project PulseRegistrationSystem.API
```

6. Acessar a documentação Swagger
https://localhost:5001/swagger
Aqui você poderá testar todos os endpoints da API.

---

## 📝 Observações
- JWT utilizado para autenticação nos endpoints da API v2
- Health Checks configurados para monitorar a saúde da API e do banco de dados
- Versionamento implementado via Asp.Versioning


