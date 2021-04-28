USE [ThomasGreg]
GO

/****** Object:  StoredProcedure [dbo].[DELETE_CLIENTE]    Script Date: 22/03/2020 18:04:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


-- =============================================
-- Author:		<Author,,Name>
-- Create date: <Create Date,,>
-- Description:	<Description,,>
-- =============================================
CREATE PROCEDURE [dbo].[DELETE_CLIENTE]
	-- Add the parameters for the stored procedure here
	@ID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE
	FROM Logradouro
	WHERE
		ClienteId = @ID;

	DELETE
	FROM Cliente 
	WHERE 
		Id = @ID;

	SELECT *
	FROM 
		Cliente (nolock)
	WHERE Id = @ID;

END
GO

CREATE PROCEDURE [dbo].[DELETE_LOGRADOURO]
	-- Add the parameters for the stored procedure here
	@ID	INT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	DELETE
	FROM Logradouro 
	WHERE 
		Id = @ID

	SELECT *
	FROM 
		Logradouro (nolock)
	WHERE Id = @ID

END
GO


CREATE PROCEDURE [dbo].[GET_CLIENTES]
	-- Add the parameters for the stored procedure here
	@ID INT = NULL,
	@NOME VARCHAR(30) NULL,
	@EMAIL VARCHAR(30) NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM 
		Cliente (nolock)
	WHERE 
		(@ID IS NULL OR Id = @ID)
		AND (@NOME IS NULL OR Nome = @NOME)
		AND (@EMAIL IS NULL OR Email = @EMAIL);
    
END
GO

CREATE PROCEDURE [dbo].[GET_LOGRADOUROS]
	-- Add the parameters for the stored procedure here
	@ID INT = NULL
	, @CLIENTEID INT NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	SELECT *
	FROM 
		Logradouro (nolock)
	WHERE 
		(@ID IS NULL OR Id = @ID)
		AND (@CLIENTEID IS NULL OR ClienteId = @CLIENTEID)
    
END
GO


CREATE PROCEDURE [dbo].[INSERT_CLIENTE]
	-- Add the parameters for the stored procedure here
	@NOME VARCHAR(30),
	@EMAIL VARCHAR(30),
	@LOGOTIPO IMAGE NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Cliente (
		Nome,
		Email,
		Logotipo
	) VALUES (
		@NOME,
		@EMAIL,
		@LOGOTIPO
	)

	SELECT *
	FROM 
		Cliente (nolock)
	WHERE Id = @@IDENTITY

END
GO

CREATE PROCEDURE [dbo].[INSERT_LOGRADOURO]
	-- Add the parameters for the stored procedure here
	@CLIENTEID INT,
	@ENDERECO varchar(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	INSERT INTO Logradouro (
		ClienteId,
		Endereco
	)
	VALUES(
		@CLIENTEID,
		@ENDERECO
	)

	SELECT *
	FROM 
		Logradouro (nolock)
	WHERE 
		Id = @@IDENTITY;
    
END
GO

CREATE PROCEDURE [dbo].[UPDATE_CLIENTE]
	-- Add the parameters for the stored procedure here
	@ID	INT,
	@NOME VARCHAR(30),
	@EMAIL VARCHAR(30),
	@LOGOTIPO varbinary(max) NULL
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Cliente 
	SET
		Nome = @NOME,
		Email = @EMAIL,
		Logotipo = @LOGOTIPO
	WHERE 
		Cliente.Id = @ID

	SELECT *
	FROM 
		Cliente (nolock)
	WHERE Id = @ID

END
GO

CREATE PROCEDURE [dbo].[UPDATE_LOGRADOURO]
	-- Add the parameters for the stored procedure here
	@ID	INT,
	@CLIENTEID INT,
	@ENDERECO VARCHAR(50)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

	UPDATE Logradouro 
	SET
		ClienteId = @CLIENTEID,
		Endereco = @ENDERECO
	WHERE 
		Id = @ID;

	SELECT *
	FROM 
		Logradouro (nolock)
	WHERE Id = @ID;

END
GO
