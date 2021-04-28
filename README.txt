# ESPECIFICAÇÕES #

- .NET CORE 3.1
- SQLSERVER 2019
- EntityFramework Core

# DIRETÓRIOS ÚTEIS #

- /postman : Contém a collection postman para ser utilizada como teste.
- /database : Contém os scripts de SQL SERVER para criar banco, tabelas e procedures utilizadas na API.

# TESTE #

Para realizar os testes da API seguir os passos:

1. Buildar o código na raíz através do cmd (dotnet build); 

2. Executar o código da raíz através do cmd (dotnet run);

3. Executar os scripts de banco de dados no SQL SERVER, presentes na pasta database conforme ordem de nomenclatura proposta. 

4. Realizar a autenticação na API requisitando um token através do endpoint "/api/token/login" (ou executar requisição GET-TOKEN na collection do postman) informando no body o usuário e senha disponibilizado, através do json:
	- {"username":"thomasgreg","password":"sons"}

5. Inserir o token gerado no passo 4 no header da requisição:
	- Key: Authorization
	- Value: Bearer TOKENGERADO  

6. Requisitar um dos endpoints listados a seguir, tendo como base e exemplo a collection do postman:
	- /api/cliente/get
	- /api/cliente/get/{id}
	- /api/cliente/post
	- /api/cliente/put/{id}
	- /api/cliente/delete/{id}
	- /api/logradouro/get
	- /api/logradouro/get/{id}
	- /api/logradouro/post
	- /api/logradouro/put/{id}
	- /api/logradouro/delete/{id}